using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer
{
    /// <summary>
    /// Linaer列挙型クラス。
    /// </summary>
    public class LinearEnum
    {
        /*
            列挙型
         */
        #region Enum

        /// <summary>
        /// プレイヤーモード
        /// </summary>
        public enum PlayMode : int
        {
            /// <summary>
            /// 標準
            /// </summary>
            NORMAL = 0,
            /// <summary>
            /// ONEOFF
            /// </summary>
            ONEOFF,
            /// <summary>
            /// REPEAT
            /// </summary>
            REPEAT,
            /// <summary>
            /// ENDLESS
            /// </summary>
            ENDLESS,
            /// <summary>
            /// RANDOM
            /// </summary>
            RANDOM,
            OVER
        }

        /// <summary>
        /// プレイリストモード
        /// </summary>
        public enum PlaylistMode : int
        {
            /// <summary>
            /// NONE(削除用)
            /// </summary>
            NONE = -1,
            /// <summary>
            /// ノーマル
            /// </summary>
            NORMAL = 0,
            /// <summary>
            /// お気に入り
            /// </summary>
            FAVORITE = 1,
            /// <summary>
            /// 除外
            /// </summary>
            EXCLUSION = 2,
            OVER
        }

        /// <summary>
        /// フィルタリングモード
        /// </summary>
        public enum FilteringMode : int
        {
            /// <summary>
            /// デフォルト
            /// </summary>
            DEFAULT = 0,
            /// <summary>
            /// アーティスト
            /// </summary>
            ARTIST = 1,
            /// <summary>
            /// アルバム
            /// </summary>
            ALBUM = 2,
            /// <summary>
            /// ジャンル
            /// </summary>
            GENRE = 3,
            /// <summary>
            /// 年
            /// </summary>
            YEAR = 4,
            /// <summary>
            /// タグ
            /// </summary>
            TAG = 5,
            /// <summary>
            /// フォルダ
            /// </summary>
            FOLDER = 6,
            OVER
        }

        /// <summary>
        /// サイレントモード
        /// </summary>
        public enum SilentMode : int {
            /// <summary>
            /// OFF
            /// </summary>
            OFF = 0,
            /// <summary>
            /// ON
            /// </summary>
            ON = 1
        }

        /// <summary>
        /// シャッフルモード
        /// </summary>
        public enum ShuffleMode : int
        {
            /// <summary>
            /// OFF
            /// </summary>
            OFF = 0,
            /// <summary>
            /// ON
            /// </summary>
            ON = 1
        }

        /// <summary>
        /// ソートモード
        /// </summary>
        public enum SortMode : int
        {
            /// <summary>
            /// DEFAULT
            /// </summary>
            DEFAULT = 0,
            /// <summary>
            /// ORIGINAL ARTIST
            /// </summary>
            ORIGINAL_ARTIST = 1
        }

        /// <summary>
        /// タイトルスクロールモード
        /// </summary>
        public enum TitleScrollMode :int
        {
            NONE = 0,
            LOOP = 1,
            REFLECT = 2,
            ROLL = 3
        }

        /// <summary>
        /// 再生方式
        /// </summary>
        public enum PlayMethod : int
        {
            /// <summary>
            /// メモリーロード
            /// </summary>
            MEMORY = 0,
            /// <summary>
            /// ディスクロード
            /// </summary>
            DISK = 1
        }

        /// <summary>
        /// 再生エンジン
        /// </summary>
        public enum PlayEngine : int
        {
            /// <summary>
            /// FMOD
            /// </summary>
            FMOD = 0,
            /// <summary>
            /// BASS
            /// </summary>
            BASS = 1
        }

        /// <summary>
        /// タグ編集ファイル情報
        /// </summary>
        public enum TagEditFileInfo : int
        {
            FILE_PATH = 0,
            TARGET_PATH = 1
        }

        /// <summary>
        /// データベースモード
        /// </summary>
        public enum DatabaseMode : int
        {
            MUSIC = 0,
            RADIO = 1
        }
        public enum RatingValue
        {
            /// <summary>
            /// 除外
            /// </summary>
            EXCLUSION = 0,
            /// <summary>
            /// 未評価
            /// </summary>
            NOTRATING = 1,
            // ノーマル
            NORMAL = 2,
            /// <summary>
            /// お気に入り
            /// </summary>
            FAVORITE = 9
        }

        public enum EnumGroupListOrder
        {
            COUNT_DESC,
            ALPHABET_ASC
        }

        #endregion

    }
}
