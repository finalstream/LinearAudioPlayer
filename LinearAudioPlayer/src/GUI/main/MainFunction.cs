using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Windows.Forms;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Grid;
using System.Diagnostics;
using System.Data.SQLite;
using FINALSTREAM.LinearAudioPlayer.Core;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Resources;


namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    class MainFunction
    {
        /*
         * パブリックメソッド 
         */
        #region Public Method

        private static bool isReverse = false;
        public static bool isRoll = false;
        public static int rolltop = 0;
        public static long rollid;
        /// <summary>
        /// タイトルをスクロールする
        /// </summary>
        /// <param name="title"></param>
        public void scrollTitle(Label title,int baseleft){

            switch (LinearGlobal.LinearConfig.ViewConfig.TitleScrollMode)
            {
                case LinearEnum.TitleScrollMode.LOOP:

                    title.Left = title.Left - 1;
                    if (title.Left <= -title.Width)
                    {
                        title.Left = baseleft;
                    }
                    break;
                case LinearEnum.TitleScrollMode.REFLECT:
                    if (!isReverse)
                    {
                        title.Left -= 1;
                        if (title.Left <= 0)
                        {
                            isReverse = true;
                        }
                    }
                    else
                    {
                        title.Left += 1;
                        if (title.Left + title.Width >= baseleft)
                        {
                            isReverse = false;
                        }
                    }
                    
                    
                    break;
                case LinearEnum.TitleScrollMode.ROLL:
                    if (isRoll)
                    {
                        title.Top = title.Top + 1;
                        if (title.Top >= title.Height)
                        {
                            title.Top = -title.Height;
                            LinearGlobal.MainForm.setTitle(LinearAudioPlayer.PlayController.createTitle());
                            LinearGlobal.MainForm.setTitleCentering();
                            rollid = LinearGlobal.CurrentPlayItemInfo.Id;
                        }
                        if (title.Top == rolltop && rollid == LinearGlobal.CurrentPlayItemInfo.Id)
                        {
                            isRoll = false;
                        }
                        else if (title.Top == rolltop && rollid != LinearGlobal.CurrentPlayItemInfo.Id)
                        {
                            isRoll = true;
                        }
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        /// <summary>
        /// プレイヤーの終了処理
        /// </summary>
        public void endPlayer() {
            LinearGlobal.MainForm.setTitle("Now Finalize...");
            LinearGlobal.MainForm.setTitleCentering();
            LinearGlobal.MainForm.Refresh();
            
            // 再生中の場合、レジューム情報保存
            if (LinearAudioPlayer.PlayController.isPlaying())
            {
                LinearGlobal.LinearConfig.PlayerConfig.ResumePosition = (int)LinearAudioPlayer.PlayController.getPosition();
            }
            else
            {
                LinearGlobal.LinearConfig.PlayerConfig.ResumePosition = -1;
            }

            // 終了する際は必ずフェードアウトする。
            bool bkFadeEffect = 
                LinearGlobal.LinearConfig.SoundConfig.FadeEffect;
            LinearGlobal.LinearConfig.SoundConfig.FadeEffect = true;
            LinearAudioPlayer.PlayController.stop();
            LinearGlobal.LinearConfig.SoundConfig.FadeEffect = bkFadeEffect;

            System.Threading.Thread.Sleep((int) LinearGlobal.LinearConfig.SoundConfig.FadeDuration);

            LinearAudioPlayer.endApplication();
        }


        /// <summary>
        /// IDがDBに存在するか確認する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool isIdRegistDatabase(long id)
        {
            long result = (long) SQLiteManager.Instance.executeQueryOnlyOne(
                SQLResource.SQL015, new SQLiteParameter("Id", id));

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 再生回数をカウントアップする
        /// </summary>
        public static long  incrementPlayCountUp()
        {
            long playcount = -1;

            if (LinearGlobal.CurrentPlayItemInfo.Id != -1)
            {
                List<DbParameter> paramList = new List<DbParameter>();
                paramList.Add(new SQLiteParameter("Id", LinearGlobal.CurrentPlayItemInfo.Id));
                object result = SQLiteManager.Instance.executeQueryOnlyOne(
                    SQLResource.SQL017, paramList);

                if (result != null && !"".Equals(result))
                {
                    playcount = (long)result;
                    playcount++;
                    paramList.Add(new SQLiteParameter("PlayCount", playcount));
                    SQLiteManager.Instance.executeNonQuery(SQLResource.SQL022, paramList);

                    
                }

                
            }

            return playcount;

        }

        #endregion


        #region Private Method



        #endregion

    }
}
