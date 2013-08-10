using System;
using System.Data.SQLite;
using System.IO;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.LinearAudioPlayer.Database
{
    [SQLiteFunction(Name = "GETFILESIZE", FuncType = FunctionType.Scalar, Arguments = 2)]
    class GetFileSizeSQLiteFunction : SQLiteFunction
    {

        public override object Invoke(object[] args)
        {
            string filePath = args[0].ToString();
            string option = args[1].ToString();
            //string archiveTempFilePath = "";
            long result = 0;


            if (File.Exists(filePath))
            {
                if (!String.IsNullOrEmpty(option))
                {

                    result = SevenZipManager.Instance.getFileSize(filePath, option);

                }
                else
                {
                    result = FileUtils.getFileSize(filePath);

                }
            }

            return result;

        }

    }
}
