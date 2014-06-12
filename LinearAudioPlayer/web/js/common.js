
	$(document).ready(function(){
	  // ready

	  var pollingTimer;
	  var pollingIntervalMs = 1000; // polling interval millisecond
	  var isChangeTrack = false;  // track change flag
	  var stopCount = 0;
	  var hideMessageTimer;
	  var beforeId = -1;

	  $("#error-message").hide();
	  $("#status-message").hide();
	  $("#success-message").hide();
	  
	  // init request
	  polling();
	  $("#rating-on").hide();
	  
	  // polling
	  function polling() {
	    requestAction({action:"getplayinfo"});
	    clearTimeout(pollingTimer);
	    pollingTimer = setTimeout(function(){ 
	      polling();
	    }, pollingIntervalMs);
	  }
	  
	  // ajax request
	  function requestAction(req) {
	  
	    /*
	    switch(req.action) {
	          case "previous":
	          case "forward":
	            break;
	          case "getplayinfo":
	            break;
	    }*/
	    
	    //console.log("requestAction: " + action);
	    $.ajax({
	      url: "LinearWebService",
	      data: req,
	      contentType: "application/json; charset=utf-8",
	      dataType: "json",
	      success: function(res) {
	        //console.log("success! requestAction");
	        //console.log(res);
	        $("#error-message").fadeOut();
	        
	        switch(res.action) {
	          //case "previous":
	          //case "forward":
	            //isChangeTrack = true;
	          //  break;
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
	          			if (stopCount > 3){
	          				$("#play-status").text("Stopped");
	          				$("#status-message").fadeIn("slow");
	          			}
	          		}
	          	}  else {
	          		// Playing
	          		stopCount = 0;
	          		$("#status-message").fadeOut();
	          	}
	          	
	            if (!isChangeTrack) {
	              // change playinfo
	              setPlayInfo(res.playInfo);
	            } else {
	              // fade change playinfo
	              $("#playing_info").find("*").animate({opacity:0}, "slow",function(){
	                  setPlayInfo(res.playInfo);
	                  $("#playing_info").find("*").animate(
	                    {opacity:1},
	                    {duration: "slow"});
	                  isChangeTrack = false;
	              });
	              // reload now playing
	              requestAction({action:"getnowplaying"});
	            }
	            $('.progress-bar').css("width",res.seekRatio + "%");
	            beforeId = res.playInfo.Id;
	            break;
	          case "voldown":
	          case "volup":
	          	$("#success-message").text("change volume : " + res.volume);
	          	$("#success-message").fadeIn();
	          	hideMessageTimer = setTimeout(function() { $("#success-message").fadeOut(); }, 2000);
	          	break;
	          case "getnowplaying":
	          	reloadNowPlaying(res.nowPlaying);
	          	break;
	        }
	      },
	      error: function (XMLHttpRequest, textStatus, errorThrow){
	      	$("#status-message").hide();
	        $("#error-message").fadeIn("slow");
	      }
	    });
	    
	  }
	  
	  function setPlayInfo(playInfo) {
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
	  }
	  
	  function reloadNowPlaying(nowPlaying) {
	  	
	  	$("#now_playing").find("tr:not(:first)").remove();
	  	
	  	var i = 1;
	  	$(nowPlaying).each(function() {
	  		$("#now_playing").append($("<tr>")
	  				.attr("id", $(this)[0])
	  			.append($("<td>")
	  				.text(i)
	  			)
	  			.append($("<td>")
	  				.text($(this)[1])
	  			)
	  			.append($("<td>")
	  				.text($(this)[2])
	  			)
	  		);
	  		i++;
	  	});
	  	
	  }
	  
	  // bind
	  $( ".navbar-brand" ).on( "click", function( event ) {
	    polling();
	  });
	  
	  $( "#button-group #play" ).on( "click", function( event ) {
	    requestAction({action: "play"});
	  });
	  
	  $( "#button-group #pause" ).on( "click", function( event ) {
	    requestAction({action: "pause"});
	  });
	  
	  $( "#button-group #stop" ).on( "click", function( event ) {
	    requestAction({action: "stop"});
	  });
	  
	  $( "#button-group #previous" ).on( "click", function( event ) {
	    requestAction({action: "previous"});
	  });
	  
	  $( "#button-group #forward" ).on( "click", function( event ) {
	    requestAction({action: "forward"});
	  });
	  
	  $( "#button-group #voldown" ).on( "click", function( event ) {
	  	clearTimeout(hideMessageTimer);
	    requestAction({action: "voldown"});
	  });
	  
	  $( "#button-group #volup" ).on( "click", function( event ) {
	  	clearTimeout(hideMessageTimer);
	    requestAction({action: "volup"});
	  });
	  
	  $( ".progress" ).on( "mousedown", function( event ) {
	    var progressWidth = $(".progress").width();
	    var progressClickX = event.pageX - $(".progress").offset().left;
	    var seekVal = progressClickX / progressWidth;
	    //console.log(seekVal);
	    requestAction({action: "seek", seekPosition: seekVal});
	  });
	  
	});