using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Controls.Toast;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Exceptions;
using FINALSTREAM.Commons.Network;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Engine;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.LinearAudioPlayer.GUI;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Plugin;
using FINALSTREAM.LinearAudioPlayer.Resources;
using FINALSTREAM.LinearAudioPlayer.Setting;
using Finalstream.LinearAudioPlayer.Plugin;
using Microsoft.WindowsAPICodePack.Taskbar;
using SourceGrid;
using TagLib;
using File = System.IO.File;
using FINALSTREAM.Commons.Service;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    /// <summary>
    /// プレイヤーコントローラクラス
    /// </summary>
    public class PlayerController : IDisposable
    {
        /*
            プライベートメンバ 
        */
        #region Private Member

        private IPlayEngine _playEngine;
        private int errorCount = 0;

        #endregion

        // チャネル
        public static int MainChannel = 0;
        public static int SubChannel = 1;
        public const int SPECTRUMSIZE = 128;
        private PlayingListController playingListController = null;
        
        
        public MedleyInfo MedleyInfo = new MedleyInfo();

        #region delegate

        //  デリゲートを定義。

        delegate void FadeVolumeDelegate(
            FadeMode fadeMode,
            int channel,
            bool isFadeEffect,
            float fadeDuration);

        #endregion


        #region Enum

        /// <summary>
        /// フェードモード
        /// </summary>
        private enum FadeMode : int
        {
            FADE_IN = 0,
            FADE_OUT = 1
        }

        #endregion


        /*
         * プロパティ
         */
        #region Property


        #endregion



        /*
            パブリックメソッド
        */
        #region Public Mehod

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerController()
        {
            playingListController = new PlayingListController();

            switch (LinearGlobal.LinearConfig.EngineConfig.PlayEngine)
            {
                case LinearEnum.PlayEngine.FMOD:
                    this._playEngine = new FMODEngine();
                    break;
                case LinearEnum.PlayEngine.BASS:
                    this._playEngine = new BASSEngine();
                    break;
            }

            this._playEngine.init();

           
            
        }

        /// <summary>
        /// 再生前に行う処理
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        private string beforePlay(PlayItemInfo pii)
        {
            MainFunction.isRoll = true;

            // IDからFILEPATHとOPTIONを取得。
            IList<object> resultList = SQLiteManager.Instance.executeQueryOneRecord(
                SQLResource.SQL038, new SQLiteParameter("Id", pii.Id));
            if (resultList.Count > 0)
            {
                pii.FilePath = resultList[0].ToString();
                pii.Option = resultList[1].ToString();
                pii.Time = resultList[2].ToString();
                pii.Rating = unchecked((int)((long) resultList[3]));
                pii.PlayCount = unchecked((int)((long)resultList[4]));
            }
            else
            {
                return "";
            }


            if (!String.IsNullOrEmpty(pii.Option))
            {
                // 書庫ファイルだったら解凍する(オプションがあるのは書庫ファイルということ)
                string filePath = Path.Combine(LinearGlobal.TempDirectory, pii.Option);

                if (!File.Exists(filePath))
                {
                    SevenZipManager.Instance.extract(pii.FilePath, pii.Option, LinearGlobal.TempDirectory);
                }
                pii.FilePath = filePath;



            }

            if (!File.Exists(pii.FilePath))
            {
                // 解凍失敗
                return "";
            }

            // チャンネルセレクト
            if (MainChannel == 0)
            {
                MainChannel = 1;
                SubChannel = 0;
            }
            else
            {
                MainChannel = 0;
                SubChannel = 1;
            }

            // 現在再生している場合は停止してリリースする。
            if (_playEngine.isPlaying(MainChannel))
            {
                stop(MainChannel);
            }

            // ファイルをオープン
            _playEngine.open(pii.FilePath);

            if (LinearGlobal.CurrentPlayItemInfo.Id != -1)
            {
                // 現在(一つ前)のRowNo色とアイコンを戻す
                setNoPlayStatus();
            }

            

            return pii.FilePath;

        }
        
        /// <summary>
        /// 再生してない状態にセット（カラーとアイコン)
        /// </summary>
        private void setNoPlayStatus()
        {
            int prevRowNo = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID, LinearGlobal.CurrentPlayItemInfo.Id.ToString());
            LinearAudioPlayer.GridController.setRowColor(prevRowNo, GridController.EnuPlayType.NOPLAY);
            if (!LinearGlobal.MainForm.ListForm.Grid.Focused)
            {
                LinearAudioPlayer.GridController.Grid.Selection.SelectRow(prevRowNo, false);

            }
            LinearAudioPlayer.GridController.setPlayIcon(prevRowNo, GridController.EnuGridIcon.NONE, false);

        }

        /// <summary>
        /// Idを再生する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPause"></param>
        public void play(long id, bool isPause, bool isInterrupt)
        {
            PlayItemInfo pii = new PlayItemInfo();
            pii.Id = id;
            play(pii, isPause, isInterrupt);
        }

        /// <summary>
        /// Idを再生する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPause"></param>
        public void playForGridNo(int rowno, bool isPause)
        {
            if (LinearAudioPlayer.GridController.getRowCount() > 0)
            {
                PlayItemInfo pii = new PlayItemInfo();
                pii.GridRowNo = rowno;
                pii.Id = (long) LinearAudioPlayer.GridController.Grid[rowno, (int) GridController.EnuGrid.ID].Value;

                play(pii, isPause, false);
            }
        }

        /// <summary>
        /// PlayItemInfoを再生をする
        /// </summary>
        /// <param name="filePath">GridRowNo</param>
        public void play(PlayItemInfo pii, bool isPause, bool isInterrupt)
        {

            // 同じIDなら一時停止なら再開して抜ける
            if (_playEngine.isPasued()
                && LinearGlobal.CurrentPlayItemInfo.Id == pii.Id)
            {
                pause();
                return;
            }


            // 再生前処理
            pii.FilePath = beforePlay(pii);

            if (LinearGlobal.LinearConfig.SoundConfig.IsVolumeNormalize)
            {
                applyNormalize(true);
            }

            // 再生
            setVolume(MainChannel,0);
            if (pii.FilePath != "" && _playEngine.play(isPause))
            {
                


                this.startFadeIn();

                // クロスフェード
                this.startFadeOut(SubChannel, false);
                    //this.setVolume(LinearGlobal.Volume);
                // 再生後処理
                afterPlay(pii, isInterrupt);

                errorCount = 0;
            }
            else
            {
                // 再生に失敗したとき
                int rowno = LinearAudioPlayer.GridController.Find((int) GridController.EnuGrid.ID, pii.Id.ToString());

                if (rowno >= 0)
                {
                    updateNotfound(rowno, true);
                }

                // リピート以外は次の曲
                if (LinearEnum.PlayMode.REPEAT != LinearGlobal.PlayMode)
                {
                    if (errorCount > 10)
                    {
                        stop(MainChannel);
                        return;
                    }
                    errorCount++;
                    this.endOfStream();
                }
                else
                {
                    stop(MainChannel);
                }
                
            }

        }

        /// <summary>
        /// NOTFOUND状態を更新する。
        /// </summary>
        /// <param name="rowNo"></param>
        public void updateNotfound(int rowNo, bool isNotFound)
        {
            GridItemInfo gi = (GridItemInfo)LinearAudioPlayer.GridController.getRowGridItem(rowNo);
            // NOTFOUNDフラグ更新
            if (isNotFound)
            {
                gi.NotFound = LinearConst.FLG_ON;
            }
            else
            {
                gi.NotFound = LinearConst.FLG_OFF;
            }

            updateGridItem(gi, rowNo);
            if (isNotFound)
            {
                LinearAudioPlayer.GridController.setPlayIcon(rowNo, GridController.EnuGridIcon.NOTFOUND, true);
            }
            else
            {
                LinearAudioPlayer.GridController.setPlayIcon(rowNo, GridController.EnuGridIcon.NONE, false);
            }
            
            LinearAudioPlayer.GridController.setRowColor(rowNo, GridController.EnuPlayType.NOPLAY);
        }

        /// <summary>
        /// 割り込みリストに追加
        /// </summary>
        /// <param name="gridRowNo"></param>
        public void addInterruptItem(int gridRowNo)
        {
            List<int> rowNoList = new List<int>();
            rowNoList.Add(gridRowNo);
            addInterruptItem(rowNoList);
        }

        /// <summary>
        /// 割り込みリストに追加
        /// </summary>
        /// <param name="rowNolist"></param>
        public void addInterruptItem(IList<int> rowNoList)
        {
           foreach (var rowNo in rowNoList)
            {
                /*
                PlayItemInfo iii = new PlayItemInfo();
                iii.Id = long.Parse(LinearAudioPlayer.GridController.getValue(rowNo, (int)GridController.EnuGrid.ID));
                iii.FilePath = LinearAudioPlayer.GridController.getValue(rowNo, (int)GridController.EnuGrid.FILEPATH);
                iii.Option = LinearAudioPlayer.GridController.getValue(rowNo, (int)GridController.EnuGrid.OPTION);
                iii.Title = LinearAudioPlayer.GridController.getValue(rowNo, (int)GridController.EnuGrid.TITLE);
                iii.Artist = LinearAudioPlayer.GridController.getValue(rowNo, (int)GridController.EnuGrid.ARTIST);

                LinearGlobal.MainForm.ListForm.InterruptForm.InterruptListBox.Items.Add(iii);
                 * */
                playingListController.interruptRowNo(rowNo);
            }
            LinearGlobal.MainForm.setTitle(createTitle());
        }

        /// <summary>
        /// 再生後処理をする
        /// </summary>
        private void afterPlay(PlayItemInfo pii, bool isInterrupt)
        {
            

            if (MedleyInfo.Enable)
            {
                MedleyInfo.initMedley(pii.Id, (int) getLength());
                setPosition(
                    (uint) LinearAudioPlayer.PlayController.MedleyInfo.StartPoint);
            }
            
            // グリッドの情報を更新する
            //bool isTotalTimeUpdate = false;
            //string timestr = LinearAudioPlayer.GridController.getValue(pii.GridRowNo, (int)GridController.EnuGrid.TIME);
            //if (pii.GridRowNo > -1 && String.IsNullOrEmpty(pii.Time))
            //{
            //    isTotalTimeUpdate = true;
            //}
            GridItemInfo gi = LinearAudioPlayer.GridController.createNewGridItem(pii.Id.ToString());
            
            gi.FilePath = pii.FilePath;

            this.getTag(gi);
            // たぶんいらない gi.Title = gc.getValue(pii.GridRowNo, (int)GridController.EnuGrid.TITLE);
            gi.Lastplaydate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            gi.Rating = pii.Rating;
            gi.PlayCount = pii.PlayCount;

            

            if (!String.IsNullOrEmpty(gi.Time))
            {
                string time = gi.Time;
                if (time.Length < 6)
                {
                    time = "0:" + gi.Time;
                }
                double totalseconds = TimeSpan.Parse(time).TotalSeconds;
                LinearGlobal.CurrentPlayItemInfo.PlayCountUpSecond = (long) (totalseconds*
                                                                             LinearGlobal.LinearConfig.PlayerConfig.
                                                                                  PlayCountUpRatio/100D);
                //if (isTotalTimeUpdate)
                //{
                //    LinearGlobal.TotalSec += totalseconds;
                //    LinearGlobal.MainForm.ListForm.setGridInfoString(LinearAudioPlayer.GridController.getRowCount());
                //}
            }

            

            // 現在Noを更新
            LinearGlobal.CurrentPlayItemInfo.GridRowNo = pii.GridRowNo;
            LinearGlobal.CurrentPlayItemInfo.Id = gi.Id;
            LinearGlobal.CurrentPlayItemInfo.FilePath = pii.FilePath;
            LinearGlobal.CurrentPlayItemInfo.Option = pii.Option;
            LinearGlobal.CurrentPlayItemInfo.ArtworkUrl = gi.PictureUrl;
            LinearGlobal.CurrentPlayItemInfo.LastPlayDate = gi.Lastplaydate;
            

            // 現在RowNoの色を変更する
            int curRowNo = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID, LinearGlobal.CurrentPlayItemInfo.Id.ToString());
            if (curRowNo != -1)
            {
                LinearAudioPlayer.GridController.setPlayIcon(curRowNo, GridController.EnuGridIcon.PLAYING, true);
                LinearAudioPlayer.GridController.setRowColor(curRowNo, GridController.EnuPlayType.PLAYING);
            }


            gi.IsChangeAlbum = false;
            if (LinearGlobal.CurrentPlayItemInfo.Album != gi.Album)
            {
                gi.IsChangeAlbum = true;
            }
            
            // タイトル変更
            LinearGlobal.CurrentPlayItemInfo.Title = gi.Title;
            LinearGlobal.CurrentPlayItemInfo.Album = gi.Album;
            LinearGlobal.CurrentPlayItemInfo.Artist = gi.Artist;
            LinearGlobal.CurrentPlayItemInfo.Time = gi.Time;
            LinearGlobal.CurrentPlayItemInfo.Track = gi.Track;
            LinearGlobal.CurrentPlayItemInfo.Genre = gi.Genre;
            LinearGlobal.CurrentPlayItemInfo.Year = gi.Year;
            LinearGlobal.CurrentPlayItemInfo.Duration = gi.Duration;
            LinearGlobal.CurrentPlayItemInfo.Rating = gi.Rating;

            

            updateGridItem(gi, curRowNo);
            

            // レーティング設定
            LinearGlobal.MainForm.setRating(gi.Rating);

            LinearGlobal.MainForm.setTitleCentering();

            // タイマー開始
            LinearGlobal.MainForm.ListForm.setProgressBarMax((int)_playEngine.getLength());
            LinearGlobal.MainForm.setMiniProgressSeekBarMax((int)_playEngine.getLength());
            LinearGlobal.MainForm.setTimer(true);
            Debug.WriteLine("total:" + LinearAudioPlayer.GridController.getRowCount() + " " + "current:" + pii.GridRowNo);

            // 再生回数カウントストップウオッチ開始
            LinearGlobal.PlayCountUpStopwatch.Reset();
            LinearGlobal.PlayCountUpStopwatch.Start();

            if (LinearGlobal.LinearConfig.ViewConfig.TitleScrollMode != LinearEnum.TitleScrollMode.ROLL)
            {
                LinearGlobal.MainForm.setTitle(createTitle());
            }

            


            

            // 再生中リスト更新
            if (pii.GridRowNo != -1)
            {
                if (isInterrupt)
                {
                    // 何もしない
                }
                else
                {
                    playingListController.insertPlayingList(pii.GridRowNo);
                }
            }

            if (LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary)
            {
                LinearGlobal.MainForm.ListForm.setLinkLibrary(gi, false);
            }

            // LastPlay更新
            updateLastPlayDate();

            // 別スレッドアクション
            Action ac = () =>
                {
                    // アートワークが埋め込まれてなかったらネットワークから取得
                    {
                        LinearAudioPlayer.PlayController.getPicture(gi);
                        LinearGlobal.MainForm.setNotificationWindow(gi);

                        Action showNotificationAc = () =>
                            {
                                LinearGlobal.MainForm.showNotificationWindow(gi);
                            };
                        LinearGlobal.MainForm.BeginInvoke(showNotificationAc);
                    }

                    // LinkLibrary beta
                    {
                        if (LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary)
                        {

                            //LinearGlobal.MainForm.ListForm.setAlbumDescription("");
                            LinearAudioPlayer.LinkGridController.getSameArtistTrackList();
                            loadLinkLibraryData(gi);

                            Action uiAction = () =>
                                {
                                    LinearGlobal.MainForm.ListForm.setLinkLibrary(gi, true);
                                    if (LinearAudioPlayer.LinkGridController.mode ==
                                        LinkGridController.EnuMode.SAMEARTIST)
                                    {
                                        LinearAudioPlayer.LinkGridController.reloadGrid();
                                    }

                                };
                            LinearGlobal.MainForm.ListForm.BeginInvoke(uiAction);

                            // アートワークのフェードチェンジ
                            if (gi.IsChangeAlbum)
                            {
                                LinearGlobal.MainForm.ListForm.setArtworkFadeChange(gi.Picture, gi.IsNoPicture);
                            }
                        }
                    }

                    // プラグイン処理(再生後処理)
                    {
                        afterPlayPlugin(LinearGlobal.CurrentPlayItemInfo);
                    }

                    // タグ更新(ストック)
                    {
                        // もしタグ編集ストックがあれば更新する
                        if (LinearGlobal.StockTagEditList.Count > 0)
                        {
                            SaveTag();
                        }
                    }

            };
            LinearAudioPlayer.WorkerThread.EnqueueTask(ac);


            

        }

        private void updateLastPlayDate()
        {
            
            List<object[]> lastPlayDateList = new List<object[]>();
            RowInfo[] allrows = LinearAudioPlayer.GridController.getAllRowsInfo();
            foreach (var rowInfo in allrows)
            {
                object lastplaydate =
                    LinearAudioPlayer.GridController.Grid[rowInfo.Index, (int)GridController.EnuGrid.DATE].Value;
                lastPlayDateList.Add(new object[] { lastplaydate, rowInfo.Index, LinearAudioPlayer.GridController.Grid[rowInfo.Index, (int)GridController.EnuGrid.DATE].Tag.
                                ToString() });
            }

            Action ac = () =>
                {

                    // 最終再生日時更新
                    Dictionary<int, string> lastPlayDateUpdateTargetDic = new Dictionary<int, string>();

                    foreach (var o in lastPlayDateList)
                    {
                        if (o[0] != null && ("さっき".Equals(o[0])
                                                     || o[0].ToString().IndexOf("秒") > 0
                                                     || o[0].ToString().IndexOf("分") > 0
                                                     || o[0].ToString().IndexOf("時間") > 0))
                        {
                            lastPlayDateUpdateTargetDic.Add((int) o[1], (string) o[2]);
                        }
                    }

                    int[] keys = new int[lastPlayDateUpdateTargetDic.Keys.Count];
                    lastPlayDateUpdateTargetDic.Keys.CopyTo(keys,0);
                    foreach (var key in keys)
                    {
                        lastPlayDateUpdateTargetDic[key] = DateTimeUtils.getRelativeTimeJapaneseString(lastPlayDateUpdateTargetDic[key]);
                    }

                    Action uiAc = () => {
                                            foreach (var key in lastPlayDateUpdateTargetDic.Keys)
                                            {
                                                LinearAudioPlayer.GridController.Grid[
                                                    key, (int) GridController.EnuGrid.DATE].Value = lastPlayDateUpdateTargetDic[key];
                                            }
                    };
                    LinearGlobal.MainForm.ListForm.BeginInvoke(uiAc);

                };
            LinearAudioPlayer.WorkerThread.EnqueueTask(ac);

        }

        delegate void AfterDoLinkLibraryDelegate(Object arg);

        delegate void AfterPlayPluginDelegate(LinkLibraryInfo arg);

        
        private void loadLinkLibraryData(GridItemInfo gi)
        {

            // 画像をダウンロード
            if (gi.IsChangeAlbum)
            {
                getPicture(gi);

                // Yahooショッピングの説明を取得
                gi.AlbumDescription = "";

                // DBから探す
                List<DbParameter> paramList = new List<DbParameter>();
                paramList.Add(new SQLiteParameter("Artist", gi.Artist));
                paramList.Add(new SQLiteParameter("Album", gi.Album));
                object result = SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL046, paramList);
                if (result != null)
                {
                    gi.AlbumDescription = (string)result;
                }

                if (String.IsNullOrEmpty(gi.AlbumDescription))
                {
                    // DBになかったらYahoo Shopping Apiから取得
                    // アルバムとアーティストで
                    if (!String.IsNullOrEmpty(gi.Album) || !String.IsNullOrEmpty(gi.Artist))
                    {
                        gi.AlbumDescription =
                            YahooHelper.getShoppingDescription("\"" + gi.Album + "\"" + " " + "\"" + gi.Artist + "\"");
                    }

                    if (String.IsNullOrEmpty(gi.AlbumDescription))
                    {
                        // アルバムだけで
                        gi.AlbumDescription = YahooHelper.getShoppingDescription(gi.Album);
                    }

                    if (!String.IsNullOrEmpty(gi.AlbumDescription))
                    {
                        // 取得できたらDBを更新
                        paramList.Add(new SQLiteParameter("Description", gi.AlbumDescription));
                        SQLiteTransaction sqLiteTransaction = null;
                        try
                        {
                            sqLiteTransaction = SQLiteManager.Instance.beginTransaction();
                            SQLiteManager.Instance.executeNonQuery(SQLResource.SQL047, paramList);
                            sqLiteTransaction.Commit();
                        }
                        catch (SQLiteException sqlex)
                        {
                            if (sqLiteTransaction != null)
                            {
                                try
                                {
                                    sqLiteTransaction.Rollback();
                                }
                                catch (SQLiteException) { }
                            }
                            LinearAudioPlayer.Logger.TraceEvent(TraceEventType.Warning, 0, sqlex.Message);
                        }
                    }
                }

            }
            else
            {
                gi.AlbumDescription = LinearGlobal.MainForm.ListForm.getAlbumDescription();
            }

            LinearGlobal.CurrentPlayItemInfo.AlbumDescription = gi.AlbumDescription;



        }


        private void afterPlayPlugin(PlayItemInfo pii)
        {
            foreach (var plugin in LinearGlobal.Plugins)
            {
                if (plugin.Enable)
                {
                    try
                    {
                        LinkLibraryInfo linkLibraryInfo = plugin.RunAfterPlay(pii.Title,
                                            pii.Album,
                                            pii.Artist,
                                            pii.Track,
                                            pii.Duration,
                                            LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary);

                        if (LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary
                            && plugin is ILinkLibrary)
                        {
                            ILinkLibrary linkLibraryPlugin = (ILinkLibrary) plugin;
                            linkLibraryInfo.Title = linkLibraryPlugin.getLinkLibraryTitle();
                            linkLibraryInfo.Description = linkLibraryPlugin.getLinkLibraryDescription(linkLibraryInfo);

                            Action afterPlayPluginAfterAction = () =>
                                {
                                    LinearGlobal.MainForm.ListForm.setLinkLibrary(linkLibraryInfo);
                                };
                            LinearGlobal.MainForm.ListForm.BeginInvoke(afterPlayPluginAfterAction);

                        }

                    }
                    catch (Exception ex)
                    {
                        LinearAudioPlayer.writeErrorMessage(ex);
                    }
                }
            }
        }

        private void afterPlayPluginAfter(LinkLibraryInfo lastFmInfo)
        {
            
            LinearGlobal.MainForm.ListForm.setLinkLibrary(lastFmInfo);

        }


        delegate void afterPlayPluginDelegate(PlayItemInfo pii);
        delegate void SaveTagEndDelegate(Dictionary<long, Tag> saveTagResult);

        private void SaveTag()
        {
            if (LinearGlobal.LinearConfig.SoundConfig.FadeEffect )
            {
                Thread.Sleep((int) (LinearGlobal.LinearConfig.SoundConfig.FadeDuration * 1000));
            }

            var saveTagResult = saveTag();
            if (saveTagResult.Count > 0)
            {

                Action uiAction = () =>
                    {
                        saveTagEnd(saveTagResult);
                    };
                LinearGlobal.MainForm.ListForm.BeginInvoke(uiAction);

            }

            
        }


        /// <summary>
        /// 一時停止する
        /// </summary>
        public bool pause()
        {
            bool result;
            if (!_playEngine.isPasued())
            {
                // ポーズ
                LinearGlobal.PlayCountUpStopwatch.Stop();
                startFadeOut(MainChannel, true);
                result = _playEngine.pause();
                if (WindowsUtils.IsWindows7Later())
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
                }
            }
            else
            {
                // 再開
                LinearGlobal.PlayCountUpStopwatch.Start();
                
                result = _playEngine.pause();
                startFadeIn();
                if (WindowsUtils.IsWindows7Later())
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                }
            }

            // タイマー動作変更
            LinearGlobal.MainForm.setTimer(!result);

            return result;

        }


        /// <summary>
        /// 停止する
        /// </summary>
        public void stop()
        {
            LinearGlobal.PlayCountUpStopwatch.Stop();
            // フェードアウトストップ
            startFadeOut(MainChannel, true);

            _playEngine.stop();

            setNoPlayStatus();

            // タイマー停止
            LinearGlobal.MainForm.setTimer(false);

            // 時間表示取得
            LinearGlobal.MainForm.setTime(LinearConst.DISPLAYTIME_DEFAULT);
            // プログレスバー更新
            LinearGlobal.MainForm.ListForm.setProgressBarValue(0);
            LinearGlobal.MainForm.setMiniProgressSeekBarValue(0);
            if (WindowsUtils.IsWindows7Later())
            {
                TaskbarManager.Instance.SetProgressValue(0, 100);
            }

            LinearGlobal.MainForm.clearSpectrum();
        }


        /// <summary>
        /// 停止する
        /// </summary>
        public void stop(int channel)
        {
            // フェードアウトストップ
            startFadeOut(channel, true);

            _playEngine.stop(channel);

            // タイマー停止
            LinearGlobal.MainForm.setTimer(false);

            // 時間表示取得
            LinearGlobal.MainForm.setTime(LinearConst.DISPLAYTIME_DEFAULT);
            // プログレスバー更新
            LinearGlobal.MainForm.ListForm.setProgressBarValue(0);
            LinearGlobal.MainForm.setMiniProgressSeekBarValue(0);

            LinearGlobal.MainForm.clearSpectrum();
        }


        /// <summary>
        /// 再生時間を取得
        /// </summary>
        public string getDisplayTime()
        {

            uint ms = _playEngine.getPosition();
            if (LinearGlobal.TimeDisplayMode == LinearConst.DISPLAYTIMEMODE_NORMAL)
            {
                return String.Format("{0,0:D2}:{1,0:D2}", (ms / 1000 / 60), (ms / 1000 % 60));
            }
            else
            {
                uint lenms = _playEngine.getLength();
                return String.Format("{0,0:D2}:{1,0:D2}", ((lenms - ms) / 1000 / 60), ((lenms - ms) / 1000 % 60));
            }

        }

        
        /// <summary>
        /// 再生位置を取得する
        /// </summary>
        /// <returns>再生位置(ミリ秒)</returns>
        public uint getPosition()
        {
            return _playEngine.getPosition();
        }

        /// <summary>
        /// ファイルの長さを取得する
        /// </summary>
        /// <returns></returns>
        public uint getLength()
        {
            return _playEngine.getLength();
        }

        /// <summary>
        /// 再生位置を設定する
        /// </summary>
        /// <param name="ms"></param>
        public void setPosition(uint ms)
        {
            
            
            _playEngine.setPosition(ms);
            
            if(_playEngine.isPasued()) {
                _playEngine.pause();
            }
            startFadeIn();
        }

        /// <summary>
        /// 再生中であるか確認する
        /// </summary>
        /// <returns></returns>
        public bool isPlaying()
        {
            return _playEngine.isPlaying(MainChannel);
        }


        public float[] getSpectrum()
        {
            return _playEngine.getSpectrum();
        }

        /// <summary>
        /// 再生中であるか確認する
        /// </summary>
        /// <returns></returns>
        public bool isPlaying(int channel)
        {
            return _playEngine.isPlaying(channel);
        }

        /*
        /// <summary>
        /// 次のプレイリストを作成する
        /// </summary>
        /// <param name="gridRowNo">作成開始位置</param>
        /// <param name="num">作成数</param>
        public void createNextPlaylist(int gridRowNo, int num)
        {

            if (gridRowNo == -1)
            {
                gridRowNo = 0;
            }

            if (LinearAudioPlayer.GridController.getRowCount() < num)
            {
                num = LinearAudioPlayer.GridController.getRowCount();
            }

            Random rnd = new Random();

            PlayItemInfo pii = new PlayItemInfo();
            if (LinearGlobal.NextPlaylist.Count == 0)
            {
                pii.GridRowNo = gridRowNo;
            } else
            {
                pii.GridRowNo = LinearGlobal.NextPlaylist[LinearGlobal.NextPlaylist.Count - 1].GridRowNo;
            }

            while (LinearGlobal.NextPlaylist.Count < num)
            {
                if (pii == null)
                {
                    pii = new PlayItemInfo();
                    pii.GridRowNo = LinearGlobal.NextPlaylist[LinearGlobal.NextPlaylist.Count -1].GridRowNo;
                }

                switch (LinearGlobal.PlayMode)
                {
                    case (int)LinearEnum.PlaylistMode.NORMAL:
                        // NORMAL MODE
                        pii.GridRowNo++;
                        if (LinearAudioPlayer.GridController.getRowCount() < pii.GridRowNo)
                        {
                            // 最後までいったら停止する
                            
                            return;
                        }
                        break;

                    case LinearEnum.PlayMode.ONEOFF:
                        // ONEOFF MODE
                        LinearGlobal.NextPlaylist.Clear();
                        return;

                    case LinearEnum.PlayMode.REPEAT:
                        // REPEAT MODE
                        // 同じのをもっかい再生
                        break;


                    case LinearEnum.PlayMode.ENDLESS:
                        // ENDLESS MODE
                        pii.GridRowNo++;
                        if (LinearAudioPlayer.GridController.getRowCount() < pii.GridRowNo)
                        {
                            // 最後までいったとき最初に戻す
                            pii.GridRowNo = 1;
                        }
                        break;

                        
                }

                pii.Title = LinearAudioPlayer.GridController.getValue(pii.GridRowNo, (int)GridController.EnuGrid.TITLE);
                pii.Artist = LinearAudioPlayer.GridController.getValue(pii.GridRowNo, (int)GridController.EnuGrid.ARTIST);
                
                LinearGlobal.NextPlaylist.Add(pii);

                pii = null;
            }

        }*/

        /*
        public void updateNextPlaylist(int num)
        {
            LinearGlobal.NextPlaylist.Clear();

            createNextPlaylist(
                LinearAudioPlayer.GridController.Find(
                (int)GridController.EnuGrid.ID,
                LinearGlobal.CurrentPlayItemInfo.Id.ToString()), num);
            LinearGlobal.MainForm.setTitle(createTitle());

        }*/

        /// <summary>
        /// 再生終了時の処理
        /// </summary>
        public void endOfStream()
        {

            if (LinearGlobal.PlayMode == LinearEnum.PlayMode.ONEOFF)
            {
                stop();
                return;
            }

            PlayItemInfo pii = new PlayItemInfo();

            //if (LinearGlobal.MainForm.ListForm.InterruptForm.InterruptListBox.Items.Count > 0) {
                // 割り込みリストがあるとき
            //    pii = (PlayItemInfo) LinearGlobal.MainForm.ListForm.InterruptForm.InterruptListBox.Items[0];
            //    LinearGlobal.MainForm.ListForm.InterruptForm.InterruptListBox.Items.RemoveAt(0);
            //    pii.GridRowNo = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID, pii.Id.ToString());


            //} else {

            switch (LinearGlobal.PlayMode)
            {
                case LinearEnum.PlayMode.ONEOFF:
                    stop();
                    return;
                case LinearEnum.PlayMode.NORMAL:
                    pii.Id = playingListController.getNextId(false);
                    if (pii.Id == -1)
                    {
                        stop();
                        return;
                    }
                    break;
                case LinearEnum.PlayMode.REPEAT:
                    pii.Id = LinearGlobal.CurrentPlayItemInfo.Id;
                    break;
                case LinearEnum.PlayMode.ENDLESS:
                    pii.Id = playingListController.getNextId(true);
                    break;
                case LinearEnum.PlayMode.RANDOM:
                    pii.Id = (long) SQLiteManager.Instance.executeQueryOnlyOne(
                        SQLResource.SQL013.Replace("#RATING#", SQLBuilder.getRatingWhereString(LinearGlobal.PlaylistMode)));
                    break;
            }

                //pii.GridRowNo = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID, LinearGlobal.CurrentPlayItemInfo.Id.ToString());

                
                //if (LinearGlobal.NextPlaylist.Count > 0)
                //{
                //    pii.GridRowNo = LinearGlobal.NextPlaylist[0].GridRowNo;
                //    LinearGlobal.NextPlaylist.RemoveAt(0);
                //}
                //else
                //{
                //    if (pii.GridRowNo == LinearAudioPlayer.GridController.getRowCount() && LinearGlobal.PlayMode == LinearEnum.PlayMode.NORMAL)
                //    {
                //        stop();
                //        return;
                //    }
                    
                //}

                //if (pii.GridRowNo == -1)
                //{
                //    pii.GridRowNo = 1;
                //}
                
            //}

            // 再生する必要があるとき
            //if (LinearAudioPlayer.GridController.getRowCount() > 0)
            //{
            //    if (!LinearGlobal.MainForm.ListForm.Grid.Focused)
            //    {
                    LinearAudioPlayer.GridController.setFocusRowNo(pii.GridRowNo);
            //    }

                
            //}
                    this.play(pii, false, false);
        }

        /// <summary>
        /// エンジンのバージョンを取得する
        /// </summary>
        /// <returns></returns>
        public string getEngineVersion()
        {
            string version = _playEngine.getVersion().ToString("X");
            return version.Substring(0,1) + "." + version.Substring(1,2) + "." + version.Substring(3,2);
        }

        /// <summary>
        /// ボリュームを設定する
        /// </summary>
        /// <param name="volume">ボリューム(0-100)</param>
        public void setVolume(
            float volume)
        {
            float vol = volume / (float)100;
            _playEngine.setVolume(MainChannel, vol);
        }

        /// <summary>
        /// ボリュームを設定する
        /// </summary>
        /// <param name="channel">チャネル</param>
        /// <param name="volume">ボリューム(0-100)</param>
        public void setVolume(
            int channel,
            float volume)
        {
            float vol = volume / (float)100;
            _playEngine.setVolume(channel, vol);
        }


        public bool isUpdateMetadata()
        {
            return _playEngine.isUpdateMetadata();
        }

        /// <summary>
        /// タイトルを作成する。
        /// </summary>
        /// <param name="gi"></param>
        /// <returns></returns>
        public string createTitle()
        {
            GridItemInfo gi = new GridItemInfo();
            gi.Title = LinearGlobal.CurrentPlayItemInfo.Title;
            gi.Album = LinearGlobal.CurrentPlayItemInfo.Album;
            gi.Artist = LinearGlobal.CurrentPlayItemInfo.Artist;
            gi.Time = LinearGlobal.CurrentPlayItemInfo.Time;

            return createTitle(gi);
        }

        /// <summary>
        /// タイトルを作成する。
        /// </summary>
        /// <param name="gi"></param>
        /// <returns></returns>
        public string createTitle(GridItemInfo gi)
        {
            string titleFormat = LinearGlobal.LinearConfig.ViewConfig.TitleTemplete;

            if (String.IsNullOrEmpty(gi.Album))
            {
                gi.Album = "NoAlbum";
            }
            if (String.IsNullOrEmpty(gi.Artist))
            {
                gi.Artist = "Unknown";
            }

            titleFormat = titleFormat.Replace("%A", gi.Album)
                .Replace("%T", gi.Title)
                .Replace("%R", gi.Artist)
                .Replace("%L", gi.Time)
                .Replace("%N", getNextPlayTitle());

            return titleFormat;
        }

        /// <summary>
        /// WASAPIが有効かどうか
        /// </summary>
        /// <returns></returns>
        public bool isEnableWasapi()
        {
            return _playEngine.isEnableWasapi();
        }

        public void reInsertPlayingList(int rowno)
        {
            playingListController.insertPlayingList(rowno);
            
        }

        #endregion

        /*
         * プライベートメソッド
         */
        #region Privete Method

        /// <summary>
        /// NextPlayタイトル取得する
        /// </summary>
        /// <returns></returns>
        private string getNextPlayTitle()
        {
            string result = "";
            PlayItemInfo pii = null;

            switch (LinearGlobal.PlayMode)
            {
                case LinearEnum.PlayMode.ONEOFF:
                case LinearEnum.PlayMode.RANDOM:
                    break;
                case LinearEnum.PlayMode.NORMAL:
                case LinearEnum.PlayMode.ENDLESS:
                    pii= playingListController.getNextPlayInfo();
                    break;
                case LinearEnum.PlayMode.REPEAT:
                    pii = LinearGlobal.CurrentPlayItemInfo;
                    break;
            }

            /*
            pii = getInterruptItem();

            if (pii == null && LinearGlobal.NextPlaylist.Count > 0)
            {
                pii = LinearGlobal.NextPlaylist[0];
            }*/

            if (pii != null)
            {
                result = " →  " + pii.ToString();
            }

            return result;
        }

        /*
        private PlayItemInfo getInterruptItem()
        {
            PlayItemInfo result = null;
            if (LinearGlobal.MainForm.ListForm.InterruptForm.InterruptListBox.Items.Count > 0)
            {
                result = (PlayItemInfo) LinearGlobal.MainForm.ListForm.InterruptForm.InterruptListBox.Items[0];
            }
            return result;
        }*/

        /// <summary>
        /// グリッドアイテムを更新する
        /// </summary>
        /// <param name="gi"></param>
        private void updateGridItem(GridItemInfo gi, int gridRowNo)
        {

            Action ac = () =>
                {
                    List<DbParameter> paramList = new List<DbParameter>();

                    paramList.Add(new SQLiteParameter("Title", gi.Title));
                    paramList.Add(new SQLiteParameter("Artist", gi.Artist));
                    paramList.Add(new SQLiteParameter("Bitrate", gi.Bitrate));
                    paramList.Add(new SQLiteParameter("Album", gi.Album));
                    paramList.Add(new SQLiteParameter("Track", gi.Track));
                    paramList.Add(new SQLiteParameter("Genre", gi.Genre));
                    paramList.Add(new SQLiteParameter("Year", gi.Year));
                    //paramDic.Add("Rating", gi.Rating);
                    paramList.Add(new SQLiteParameter("Time", gi.Time));
                    paramList.Add(new SQLiteParameter("NotFound", gi.NotFound));
                    paramList.Add(new SQLiteParameter("Date", gi.Date));
                    paramList.Add(new SQLiteParameter("Id", gi.Id));
                    paramList.Add(new SQLiteParameter("LastPlayDate", gi.Lastplaydate));
                    SQLiteManager.Instance.executeNonQuery(SQLResource.SQL007, paramList);
                };

            LinearAudioPlayer.WorkerThread.EnqueueTask(ac);
            
            LinearAudioPlayer.GridController.updateItem(gi, gridRowNo);

            playingListController.updatePlayingList(gi);

        }

        /// <summary>
        /// タグ情報を取得する。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        public void getTag(GridItemInfo gi)
        {

            if (!File.Exists(gi.FilePath))
            {
                return;
            }

            gi.Date = FileUtils.getCreationDateTime(gi.FilePath);

            try
            {
                TagLib.File file = TagLib.File.Create(gi.FilePath);
            
            // タイトル
            if (file.Tag.Title != null)
            {
                gi.Title = file.Tag.Title;
                if (gi.Title != null)
                {
                    gi.Title = gi.Title.Trim();
                }

            }
            else
            {
                gi.Title = Path.GetFileNameWithoutExtension(gi.FilePath);
            }

            // アーティスト
            if (file.Tag.Performers.Length > 0)
            {
                gi.Artist = file.Tag.Performers[0];
                if (gi.Artist != null)
                {
                    gi.Artist = gi.Artist.Trim();
                }
            }
            else
            {
                if (file.Tag.FirstAlbumArtist != null)
                {
                    gi.Artist = file.Tag.FirstAlbumArtist;
                } else
                {
                    gi.Artist = String.Empty;
                }
            }

            // アルバム
            gi.Album = file.Tag.Album;
            if (gi.Album != null)
            {
                gi.Album = gi.Album.Trim();
            }

            // トラック
            if (file.Tag.Track != 0)
            {
                gi.Track = (int)file.Tag.Track;
            }
            else
            {
                gi.Track = 0;
            }

            // ジャンル
            if (file.Tag.Genres.Length > 0)
            {
                gi.Genre = file.Tag.Genres[0];
            }
            else
            {
                gi.Genre = String.Empty;
            }

            // 年
            if (file.Tag.Year != 0)
            {
                gi.Year = file.Tag.Year.ToString();
            }
            else
            {
                gi.Year = gi.Date.Substring(0,4);
            }

            // ビットレート
            TagLib.Properties prop =
                new TagLib.Properties(
                    file.Properties.Duration, file.Properties.Codecs);
            gi.Bitrate = file.Properties.AudioBitrate.ToString();

            // 時間
            if (file.Properties.Duration.Hours > 0)
            {
                gi.Time = new DateTime(0).Add(file.Properties.Duration).ToString("H:mm:ss");
            } else
            {
                gi.Time = new DateTime(0).Add(file.Properties.Duration).ToString("m:ss");
            }
            
            gi.Duration = (int)file.Properties.Duration.TotalSeconds;

            
            //if (isLoadPicture)
            //{
                //bool isFailFilePictureLoad = false;
                if (file.Tag.Pictures.Length > 0)
                {
                    IPicture pic = file.Tag.Pictures[0];
                    ImageConverter ic = new ImageConverter();
                    try
                    {
                        gi.Picture = (Image)ic.ConvertFrom(pic.Data.Data);
                    }
                    catch (ArgumentException)
                    {
                        //isFailFilePictureLoad = true;
                    }
                
                }
            }
            catch (Exception)
            {
                LinearAudioPlayer.Logger.TraceEvent(System.Diagnostics.TraceEventType.Warning,
                    0, "UnsupportedFormat: " + gi.FilePath);
                LinearAudioPlayer.Logger.Flush();
                gi.Title = Path.GetFileNameWithoutExtension(gi.FilePath);
            }
            

        }

        /// <summary>
        /// Google Imageから画像を取得する。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        public void getPicture(GridItemInfo gi)
        {

            if (gi.Picture == null && LinearGlobal.LinearConfig.ViewConfig.isGetNetworkArtwork)
            {

                try
                {
                    gi.PictureUrl = new WebManager().getAlbumArtworkUrlFromGoogleImage(gi.Artist, gi.Album, gi.Title);
                    WebClient wc = new WebClient();
                    byte[] imageByteData = wc.DownloadData(gi.PictureUrl);
                    ImageConverter ic = new ImageConverter();
                    gi.Picture = (Image)ic.ConvertFrom(imageByteData);
                    gi.IsNoPicture = false;
                }
                catch (FinalstreamException fex)
                {
                    LinearAudioPlayer.Logger.TraceEvent(System.Diagnostics.TraceEventType.Warning,
                    0, fex.ToString() +"\n" + fex.InnerException.Message);
                    LinearAudioPlayer.Logger.Flush();
                }

                if (gi.Picture == null)
                {
                    // 取得に失敗
                    gi.Picture = LinearAudioPlayer.StyleController.NoPictureImage;
                    gi.IsNoPicture = true;
                }

            } else if (gi.Picture == null)
            {
                gi.Picture = LinearAudioPlayer.StyleController.NoPictureImage;
                gi.IsNoPicture = true;
            }

        }

        /// <summary>
        /// タグ情報を保存する。
        /// </summary>
        public Dictionary<long, Tag> saveTag()
        {
            Dictionary<long, Tag> result = new Dictionary<long, Tag>();
            List<DbParameter> paramList = new List<DbParameter>();

            SQLiteTransaction sqltran = null;
            try
            {
                sqltran = SQLiteManager.Instance.beginTransaction();
                //int i = 0;
                //int length = LinearGlobal.StockTagEditList.Count;
                for (int i = 0; i < LinearGlobal.StockTagEditList.Count; i++)
                //foreach (GridItemInfo tagEditInfo in LinearGlobal.StockTagEditList)
                {
                    GridItemInfo tagEditInfo = LinearGlobal.StockTagEditList[i];

                    if (tagEditInfo.Id != LinearGlobal.CurrentPlayItemInfo.Id)
                    {
                        // 圧縮ファイルの場合は存在するか確認し、なかったら解凍する
                        if (!String.IsNullOrEmpty(tagEditInfo.Tag)
                            && !File.Exists(tagEditInfo.FilePath))
                        {
                            SevenZipManager.Instance.extract(tagEditInfo.Tag,
                                                    tagEditInfo.Option,
                                                    LinearGlobal.TempDirectory);
                        }

                        TagLib.File file = TagLib.File.Create(tagEditInfo.FilePath);

                        TagLib.Id3v2.Tag.DefaultEncoding = StringType.UTF16;
                        TagLib.Id3v2.Tag.DefaultVersion = 3;

                        file.RemoveTags(TagTypes.Id3v1);
                        
                        if (tagEditInfo.Selection == (int)TagEditDialog.EnumMode.SINGLE)
                        {
                            file.Tag.Title = tagEditInfo.Title;
                            file.Tag.Performers = new string[] { tagEditInfo.Artist };
                            file.Tag.Album = tagEditInfo.Album;
                            if (!String.IsNullOrEmpty(tagEditInfo.Track.ToString()))
                            {
                                file.Tag.Track = uint.Parse(tagEditInfo.Track.ToString().Trim());
                            }
                            file.Tag.Genres = new string[] { tagEditInfo.Genre };
                            if (!String.IsNullOrEmpty(tagEditInfo.Year))
                            {
                                file.Tag.Year = uint.Parse(tagEditInfo.Year.Trim());
                            }
                            if (tagEditInfo.Picture != null)
                            {
                                ImageConverter ic = new ImageConverter();
                                ByteVector bv =
                                    new ByteVector((byte[])ic.ConvertTo(tagEditInfo.Picture, typeof(byte[])));
                                file.Tag.Pictures = new IPicture[] { new Picture(bv) };

                            }
                            else
                            {
                                file.Tag.Pictures = null;
                            }
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(tagEditInfo.Artist))
                            {
                                file.Tag.Performers = new string[] { tagEditInfo.Artist };
                            }
                            if (!String.IsNullOrEmpty(tagEditInfo.Album))
                            {
                                file.Tag.Album = tagEditInfo.Album;
                            }
                            if (!String.IsNullOrEmpty(tagEditInfo.Genre))
                            {
                                file.Tag.Genres = new string[] { tagEditInfo.Genre };
                            }
                            if (!String.IsNullOrEmpty(tagEditInfo.Year))
                            {
                                file.Tag.Year = uint.Parse(tagEditInfo.Year.Trim());
                            }
                            //break;
                        }

                        file.Save();
                        
                        result.Add(tagEditInfo.Id, file.Tag);

                        // 圧縮ファイルの場合は再圧縮する
                        if (!String.IsNullOrEmpty(tagEditInfo.Tag))
                        {
                            SevenZipManager.Instance.compressOverwrite(tagEditInfo.Tag,
                                                              tagEditInfo.Option,
                                                              tagEditInfo.FilePath);
                        }

                        // データベース更新
                        paramList.Clear();
                        paramList.Add(new SQLiteParameter(":Id", tagEditInfo.Id));
                        paramList.Add(new SQLiteParameter(":Title", file.Tag.Title));
                        paramList.Add(new SQLiteParameter(":Artist", file.Tag.FirstPerformer));
                        paramList.Add(new SQLiteParameter(":Album", file.Tag.Album));
                        paramList.Add(new SQLiteParameter(":Track", (int)file.Tag.Track));
                        paramList.Add(new SQLiteParameter(":Genre", file.Tag.FirstGenre));
                        paramList.Add(new SQLiteParameter(":Year", (int)file.Tag.Year));
                        paramList.Add(new SQLiteParameter(":FilePath", tagEditInfo.FilePath));
                        paramList.Add(new SQLiteParameter(":Option", ""));
                        SQLiteManager.Instance.executeNonQuery(SQLResource.SQL027, paramList);

                        Debug.WriteLine("TagEditComplete! : " + tagEditInfo.Title);
                        LinearGlobal.StockTagEditList.RemoveAt(i);
                        i--;
                    }
                    //i++;
                }

                sqltran.Commit();

            }
            catch (Exception)
            {
                if (sqltran.Connection.State != ConnectionState.Closed)
                {
                    sqltran.Rollback();
                }
                //MessageUtils.showMessage(MessageBoxIcon.Error,
                //    MessageResource.E0002 + "\n" + ex.Message);
            }

            return result;


        }

        public void saveTagEnd (Dictionary<long, Tag> saveTagResult)
        {
            foreach (var tag in saveTagResult)
            {
                int rowNo = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID, tag.Key.ToString());
                if (rowNo != -1)
                {
                    LinearAudioPlayer.GridController.Grid[rowNo, (int) GridController.EnuGrid.TITLE].Value =
                        tag.Value.Title;
                    LinearAudioPlayer.GridController.Grid[rowNo, (int)GridController.EnuGrid.ARTIST].Value =
                        tag.Value.FirstPerformer;
                    LinearAudioPlayer.GridController.Grid[rowNo, (int)GridController.EnuGrid.ALBUM].Value =
                        tag.Value.Album;
                }
            }
            LinearGlobal.MainForm.ListForm.showToastMessage(MessageResource.I0002);
            Debug.Print("AsyncTagEdit End.");
        }


        /// <summary>
        /// フェードイン開始
        /// </summary>
        private void startFadeIn()
        {
            SoundConfig soundConfig = LinearGlobal.LinearConfig.SoundConfig;

            float fadeDuration;
            if (!MedleyInfo.Enable)
            {
                fadeDuration = soundConfig.FadeDuration;
            } else
            {
                fadeDuration = MedleyInfo.FadeDuration;
            }

            FadeVolumeDelegate d = new FadeVolumeDelegate(fadeVolume);
            d.BeginInvoke(FadeMode.FADE_IN,
                MainChannel,
                soundConfig.FadeEffect,
                fadeDuration,
                null,
                null);
        }

        /// <summary>
        /// フェードアウト開始
        /// </summary>
        /// <param name="channelNo">チャネルNo</param>
        /// <param name="isSync">同期かどうか</param>
        private void startFadeOut(int channel, bool isSync)
        {
            SoundConfig soundConfig = LinearGlobal.LinearConfig.SoundConfig;

            float fadeDuration;
            //if (!MedleyInfo.Enable && isSync)
            //{
            //    fadeDuration = soundConfig.FadeDuration;
            //}

            if (MedleyInfo.Enable && !isSync)
            {
                fadeDuration = MedleyInfo.FadeDuration;
            }
            else
            {
                fadeDuration = soundConfig.FadeDuration;
            }

            FadeVolumeDelegate d = new FadeVolumeDelegate(fadeVolume);
            IAsyncResult result = d.BeginInvoke(FadeMode.FADE_OUT,
                channel,
                soundConfig.FadeEffect,
                fadeDuration,
                new AsyncCallback(stopSubChannel),
                null);

            if (isSync)
            {
                // 終了まで待つ
                d.EndInvoke(result);
            }
        }


        private void stopSubChannel(IAsyncResult ar)
        {
            _playEngine.stop(SubChannel);
        }

        static readonly object syncObject = new object(); // 同期オブジェクト

        /// <summary>
        /// ボリュームをフェードする
        /// </summary>
        /// <param name="fadeMode"></param>
        /// <param name="channel"></param>
        /// <param name="isFadeEffect"></param>
        /// <param name="fadeDuration"></param>
        private void fadeVolume(FadeMode fadeMode,
            int channel,
            bool isFadeEffect,
            float fadeDuration)
        {
            if (isFadeEffect
                && fadeDuration > 0
                && LinearGlobal.Volume > 0)
            {

                // フェード更新間隔
                int DURATION_MS = 100;

                float fadeDurationMs = fadeDuration * 1000;
                float fadeCount = fadeDurationMs / (float)DURATION_MS;
                int elapsedms = 0;
                float vol;
                float volChangeValue;
                if (fadeMode == FadeMode.FADE_IN)
                {

                    vol = 0;
                    volChangeValue = LinearGlobal.Volume / fadeCount;

                }
                else
                {
                    
                    vol = LinearGlobal.Volume;
                    volChangeValue = (vol / fadeCount) * -1;

                }

                while ((fadeDurationMs >= elapsedms && this.isPlaying(channel)))
                {
                        vol += volChangeValue;
                        lock (syncObject)
                        {
                            this.setVolume(channel, vol);
                        }
                        System.Threading.Thread.Sleep(DURATION_MS);
                        elapsedms += DURATION_MS;
                }


            }
            else {

                this.setVolume(channel, LinearGlobal.Volume);

            }

        }

        /* 2011.11.11 del 通知ウインドウに切り替え
        /// <summary>
        /// バルーンを表示する
        /// </summary>
        /// <param name="message">表示するメッセージ</param>
        private void showBalloon(GridItemInfo gi)
        {
            LinearGlobal.MainForm.notifyIcon.BalloonTipTitle = gi.Title;
            LinearGlobal.MainForm.notifyIcon.BalloonTipText = gi.Artist;
            LinearGlobal.MainForm.notifyIcon.ShowBalloonTip(5000);
        }
        */

        #endregion

        public void Dispose()
        {
            this.stop();

            if (this._playEngine != null)
            {
                this._playEngine.Dispose();
                this._playEngine = null;
            }
            
        }

        public void clearPlayingLiset()
        {
            playingListController.clearPlayingList();
        }

        public int getPlayingRestCont()
        {
            return LinearGlobal.LinearConfig.PlayerConfig.RestCount;
        }

        public void applyNormalize(bool isApply)
        {
            _playEngine.applyNormalize(isApply);
        }

        public void prebiousPlay()
        {

            object sort = SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL050,
                                                                        new SQLiteParameter("Id",
                                                                                            LinearGlobal.CurrentPlayItemInfo.
                                                                                                Id));

            if (sort != null)
            {
                object id;
                if ((long)sort == 1L)
                {
                    id = SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL052);

                }
                else
                {
                    id = SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL051,
                                                                           new SQLiteParameter("Sort",
                                                                                               (long) sort));
                }

                if (id != null)
                {
                    PlayItemInfo pii = new PlayItemInfo();
                    pii.Id = (long)id;
                    play(pii, false, false);

                }

            }

        }

        /// <summary>
        /// オーディオファイル自動登録
        /// </summary>
        public void executeAutoAudioFileRegist()
        {

            if (!DirectoryUtils.isEmptyDirectory(LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.MonitoringDirectory))
            {

                Action autoAudioFileRegistAction = () =>
                    {
                        ListFunction listFunc = new ListFunction();
                        listFunc.addGridFromList(
                            new string[] { LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.MonitoringDirectory },
                            ListFunction.RegistMode.AUTOREGIST);

                        Action uiAction = () =>
                            {
                                if (LinearGlobal.MainForm != null && LinearGlobal.MainForm.ListForm != null)
                                {
                                    LinearGlobal.MainForm.ListForm.showToastMessage(MessageResource.I0007);
                                    Debug.WriteLine("Comlete! AutoRegist");
                                }
                            };
                        LinearGlobal.MainForm.ListForm.BeginInvoke(uiAction);
                        
                    };
                LinearAudioPlayer.WorkerThread.EnqueueTask(autoAudioFileRegistAction);
                
            }

        }

        /// <summary>
        /// 再生中リスト復元
        /// </summary>
        public void restorePlayingList()
        {
            playingListController.restorePlayingList();
        }

        public GridItemInfo[] getPlayingList()
        {
            return playingListController.getPlayingList();
        }

        public void savePlayingList()
        {
            playingListController.savePlayingList();
        }
    }
}
