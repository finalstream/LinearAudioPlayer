using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Xml.XPath;
using Finalstream.LinearAudioPlayer.Plugin.Lastfm.Api;
using Lpfm.LastFmScrobbler;
using RestSharp;

namespace Finalstream.LinearAudioPlayer.Plugin.Lastfm
{
    internal class LastFmTrackApi
    {
        private const string GetInfoMethodName = "track.getInfo";
        private const string UpdateNowPlayingMethodName = "track.updateNowPlaying";
        private const string ScrobbleMethodName = "track.scrobble";
        private const string LoveMethodName = "track.love";
        private const string UnLoveMethodName = "track.unlove";
        private const string BanMethodName = "track.ban";
        private const string UnBanMethodName = "track.unban";



        /// <summary>
        /// Instantiates a <see cref="TrackApi"/>
        /// </summary>
        public LastFmTrackApi()
        {

        }

        #region ITrackApi Members

        public TrackResponse GetInfo(Track track, Authentication authentication, string userName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add(Track.ArtistNameParamName, track.ArtistName);
            parameters.Add(Track.TrackNameParamName, track.TrackName);

            if (!String.IsNullOrEmpty(userName))
            {
                parameters.Add("username", userName);
            }

            var request = LastFmApiUtils.AddRequiredParams(Method.POST, parameters, GetInfoMethodName, authentication);

            var response = LastFmApiUtils.LastFmRestClient.Execute<GetInfoResponse>(request);
            Uri url = response.ResponseUri;
            LastFmApiUtils.checkResponce(response);

            if (response.Data != null && response.Data.track != null)
            {
                return response.Data.track;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// Notifies Last.fm that a user has started listening to a track. 
        /// </summary>
        /// <param name="track">A <see cref="Track"/> DTO containing track details</param>
        /// <param name="authentication"><see cref="Authentication"/> object</param>        
        /// <returns>A <see cref="ScrobbleResponse"/>DTO containing details of Last.FM's response</returns>
        /// <remarks>It is important to not use the corrections returned by the now playing service as input for the scrobble request, 
        /// unless they have been explicitly approved by the user</remarks>
        public bool UpdateNowPlaying(Track track, Authentication authentication)
        {
            Dictionary<string, string> parameters = TrackToNameValueCollection(track);

            var request = LastFmApiUtils.AddRequiredParams(Method.POST, parameters, UpdateNowPlayingMethodName, authentication);

            // send request
            try
            {
                var response = LastFmApiUtils.LastFmRestClient.Execute<UpdateNowPlayingResponse>(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Notifies Last.FM that the user UnLoves the track
        /// </summary>
        /// <param name="track">A <see cref="Track"/> DTO containing track details</param>
        /// <param name="authentication"><see cref="Authentication"/> object</param>     
        /// <returns>int LastFM return code. 0 is Success, above 0 is failure</returns>
        public bool Love(Track track, Authentication authentication)
        {
            return RateTrack(track, authentication, LoveMethodName);
        }

        /// <summary>
        /// Notifies Last.FM that the user Loves the track
        /// </summary>
        /// <param name="track">A <see cref="Track"/> DTO containing track details</param>
        /// <param name="authentication"><see cref="Authentication"/> object</param>     
        /// <returns>int LastFM return code. 0 is Success, above 0 is failure</returns>
        public bool UnLove(Track track, Authentication authentication)
        {
            return RateTrack(track, authentication, UnLoveMethodName);
        }

        /// <summary>
        /// Notifies Last.FM that the user wants to Ban the track
        /// </summary>
        /// <param name="track">A <see cref="Track"/> DTO containing track details</param>
        /// <param name="authentication"><see cref="Authentication"/> object</param>     
        /// <returns>int LastFM return code. 0 is Success, above 0 is failure</returns>
        public bool Ban(Track track, Authentication authentication)
        {
            return RateTrack(track, authentication, BanMethodName);
        }

        /// <summary>
        /// Notifies Last.FM that the user wants to UnBan the track
        /// </summary>
        /// <param name="track">A <see cref="Track"/> DTO containing track details</param>
        /// <param name="authentication"><see cref="Authentication"/> object</param>     
        /// <returns>int LastFM return code. 0 is Success, above 0 is failure</returns>
        public bool UnBan(Track track, Authentication authentication)
        {
            return RateTrack(track, authentication, UnBanMethodName);
        }

        private bool RateTrack(Track track, Authentication authentication, string method)
        {
            Dictionary<string, string> parameters = TrackToNameValueCollection(track);

            var request = LastFmApiUtils.AddRequiredParams(Method.POST, parameters, method, authentication);

            // send request
            try
            {
                LastFmApiUtils.LastFmRestClient.Execute(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Add a track-play to a user's profile
        /// </summary>
        /// <param name="track">A <see cref="Track"/> DTO containing track details</param>
        /// <param name="authentication"><see cref="Authentication"/> object</param>
        /// <returns>A <see cref="ScrobbleResponse"/>DTO containing details of Last.FM's response</returns>
        public bool Scrobble(Track track, Authentication authentication)
        {
            Dictionary<string, string> parameters = TrackToNameValueCollection(track);

            var request = LastFmApiUtils.AddRequiredParams(Method.POST, parameters, ScrobbleMethodName, authentication);

            // send request
            try
            {
                LastFmApiUtils.LastFmRestClient.Execute(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion


        /*
        private static CorrectedTrack GetCorrectedTrack(XPathNavigator item)
        {
            var track = new CorrectedTrack();
            track.TrackNameCorrected = ApiHelper.SelectSingleNode(item, "track/@corrected").ValueAsBoolean;
            track.TrackName = ApiHelper.SelectSingleNode(item, "track").Value;

            track.ArtistNameCorrected = ApiHelper.SelectSingleNode(item, "artist/@corrected").ValueAsBoolean;
            track.ArtistName = ApiHelper.SelectSingleNode(item, "artist").Value;

            track.AlbumNameCorrected = ApiHelper.SelectSingleNode(item, "album/@corrected").ValueAsBoolean;
            track.AlbumName = ApiHelper.SelectSingleNode(item, "album").Value;

            track.AlbumArtistCorrected = ApiHelper.SelectSingleNode(item, "albumArtist/@corrected").ValueAsBoolean;
            track.AlbumArtist = ApiHelper.SelectSingleNode(item, "albumArtist").Value;

            return track;
        }*/

        protected virtual Dictionary<string, string> TrackToNameValueCollection(Track track)
        {
            var nameValues = new Dictionary<string, string>();

            //Validator.ValidateObject(track, new ValidationContext(track, null, null));

            nameValues.Add(Track.ArtistNameParamName, track.ArtistName);
            nameValues.Add(Track.TrackNameParamName, track.TrackName);

            if (!string.IsNullOrEmpty(track.AlbumArtist)) nameValues.Add(Track.AlbumArtistParamName, track.AlbumArtist);
            if (!string.IsNullOrEmpty(track.AlbumName)) nameValues.Add(Track.AlbumNameParamName, track.AlbumName);
            //nameValues.Add(Track.DurationParamName, track.Duration.ToString());
            nameValues.Add(Track.DurationParamName, ((int)track.Duration.TotalSeconds).ToString());
            if (track.MusicBrainzId > 0) nameValues.Add(Track.MusicBrainzIdParamName, track.MusicBrainzId.ToString());
            if (track.TrackNumber > 0) nameValues.Add(Track.TrackNumberParamName, track.TrackNumber.ToString());

            if (track.WhenStartedPlaying.HasValue)
            {
                nameValues.Add(Track.WhenStartedPlayingParamName, DateTimeToTimestamp(track.WhenStartedPlaying.Value).ToString());
            }

            return nameValues;
        }

        /// <summary>
        /// Returns the local time value of a DateTime object to a UTC UNIX timestamp format (integer number of seconds since 00:00:00, January 1st 1970 UTC).           
        /// </summary>
        protected virtual int DateTimeToTimestamp(DateTime dateTime)
        {
            DateTime utcDateTime = dateTime.ToUniversalTime();
            var jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0);

            TimeSpan timeSinceJan1St1970 = utcDateTime.Subtract(jan1St1970);
            return (int) timeSinceJan1St1970.TotalSeconds;
        }
    }

    internal class GetInfoResponse : ErrorResponse
    {
        public TrackResponse track { get; set; }

    }

    public class TrackResponse
    {
        public string name { get; set; }
        public int listeners { get; set; }
        public int playcount { get; set; }
        public string url { get; set; }
    }

    internal class UpdateNowPlayingResponse : ErrorResponse
    {
        public NowPlaying nowplaying { get; set; }

    }

    internal class NowPlaying
    {
        public IgnoredMessage ignoredMessage { get; set; }
    }

    internal class IgnoredMessage
    {
        public string code { get; set; }
    }
}