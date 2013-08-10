using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace ColorProfileGenerator
{
    public class ColorInfo
    {
        /// <summary>
        /// カラー
        /// </summary>
        [Category("カラー")]
        [Description("作成する色を選択してください。")]
        public Color Color { set; get; }

        /// <summary>
        /// 透明度
        /// </summary>
        [Category("カラー")]
        [Description("作成する透明度を選択してくだい。0(透明)から255(不透明)です。 ")]
        public byte Alpha { set; get; }

        public ColorInfo()
        {
            Color = System.Drawing.Color.White;
            Alpha = 255;
        }

    }
}
