using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Resources;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using SearchOption = System.IO.SearchOption;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    public class WebServerThread
    {

        enum RangeType
        {
            WEEKLY,
            MONTHLY,
            LAST3MONTHS,
            LAST6MONTHS,
            LAST12MONTHS,
            ALL
        }
        private Dictionary<RangeType, string> _rangeDictionary = new Dictionary<RangeType, string>()
        {
            {RangeType.WEEKLY, "-7 days"},
            {RangeType.MONTHLY, "-1 months"},
            {RangeType.LAST3MONTHS, "-3 months"},
            {RangeType.LAST6MONTHS, "-6 months"},
            {RangeType.LAST12MONTHS, "-1 years"}
        }; 

        string docRoot = ""; // ドキュメント・ルート
        string prefix = ""; // 受け付けるURL
        private HttpListener listener = null;

        private Thread webServerThread = null;

        public WebServerThread()
        {
            docRoot = Application.StartupPath +  LinearConst.WEB_DIRECTORY_NAME;
            prefix = "http://*:" + LinearGlobal.LinearConfig.ViewConfig.WebInterfaceListenPort.ToString() + "/";
        
            webServerThread = new Thread(webServerProc);
            webServerThread.Start();

        }

        private void webServerProc()
        {
            // 管理者権限がない場合は起動しない
            if (!WindowsUtils.IsExecuteAdminRole())
            {
                MessageUtils.showMessage(MessageBoxIcon.Warning, MessageResource.W0005);
                return;
            }

            listener = new HttpListener();
            listener.Prefixes.Add(prefix); // プレフィックスの登録
            try
            {
                listener.Start();
            }
            catch (HttpListenerException httpListenerException)
            {
                // すでにポートが使用中もしくは管理者権限なし
                LinearAudioPlayer.writeErrorMessage(httpListenerException);
                return;
            }

            while (true)
            {
                HttpListenerContext context = null;
                try
                {
                    context = listener.GetContext();
                }
                catch (HttpListenerException)
                {
                    // HTTP通信中に終了されたらどうしようもないので例外を握りつぶす
                }

                HttpListenerRequest req = context.Request;
                HttpListenerResponse res = context.Response;

                Debug.WriteLine("request url: " +req.RawUrl);

                if (req.RawUrl.IndexOf("/LinearWebService") == -1)
                {
                    // WEBサーバ

                    string reqPath = req.RawUrl;
                    if (reqPath == "/")
                    {
                        reqPath = "ui/" + LinearGlobal.LinearConfig.ViewConfig.WebInterfaceTheme + ".html";
                    }

                    reqPath = reqPath.Replace("/", "\\");
                    reqPath = Regex.Replace(reqPath, "\\?.*", "");

                    // リクエストされたURLからファイルのパスを求める
                    string path = docRoot + reqPath;

                    // ファイルが存在すればレスポンス・ストリームに書き出す
                    if (File.Exists(path))
                    {
                        byte[] content = File.ReadAllBytes(path);
                        //string s = File.ReadAllText(path, Encoding.UTF8);
                        //byte[] content = Encoding.UTF8.GetBytes(s);
                        res.Headers[HttpResponseHeader.ContentType] = GuessContentType(Path.GetExtension(path));
                        res.OutputStream.Write(content, 0, content.Length);
                    }
                }
                else
                {
                    // RESTサービス
                    // TODO:変なもの受け取ったら落ちる

                    WebServiceResponseInfo response = new WebServiceResponseInfo();
                    var dict = req.QueryString;
                    var reqParamJsonString = JsonConvert.SerializeObject(
                        dict.AllKeys.ToDictionary(k => k, k => dict[k])
                        );

                    Debug.WriteLine("req data: " + reqParamJsonString);
                    WebServiceRequestInfo request =
                        JsonConvert.DeserializeObject<WebServiceRequestInfo>(reqParamJsonString);

                    response.action = request.action;
                    switch (request.action)
                    {
                        case "play":
                            Action playAction = () =>
                                {
                                    LinearAudioPlayer.PlayController.play(LinearGlobal.CurrentPlayItemInfo.Id,
                                                                            false, false);
                                };
                            LinearGlobal.MainForm.BeginInvoke(playAction);
                            break;
                        case "pause":
                            Action pauseAction = () =>
                                {
                                    LinearAudioPlayer.PlayController.pause();
                                };
                            LinearGlobal.MainForm.BeginInvoke(pauseAction);
                            break;
                        case "stop":
                            Action stopAction = () =>
                                {
                                    LinearAudioPlayer.PlayController.stop();
                                };
                            LinearGlobal.MainForm.BeginInvoke(stopAction);
                            break;

                        case "previous":
                            Action previouspAction = () =>
                                {
                                    LinearAudioPlayer.PlayController.previousPlay();
                                };
                            LinearGlobal.MainForm.BeginInvoke(previouspAction);
                            break;

                        case "forward":
                            Action forwardAction = () =>
                                {
                                    if (LinearAudioPlayer.PlayController.isPlaying())
                                    {
                                        LinearAudioPlayer.PlayController.endOfStream();
                                    }
                                };
                            LinearGlobal.MainForm.BeginInvoke(forwardAction);
                            break;

                        case "voldown":
                            Action volDownAction = () =>
                                {
                                    int vol = LinearGlobal.Volume;
                                    vol -= 5;
                                    if (vol < 0)
                                    {
                                        vol = 0;
                                    }
                                    LinearGlobal.Volume = vol;
                                    LinearGlobal.MainForm.ListForm.setVolume();
                                };
                            var voldownActionResult = LinearGlobal.MainForm.BeginInvoke(volDownAction);
                            voldownActionResult.AsyncWaitHandle.WaitOne();
                            response.volume = LinearGlobal.Volume;
                            break;

                        case "volup":
                            Action volUpAction = () =>
                                {
                                    int vol = LinearGlobal.Volume;
                                    vol += 5;
                                    if (vol > 100)
                                    {
                                        vol = 100;
                                    }
                                    LinearGlobal.Volume = vol;
                                    LinearGlobal.MainForm.ListForm.setVolume();

                                };
                            var volupActionResult = LinearGlobal.MainForm.BeginInvoke(volUpAction);
                            volupActionResult.AsyncWaitHandle.WaitOne();
                            response.volume = LinearGlobal.Volume;
                            break;

                        case "getplayinfo":
                            if (LinearAudioPlayer.PlayController != null)
                            {
                                response.playInfo = LinearGlobal.CurrentPlayItemInfo;
                                response.isPlaying = LinearAudioPlayer.PlayController.isPlaying();
                                response.isPaused = LinearAudioPlayer.PlayController.isPaused();
                                response.seekRatio = (int)(((float)LinearAudioPlayer.PlayController.getPosition() /
                                                                (float)LinearAudioPlayer.PlayController.getLength()) * 100);
                                if (response.seekRatio == 100)
                                {
                                    response.seekRatio = 0;
                                }
                            }
                            
                            break;

                        case "seek":
                            Action seekAction = () =>
                                {
                                    double value = ((double) LinearAudioPlayer.PlayController.getLength())*
                                                    request.seekPosition;
                                    LinearAudioPlayer.PlayController.setPosition((uint) value);
                                };
                            LinearGlobal.MainForm.ListForm.BeginInvoke(seekAction);
                            break;
                        case "getthemelist":
                            var themelist = new List<string>();
                            var filePaths = FileUtils.getFilePathList(
                            Application.StartupPath + LinearConst.WEB_DIRECTORY_NAME + "ui", SearchOption.TopDirectoryOnly);
                            foreach (string path in filePaths)
                            {
                                themelist.Add(Path.GetFileNameWithoutExtension(path));
                            }
                            response.themeList = themelist.ToArray();
                            break;
                        case "switchtheme":
                            LinearGlobal.LinearConfig.ViewConfig.WebInterfaceTheme = request.theme;
                            break;
                        case "getnowplaying":
                            response.nowPlaying = LinearAudioPlayer.PlayController.getNowPlayingList(10).Select(gi=> new TrackInfo( gi.Id, gi.Title, gi.Artist )).ToArray();
                            break;
                        case "addnowplaying":
                            response.nowPlaying = LinearAudioPlayer.PlayController.getNowPlayingList(request.skip, request.take).Select(gi => new TrackInfo(gi.Id, gi.Title, gi.Artist)).ToArray();
                            break;
                        case "getanalyzeinfo":
                            var ai = new AnalyzeInfo();
                            var startDate = SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL056);
                            if (startDate != null)
                            {
                                ai.StartDate = startDate.ToString();
                                ai.StartDateRelative = DateTimeUtils.getRelativeTimeString(startDate.ToString());
                            }
                            ai.TotalTracksCount = (long) SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL057);
                            ai.TotalFavoriteTracksCount = (long) SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL058);
                            ai.TotalPlayCount = (long) SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL059);
                            ai.TotalPalyHistoryCount = (long) SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL060);
                            response.analyzeOverview = ai;
                            break;
                        case "getrecentlist":
                            var paramList = new List<DbParameter>();
                            paramList.Add(new SQLiteParameter("Limit", request.limit));
                            paramList.Add(new SQLiteParameter("Offset", request.offset));
                            response.recentListen =
                                SQLiteManager.Instance.executeQueryNormal(SQLResource.SQL061, paramList).Select(o=> new TrackInfo((long)o[0], o[1].ToString(), o[2].ToString(), DateTimeUtils.getRelativeTimeString(o[3].ToString()))).ToArray();
                            response.pagerPrevious = request.offset == 0 ? -1 : request.offset - request.limit;
                            response.pagerNext = response.recentListen.Length < request.limit ? -1 : request.offset + request.limit;
                            break;
                        case "gettopartist":
                            var sql = SQLResource.SQL062;
                            var rangeType = (RangeType) Enum.Parse(typeof (RangeType), request.rangeType);
                            var where = "";
                            if (rangeType != RangeType.ALL)
                            {
                                where =
                                    string.Format(
                                        "WHERE PH.PLAYDATETIME >= DATETIME(DATETIME('NOW','LOCALTIME'), '{0}','LOCALTIME')",
                                        _rangeDictionary[rangeType]);
                            }
                            else
                            {
                                sql = SQLResource.SQL064;
                            }
                            sql = sql.Replace(":Condition", where);
                            var topArtists = SQLiteManager.Instance.executeQueryNormal(sql, new SQLiteParameter("Limit", request.limit));
                            double maxcount = topArtists.Max(o => (long) o[1]);
                            response.topLists =
                                topArtists.Select(
                                    o =>
                                        new TrackInfo(o[0].ToString(), (long) o[1],
                                            (int)((int.Parse(o[1].ToString()) / maxcount) * 100), o[2].ToString())).ToArray();
                            break;
                        case "gettoptrack":
                            var sql2 = SQLResource.SQL063;
                            var rangeType2 = (RangeType)Enum.Parse(typeof(RangeType), request.rangeType);
                            var where2 = "";
                            if (rangeType2 != RangeType.ALL)
                            {
                                where2 =
                                    string.Format(
                                        "WHERE PH.PLAYDATETIME >= DATETIME(DATETIME('NOW','LOCALTIME'), '{0}','LOCALTIME')",
                                        _rangeDictionary[rangeType2]);
                            }
                            else
                            {
                                sql2 = SQLResource.SQL065;
                            }
                            sql2 = sql2.Replace(":Condition", where2);
                            var topTracks = SQLiteManager.Instance.executeQueryNormal(sql2, new SQLiteParameter("Limit", request.limit));
                            double maxcount2 = topTracks.Max(o => (long)o[2]);
                            response.topLists =
                                topTracks.Select(
                                    o =>
                                        new TrackInfo(o[0].ToString() + " - " + o[1].ToString(), (long)o[2],
                                            (int)((int.Parse(o[2].ToString()) / maxcount2) * 100), o[3].ToString())).ToArray();
                            break;
                        case "ratingon":
                        case "ratingoff":
                            if (request.id == -1) break;
                            LinearEnum.RatingValue rating = request.action == "ratingon" ? LinearEnum.RatingValue.FAVORITE : LinearEnum.RatingValue.NORMAL;

                            Action setRatingAction = () =>
                            {
                                var rowIndex = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID, request.id.ToString());
                                if (rowIndex != -1)
                                {
                                    LinearAudioPlayer.GridController.setRatingIcon(rowIndex, rating);
                                }
                                if (request.id == LinearGlobal.CurrentPlayItemInfo.Id)
                                {
                                    LinearGlobal.MainForm.setRating((int)rating);
                                }
                            };
                            LinearGlobal.MainForm.ListForm.BeginInvoke(setRatingAction);
                            
                            SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    request.id, rating));
                            break;
                        case "skipnowplaying":
                            Action skipNowPlayingAction = () =>
                                {
                                    LinearAudioPlayer.PlayController.skipPlayingList(request.id);
                                    LinearAudioPlayer.PlayController.play(request.id, false, true);
                                };
                            LinearGlobal.MainForm.BeginInvoke(skipNowPlayingAction);
                            
                            break;
                        case "getartwork":
                            if (LinearGlobal.CurrentPlayItemInfo.Artwork != null)
                            {
                                try
                                {
                                    if (request.artworkSize == 0) request.artworkSize = 150;
                                    Bitmap thumbnail = new Bitmap(request.artworkSize, request.artworkSize);
                                    using (Graphics g = Graphics.FromImage(thumbnail))
                                    {
                                        g.InterpolationMode =
                                            System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                        g.DrawImage(LinearGlobal.CurrentPlayItemInfo.Artwork, 0, 0, request.artworkSize, request.artworkSize);
                                    }
                                    var artworkDirectoy = Application.StartupPath +
                                                          Path.Combine(LinearConst.WEB_DIRECTORY_NAME, "img");
                                    var artworkFileName = string.Format("artwork-{0}.png",
                                        LinearGlobal.CurrentPlayItemInfo.Id);
                                    thumbnail.Save(artworkDirectoy + "\\" + artworkFileName,
                                        System.Drawing.Imaging.ImageFormat.Png);
                                    thumbnail.Dispose();
                                    var oldfiles =
                                        Directory.GetFiles(artworkDirectoy, "*.png")
                                            .Where(a => Path.GetFileName(a) != artworkFileName && Path.GetFileName(a) != "blank.png");
                                    foreach (var file in oldfiles)
                                    {
                                        File.Delete(file);
                                    }
                                    response.artworkUrl = "../img/" + artworkFileName;
                                }
                                catch (Exception)
                                {
                                    response.artworkUrl = "";
                                }
                            }
                            else
                            {
                                response.artworkUrl = "";
                            }
                            break;
                    }


                    string resJsonString = JsonConvert.SerializeObject(response);
                    byte[] responseByte = Encoding.UTF8.GetBytes(resJsonString);
                    res.OutputStream.Write(responseByte, 0, responseByte.Length);

                }
                res.Close();

            }

        }

        static string GuessContentType(string ext)
        {
            switch (ext)
            {
                case ".js":
                    return "text/javascript";
                case ".htm":
                case ".html":
                    return "text/html";
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpg";
                case ".css":
                    return "text/css";

                default:
                    return "application/octet-stream";
            }
        }

        public void Kill()
        {
            if (listener != null)
            {
                listener.Close();
            }
            webServerThread.Abort();
            webServerThread.Join();
        }
        
    }
}
