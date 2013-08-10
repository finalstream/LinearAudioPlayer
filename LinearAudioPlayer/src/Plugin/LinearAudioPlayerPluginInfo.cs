using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Plugin
{
    public class LinearAudioPlayerPluginInfo
    {
        /// <summary>
        /// アセンブリファイルのパス
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// クラスの名前
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// PluginInfoクラスのコンストラクタ
        /// </summary>
        /// <param name="path">アセンブリファイルのパス</param>
        /// <param name="cls">クラスの名前</param>
        private LinearAudioPlayerPluginInfo(string path, string cls)
        {
            this.Location = path;
            this.ClassName = cls;
        }

        

        /// <summary>
        /// 有効なプラグインを探す
        /// </summary>
        /// <returns>有効なプラグインのPluginInfo配列</returns>
        public static LinearAudioPlayerPluginInfo[] FindPlugins()
        {
            System.Collections.ArrayList plugins =
                new System.Collections.ArrayList();
            //IPlugin型の名前
            string ipluginName = typeof(ILinearAudioPlayerPlugin).FullName;

            //プラグインフォルダ
            string folder = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly
                .GetExecutingAssembly().Location);
            folder += "\\plugin";
            if (!System.IO.Directory.Exists(folder))
                throw new ApplicationException(
                    "プラグインフォルダ\"" + folder +
                    "\"が見つかりませんでした。");

            //.dllファイルを探す
            string[] dlls =
                System.IO.Directory.GetFiles(folder, "*.dll");

            foreach (string dll in dlls)
            {
                try
                {
                    //アセンブリとして読み込む
                    System.Reflection.Assembly asm =
                        System.Reflection.Assembly.LoadFrom(dll);
                    foreach (Type t in asm.GetTypes())
                    {
                        //アセンブリ内のすべての型について、
                        //プラグインとして有効か調べる
                        if (t.IsClass && t.IsPublic && !t.IsAbstract &&
                            t.GetInterface(ipluginName) != null)
                        {
                            //PluginInfoをコレクションに追加する
                            plugins.Add(
                                new LinearAudioPlayerPluginInfo(dll, t.FullName));
                        }
                    }
                }
                catch
                {
                }
            }

            //コレクションを配列にして返す
            return (LinearAudioPlayerPluginInfo[])plugins.ToArray(typeof(LinearAudioPlayerPluginInfo));
        }

        /// <summary>
        /// プラグインクラスのインスタンスを作成する
        /// </summary>
        /// <returns>プラグインクラスのインスタンス</returns>
        public ILinearAudioPlayerPlugin CreateInstance()
        {
            try
            {
                //アセンブリを読み込む
                System.Reflection.Assembly asm =
                    System.Reflection.Assembly.LoadFrom(this.Location);
                //クラス名からインスタンスを作成する
                return (ILinearAudioPlayerPlugin)
                    asm.CreateInstance(this.ClassName);
            }
            catch
            {
                return null;
            }
        }
    }
}
