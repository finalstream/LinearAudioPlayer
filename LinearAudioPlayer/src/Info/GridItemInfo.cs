using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FINALSTREAM.Commons.Grid;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class GridItemInfo : ISourceGridItem
    {

        // メンバ
        long _id = 0;
        string _filePath = "";
        string _icon = "";
        string _title = "";
        string _artist = "";
        string _time = "";
        string _bitrate = "";
        string _date = "";
        string _album = "";
        int _track = 0;
        string _genre = "";
        string _year = "";
        long _selection = (int) LinearEnum.PlaylistMode.NORMAL;
        string _adddate = "";
        public string AddDateTime { get; set; }
        private string _lastplaydate = "";
        public int Duration { get; set; }
        public string AlbumDescription { get; set; }
        // TODO: このデータをもたせる場所は本来ここではない。
        public bool IsChangeAlbum { get; set; }
        public bool IsSkipped { get; set; }

        int _rating = 0;
        int _playCount = 0;
        int _notFound = 0;
        string _tag = "";
        string _option = "";

        public bool IsNoPicture { get; set; }
        public Image Picture { get; set; }
        public string PictureUrl { get; set; }
        public bool IsInterrupt { get; set; }

        public string Option
        {
            get { return _option; }
            set { _option = value; }
        }

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public int PlayCount
        {
            get { return _playCount; }
            set { _playCount = value; }
        }

        public int Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        public int Track
        {
            get { return _track; }
            set { _track = value; }
        }

        public string Album
        {
            get { return _album; }
            set { _album = value; }
        }

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string FilePath
        {
          get { return _filePath; }
          set { _filePath = value; }
        }

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public string Title
        {
            get { return _title; }
            set { 
                if(!String.IsNullOrEmpty(value)){
                    _title = value;
                }
            }
        }

        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string Bitrate
        {
            get { return _bitrate; }
            set { _bitrate = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int NotFound
        {
            get { return _notFound; }
            set { _notFound = value; }
        }

        public long Selection
        {
            get { return _selection; }
            set { _selection = value; }
        }

        public string Adddate
        {
            get
            {
                return _adddate;
            }
            set { _adddate = value; }
        }

        public string Lastplaydate
        {
            get
            {
                return _lastplaydate;
            }
            set { _lastplaydate = value; }
        }

        public GridItemInfo()
        {
            PictureUrl = "";
        }
    }

}
