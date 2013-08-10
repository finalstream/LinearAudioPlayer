using System;
using System.Collections.Generic;
using Finalstream.LinearAudioPlayer.Plugin.Lastfm;
using Lpfm.LastFmScrobbler.Api;
using System.Net;

namespace Lpfm.LastFmScrobbler
{
    /// <summary>
    /// Allows a client to "scrobble" tracks to the Last.fm webservice as they are played
    /// </summary>
    public class Scrobbler
    {
        /// <summary>
        /// The format string pattern for the Last.fm Authorisation page
        /// </summary>
        public const string RequestAuthorisationUriPattern = "http://www.last.fm/api/auth/?api_key={0}&token={1}";

        /// <summary>
        /// The minimum allowed track length for scrobbling in Seconds
        /// </summary>
        public const int MinimumScrobbleTrackLengthInSeconds = 30;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey">An API Key from Last.fm. See http://www.last.fm/api/account </param>
        /// <param name="apiSecret">An API Secret from Last.fm. See http://www.last.fm/api/account </param>
        /// <param name="sessionKey">An authorized Last.fm Session Key. See <see cref="GetSession"/></param>
        /// <param name="username"> </param>
        /// <exception cref="ArgumentNullException"/>
        public Scrobbler(string apiKey, string apiSecret, Session session)
        {
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException("apiKey");
            if (string.IsNullOrEmpty(apiSecret)) throw new ArgumentNullException("apiSecret");

            Authentication = new Authentication {ApiKey = apiKey, ApiSecret = apiSecret};

            if (!string.IsNullOrEmpty(session.Key)) Authentication.Session = session;

            //AuthApi = new AuthApi();
            AuthApi = new LastFmAuthApi();
            //TrackApi = new TrackApi();
            TrackApi = new LastFmTrackApi();
            LibraryApi = new LastFmLibraryApi();
            ArtistApi = new LastFmArtistApi();
        }

        /// <summary>
        /// Pass in a WebProxy to be used by LPFM
        /// </summary>
        /// <param name="proxy">A configured WebProxy object</param>
        public static void SetWebProxy(WebProxy proxy)
        {
            // TODO:プロキシは不要。
            //Lpfm.LastFmScrobbler.Api.WebRequestRestApi.SetWebProxy(proxy);
        }

        public bool HasSession { get { return Authentication.Session != null; } }

        private Authentication Authentication { get; set; }
        internal AuthenticationToken AuthenticationToken { get; set; }

        internal LastFmAuthApi AuthApi { get; set; }
        //internal IAuthApi AuthApi { get; set; }
        //internal ITrackApi TrackApi { get; set; }
        internal LastFmTrackApi TrackApi { get; set; }
        internal LastFmLibraryApi LibraryApi { get; set; }
        internal LastFmArtistApi ArtistApi { get; set; }

        /// <summary>
        /// Returns a URI that the user should navigate to in their browser to authorise this Application (API Key) to access their account
        /// </summary>
        /// <remarks>See http://www.last.fm/api/desktopauth </remarks>
        public string GetAuthorisationUri()
        {
            // Fetch a request token
            GetAuthenticationToken();

            // Format a URI
            return string.Format(RequestAuthorisationUriPattern, Authentication.ApiKey, AuthenticationToken.Value);
        }

        /// <summary>
        /// Gets an authorised Session Key for the API Key and Secret provided. You must Authorise first, see <see cref="GetAuthorisationUri"/>. 
        /// See http://www.last.fm/api/desktopauth for the full Authorisation process. Sets the Authentication Session for this instance at the same time
        /// </summary>
        /// <returns>The session key returned from Last.fm as string to be cached or stored by the client</returns>
        /// <remarks>Session Keys are forever. Once a client has obtained a session key it should be cached or stored by the client, and the next time this
        /// Scrobbler is instantiated, it should be passed in with the constructor arguments</remarks>
        /// <exception cref="InvalidOperationException"/>
        public Session GetSession()
        {
            //if (Authentication.Session != null && !string.IsNullOrEmpty(Authentication.Session.Key))
            //    throw new InvalidOperationException("This Scrobbler already has a Session Key");

            // Get a token
            GetAuthenticationToken();

            // Request a Session
            AuthApi.GetSession(Authentication, AuthenticationToken);

            if (Authentication.Session == null) throw new InvalidOperationException();

            return Authentication.Session;
        }

        /// <summary>
        /// Submits a Track Update Now Playing request to the Last.fm web service
        /// </summary>
        /// <param name="track">A <see cref="Track"/></param>
        /// <returns>A <see cref="NowPlayingResponse"/></returns>
        public bool NowPlaying(Track track)
        {
            return TrackApi.UpdateNowPlaying(track, Authentication);
        }

