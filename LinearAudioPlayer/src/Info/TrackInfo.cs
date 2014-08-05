using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class TrackInfo
    {

        public long Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string PlayDateTime { get; set; }

        public string PlayDateTimeRelative { get; set; }

        public long Count { get; set; }

        public string TotalPlayTime { get; set; }

        public string PlaySeconds { get; set; }

        public int Rate { get; set; }

        public bool IsFavorite { get; set; }

        public TrackInfo(long id, string title, string artist, int rating)
        {
            Id = id;
            Title = title;
            Artist = artist;
            IsFavorite = rating == (int)LinearEnum.RatingValue.FAVORITE;
        }

        public TrackInfo(long id, string title, string artist, string playsec, string playDateTime, int rating)
        {
            Id = id;
            Title = title;
            Artist = artist;
            if (!string.IsNullOrEmpty(playsec)) PlaySeconds = TimeSpan.FromSeconds(int.Parse(playsec)).ToString();
            PlayDateTime = playDateTime;
            PlayDateTimeRelative = DateTimeUtils.getRelativeTimeString(playDateTime);
            IsFavorite = rating == (int)LinearEnum.RatingValue.FAVORITE;
        }

        public TrackInfo(string title,  long count,  int rate, string playtime, int rating = -1)
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
            IsFavorite = rating == (int)LinearEnum.RatingValue.FAVORITE;
        }

    }
}
