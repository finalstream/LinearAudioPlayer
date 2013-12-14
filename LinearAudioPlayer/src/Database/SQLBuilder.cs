using System;
using System.Collections.Generic;
using System.Text;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Resources;

namespace FINALSTREAM.LinearAudioPlayer.Database
{
    public class SQLBuilder
    {

        /*
            定数
        */
        #region Const

        private const string WHERE_STRING = " WHERE ";

        private const string AND_STRING = " AND ";

        private const string OR_STRING = " OR ";

        private const string INNERJOIN_STRING = " INNER JOIN ";

        // TODO:ソート(SORT)の使い方を再考する。
        private const string ORDERBY_STRING = " ORDER BY PL.ADDDATETIME DESC, PL.ALBUM, ABS(PL.TRACK), PL.ID";
        private const string ORDERBY_STRING_ORIGINAL_SORT = " ORDER BY PL.SORT, PL.ADDDATETIME DESC, ABS(PL.TRACK), PL.ID";

        private const string PLAYLIST_TABLE_NAME = " PLAYLIST ";

        //private const string SPECIALLIST_TABLE_NAME = " SPECIALLIST ";

        //private static string EXCLUSION_COND = "NOT EXISTS(SELECT ID FROM (SELECT ID,CLASS FROM SPECIALLIST WHERE CLASS = " + (int)LinearEnum.PlaylistMode.EXCLUSION + ") SL WHERE PL.ID = SL.ID)";

        //private static string CLEAR_FAVORITE_COND = " EXISTS(SELECT ID FROM (SELECT ID,CLASS FROM SPECIALLIST WHERE CLASS = " + (int)LinearEnum.PlaylistMode.FAVORITE + ") SL WHERE PLAYLIST.ID = SL.ID)";

        //private static string CLEAR_EXCLUSION_COND = " EXISTS(SELECT ID FROM (SELECT ID,CLASS FROM SPECIALLIST WHERE CLASS = " + (int)LinearEnum.PlaylistMode.EXCLUSION + ") SL WHERE PLAYLIST.ID = SL.ID)";

        private static string LAST_WHERE_SQL = "";

        #endregion

        /*
            パブリックメソッド 
        */
        #region Public Method

