using System;
using System.Collections.Generic;
using System.Text;
using FINALSTREAM.Commons.Grid;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class AnyGridItemInfo : ISourceGridItem
    {

        string _displayValue;
        object _value;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string DisplayValue
        {
            get { return _displayValue; }
            set { _displayValue = value; }
        }

        public override string ToString()
        {
            return _displayValue;
        }

    }
}
