using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    /// <summary>
    /// メドレー情報
    /// </summary>
    public class MedleyInfo
    {

        public enum EnumStartPosition
        {
            DEFAULT,
            OPENING,
            MIDDLE,
            ENDING
        }

        public enum EnumPlayTime
        {
            DEFAULT,
            SHORT,
            LONG,
            HALF
        }

        public long MedleyId { get; set; }

        /// <summary>
        /// 有効／無効
        /// </summary>
        public bool Enable { get; set; }


        public float FadeDuration { get; set; }

        /// <summary>
        /// 再生時間min(%)
        /// </summary>
        public double PlaytimeRatioMin { get; set; }

        /// <summary>
        /// 再生時間max(%)
        /// </summary>
        public double PlaytimeRatioMax { get; set; }

        /// <summary>
        /// 再生位置min(%)
        /// </summary>
        public double PlayPositionRatioMin { get; set; }

        /// <summary>
        /// 再生位置max(%)
        /// </summary>
        public double PlayPositionRatioMax { get; set; }

        /// <summary>
        /// 再生開始位置
        /// </summary>
        public int StartPoint { get; set; }

        /// <summary>
        /// 再生終了位置
        /// </summary>
        public int EndPoint { get; set; }

        public EnumStartPosition StartPosition { get; set; }
        public EnumPlayTime PlayTime { get; set; }

        public MedleyInfo()
        {
            Enable = false;

            FadeDuration= 5.0f;

            setMedleyStartPosition(EnumStartPosition.DEFAULT);
            setMedleyPlaytime(EnumPlayTime.DEFAULT);

            EndPoint = int.MaxValue;
        }

        public void initMedley(long id, int length)
        {
            MedleyId = id;
            Random random = new Random();

            double startRatio = random.NextDouble() * (PlayPositionRatioMax - PlayPositionRatioMin) + PlayPositionRatioMin;
            double playRatio = random.NextDouble() * (PlaytimeRatioMax - PlaytimeRatioMin) + PlaytimeRatioMin;

            int playtime = (int)(length * (playRatio/100));
            int startPosition = (int) (length* (startRatio/100));

            StartPoint = startPosition;
            EndPoint = startPosition + playtime;

        }

        public void setMedleyStartPosition(EnumStartPosition enumStartPosition)
        {
            switch (enumStartPosition)
            {
                    case EnumStartPosition.DEFAULT:
                        PlayPositionRatioMin = 10.0;
                        PlayPositionRatioMax = 50.0;
                        break;
                    case EnumStartPosition.OPENING:
                        PlayPositionRatioMin = 10.0;
                        PlayPositionRatioMax = 30.0;
                        break;
                    case EnumStartPosition.MIDDLE:
                        PlayPositionRatioMin = 40.0;
                        PlayPositionRatioMax = 60.0;
                        break;
                    case EnumStartPosition.ENDING:
                        PlayPositionRatioMin = 60.0;
                        PlayPositionRatioMax = 80.0;
                        break;
            }
            StartPosition = enumStartPosition;

        }

        public void setMedleyPlaytime(EnumPlayTime enumPlayTime)
        {

            switch (enumPlayTime)
            {
                case EnumPlayTime.DEFAULT:
                    PlaytimeRatioMin = 15.0;
                    PlaytimeRatioMax = 30.0;
                    break;
                case EnumPlayTime.SHORT:
                    PlaytimeRatioMin = 5.0;
                    PlaytimeRatioMax = 10.0;
                    break;
                case EnumPlayTime.LONG:
                    PlaytimeRatioMin = 30.0;
                    PlaytimeRatioMax = 50.0;
                    break;
                case EnumPlayTime.HALF:
                    PlaytimeRatioMin = 50.0;
                    PlaytimeRatioMax = 70.0;
                    break;
            }
            PlayTime = enumPlayTime;
        }

    }
}
