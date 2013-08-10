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
    internal class LastFmArtistApi
    {
        private const string GetTracksMethodName = "artist.getSimilar";

        /// <summary>
        /// Instantiates a
        /// </summary>
        public LastFmArtistApi()
        {

        }

        #region ITrackApi Members

        public List<Artist> GetSimilar(Track track, Authentication authentication)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add(Track.ArtistNameParamName, track.ArtistName);
            parameters.Add("limit", "6");

            var request = LastFmApiUtils.AddRequiredParams(Method.POST, parameters, GetTracksMethodName, authentication);

            
            var response = LastFmApiUtils.LastFmRestClient.Execute<GetSimilarResponse>(request);

            LastFmApiUtils.checkResponce(response);

            if (response.Data != null && response.Data.similarartists != null)
            {
                if (response.Data.similarartists.artist.Count > 0)
                {
                    return response.Data.similarartists.artist;
                }
            }
            
            return null;


        }

        #endregion

    }

    internal class GetSimilarResponse
    {
        public Similarartists similarartists { get; set; }
    }

    internal class Similarartists
    {
        public List<Artist> artist { get; set; }

    }

    internal class Artist
    {
        public string name { get; set; }

    }
}