using System;
using System.Data.SQLite;
using System.IO;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.LinearAudioPlayer.Database
{
    [SQLiteFunction(Name = "GETDIRNAME", FuncType = FunctionType.Scalar, Arguments = 1)]
    class GetDirNameSQLiteFunction : SQLiteFunction
    {

        public override object Invoke(object[] args)
        {
            string filePath = args[0].ToString();
            //string archiveTempFilePath = "";
            string result = "";


            if (File.Exists(filePath))
            {
                result = Path.GetDirectoryName(filePath);
            }

            return result;

        }

    }
}
