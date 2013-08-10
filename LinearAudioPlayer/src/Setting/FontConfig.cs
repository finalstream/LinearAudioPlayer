using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FontConfig
    {
        public string FontName { get; set; }
        public float FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
    }
}
