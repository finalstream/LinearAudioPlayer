﻿using System;
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
                gi.IsInterrupt = true;
                var interruptNode = playingList.Find(playingList.LastOrDefault(g => g.IsInterrupt));
                if (interruptNode == null)
                {
                    interruptNode = playingList.First;
                }
                playingList.AddAfter(interruptNode, gi);

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
                nowNode.Value.IsInterrupt = false;
                playingList.RemoveFirst();
                playingList.AddLast(nowNode);

                nextid = playingList.First.Value.Id;
            }

            if (!LinearGlobal.invalidIdTable.Contains(nextid))
            {
                return nextid;
            }
            else
            {
                // 一時無効リストにあるIDだったら次の曲を取得
                LinearGlobal.LinearConfig.PlayerConfig.RestCount++;
                return getNextId(isEndless);
            }

            
        }

        public long getPreviousId()
        {
            if (playingList.Count > 0)
            {
                // 最後のノードがスキップだったらもっかいループ
                LinkedListNode<GridItemInfo> lastNode;
                do
                {
                    lastNode = playingList.Last;
                    playingList.RemoveLast();
                    playingList.AddFirst(lastNode);
                } while (lastNode.Value.IsSkipped);
                
                return lastNode.Value.Id;
            }
            else
            {
                return -1;
            }
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
                    gridItemInfo.IsSkipped = false;
                    break;
                }
            }

        }

        public GridItemInfo[] getNowPlayingList(int listcount)
        {
            if (playingList.Count > 0)
            {
                return playingList.Where(p=>!LinearGlobal.invalidIdTable.Contains(p.Id)).Take(listcount).ToArray();
            }
            else
            {
                return new GridItemInfo[]{};
            }
            
        }

        public GridItemInfo[] getNowPlayingList(int skipcount, int takecount)
        {
            if (playingList.Count > 0)
            {
                return playingList.Where(p => !LinearGlobal.invalidIdTable.Contains(p.Id)).Skip(skipcount).Take(takecount).ToArray();
            }
            else
            {
                return new GridItemInfo[] { };
            }

        }

        public void skipPlayingList(long id)
        {
            if (playingList.First.Value.Id == id) return; 
            bool isHit = false;
            List<GridItemInfo> removeList = new List<GridItemInfo>();

            var i = 0;
            foreach (var gi in playingList)
            {
                if (i == 0)
                {
                    // 最初はスキップ扱いしない
                    removeList.Add(gi);
                } else if (gi.Id == id)
                {
                    isHit = true;
                    break;
                }
                else
                {
                    gi.IsSkipped = true;
                    removeList.Add(gi);
                }
                i++;
            }

            if (isHit)
            {
                foreach (var gi in removeList.ToArray())
                {
                    //playingList.Remove(gi);
                    playingList.Remove(gi);
                    playingList.AddLast(gi);
                }
            }
        }

        public void ClearInterrupt()
        {
            foreach (var gridItemInfo in playingList.Where((g=> g.IsInterrupt)))
            {
                gridItemInfo.IsInterrupt = false;
            }
        }

        public void resortPlayingList(IEnumerable<long> ids)
        {
            ids = ids.Reverse();

            foreach (var id in ids)
            {
                if(playingList.First.Value.Id == id) continue;
                // idを探す
                var gi = playingList.FirstOrDefault(g => g.Id == id);
                if (gi != null)
                {
                    playingList.Remove(gi);
                    playingList.AddAfter(playingList.First ,gi);
                }
            }

        }

        public void removePlayingList(long id)
        {
            // idを探す
            var gi = playingList.FirstOrDefault(g => g.Id == id);
            if (gi != null)
            {
                playingList.Remove(gi);
            }
        }
    }
}
