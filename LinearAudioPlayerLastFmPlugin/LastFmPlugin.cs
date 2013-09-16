using System;
using System.Diagnostics;
using System.Windows.Forms;
using FINALSTREAM.LinearAudioPlayer.Plugin;
using Lpfm.LastFmScrobbler;
using Lpfm.LastFmScrobbler.Api;

namespace Finalstream.LinearAudioPlayer.Plugin.Lastfm
{
    public class LastFmPlugin : ILinearAudioPlayerPlugin, ILinkLibrary
    {
        private const string ApiKey = "3608ae406292d722ef0548a749a09067";
        private const string ApiSecret = "f9aa18439610b324c7cd971ca21079d1";
        private Scrobbler _scrobbler;
        private Track CurrentTrack { get; set; }

        public LastFmPlugin()
        {
            Enable = false;
        }

        public string Name
        {
            get { return "Last.fm Plugin"; }
        }

        public string Author
        {
            get { return "FINALSTREAM"; }
        }

        public string Version
        {
            get { return "ver.1.10"; }
        }

        public string Description
        {
            get { return "Last.fmへScrobbleすることができます。Last.fmのアカウントと連携することで再生情報の統計や共有ができます。"; }
        }

        public bool Enable { get; set; }

        public bool Init()
        {
            //Properties.Settings.Default.Reload();
            //Properties.Settings.Default.Upgrade();

            Session session = GetSession();
            // instantiate the async scrobbler
            if (session != null && !String.IsNullOrEmpty(session.Key))
            {
                _scrobbler = new Scrobbler(ApiKey, ApiSecret, session);
                return true;
            }
            return false;
        }

        public LinkLibraryInfo RunAfterPlay(string title,
            string album,
            string artist,
            int trackNo,
            int duration,
            bool isLinkLibraryEnable
            )
        {
            LinkLibraryInfo lastFmInfo = new LinkLibraryInfo();

            if (_scrobbler == null)
            {
                return lastFmInfo;
            }

            CurrentTrack = new Track
            {
                TrackName = title,
                AlbumName = album,
                ArtistName = artist,
                TrackNumber = trackNo,
                Duration = new TimeSpan(0, 0, 0, duration)
            };
            CurrentTrack.WhenStartedPlaying = DateTime.Now;

            _scrobbler.NowPlaying(CurrentTrack);

            if (isLinkLibraryEnable)
            {
                // トラック情報取得
                var trackInfo = _scrobbler.GetTrackInfo(CurrentTrack, false);

                if (trackInfo != null)
                {
                    lastFmInfo.Listener = trackInfo.listeners;
                    lastFmInfo.AllPlaycount = trackInfo.playcount;
                }

                trackInfo = _scrobbler.GetLibraryTrack(CurrentTrack, true);

                if (trackInfo != null)
                {
                    lastFmInfo.UserPlaycount = trackInfo.playcount;
                }
                else
                {
                    lastFmInfo.UserPlaycount = -1;
                }

                lastFmInfo.SimilarArtists = _scrobbler.GetSimilarArtist(CurrentTrack);

            }

            return lastFmInfo;
        }

        public void RunAfterPlayCountUp(int rating)
        {
            if (_scrobbler == null)
            {
                return;
            }

            _scrobbler.Scrobble(CurrentTrack);

            if (rating == 9)
            {
                _scrobbler.Love(CurrentTrack);
            }
            else
            {
                _scrobbler.UnLove(CurrentTrack);
            }
        }

        public void Disabled()
        {
            Properties.Settings.Default.SessionKey = "";
            Properties.Settings.Default.Save();
        }

        public void Close()
        {
            
        }

        private Session GetSession()
        {

            // try get the session key from the registry
            Session session = new Session();

            Properties.Settings.Default.Upgrade();
                
            session.Key = Properties.Settings.Default.SessionKey;
            session.Username = Properties.Settings.Default.UserName;
  
            if (string.IsNullOrEmpty(session.Key) || string.IsNullOrEmpty(session.Username))
            {
                // instantiate a new scrobbler
                var scrobbler = new Scrobbler(ApiKey, ApiSecret, new Session(){Key = session.Key});

                //NOTE: This is demo code. You would not normally do this in a production application
                while (string.IsNullOrEmpty(session.Key) || string.IsNullOrEmpty(session.Username))
                {
                    // Try get session key from Last.fm
                    try
                    {
                        session = scrobbler.GetSession();

                        // successfully got a key. Save it to the registry for next time
                        Properties.Settings.Default.SessionKey = session.Key;
                        Properties.Settings.Default.UserName = session.Username;
                        Properties.Settings.Default.Save();
                    }
                    catch (LastFmApiException)
                    {
                        // get session key from Last.fm failed
                        //MessageBox.Show(exception.Message);
                        if (MessageBox.Show(
                            "Linear Audio Playerはlast.fmに対応しています。last.fmを使用しますか？\nはいを選択した場合はlast.fmのアカウントへの認証設定のページを開きます。","use last.fm?",MessageBoxButtons.YesNo)== DialogResult.No)
                        {
                            return null;
                        }

                        // get a url to authenticate this application
                        string url = scrobbler.GetAuthorisationUri();

                        // open the URL in the default browser
                        Process.Start(url);

                        // Block this application while the user authenticates
                        MessageBox.Show("ブラウザで許可したあとOKボタンをクリックしてください。");
                    }
                 }
            }

            return session;
        }

        public string getLinkLibraryTitle()
        {
            return "Last.fm   ";
        }

        public string getLinkLibraryDescription(LinkLibraryInfo linkLibraryInfo)
        {
            string yourScrobbleCount = "-";
            if (linkLibraryInfo.UserPlaycount != -1)
            {
                yourScrobbleCount = linkLibraryInfo.UserPlaycount.ToString();
            }

            return "Listeners: " + linkLibraryInfo.Listener + "   "
                               + "Scrobbles: " + linkLibraryInfo.AllPlaycount + "   "
                               + "Your Scrobbles: " + yourScrobbleCount;
        }
    }
}