        /// <summary>
        /// Submits a Track Scrobble request to the Last.fm web service
        /// </summary>
        /// <param name="track">A <see cref="Track"/></param>
        /// <returns>A <see cref="ScrobbleResponse"/></returns>
        /// <remarks>A track should only be scrobbled when the following conditions have been met: The track must be longer than 30 seconds. 
        /// And the track has been played for at least half its duration, or for 4 minutes (whichever occurs earlier). See http://www.last.fm/api/scrobbling </remarks>
        /// <exception cref="InvalidOperationException"/>
        public bool Scrobble(Track track)
        {
            /*
            if (track.Duration.TotalSeconds < MinimumScrobbleTrackLengthInSeconds)
            {
                return null;
                throw new InvalidOperationException(string.Format("Duration is too short. Tracks shorter than {0} seconds in duration must not be scrobbled",
                                                                  MinimumScrobbleTrackLengthInSeconds));
            }*/

            if (!track.WhenStartedPlaying.HasValue) throw new ArgumentException("A Track must have a WhenStartedPlaying value when Scrobbling");

            /*
            int minimumPlayingTime = (int) track.Duration.TotalSeconds/2;
            if (minimumPlayingTime > (4*60)) minimumPlayingTime = (4*60);
            if (track.WhenStartedPlaying > DateTime.Now.AddSeconds(-minimumPlayingTime))
            {
                throw new InvalidOperationException(
                    "Track has not been playing long enough. A scrobbled track must have been played for at least half its duration, or for 4 minutes (whichever occurs earlier)");
            }*/

            return TrackApi.Scrobble(track, Authentication);
        }

        /// <summary>
        /// Submits a Track Love request to the Last.fm web service
        /// </summary>
        /// <param name="track">A <see cref="Track"/></param>
        /// <returns>A <see cref="RatingResponse"/></returns>
        /// <remarks>The <see cref="Track"/> passed in must be a "Corrected Track" as 
        /// returned in <see cref="ScrobbleResponse"/> or <see cref="NowPlayingResponse"/>
        public bool Love(Track track)
        {
            var result = TrackApi.Love(track, Authentication);
            //result.Track = track;
            return result;
        }

        /// <summary>
        /// Submits a Track unLove request to the Last.fm web service
        /// </summary>
        /// <param name="track">A <see cref="Track"/></param>
        /// <returns>A <see cref="RatingResponse"/></returns>
        /// <remarks>The <see cref="Track"/> passed in must be a "Corrected Track" as 
        /// returned in <see cref="ScrobbleResponse"/> or <see cref="NowPlayingResponse"/>
        public bool UnLove(Track track)
        {
            var result = TrackApi.UnLove(track, Authentication);
            //result.Track = track;
            return result;
        }

        /// <summary>
        /// Submits a Track Ban request to the Last.fm web service
        /// </summary>
        /// <param name="track">A <see cref="Track"/></param>
        /// <returns>A <see cref="RatingResponse"/></returns>
        /// <remarks>The <see cref="Track"/> passed in must be a "Corrected Track" as 
        /// returned in <see cref="ScrobbleResponse"/> or <see cref="NowPlayingResponse"/>
        public bool Ban(Track track)
        {
            var result = TrackApi.Ban(track, Authentication);
            //result.Track = track;
            return result;
        }

        /// <summary>
        /// Submits a Track UnBan request to the Last.fm web service
        /// </summary>
        /// <param name="track">A <see cref="Track"/></param>
        /// <returns>A <see cref="RatingResponse"/></returns>
        /// <remarks>The <see cref="Track"/> passed in must be a "Corrected Track" as 
        /// returned in <see cref="ScrobbleResponse"/> or <see cref="NowPlayingResponse"/>
        public bool UnBan(Track track)
        {
            var result = TrackApi.UnBan(track, Authentication);
            //result.Track = track;
            return result;
        }

        public TrackResponse GetTrackInfo (Track track, bool isUserOnly)
        {
            string userName = null;

            if (isUserOnly)
            {
                userName = Authentication.Session.Username;
            }

            var result = TrackApi.GetInfo(track, Authentication, userName);

            return result;
        }

        public TrackResponse GetLibraryTrack(Track track, bool isUserOnly)
        {
            string userName = null;

            if (isUserOnly)
            {
                userName = Authentication.Session.Username;
            }

            var result = LibraryApi.GetTracks(track, Authentication, userName);

            return result;
        }

        public List<string> GetSimilarArtist(Track track)
        {

            var result = ArtistApi.GetSimilar(track, Authentication);

            List<string> artistList = new List<string>();
            
            if (result != null)
            {
                foreach (var artist in result)
                {
                    artistList.Add(artist.name);
                }
            }

            return artistList;
        }

        protected virtual void GetAuthenticationToken()
        {
            if (AuthenticationToken != null && AuthenticationToken.IsValid()) return;

            AuthenticationToken = AuthApi.GetToken(Authentication);
        }
    }
}