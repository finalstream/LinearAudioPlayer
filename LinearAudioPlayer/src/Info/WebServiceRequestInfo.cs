using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class WebServiceRequestInfo
    {
        /// <summary>
        /// Action Name
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// Seek位置(%)
        /// </summary>
        public double seekPosition { get; set; }

    }
}
