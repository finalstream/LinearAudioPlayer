using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FINALSTREAM.LinearAudioPlayer.Info;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    /// <summary>
    /// プレイヤー設定クラス
    /// </summary>
    public class PlayerConfig
    {

        int _playMode;
        int _playlistMode;
        string _selectDatabase;
        int _filteringMode;
        private string _selectFilter;
        string _supportAudioExtension;
        long _resumeId;
        int _resumePosition;
        private bool _resumePlay;
        private string[] _exclusionKeywords = {};
        //public int PlayCountUpSeconds { get; set; }
        public int PlayCountUpRatio { get; set; }
        public string TempDirectory { get; set; }
        public bool isRandomStyleSelect { get; set; }
        public bool isAlbumAutoRename { get; set; }
        public string albumAutoRenameTemplete { get; set; }
        public int GroupMode { get; set; }

        public bool IsOpenPlaylist { get; set; }

        public int SortMode { get; set; }
        public bool IsLinkLibrary { get; set; }
        public bool IsAutoUpdate { get; set; }

        public string MovieSearchUrl { get; set; }

        /// <summary>
        /// 除外するプラグイン
        /// </summary>
        public string[] ExcludePlugins { get; set; }

        /// <summary>
        /// シャッフルモード
        /// </summary>
        public int ShuffleMode { get; set; }

        public AudioFileRegistInfo AudioFileAutoRegistInfo { get; set; }


        /// <summary>
        /// 再生モード
        /// </summary>
        public int PlayMode
        {
            get { return _playMode; }
            set { _playMode = value; }
        }

        /// <summary>
        /// プレイリストモード
        /// </summary>
        public int PlaylistMode
        {
            get { return _playlistMode; }
            set { _playlistMode = value; }
        }

        /// <summary>
        /// 選択データベース
        /// </summary>
        public string SelectDatabase
        {
            get { return _selectDatabase; }
            set { _selectDatabase = value; }
        }

        /// <summary>
        /// フィルタリングモード
        /// </summary>
        public int FilteringMode
        {
            get { return _filteringMode; }
            set { _filteringMode = value; }
        }

        /// <summary>
        /// 対応オーディオ拡張子
        /// </summary>
        public string SupportAudioExtension
        {
            get { return _supportAudioExtension; }
            set { _supportAudioExtension = value; }
        }

        /// <summary>
        /// レジュームID
        /// </summary>
        public long ResumeId
        {
            get { return _resumeId; }
            set { _resumeId = value; }
        }

        /// <summary>
        /// レジューム位置
        /// </summary>
        public int ResumePosition
        {
            get { return _resumePosition; }
            set { _resumePosition = value; }
        }

        /// <summary>
        /// レジューム再生
        /// </summary>
        public bool ResumePlay
        {
            get { return _resumePlay; }
            set { _resumePlay = value; }
        }

        /// <summary>
        /// 除外キーワード
        /// </summary>
        public string[] ExclusionKeywords
        {
            get { return _exclusionKeywords; }
            set { _exclusionKeywords = value; }
        }

        /// <summary>
        /// 選択フィルタ
        /// </summary>
        public string SelectFilter
        {
            get { return _selectFilter; }
            set { _selectFilter = value; }
        }

        /// <summary>
        /// 残り回数(Normal)
        /// </summary>
        public int RestCount { get; set; }

        /// <summary>
        /// Max回数(Normal)
        /// </summary>
        public int RestMaxCount { get; set; }

        public PlayerConfig()
        {

            this._playMode = (int)LinearEnum.PlayMode.NORMAL;
            this._playlistMode = (int)LinearEnum.PlaylistMode.NORMAL;
            this._selectDatabase = 
                Path.GetFileNameWithoutExtension(
                LinearConst.LINEAR_DBFILE);
            this._filteringMode = (int)LinearEnum.FilteringMode.DEFAULT;
            this._selectFilter = "ALL MUSIC";
            this._supportAudioExtension = ".mp3,.ogg,.wma,.flac,.wav,.aac,.m4a";
            this._resumeId = -1;
            this._resumePosition = -1;
            this._resumePlay = true;
            PlayCountUpRatio = 50;
            TempDirectory = Path.GetTempPath();
            isAlbumAutoRename = false;
            albumAutoRenameTemplete = LinearConst.DEFAULT_ALBUM_RENAME_TEMPLETE;
            ExcludePlugins = new string[]{};
            ShuffleMode = (int)LinearEnum.ShuffleMode.OFF;
            IsOpenPlaylist = true;
            SortMode = (int) LinearEnum.SortMode.DEFAULT;
            IsLinkLibrary = true;
            RestCount = 0;
            RestMaxCount = -1;
            this.AudioFileAutoRegistInfo = new AudioFileRegistInfo();
            IsAutoUpdate = true;
            MovieSearchUrl = "http://www.youtube.com/results?search_sort=video_view_count&search_query=#KEYWORD#";
        }

    }
}
