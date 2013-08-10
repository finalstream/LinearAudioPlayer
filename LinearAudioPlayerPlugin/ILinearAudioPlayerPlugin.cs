using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Plugin
{
    /// <summary>
    /// Linear Audio Player プラグインインタフェース
    /// </summary>
    public interface ILinearAudioPlayerPlugin
    {
        /// <summary>
        /// プラグインの名前
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 作成者
        /// </summary>
        string Author { get; }

        /// <summary>
        /// プラグインのバージョン
        /// </summary>
        string Version { get;}

        /// <summary>
        /// プラグインの説明
        /// </summary>
        string Description { get; }

        bool Enable { get; set; }

        /// <summary>
        /// 初期化する
        /// </summary>
        bool Init();

        /// <summary>
        /// 再生後実行
        /// </summary>
        /// <param name="arg"></param>
        LinkLibraryInfo RunAfterPlay(string title,
            string album,
            string artist,
            int trackNo,
            int duration,
            bool isLinkLibraryEnable
            );

        /// <summary>
        /// 再生カウントアップ後実行
        /// </summary>
        /// <param name="arg"></param>
        void RunAfterPlayCountUp(int rating);

        /// <summary>
        /// 無効化する
        /// </summary>
        void Disabled();

        /// <summary>
        /// クローズする
        /// </summary>
        void Close();
    }
}
