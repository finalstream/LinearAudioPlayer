using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class WebServiceResponseInfo
    {
        public TrackInfo[] recentListen;
        public TrackInfo[] topLists;
        public int pagerPrevious { get; set; }
        public int pagerNext { get; set; }
        public bool error { get; set; }
        public string action { get; set; }
        public PlayItemInfo playInfo { get; set; }
        public int seekRatio { get; set; }
        public bool isPlaying { get; set; }
        public bool isPaused { get; set; }
        public int volume { get; set; }
        public TrackInfo[] nowPlaying { get; set; }
        public string artworkUrl { get; set; }
        public string artworkThumbUrl { get; set; }
        public string[] themeList { get; set; }
        public AnalyzeInfo analyzeOverview { get; set; }
    }
}
