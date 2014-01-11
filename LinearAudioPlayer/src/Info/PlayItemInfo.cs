using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class PlayItemInfo
    {
        public string Title { set; get; }
        public string Artist { set; get; }
        public string Album { set; get; }
        public string Time { set; get; }
        public int GridRowNo { set; get; }
        public long Id { set; get; }
        public string FilePath { set; get; }
        public string Option { set; get; }
        public int Track { set; get; }
        public string Genre { set; get; }
        public string Year { set; get; }
        public string ArtworkUrl { set; get; }
        public int Duration { set; get; }
        public int Rating { set; get; }
        public int PlayCount { set; get; }
        public long PlayCountUpSecond { set; get; }
        public string LastPlayDate { set; get; }
        public string AlbumDescription { set; get; }

        public override string ToString()
        {
            return Title + " - " + Artist;
        }
        
        public PlayItemInfo()
        {
            GridRowNo = -1;
            Id = -1;
            Title = "";
            Artist = "";
            Album = "";
        }

    }
}
