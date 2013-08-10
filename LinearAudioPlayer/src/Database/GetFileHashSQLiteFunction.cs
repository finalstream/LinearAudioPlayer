using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using FINALSTREAM.Commons.Algorithm;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.LinearAudioPlayer.Database
{

    [SQLiteFunction(Name = "GETFILEHASH", FuncType = FunctionType.Scalar, Arguments = 2)]
    public class GetFileHashSQLiteFunction : SQLiteFunction
    {

        public override object Invoke(object[] args)
        {
            string filePath = args[0].ToString();
            string option = args[1].ToString();
            string result = "";

            if (File.Exists(filePath))
            {
                if (!String.IsNullOrEmpty(option))
                {

                    result = SevenZipManager.Instance.getCrc(filePath, option);

                }
                else
                {

                    result = FileUtils.getFileCrc32(filePath);

                }
            }

            return result;
        }
    }
}
