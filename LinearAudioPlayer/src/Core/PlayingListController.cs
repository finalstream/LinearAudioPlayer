using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Resources;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    public class PlayingListController
    {
        //private IList<long> playingList = null;


        public PlayingListController()
        {
            LinearGlobal.LinearConfig.PlayerConfig.RestCount = -1;
        }

        delegate void InsertPlayingListDelegate(int rowNo);

        /// <summary>
        /// 再生中リストにプレイリストのすべてのデータをいれる。
        /// </summary>
        /// <param name="rowNo"></param>
        public void insertPlayingList(int gridrowno)
        {
            InsertPlayingListDelegate ipldel = 
                new  InsertPlayingListDelegate(delegate(int rowNo)
                                                   {
                                                       

                                                       IList<long> playingList = new List<long>();
                                                       for (int i = rowNo; i <= LinearAudioPlayer.GridController.getRowCount(); i++)
                                                       {
                                                           playingList.Add(long.Parse(LinearAudioPlayer.GridController.getValue(i, (int)GridController.EnuGrid.ID)));
                                                       }
                                                       for (int i = 1; i < rowNo; i++)
                                                       {
                                                           playingList.Add(long.Parse(LinearAudioPlayer.GridController.getValue(i, (int)GridController.EnuGrid.ID)));
                                                       }

                                                       // DBにインサート。
                                                       SQLiteTransaction sqltran = null;
                                                       try
                                                       {
                                                           sqltran = SQLiteManager.Instance.beginTransaction();
                                                           
                                                           clearPlayingList();

                                                           
                                                           List<DbParameter> paramList = new List<DbParameter>();
                                                           int i = 1;
                                                           foreach (var l in playingList)
                                                           {
                                                               paramList.Clear();
                                                               paramList.Add(new SQLiteParameter("Id", l));
                                                               paramList.Add(new SQLiteParameter("Sort", i));
                                                               SQLiteManager.Instance.executeNonQuery(SQLResource.SQL032, paramList);
                                                               i++;
                                                           }
                                                           sqltran.Commit();
                                                       }
                                                       catch (SQLiteException)
                                                       {
                                                           try
                                                           {
                                                               sqltran.Rollback();
                                                           }
                                                           catch (SQLiteException) { }
                                                       }
                                                       LinearGlobal.LinearConfig.PlayerConfig.RestCount = playingList.Count;
                                                       LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount = playingList.Count;
                                                       // 一番目は再生済みなので削除
                                                       //playingList.RemoveAt(0);

                                                   });
            ipldel.BeginInvoke(gridrowno, null, null);
        }

        public void clearPlayingList()
        {
            SQLiteManager.Instance.executeNonQuery(SQLResource.SQL031);
        }

        /// <summary>
        /// IDを割り込みする
        /// </summary>
        /// <param name="id"></param>
        public void interruptId(long id)
        {
            long playingcount = (long) SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL034);
            if (playingcount > 0)
            {
                // sortが2以降をインクリメント
                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL033);
                // idをsortが2でいれる。
                List<DbParameter> paramList = new List<DbParameter>();
                paramList.Add(new SQLiteParameter("Id", id));
                paramList.Add(new SQLiteParameter("Sort", 2));
                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL032, paramList);

                if (LinearGlobal.LinearConfig.PlayerConfig.RestCount != -1)
                {
                    LinearGlobal.LinearConfig.PlayerConfig.RestCount++;
                }
                LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount++;
            } else
            {
                List<DbParameter> paramList = new List<DbParameter>();
                paramList.Add(new SQLiteParameter("Id", id));
                paramList.Add(new SQLiteParameter("Sort", 1));
                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL032, paramList);
                LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount = 1;
            }

        }

        /// <summary>
        /// 次のID取得
        /// </summary>
        /// <param name="isEndless">エンドレスかどうか</param>
        /// <returns></returns>
        public long getNextId(bool isEndless)
        {
            long nextid = -1;

            long playingcount = (long)SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL034);
            if (playingcount > 0)
            {
                if (!isEndless)
                {
                    LinearGlobal.LinearConfig.PlayerConfig.RestCount--;

                    if (LinearGlobal.LinearConfig.PlayerConfig.RestCount == 0)
                    {
                        return -1;
                    }
                }

                SQLiteTransaction sqltran = null;
                try
                {
                    sqltran = SQLiteManager.Instance.beginTransaction();

                    // dbのsort値更新 sortを-1する
                    SQLiteManager.Instance.executeNonQuery(SQLResource.SQL036);
                    // dbのsort値更新 sortが0になったものをmaxに
                    SQLiteManager.Instance.executeNonQuery(SQLResource.SQL037, new SQLiteParameter("Sort", LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount));

                    object result = SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL035);
                    if (result != null && !"".Equals(result))
                    {
                        nextid = (long)result;
                    }

                   sqltran.Commit();
                }
                catch (SQLiteException)
                {
                    sqltran.Rollback();
                }
            }

            /*
            // todo:dbからid取得 sort=1
            object result = SQLiteUtils.executeQueryOnlyOne(SQLResource.SQL035, null);
            if (!DBNull.Value.Equals(result))
            {
                nextid = (long) result;

                if (nextid != -1)
                {
                    // dbのsort値更新 sortを-1する
                    SQLiteUtils.executeNonQuery(SQLResource.SQL036);
                    // dbのsort値更新 sortが0になったものをmaxに
                    Dictionary<string, object> paramDic = new Dictionary<string, object>();
                    paramDic.Add("Sort", maxcount);
                    SQLiteUtils.executeNonQuery(SQLResource.SQL037, paramDic);
                }

                if (!isEndless)
                {
                    RestCount--;

                    if (RestCount == 0)
                    {
                        nextid = -1;
                    }
                }
            }*/

            

            return nextid;
        }

        public PlayItemInfo getNextPlayInfo()
        {
            if (LinearGlobal.LinearConfig.PlayerConfig.RestCount == 0)
            {
                return null;
            }

            // sort=2をタイトルとアーティストを取得。取得できない場合はnullを返す
            object result = SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL039);
            if (result != null && !"".Equals(result))
            {
                PlayItemInfo pii = new PlayItemInfo();
                pii.Id = (long)result;

                IList<object> resultList =
                    SQLiteManager.Instance.executeQueryOneRecord(SQLResource.SQL030, new SQLiteParameter("Id", pii.Id));
                if (resultList.Count > 0)
                {
                    pii.Title = resultList[0].ToString();
                    pii.Artist = resultList[1].ToString();
                }

                return pii;
            }
            else
            {
                return null;
            }
        }

    }
}
