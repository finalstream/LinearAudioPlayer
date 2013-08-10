using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    /// <summary>
    /// タグ編集用ファイル情報クラス。
    /// </summary>
    public class TagEditFileInfo
    {
        public long Id { get; set; }
        string _filePath = "";
        string _archiveFilePath = "";
        string _targetPath = "";
        
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string ArchiveFilePath
        {
            get { return _archiveFilePath; }
            set { _archiveFilePath = value; }
        }
        public string TargetPath
        {
            get { return _targetPath; }
            set { _targetPath = value; }
        }


    }
}
