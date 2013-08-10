using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Xml.XPath;
using Finalstream.LinearAudioPlayer.Plugin.Lastfm.Api;
using Lpfm.LastFmScrobbler;
using RestSharp;

namespace Finalstream.LinearAudioPlayer.Plugin.Lastfm
{
    internal class LastFmLibraryApi
    {
        private const string GetTracksMethodName = "library.getTracks";

        /// <summary>
        /// Instantiates a
        /// </summary>
        public LastFmLibraryApi()
        {

        }

        #region ITrackApi Members

        public TrackResponse GetTracks(Track track, Authentication authentication, string userName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add(Track.ArtistNameParamName, track.ArtistName);
            parameters.Add(Track.TrackNameParamName, track.TrackName);
            parameters.Add("limit", "1");
            parameters.Add("page", "1");

            if (!String.IsNullOrEmpty(userName))
            {
                parameters.Add("user", userName);
            }

            var request = LastFmApiUtils.AddRequiredParams(Method.POST, parameters, GetTracksMethodName, authentication);

            
            var response = LastFmApiUtils.LastFmRestClient.Execute<GetTracksResponse>(request);

            // TEST
            //request.Method = Method.GET;
            //var uri = LastFmApiUtils.LastFmRestClient.BuildUri(request);
            //Debug.WriteLine(uri);
            //Uri url = response.ResponseUri;
            LastFmApiUtils.checkResponce(response);

            if (response.Data != null && response.Data.tracks != null)
            {
                return response.Data.tracks.track;
            }
            
            return null;


        }

        #endregion

    }

    internal class GetTracksResponse : ErrorResponse
    {
        public Tracks tracks { get; set; }
    }

    internal class Tracks
    {
        public TrackResponse track { get; set; }
    }
}