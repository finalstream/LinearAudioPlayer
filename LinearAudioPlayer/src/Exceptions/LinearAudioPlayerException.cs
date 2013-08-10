using System;
using System.Collections.Generic;
using System.Text;
using FINALSTREAM.Commons.Exceptions;

namespace FINALSTREAM.LinearAudioPlayer.Exceptions
{
    public class LinearAudioPlayerException : FinalstreamException
    {

        public LinearAudioPlayerException() { }

        public LinearAudioPlayerException(string message) : base(message) { }

        public LinearAudioPlayerException(string message, Exception inner) : base(message) { }

        public LinearAudioPlayerException(ERROR_LEVEL errorLevel, string message) :base(message)
        {
            this.ErrorLevel = errorLevel;

        }
    }
}
