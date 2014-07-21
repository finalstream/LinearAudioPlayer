using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class TrackInfo
    {

        public long Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string PlayDateTime { get; set; }

        public long Count { get; set; }

        public string TotalPlayTime { get; set; }

        public int Rate { get; set; }

        public TrackInfo(long id, string title, string artist)
        {
            Id = id;
            Title = title;
            Artist = artist;
        }

        public TrackInfo(long id, string title, string artist, string playDateTime)
        {
            Id = id;
            Title = title;
            Artist = artist;
            PlayDateTime = playDateTime;
        }

        public TrackInfo(string title,  long count,  int rate, string playtime)
        {
            Title = title;
            Count = count;
            Rate = rate;
            if (!string.IsNullOrEmpty(playtime))
            {
                var playtimespan = TimeSpan.FromSeconds(int.Parse(playtime));
                TotalPlayTime = playtimespan.ToString();
            }
            else
            {
                TotalPlayTime = "";
            }
        }

    }
}
