using System;
using System.Diagnostics;

namespace IVKeyPro
{
    public partial class InGame
    {
        public int comboEffect;
        public long getns()
        {
            double timestamp = Stopwatch.GetTimestamp();
            double nanoseconds = 1_000_000_000.0 * timestamp / Stopwatch.Frequency;
            return (long)nanoseconds;
        }
        public void Animate()
        {
            int deltatime = Convert.ToInt32(Math.Round(ms - lastms));
            //Trace.WriteLine(deltatime);
            for (int i = 0; i < 4; i++) {
                if (keyEffect[i] > 0)
                {
                    //Trace.WriteLine(keyEffect[i]);
                    keyEffect[i] -= deltatime;
                    if (keyEffect[i] < 0)
                    {
                        keyEffect[i] = 0;
                    }
                }
            }
            if(judgementDisplay[1] > 0)
            {
                judgementDisplay[1] -= deltatime;
            }
            if (comboEffect > 0)
            {
                comboEffect -= deltatime;
            }
        }
    }
}
