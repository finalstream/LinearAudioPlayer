using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FINALSTREAM.LinearAudioPlayer.Plugin;

namespace Finalstream.LinearAudioPlayer.Plugin
{
    public interface ILinkLibrary
    {
        string getLinkLibraryTitle();

        string  getLinkLibraryDescription(LinkLibraryInfo linkLibraryInfo);


    }
}
