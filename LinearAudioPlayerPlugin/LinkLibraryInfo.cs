using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Plugin
{
    public class LinkLibraryInfo
    {
        public int Listener { get; set; }

        public int AllPlaycount { get; set; }

        public int UserPlaycount { get; set; }

        public List<string> SimilarArtists { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// 画面に表示するタイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 画面に表示する情報
        /// </summary>
        public string Description { get; set; }

    }
}
