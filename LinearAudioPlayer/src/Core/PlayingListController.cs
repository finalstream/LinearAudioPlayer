using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Resources;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    public class PlayingListController
    {
        //private IList<long> playingList = null;

        private LinkedList<GridItemInfo> playingList = null; 

        private const string SQL = "INNER JOIN (SELECT ID,SORT FROM PLAYINGLIST) PLAYING ON PL.ID=PLAYING.ID ORDER BY PLAYING.SORT";

        public PlayingListController()
        {
            playingList = new LinkedList<GridItemInfo>();
            LinearGlobal.LinearConfig.PlayerConfig.RestCount = -1;
        }

        delegate void InsertPlayingListDelegate(int rowNo);

        /// <summary>
        /// 再生中リストにプレイリストのすべてのデータをいれる。
        /// </summary>
        /// <param name="rowNo"></param>
        public void insertPlayingList(int gridrowno)
        {

            clearPlayingList();

            for (int i = gridrowno; i <= LinearAudioPlayer.GridController.getRowCount(); i++)
            {
                playingList.AddLast(
                    (GridItemInfo) LinearAudioPlayer.GridController.getRowGridItem(i));
            }
            for (int i = 1; i < gridrowno; i++)
            {
                playingList.AddLast(
                    (GridItemInfo)LinearAudioPlayer.GridController.getRowGridItem(i));
            }

            LinearGlobal.LinearConfig.PlayerConfig.RestCount = playingList.Count;
            LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount = playingList.Count;

            /*
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
             * */
        }

        public void clearPlayingList()
        {
            playingList.Clear();
            //SQLiteManager.Instance.executeNonQuery(SQLResource.SQL031);
        }

        /// <summary>
        /// RowNoにあるデータを割り込みする(次に再生する)
        /// </summary>
        /// <param name="id"></param>
        public void interruptRowNo(int rowNo)
        {

            long playingcount = playingList.Count;
            if (playingcount > 0)
            {
                // 既存のリストに割り込み
                GridItemInfo gi = (GridItemInfo)LinearAudioPlayer.GridController.getRowGridItem(rowNo);
                playingList.AddAfter(playingList.First, gi);

                if (LinearGlobal.LinearConfig.PlayerConfig.RestCount != -1)
                {
                    LinearGlobal.LinearConfig.PlayerConfig.RestCount++;
                }
                LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount++;
            }
            else
            {
                // リストを作成して追加
                GridItemInfo gi = (GridItemInfo) LinearAudioPlayer.GridController.getRowGridItem(rowNo);
                playingList.AddLast(gi);
                LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount = 1;

            }

            /*
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
            */
        }

        /// <summary>
        /// 次のID取得
        /// </summary>
        /// <param name="isEndless">エンドレスかどうか</param>
        /// <returns></returns>
        public long getNextId(bool isEndless)
        {
            long nextid = -1;

            long playingcount = playingList.Count;
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

                var nowNode = playingList.First;
                playingList.RemoveFirst();
                playingList.AddLast(nowNode);

                nextid = playingList.First.Value.Id;
            }

            /*
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
             */

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

            // 2番目のタイトルとアーティストを取得。取得できない場合はnullを返す
            var nextNode = playingList.First.Next;

            if (nextNode == null)
            {
                return null;
            }

            PlayItemInfo pii = new PlayItemInfo();
            GridItemInfo nextItem = nextNode.Value;
            pii.Id = nextItem.Id;
            pii.Title = nextItem.Title;
            pii.Artist = nextItem.Artist;

            return pii;
            /*
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
            }*/
        }

        /// <summary>
        /// 再生中リストをDBから復元
        /// </summary>
        public void restorePlayingList()
        {
            ConditionGridItemInfo cgi = new ConditionGridItemInfo();

            cgi.Value = SQL;

            object[][] resultList = SQLiteManager.Instance.executeQueryNormal(
                SQLBuilder.selectPlaylist(
                    SQLResource.SQL001,
                    LinearGlobal.PlaylistMode,
                    "",
                    LinearEnum.FilteringMode.DEFAULT, 
                    cgi));

            foreach (object[] recordList in resultList)
            {

                GridItemInfo gi = LinearAudioPlayer.GridController.createLoadGridItem(recordList);
                playingList.AddLast(gi);

            }

            LinearGlobal.LinearConfig.PlayerConfig.RestCount = playingList.Count;
            LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount = playingList.Count;

        }

        public GridItemInfo[] getPlayingList()
        {
            if (playingList.Count == 0)
            {
                return (GridItemInfo[])Enumerable.Empty<GridItemInfo>();
            }

            return playingList.ToArray();

        }

        public void savePlayingList()
        {

            SQLiteManager.Instance.executeNonQuery(SQLResource.SQL031);

            SQLiteTransaction sqltran = null;
            try
            {
                sqltran = SQLiteManager.Instance.beginTransaction();

                List<DbParameter> paramList = new List<DbParameter>();
                int i = 1;
                foreach (var gi in playingList)
                {
                    paramList.Clear();
                    paramList.Add(new SQLiteParameter("Id", gi.Id));
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

        }

        public void updatePlayingList(GridItemInfo gi)
        {

            // todo:ラムダ式に変える?
            foreach (var gridItemInfo in playingList)
            {
                if (gridItemInfo.Id== gi.Id)
                {
                    // todo:最終再生日時だけで十分？
                    gridItemInfo.Lastplaydate = gi.Lastplaydate;
                    break;
                }
            }

        }

        static Predicate<GridItemInfo> ById(long id)
        {
            return delegate(GridItemInfo gi)
            {
                return gi.Id == id;
            };
        }
    }
}
