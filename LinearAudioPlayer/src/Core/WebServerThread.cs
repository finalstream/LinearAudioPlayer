using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using FINALSTREAM.LinearAudioPlayer.Info;
using Newtonsoft.Json;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    public class WebServerThread
    {
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
                        reqPath = "index.html";
                    }

                    reqPath = reqPath.Replace("/", "\\");

                    // リクエストされたURLからファイルのパスを求める
                    string path = docRoot + reqPath;

                    // ファイルが存在すればレスポンス・ストリームに書き出す
                    if (File.Exists(path))
                    {
                        byte[] content = File.ReadAllBytes(path);
                        //string s = File.ReadAllText(path, Encoding.UTF8);
                        //byte[] content = Encoding.UTF8.GetBytes(s);
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
                    WebServiceRequestInfo request = JsonConvert.DeserializeObject<WebServiceRequestInfo>(reqParamJsonString);

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
                                    vol-= 5;
                                    if (vol < 0)
                                    {
                                        vol = 0;
                                    }
                                    LinearGlobal.Volume = vol;
                                    LinearGlobal.MainForm.ListForm.setVolume();
                                };
                            LinearGlobal.MainForm.BeginInvoke(volDownAction);
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
                            LinearGlobal.MainForm.BeginInvoke(volUpAction);
                            break;

                        case "getplayinfo":
                            response.playInfo = LinearGlobal.CurrentPlayItemInfo;

                            response.seekRatio = (int) (((float)LinearAudioPlayer.PlayController.getPosition() /
                                                        (float)LinearAudioPlayer.PlayController.getLength()) * 100);
                            break;

                        case "seek":
                            Action seekAction = () =>
                            {
                                double value = ((double)LinearAudioPlayer.PlayController.getLength()) * request.seekPosition;
                                LinearAudioPlayer.PlayController.setPosition((uint)value);
                            };
                            LinearGlobal.MainForm.ListForm.BeginInvoke(seekAction);
                            break;

                    }


                    string resJsonString = JsonConvert.SerializeObject(response);
                    byte[] responseByte = Encoding.UTF8.GetBytes(resJsonString);
                    res.OutputStream.Write(responseByte, 0, responseByte.Length);

                }
                res.Close();

            }

        }

        public void Kill()
        {
            listener.Close();
            webServerThread.Abort();
            webServerThread.Join();
        }
    }
}
