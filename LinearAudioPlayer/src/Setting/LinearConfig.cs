using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FINALSTREAM.LinearAudioPlayer.Grid;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    public class LinearConfig
    {

        /*
         * プライベートメンバ
         */
        #region Private Member

        string _version;
        ViewConfig _viewConfig;
        PlayerConfig _playerConfig;
        SoundConfig _soundConfig;
        DatabaseConfig _databaseConfig;
        EngineConfig _engineConfig;

        

        
        #endregion

        /*
         * プロパティ
         */
        #region Property

        /// <summary>
        /// バージョン
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 表示設定
        /// </summary>
        public ViewConfig ViewConfig
        {
            get { return _viewConfig; }
            set { _viewConfig = value; }
        }

        /// <summary>
        /// プレイヤー設定
        /// </summary>
        public PlayerConfig PlayerConfig
        {
            get { return _playerConfig; }
            set { _playerConfig = value; }
        }

        /// <summary>
        /// サウンド設定
        /// </summary>
        public SoundConfig SoundConfig
        {
            get { return _soundConfig; }
            set { _soundConfig = value; }
        }

        /// <summary>
        /// データベース設定
        /// </summary>
        public DatabaseConfig DatabaseConfig
        {
            get { return _databaseConfig; }
            set { _databaseConfig = value; }
        }

        /// <summary>
        /// エンジン設定
        /// </summary>
        public EngineConfig EngineConfig
        {
            get { return _engineConfig; }
            set { _engineConfig = value; }
        }


        #endregion


        public LinearConfig()
        {
            this._version = "";
            ViewConfig vc = new ViewConfig();
            this.ViewConfig = vc;
            PlayerConfig pc = new PlayerConfig();
            this.PlayerConfig = pc;
            SoundConfig sc = new SoundConfig();
            this.SoundConfig = sc;
            DatabaseConfig dc = new DatabaseConfig();
            this.DatabaseConfig = dc;
            EngineConfig ec = new EngineConfig();
            this.EngineConfig = ec;

        }

    }
}
