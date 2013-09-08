using System;
using System.Data.SQLite;

namespace FINALSTREAM.LinearAudioPlayer.Database
{
    [SQLiteFunction(Name = "ISMATCHTAG", FuncType = FunctionType.Scalar, Arguments = 2)]
    public class IsMatchTagSQLiteFunction : SQLiteFunction
    {
        
        public override object Invoke(object[] args)
        {

            string[] tags = args[0].ToString().Split(new string[] { " , " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tag in tags)
            {
                if(args[1].ToString().Equals(tag))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
