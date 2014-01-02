using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Resources;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    public class PlayingListController
    {
    
        /// <summary>
        ///  再生中リスト
        /// </summary>
        private LinkedList<GridItemInfo> playingList = null; 

        /// <summary>
        /// 再生中リスト取得するSQL
        /// </summary>
        private const string SQL = "INNER JOIN (SELECT ID,SORT FROM PLAYINGLIST) PLAYING ON PL.ID=PLAYING.ID ORDER BY PLAYING.SORT";

        public PlayingListController()
        {
            playingList = new LinkedList<GridItemInfo>();
            LinearGlobal.LinearConfig.PlayerConfig.RestCount = -1;
        }

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

        }

        public void clearPlayingList()
        {
            playingList.Clear();
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

    }
}
