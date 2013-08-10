using System;
using System.Collections.Generic;
using System.Text;
using FINALSTREAM.Commons.Grid;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class ConditionGridItemInfo : ISourceGridItem
    {

        string _displayValue;
        string _value;

        public string Value
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
