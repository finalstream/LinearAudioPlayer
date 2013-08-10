using System.Collections.Generic;
using System.Xml.XPath;
using Finalstream.LinearAudioPlayer.Plugin.Lastfm;
using Finalstream.LinearAudioPlayer.Plugin.Lastfm.Api;
using Lpfm.LastFmScrobbler;
using Lpfm.LastFmScrobbler.Api;
using RestSharp;

namespace Finalstream.LinearAudioPlayer.Plugin.Lastfm
{
    /// <summary>
    /// An API for some of the methods of the Last.fm Auth (Authentication) API
    /// </summary>
    /// <remarks>This class is named for the API that is wraps</remarks>
    internal class LastFmAuthApi
    {
        private const string AuthGetTokenXPath = "/lfm/token";
        private const string GetTokenMethodName = "auth.getToken";
        internal const string GetSessionMethodName = "auth.getSession";



        internal LastFmAuthApi()
        {
            LastFmRestApi = new LastFmRestApi();
        }

        // REST API
        private LastFmRestApi LastFmRestApi { get; set; }

        #region IAuthApi Members

        /// <summary>
        /// Fetch an unathorized request token for an API account
        /// </summary>
        public AuthenticationToken GetToken(Authentication authentication)
        {

            var parameters = new Dictionary<string, string>();
            var request  = LastFmApiUtils.AddRequiredParams(Method.GET, parameters, GetTokenMethodName, authentication, false);

            var response = LastFmApiUtils.LastFmRestClient.Execute<GetTokenResponse>(request);

            LastFmApiUtils.checkResponce(response);

            return new AuthenticationToken(response.Data.token);
        }

        /// <summary>
        /// Fetch a session key for a user
        /// </summary>
        public void GetSession(Authentication authentication, AuthenticationToken token)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("token", token.Value);

            var request = LastFmApiUtils.AddRequiredParams(Method.GET, parameters, GetSessionMethodName, authentication);

            var response = LastFmApiUtils.LastFmRestClient.Execute<GetSessionResponse>(request);

            LastFmApiUtils.checkResponce(response);

            if (response.Data == null || response.Data.error != null)
            {
                throw new LastFmApiException("token error");
            }

            authentication.Session = new Session
            {
                Subscriber = response.Data.session.subscriber,
                Key = response.Data.session.key,
                Username = response.Data.session.name
            };
        }

        #endregion

    }

    internal class GetSessionResponse : ErrorResponse
    {
        public SessionResponse session { get; set; }
    }

    internal class SessionResponse
    {
        public string name { get; set; }
        public string key { get; set; }
        public string subscriber { get; set; }
    }

    internal class GetTokenResponse
    {
        public string token { get; set; }

    }
}