﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.18052
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FINALSTREAM.LinearAudioPlayer.Resources {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SQLResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SQLResource() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FINALSTREAM.LinearAudioPlayer.Resources.SQLResource", typeof(SQLResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  PL.ID,
        ///  FILEPATH,
        ///  null,
        ///  TITLE,
        ///  ARTIST,
        ///  ALBUM,
        ///  TRACK,
        ///  LENGTH,
        ///  BITRATE,
        ///  GENRE,
        ///  YEAR,
        ///  LASTPLAYDATE,
        ///  DATE,
        ///  RATING,
        ///  TAG,
        ///  PLAYCOUNT,
        ///  NOTFOUND,
        ///  OPTION
        ///FROM PLAYLIST PL に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL001 {
            get {
                return ResourceManager.GetString("SQL001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  COUNT(ID) 
        ///FROM PLAYLIST 
        ///WHERE FILEPATH = :FilePath
        ///AND OPTION = :Option に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL002 {
            get {
                return ResourceManager.GetString("SQL002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   INSERT INTO PLAYLIST(FILEPATH, TITLE, ARTIST, ALBUM, TRACK, LENGTH, GENRE, RATING, PLAYCOUNT, YEAR, BITRATE, DATE, SORT , NOTFOUND, OPTION, TAG, ADDDATE, LASTPLAYDATE, ADDDATETIME,  FILESIZE) 
        ///VALUES (:FilePath, :Title, :Artist, :Album, :Track, :Time, :Genre, :Rating, :PlayCount, :Year, :Bitrate, :Date, null, :NotFound, :Option, null, :AddDate, null, :AddDateTime, :FileSize) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL003 {
            get {
                return ResourceManager.GetString("SQL003", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT last_insert_rowid() AS LASTROWID に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL004 {
            get {
                return ResourceManager.GetString("SQL004", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET
        ///TITLE = :Title ,
        ///PLAYCOUNT = :PlayCount ,
        ///RATING = :Rating ,
        ///DATE = :Date,
        ///ADDDATE = :AddDate,
        ///ADDDATETIME = :AddDateTime,
        ///NOTFOUND = :NotFound
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL005 {
            get {
                return ResourceManager.GetString("SQL005", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET
        ///TITLE = :Title ,
        ///ARTIST = :Artist, 
        ///BITRATE = :Bitrate,
        ///ALBUM = :Album,
        ///TRACK = :Track,
        ///GENRE = :Genre,
        ///YEAR = :Year,
        ///LENGTH = :Time,
        ///NOTFOUND = :NotFound,
        ///DATE = :Date,
        ///LASTPLAYDATE = :LastPlayDate
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL007 {
            get {
                return ResourceManager.GetString("SQL007", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST
        ///SET TAG = :Tag
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL008 {
            get {
                return ResourceManager.GetString("SQL008", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET SORT = :Sort
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL009 {
            get {
                return ResourceManager.GetString("SQL009", resourceCulture);
            }
        }
        
        /// <summary>
        ///   DELETE FROM PLAYLIST に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL010 {
            get {
                return ResourceManager.GetString("SQL010", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT #condMode# , count(*)
        ///FROM PLAYLIST PL
        ///WHERE #condMode# IS NOT NULL
        ///AND #RATING#
        ///#FILTERING#
        ///GROUP BY #condMode#
        ///HAVING COUNT(*) &gt;= #condCount# に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL012 {
            get {
                return ResourceManager.GetString("SQL012", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT ID
        ///FROM PLAYLIST
        ///WHERE #RATING#
        ///ORDER BY RANDOM() LIMIT 1 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL013 {
            get {
                return ResourceManager.GetString("SQL013", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT TAG
        ///FROM PLAYLIST
        ///WHERE TAG IS NOT NULL に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL014 {
            get {
                return ResourceManager.GetString("SQL014", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT COUNT(*) FROM PLAYLIST WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL015 {
            get {
                return ResourceManager.GetString("SQL015", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT sum(round((julianday(&apos;00:&apos; || length)-julianday(&apos;00:00:00&apos;)) * 86400))
        ///FROM PLAYLIST PL に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL016 {
            get {
                return ResourceManager.GetString("SQL016", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  PLAYCOUNT 
        ///FROM PLAYLIST 
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL017 {
            get {
                return ResourceManager.GetString("SQL017", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET FILEPATH = :NewFilePath
        ///WHERE FILEPATH = :OldFilePath に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL018 {
            get {
                return ResourceManager.GetString("SQL018", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  FILEPATH,
        ///  OPTION
        ///FROM PLAYLIST 
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL019 {
            get {
                return ResourceManager.GetString("SQL019", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  GETDIRPATH(FILEPATH),
        ///  count(*)
        ///FROM PLAYLIST 
        ///GROUP BY GETDIRPATH(FILEPATH)
        ///order by 2 desc に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL020 {
            get {
                return ResourceManager.GetString("SQL020", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST
        ///SET FILEPATH = REPLACE(FILEPATH,:BeforeString,:AfterString)
        ///,NOTFOUND = 0
        ///WHERE FILEPATH LIKE :BeforeLikeString に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL021 {
            get {
                return ResourceManager.GetString("SQL021", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET
        ///PLAYCOUNT = :PlayCount
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL022 {
            get {
                return ResourceManager.GetString("SQL022", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET
        ///TITLE = :Title ,
        ///ARTIST = :Artist, 
        ///BITRATE = :Bitrate,
        ///ALBUM = :Album,
        ///TRACK = :Track,
        ///GENRE = :Genre,
        ///YEAR = :Year,
        ///LENGTH = :Time,
        ///NOTFOUND = :NotFound,
        ///DATE = :Date,
        ///DESCRIPTION = null,
        ///FILESIZE = GETFILESIZE(:FilePath,:Option)
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL023 {
            get {
                return ResourceManager.GetString("SQL023", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET
        ///NOTFOUND = :NotFound
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL024 {
            get {
                return ResourceManager.GetString("SQL024", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST
        ///SET FILEPATH = :FilePath,
        ///OPTION = :Option,
        ///NOTFOUND = 0
        ///WHERE Id = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL025 {
            get {
                return ResourceManager.GetString("SQL025", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  ID,
        ///  FILEPATH,
        ///  OPTION,
        ///  TRACK,
        ///  GENRE
        ///FROM PLAYLIST 
        ///WHERE ARTIST = :Artist
        ///AND ALBUM = :Album に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL026 {
            get {
                return ResourceManager.GetString("SQL026", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST 
        ///SET
        ///TITLE = :Title ,
        ///ARTIST = :Artist, 
        ///ALBUM = :Album,
        ///TRACK = :Track,
        ///GENRE = :Genre,
        ///YEAR = :Year,
        ///FILESIZE = GETFILESIZE(:FilePath,:Option)
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL027 {
            get {
                return ResourceManager.GetString("SQL027", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST
        ///SET FILEPATH = :NewFilePath
        ///WHERE FILEPATH = :OldFilePath に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL028 {
            get {
                return ResourceManager.GetString("SQL028", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT *
        ///FROM ( #SQL# ) 
        ///ORDER BY random() に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL029 {
            get {
                return ResourceManager.GetString("SQL029", resourceCulture);
            }
        }
        
        /// <summary>
        ///   DELETE FROM PLAYINGLIST に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL031 {
            get {
                return ResourceManager.GetString("SQL031", resourceCulture);
            }
        }
        
        /// <summary>
        ///   INSERT INTO PLAYINGLIST(ID, SORT) VALUES (:Id, :Sort) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL032 {
            get {
                return ResourceManager.GetString("SQL032", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  FILEPATH,
        ///  OPTION,
        ///  LENGTH,
        ///  RATING
        ///FROM PLAYLIST 
        ///WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL038 {
            get {
                return ResourceManager.GetString("SQL038", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT TITLE || ARTIST || FILESIZE FROM PLAYLIST GROUP BY TITLE || ARTIST || FILESIZE に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL040 {
            get {
                return ResourceManager.GetString("SQL040", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT FILEPATH || OPTION FROM PLAYLIST GROUP BY FILEPATH || OPTION に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL041 {
            get {
                return ResourceManager.GetString("SQL041", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT ID, FILEPATH, OPTION FROM PLAYLIST WHERE TITLE = :Title AND ARTIST = :Artist AND FILESIZE = :FileSize ORDER BY ID に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL042 {
            get {
                return ResourceManager.GetString("SQL042", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT 
        ///  FILEPATH,
        ///  TITLE,
        ///  ARTIST,
        ///  LENGTH
        ///FROM PLAYLIST
        ///WHERE OPTION = &quot;&quot; OR OPTION IS NULL
        ///ORDER BY ID ASC に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL043 {
            get {
                return ResourceManager.GetString("SQL043", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST SET SORT = :Sort WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL044 {
            get {
                return ResourceManager.GetString("SQL044", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT *
        ///FROM (SELECT 
        ///  ID,
        ///  TITLE
        ///FROM PLAYLIST
        ///WHERE #RATING#
        ///AND ID &lt;&gt; :Id
        ///AND ARTIST = :Artist)
        ///ORDER BY random() LIMIT #LIMIT# に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL045 {
            get {
                return ResourceManager.GetString("SQL045", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT DESCRIPTION
        ///FROM PLAYLIST
        ///WHERE ARTIST = :Artist
        ///AND ALBUM = :Album
        ///ORDER BY ID ASC
        ///LIMIT 1 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL046 {
            get {
                return ResourceManager.GetString("SQL046", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST
        ///SET DESCRIPTION = :Description
        ///WHERE ID = (SELECT MIN(ID) FROM PLAYLIST WHERE ARTIST = :Artist AND ALBUM = :Album) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL047 {
            get {
                return ResourceManager.GetString("SQL047", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT
        /// ID,
        /// FILEPATH,
        /// OPTION,
        /// TRACK,
        /// GENRE
        ///FROM PLAYLIST
        ///WHERE ADDDATETIME = (SELECT ADDDATETIME FROM PLAYLIST WHERE ID = :Id) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL048 {
            get {
                return ResourceManager.GetString("SQL048", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT ID
        ///FROM PLAYLIST
        ///WHERE LASTPLAYDATE &lt; :LastPlayDate
        ///ORDER BY LASTPLAYDATE DESC LIMIT 1 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL049 {
            get {
                return ResourceManager.GetString("SQL049", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT SORT FROM PLAYINGLIST WHERE ID = :Id に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL050 {
            get {
                return ResourceManager.GetString("SQL050", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT ID
        ///FROM PLAYINGLIST
        ///WHERE SORT &lt; :Sort
        ///ORDER BY SORT DESC LIMIT 1 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL051 {
            get {
                return ResourceManager.GetString("SQL051", resourceCulture);
            }
        }
        
        /// <summary>
        ///   SELECT ID FROM PLAYINGLIST WHERE SORT = (SELECT MAX(SORT) FROM PLAYINGLIST) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL052 {
            get {
                return ResourceManager.GetString("SQL052", resourceCulture);
            }
        }
        
        /// <summary>
        ///   UPDATE PLAYLIST
        ///SET DESCRIPTION = :Description
        ///WHERE ID = (
        ///SELECT ID
        ///FROM PLAYLIST
        ///WHERE ARTIST = :Artist
        ///AND ALBUM = :Album
        ///ORDER BY ID ASC
        ///LIMIT 1) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SQL053 {
            get {
                return ResourceManager.GetString("SQL053", resourceCulture);
            }
        }
    }
}
