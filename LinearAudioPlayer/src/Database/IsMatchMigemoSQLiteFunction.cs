using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FINALSTREAM.Commons.Exceptions;
using FINALSTREAM.Commons.Library.Migemo;

namespace FINALSTREAM.LinearAudioPlayer.Database
{
    [SQLiteFunction(Name = "ISMATCHMIGEMO", FuncType = FunctionType.Scalar, Arguments = 2)]
    public class IsMatchMigemoSQLiteFunction : SQLiteFunction
    {
        
        private Migemo migemo;
        private static Migemo usermigemo = null;
        public IsMatchMigemoSQLiteFunction()
        {
            migemo = new Migemo(Application.StartupPath + LinearConst.MIGEMO_DICTIONARY_NAME);
            if (File.Exists(Application.StartupPath + LinearConst.MIGEMO_USERDICTIONARY_NAME))
            {
                usermigemo = new Migemo(Application.StartupPath + LinearConst.MIGEMO_USERDICTIONARY_NAME);
            }
        }
        public override object Invoke(object[] args)
        {
            bool result = false;

            try
            {
                result = migemo.GetRegex(args[0].ToString()).IsMatch(args[1].ToString());



                if (!result && usermigemo != null)
                {
                    result = usermigemo.GetRegex(args[0].ToString()).IsMatch(args[1].ToString());
                }
            }
            catch (ArgumentException)
            {
                // 正規表現の解析に失敗した
                return false;
            }


            return result;
        }

        public static void refreshUserDict()
        {
            if (File.Exists(Application.StartupPath + LinearConst.MIGEMO_USERDICTIONARY_NAME))
            {
                usermigemo = new Migemo(Application.StartupPath + LinearConst.MIGEMO_USERDICTIONARY_NAME);
            }
        }
    }
}