        /// <summary>
        /// プレイリスト取得用SQL生成
        /// </summary>
        /// <param name="baseSql"></param>
        /// <param name="playlistMode"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public static string selectPlaylist(
            string baseSql, 
            LinearEnum.PlaylistMode playlistMode, 
            string filterString,
            LinearEnum.FilteringMode filteringMode,
            ConditionGridItemInfo conditionItem)
        {

            bool isNotOrder = false;
            bool isFullSQL = false;
            StringBuilder sb = new StringBuilder();

            //sb.Append(baseSql);

            // プレイリストによる絞り込み
            sb.Append(WHERE_STRING);
            sb.Append(getRatingWhereString(playlistMode));
            
            if (!String.IsNullOrEmpty(filterString))
            {
                sb.Append(AND_STRING);
                if (filterString.Length < 3)
                {
                    sb.Append(
                        "IFNULL(TITLE,'') || IFNULL(ARTIST,'') || IFNULL(ALBUM,'') || IFNULL(TAG,'') LIKE '%" +
                        escapeSQL(filterString) + "%'");
                }
                else
                {
                    sb.Append(
                        "(( IFNULL(TITLE,'') || IFNULL(ARTIST,'') || IFNULL(ALBUM,'') || IFNULL(TAG,'') LIKE '%" +
                        escapeSQL(filterString) + "%' ) OR (");
                    sb.Append("ISMATCHMIGEMO('" + escapeSQL(filterString) +
                              "',IFNULL(TITLE,'') || IFNULL(ARTIST,'') || IFNULL(ALBUM,'') || IFNULL(TAG,'')) ))");
                }
                //sb.Append(AND_STRING);
            }

            // コンディションによる絞り込み
            switch (filteringMode)
            {
                case LinearEnum.FilteringMode.DEFAULT:
                    // カスタム
                    if (!String.IsNullOrEmpty(conditionItem.Value))
                    {
                        if (!(conditionItem.Value.Substring(0, 3).ToUpper().Equals("AND") || conditionItem.Value.Substring(0, 2).ToUpper().Equals("OR")))
                        {
                            isFullSQL = true;
                        }
                        sb.Append(" ");

                        string customSql = conditionItem.Value;

                        if (!StringUtils.hasString(customSql, "#LIMIT#")
                            || LinearGlobal.LinearConfig.DatabaseConfig.LimitCount == 0)
                        {
                            customSql = customSql.Replace("#LIMIT#", "");
                        }
                        else
                        {
                            customSql = customSql.Replace(
                                "#LIMIT#", 
                                " LIMIT " + LinearGlobal.LinearConfig.DatabaseConfig.LimitCount.ToString());
                        }
                        
                        sb.Append(customSql);

                        isNotOrder = true;
                    }
                    break;
                
                case LinearEnum.FilteringMode.TAG:

                    if (conditionItem.Value != null)
                    {
                        sb.Append(AND_STRING);
                        if (!String.IsNullOrEmpty(conditionItem.Value))
                        {
                            sb.Append("ISMATCHTAG(TAG, '" + escapeSQL(conditionItem.Value) + "')");
                        }
                        else
                        {
                            sb.Append("(");
                            sb.Append(filteringMode.ToString() + " = ''");
                            sb.Append(OR_STRING);
                            sb.Append(filteringMode.ToString() + " IS NULL");
                            sb.Append(")");
                        }
                    }
                    break;

                case LinearEnum.FilteringMode.FOLDER:
                    if (conditionItem.Value != null)
                    {
                        sb.Append(AND_STRING);
                        if (!String.IsNullOrEmpty(conditionItem.Value))
                        {
                            sb.Append("GETDIRNAME(FILEPATH) = '" + escapeSQL(conditionItem.Value) + "'");
                        }
                        else
                        {
                            sb.Append("GETDIRNAME(FILEPATH) = ''");
                            sb.Append(OR_STRING);
                            sb.Append("GETDIRNAME(FILEPATH) IS NULL");
                        }
                    }
                    break;

                default:
                    if (conditionItem.Value != null)
                    {
                        sb.Append(AND_STRING);
                        if (!String.IsNullOrEmpty(conditionItem.Value))
                        {
                            if (filteringMode != LinearEnum.FilteringMode.GENRE)
                            {
                                sb.Append(filteringMode.ToString() + " = '" + escapeSQL(conditionItem.Value) + "'");
                            }
                            else
                            {
                                // ジャンルだけ大文字でマッチングする
                                sb.Append("trim(upper(" + filteringMode.ToString() + ")) = '" + escapeSQL(conditionItem.Value) + "'");
                            }
                        }
                        else
                        {
                            sb.Append(filteringMode.ToString() + " = ''");
                            sb.Append(OR_STRING);
                            sb.Append(filteringMode.ToString() + " IS NULL");
                        }

                        if (filteringMode == LinearEnum.FilteringMode.ALBUM)
                        {
                            sb.Append("ORDER BY ABS(PL.TRACK)");
                            isNotOrder = true;
                        }
                    }
                    break;
            }
            LAST_WHERE_SQL = sb.ToString();

            if (!isNotOrder)
            {
                if (LinearGlobal.SortMode == LinearEnum.SortMode.DEFAULT)
                {
                    sb.Append(ORDERBY_STRING);
                } else
                {
                    if (LinearGlobal.FilteringMode == LinearEnum.FilteringMode.ARTIST &&
                        LinearGlobal.ShuffleMode == LinearEnum.ShuffleMode.OFF){
                        sb.Append(ORDERBY_STRING_ORIGINAL_SORT);
                    } else
                    {
                        sb.Append(ORDERBY_STRING);
                    }
                }

                LAST_WHERE_SQL = sb.ToString();
            }

            if (isFullSQL)
            {
                return baseSql + " " + conditionItem.Value;
            } 
            if (LinearGlobal.ShuffleMode == LinearEnum.ShuffleMode.OFF)
            {
                LAST_WHERE_SQL = LAST_WHERE_SQL.Replace("#NOSHUFFLE#", "");
                return baseSql + LAST_WHERE_SQL;
            }
            else
            {
                return SQLResource.SQL029.Replace("#SQL#", baseSql + LAST_WHERE_SQL);
            }
            
        }

        public static string getRatingWhereString(LinearEnum.PlaylistMode playlistMode)
        {
            switch (playlistMode)
            {
                case LinearEnum.PlaylistMode.NORMAL:
                    return "RATING > 0 ";
                case LinearEnum.PlaylistMode.FAVORITE:
                    return "RATING = 9 ";
                case LinearEnum.PlaylistMode.EXCLUSION:
                    return "RATING = 0 ";
            }
            return "";
        }

