using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Database
{
    [SQLiteFunction(Name = "GETDIRPATH", FuncType = FunctionType.Scalar, Arguments = 1)]
    public class GetDirectoryPathSQLiteFunction : SQLiteFunction
    {
        public override object Invoke(object[] args)
        {
            return Path.GetDirectoryName(args[0].ToString());
        }
    }
}
