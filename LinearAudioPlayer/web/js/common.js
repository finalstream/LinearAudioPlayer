/*!
 * Linear Audio Player WEBUI Commons ver.0.9.4 (http://www.finalstream.net)
 * Copyright 2014 Finalstream
 */
$(document).ready(function() {
	// ready

	var pollingTimer;
	var pollingIntervalMs = 1000; // polling interval millisecond
	var isChangeTrack = false; // track change flag
	var stopCount = 0;
	var hideMessageTimer;
	var beforeId = -1;
	var currentId = -1;
	var artworkUrl = "../img/blank.png";
	var artworkThumbUrl = "../img/blank.png";
	var recentLimit = 5;
	var lastRecentOffset = 0;
	var topLimit = 10;
	var lastTopArtistRangeType = "WEEKLY";
	var lastTopTrackRangeType = "WEEKLY";
	var artworkTemplete = "<img width='50' height='50' class='img-rounded'>";

	$("#error-message").hide();
	$("#status-message").hide();
	$("#success-message").hide();

	// init request
	polling();
	$("#rating-on").hide();
	requestAction({ action: "getthemelist" });

	function refreshAnalyzeInfo()
	{
		requestAction({ action: "getanalyzeinfo" });
		requestAction({ action: "getrecentlist", offset: lastRecentOffset, limit: recentLimit });
		requestAction({ action: "gettopartist", rangeType: lastTopArtistRangeType, limit: topLimit });
		requestAction({ action: "gettoptrack", rangeType: lastTopTrackRangeType, limit: topLimit });
	}

	// polling
	function polling() {
		requestAction({ action: "getplayinfo" });
		clearTimeout(pollingTimer);
		pollingTimer = setTimeout(function() {
			polling();
		}, pollingIntervalMs);
	}

	// ajax request
	function requestAction(req) {

		$.ajax({
			url: "LinearWebService",
			data: req,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function(res) {
				$("#error-message").fadeOut();

				switch (res.action) {
					case "getplayinfo":

						if (res.playInfo.Id != beforeId) {
							isChangeTrack = true;
						}

						if (res.isPaused || !res.isPlaying) {
							// Paused or Stopped
							if (res.isPaused) {
								$("#play-status").text("Paused");
								$("#status-message").fadeIn("slow");
							} else {
								stopCount += 1;
								if (stopCount > 3) {
									$("#play-status").text("Stopped");
									$("#status-message").fadeIn("slow");
								}
							}
						} else {
							// Playing
							stopCount = 0;
							$("#status-message").fadeOut();
						}

						if (!isChangeTrack) {
							// change playinfo
							setPlayInfo(res.playInfo);
						} else {
							// fade change playinfo
							isChangeTrack = false;
							requestAction({ action: "getartwork", artworkSize: 150 });
							$("#playing_info").find("*").animate({ opacity: 0 }, "slow", function() {
								setPlayInfo(res.playInfo);
								$("#playing_info").find("*").animate(
								{ opacity: 1 },
								{ duration: "slow" });
								
							});
							// reload now playing
							requestAction({ action: "getnowplaying" });
							requestAction({ action: "getrecentlist", offset: lastRecentOffset, limit: recentLimit });
						}
						$('#seekbar').css("width", res.seekRatio + "%");
						beforeId = res.playInfo.Id;
						break;
					case "voldown":
					case "volup":
						$("#success-message").text("change volume : " + res.volume);
						$("#success-message").fadeIn();
						hideMessageTimer = setTimeout(function() { $("#success-message").fadeOut(); }, 2000);
						break;
					case "getnowplaying":
						reloadNowPlaying(res.nowPlaying, true);
						break;
					case "addnowplaying":
						reloadNowPlaying(res.nowPlaying, false);
						break;
					case "getrecentlist":
						reloadRecentListen(res.recentListen, res.pagerPrevious, res.pagerNext);
						break;
					case "getanalyzeinfo":
						setAnalyzeInfo(res.analyzeOverview);
						break;
					case "gettopartist":
						reloadTopList("#top_artist", res.topLists);
						break;
					case "gettoptrack":
						reloadTopList("#top_track", res.topLists);
						break;
					case "getartwork":
						if (res.artworkUrl == "") {
							setTimeout(requestAction({ action: "getartwork", artworkSize: 150 }), 1000);
						} else {
							artworkUrl = res.artworkUrl;
							artworkThumbUrl = res.artworkThumbUrl;
						}
						break;
					case "getthemelist":
						$(res.themeList).each(function () {
							var themename = this;
							$("#theme_list").append(
								$("<li>").append(
								$("<a>").text(themename)
								).on("click", function (event) {
									requestAction({ theme: themename, action: "switchtheme" });
								}));
						});
						break;
					case "switchtheme":
						location.reload();
						break;
				}
			},
			error: function(XMLHttpRequest, textStatus, errorThrow) {
				beforeId = -1;
				$("#status-message").hide();
				$("#error-message").fadeIn("slow");
			}
		});

	}

	function setPlayInfo(playInfo) {
		currentId = playInfo.Id;
		$("#title").text(playInfo.Title);
		$("#artist").text(playInfo.Artist);
		var year = "";
		if (playInfo.Year.length == 4) {
			year = " [" + playInfo.Year + "]";
		}
		$("#album").text(playInfo.Album + year);
		if (playInfo.Rating == 9) {
			$("#rating-on").show();
			$("#rating-off").hide();
		} else {
			$("#rating-on").hide();
			$("#rating-off").show();
		}
		$("#artworkimg").attr("src", artworkThumbUrl);
		$("#artwork").attr("href", artworkUrl);
		$("#artwork").attr("data-title", playInfo.Title);
	}

	function setAnalyzeInfo(analyzeOverview) {
		$("#startDate").find("span").remove();
		$("#tracksCount").find("span").remove();
		$("#playCount").find("span").remove();
		$("#startDate").append($("<span>").text("since " + analyzeOverview.StartDate).append($("<small>").append(" " + analyzeOverview.StartDateRelative)));
		$("#tracksCount").append($("<span>").text(analyzeOverview.TotalTracksCount + " tracks ").append($("<small>").append("favorite " + analyzeOverview.TotalFavoriteTracksCount + " tracks")));
		$("#playCount").append($("<span>").text(analyzeOverview.TotalPlayCount + " plays ").append($("<small>").append("history " + analyzeOverview.TotalPalyHistoryCount + " plays")));
	}

	function reloadNowPlaying(nowPlaying, isAll) {

		if (isAll) {
			$("#now_playing tbody tr").remove();

			// read more row
			$("#now_playing tbody").append($("<tr>")
				.append($("<td>").css("text-align", "center").attr("colspan", "4")
					.append("<i class='glyphicon glyphicon-chevron-down'></i>  read more"))
				.on("click", function() {
					requestAction({ skip: $("#now_playing tbody tr").length - 1, take: 10, action: "addnowplaying" });
				}));
		}

		var i = $("#now_playing tbody tr").length;
		$(nowPlaying).each(function() {
			var id = this.Id;
			var artwork = $(artworkTemplete);
			artwork.hide();
			$("#now_playing tbody").find("tr:last").before($("<tr style='height:70px'>")
				.attr("id", id)
				.append($("<td>")
					.text(i)
				)
				.append($("<td>")
					.addClass("hidden-xs")
					.append(artwork)
				)
				.append($("<td>")
					.text(this.Title)
				)
				.append($("<td>")
					.text(this.Artist)
				)
				.on("click", function (event) {
					if (event.target.localName == "img") return;
					requestAction({ id: id, action: "skipnowplaying" });
				})
			);
			getArtwork(artwork, this.Title + " " + this.Artist, "image-nowplaying");
			i++;
		});

	}

	function reloadRecentListen(recentListen, previous, next) {

		if (recentListen.length == 0) {
			next = -1;
		} else {

			$("#recent_list tbody tr").remove();

			if (recentListen.length < recentLimit) {
				for (var i = recentListen.length; i < recentLimit; i++) {
					recentListen.push({ Id: "&nbsp;", Title: "&nbsp;", Artist: "&nbsp;", PlaySeconds: "&nbsp;", PlayDateTime: "&nbsp;" });
				}
			}

			
			$(recentListen).each(function() {
				var id = this.Id;
				var artwork = $(artworkTemplete);
				artwork.hide();
				var playseconds = this.PlaySeconds;
				if (playseconds == null) {
					var j = 0;
					var message = "Now Playing";
					playseconds = $("<span>").text(message);
					setInterval(function () {
						j = ++j % 4;
						playseconds.text(message + Array(j + 1).join("."));
					}, 500);
				}
				$("#recent_list tbody").append($("<tr style='height:70px'>")
					.attr("id", id)
					.append($("<td>")
						.addClass("hidden-xs")
						.append(artwork)
					)
					.append($("<td>")
						.append(this.Title)
					)
					.append($("<td>")
						.append(this.Artist)
					)
					.append($("<td>")
						.append(playseconds)
					)
					.append($("<td>")
						.append($("<span>")
							.append(this.PlayDateTimeRelative)
								.attr("data-toggle", "tooltip")
								.attr("data-placement", "right")
								.attr("title", this.PlayDateTime).tooltip()
						)	
					)
				);
				if (this.Id != "&nbsp;") getArtwork(artwork, this.Title + " " + this.Artist, "image-recent");
			});

		}

		// pager
		var pager = $("#recent_pager");
		pager.find("li").remove();
		if (previous != -1) {
			pager.append($("<li>").attr("class", "previous").append(
				$("<a>").attr("href", "javascript:void(0)").append("&larr; Newer")
				.on("click", function (event) {
					lastRecentOffset = previous;
					requestAction({ action: "getrecentlist", offset: previous, limit: recentLimit });
				})));
		}
		if (next != -1) {
			pager.append($("<li>").attr("class", "next").append(
				$("<a>").attr("href", "javascript:void(0)").append("Older &rarr;")
				.on("click", function (event) {
					lastRecentOffset = next;
					requestAction({ action: "getrecentlist", offset: next, limit: recentLimit });
				})));
		}

	}

	function reloadTopList(targetList, topLists) {

		var target = $(targetList);

		target.find("tbody tr").remove();

		var prevCount = -1;
		var rank = 1;
		$(topLists).each(function () {
			if (prevCount != -1 && prevCount != this.Count) rank++;

			var playtime = "";
			if (this.TotalPlayTime != "") playtime = this.TotalPlayTime;

			var theme = target.attr("aria-valuetext");
			var artwork = $(artworkTemplete);
			artwork.hide();
			target.find("tbody").append($("<tr style='height:70px'>")
				.append($("<td>")
					.append(rank)
				)
				.append($("<td>")
					.addClass("hidden-xs")
					.append(artwork)
				)
				.append($("<td style='width:30%'>")
					.append(this.Title)
				)
				.append($("<td style='width:70%'>")
					.append($("<div>").attr("class", "progress").css("width", this.Rate + "%")
						.append($("<div>")
							.attr("class", "progress-bar " + theme)
							.attr("aria-valuenow", "100")
							.attr("aria-valuemin", "0")
							.attr("aria-valuemax", "100")
							.css("width", "100%")
							.append(this.Count)
							.attr("data-toggle", "tooltip")
							.attr("data-placement", "left")
							.attr("title", playtime).tooltip()
					))
				)
			);
			getArtwork(artwork, this.Title.replace(" - ", " "), targetList);
			prevCount = this.Count;
		});

	}

	function getArtwork(img, keyword, imggroup) {

	    var url = "http://ajax.googleapis.com/ajax/services/search/images?q=" + encodeURIComponent(keyword) + "&v=1.0";

		$.ajax({
			url: url,
			dataType: 'jsonp',
			type: "GET",
			success: function (res) {
				try {
					var results = res.responseData.results;
					var imageData = results.shift();
					img.attr("src", imageData.url);
					img.on('load', function () {
					    img.wrap($("<a data-lightbox='" + imggroup + "' data-title='" + keyword + "'>").attr("href", img.attr("src")));
						img.show();
					});
					img.on('error', function() {
						var nextData = results.shift();
						img.attr("src", nextData.url);
					});

				} catch (ex) {
					console.log(ex);
					img.attr("src", "../img/nocover.png");
					img.show();
				}
			}
		});

	}


	// bind
	$(".navbar-brand").on("click", function(event) {
		polling();
	});

	$("#button-group #play").on("click", function(event) {
		requestAction({ action: "play" });
	});

	$("#button-group #pause").on("click", function(event) {
		requestAction({ action: "pause" });
	});

	$("#button-group #stop").on("click", function(event) {
		requestAction({ action: "stop" });
	});

	$("#button-group #previous").on("click", function(event) {
		requestAction({ action: "previous" });
	});

	$("#button-group #forward").on("click", function(event) {
		requestAction({ action: "forward" });
	});

	$("#button-group #voldown").on("click", function(event) {
		clearTimeout(hideMessageTimer);
		requestAction({ action: "voldown" });
	});

	$("#button-group #volup").on("click", function(event) {
		clearTimeout(hideMessageTimer);
		requestAction({ action: "volup" });
	});

	$("#seek").on("mousedown", function(event) {
		var progressWidth = $("#seek").width();
		var progressClickX = event.pageX - $("#seek").offset().left;
		var seekVal = progressClickX / progressWidth;
		//console.log(seekVal);
		requestAction({ action: "seek", seekPosition: seekVal });
	});

	$("#button-group #rating-on").on("click", function(event) {
		requestAction({ id: currentId, action: "ratingoff" });
	});

	$("#button-group #rating-off").on("click", function(event) {
		requestAction({ id: currentId, action: "ratingon" });
	});

	$("#button-group #rating-off").on("click", function(event) {
		requestAction({ id: currentId, action: "ratingon" });
	});

	$("#top_artist_tab a").on("click", function (event) {
		lastTopArtistRangeType = this.hash.substr(1);
		requestAction({ action: "gettopartist", rangeType: lastTopArtistRangeType, limit: topLimit });
	});

	$("#top_track_tab a").on("click", function (event) {
		lastTopTrackRangeType = this.hash.substr(1);
		requestAction({ action: "gettoptrack", rangeType: lastTopTrackRangeType, limit: topLimit });
	});

	$("#menu_analyze").on("click", function (event) {
		refreshAnalyzeInfo();
	});

});