        /// <summary>
        /// データを削除するSQLを生成する。
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="id"></param>
        /// <param name="speciallistOnly"></param>
        /// <returns></returns>
        public static string deleteRecord(LinearEnum.PlaylistMode mode, long id, bool speciallistOnly)
        {
            StringBuilder sb = new StringBuilder();
            string targetTable = "";
            switch (mode)
            {
                case LinearEnum.PlaylistMode.NORMAL:
                    targetTable = PLAYLIST_TABLE_NAME;
                    break;
                default:
                    //targetTable = SPECIALLIST_TABLE_NAME;
                    break;
            }
            sb.Append("DELETE FROM ");
            sb.Append(targetTable);
            sb.Append(" WHERE ID = ");
            sb.Append(id.ToString());
            if (speciallistOnly)
            {
               sb.Append(" AND CLASS = ");
               sb.Append(((int)mode).ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// フィルタリングコンボ選択用SQL
        /// </summary>
        /// <param name="playlistMode"></param>
        /// <param name="condtionModeName"></param>
        /// <returns></returns>
        public static string selectConditionList(
            LinearEnum.PlaylistMode playlistMode,
            LinearEnum.FilteringMode condtionMode,
            string filterString)
        {
            string conditionModeName;
            if (condtionMode != LinearEnum.FilteringMode.FOLDER)
            {
                conditionModeName = Enum.GetName(typeof (LinearEnum.FilteringMode), condtionMode);
            } else
            {
                conditionModeName = "GETDIRNAME(FILEPATH)";
            }

            string ratingCondition = getRatingWhereString(playlistMode);

            string result = "";
            if (condtionMode != LinearEnum.FilteringMode.GENRE)
            {
                result = SQLResource.SQL012.Replace("#condMode#", conditionModeName)
                                           .Replace("#RATING#", ratingCondition);
            }
            else
            {
                // ジャンルだけすべて大文字でグループ化する
                result = SQLResource.SQL012.Replace("#condMode#", "trim(upper(" + conditionModeName+ "))")
                                           .Replace("#RATING#", ratingCondition);
            }

            if (!String.IsNullOrEmpty(filterString))
            {
                if (filterString.Length < 3)
                {
                    result = result.Replace("#FILTERING#", AND_STRING +
                        "IFNULL(" + conditionModeName + ",'') LIKE '%" +
                        escapeSQL(filterString) + "%' ");
                }
                else
                {
                    result = result.Replace("#FILTERING#", AND_STRING +
                        "(( IFNULL(" + conditionModeName + ",'') LIKE '%" + escapeSQL(filterString) + "%' ) OR (" 
                        + "ISMATCHMIGEMO('" + escapeSQL(filterString) +"',IFNULL(" + conditionModeName + ",'')) ))");
                }
            }
            else
            {
                result = result.Replace("#FILTERING#", "");
            }

            if (LinearEnum.FilteringMode.ALBUM.ToString().Equals(conditionModeName.ToUpper()))
            {
                result = result.Replace("#condCount#", LinearGlobal.LinearConfig.ViewConfig.AlbumJudgeCount.ToString());
            }
            else
            {
                result = result.Replace("#condCount#", "2");
            }

            if (LinearGlobal.LinearConfig.ViewConfig.GroupListOrder 
                == (int) LinearEnum.EnumGroupListOrder.COUNT_DESC)
            {
                switch (condtionMode)
                {
                    case LinearEnum.FilteringMode.ARTIST:
                    case LinearEnum.FilteringMode.GENRE:
                    case LinearEnum.FilteringMode.TAG:
                    case LinearEnum.FilteringMode.FOLDER:
                        result += " ORDER BY COUNT(*) DESC";
                        break;
                    case LinearEnum.FilteringMode.ALBUM:
                        result += " ORDER BY MAX(DATE) DESC";
                        break;
                    case LinearEnum.FilteringMode.YEAR:
                        result += " ORDER BY " + conditionModeName + " DESC";
                        break;
                }
            }else
            {
                switch (condtionMode)
                {
                    case LinearEnum.FilteringMode.ARTIST:
                        result += " ORDER BY ARTIST ASC";
                        break;
                    case LinearEnum.FilteringMode.GENRE:
                        result += " ORDER BY GENRE ASC";
                        break;
                    case LinearEnum.FilteringMode.TAG:
                        result += " ORDER BY TAG ASC";
                        break;
                    case LinearEnum.FilteringMode.FOLDER:
                        result += " ORDER BY GETDIRNAME(FILEPATH) ASC";
                        break;
                    case LinearEnum.FilteringMode.ALBUM:
                        result += " ORDER BY ALBUM ASC";
                        break;
                    case LinearEnum.FilteringMode.YEAR:
                        result += " ORDER BY YEAR ASC";
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 同じアーティストの曲を選択するSQL
        /// </summary>
        /// <returns></returns>
        public static string selectSameArtistTrackList()
        {
            return SQLResource.SQL045
                .Replace("#RATING#", getRatingWhereString(LinearGlobal.PlaylistMode))
                .Replace("#LIMIT#", "6");
        }

        /// <summary>
        /// 長さの合計を秒で取得する。
        /// </summary>
        /// <returns></returns>
        public static string selectSumLengthSec()
        {
            return SQLResource.SQL016 + LAST_WHERE_SQL;
        }

        public static string updateRating(long id, LinearEnum.RatingValue rating)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE PLAYLIST ");
            sb.Append(" SET RATING = ");
            sb.Append((int)rating);
            sb.Append(" WHERE ID = ");
            sb.Append(id);

            return sb.ToString();

        }



        #endregion

        /*
            プライベートメソッド
        */
        #region Private Method

        private static string escapeSQL(string sql){
            return sql.Replace("'","''");
        }

        #endregion
    }
}
