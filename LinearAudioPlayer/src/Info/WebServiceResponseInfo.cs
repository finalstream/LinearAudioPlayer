using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class WebServiceResponseInfo
    {
        public bool error { get; set; }
        public string action { get; set; }
        public PlayItemInfo playInfo { get; set; }
        public int seekRatio { get; set; }
        public bool isPlaying { get; set; }
        public bool isPaused { get; set; }
        public int volume { get; set; }
    }
}
