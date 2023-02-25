using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace IVKeyPro
{
    public partial class InGame
    {
        double pauseTime = 0;
        double pauseStart;
        public void UpdatePause()
        {
            if (isPause)
            {
                if(_keyChecker.keyPress(Keys.F2))
                {
                    Setting.noteVelocity += 0.1f ;
                    if (Setting.noteVelocity > 3.5f)
                    {
                        Setting.noteVelocity = 3.5f;
                    }
                }
                if (_keyChecker.keyPress(Keys.F1))
                {
                    Setting.noteVelocity -= 0.1f;
                    if (Setting.noteVelocity < 1)
                    {
                        Setting.noteVelocity = 1;
                    }
                }
                if (_keyChecker.keyPress(Keys.F4))
                {
                    Setting.noteWidth += 5;
                    if (Setting.noteWidth > 200)
                    {
                        Setting.noteWidth = 200;
                    }
                }
                if (_keyChecker.keyPress(Keys.F3))
                {
                    Setting.noteWidth -= 5;
                    if (Setting.noteWidth < 50)
                    {
                        Setting.noteWidth = 50;
                    }
                }
                if (_keyChecker.keyPress(Keys.F6))
                {
                    Setting.noteHeight += 5;
                    if (Setting.noteHeight > 100)
                    {
                        Setting.noteHeight = 100;
                    }
                }
                if (_keyChecker.keyPress(Keys.F5))
                {
                    Setting.noteHeight -= 5;
                    if (Setting.noteHeight < 20)
                    {
                        Setting.noteHeight = 20;
                    }
                }
                if (_keyChecker.keyPress(Keys.Back))
                {
                    Setting.state = State.SongSelct;
                }
            }
        }
        public void Pause(bool b,double t)
        {
            if (b)
            {
                isPause = true;
                pauseStart = t;
                songPlayer.controls.pause();
              }
            else
            {
                pauseTime += t - pauseStart;
                isPause = false;
                ms = ((getns() - starttime) / 1000000) - pauseTime;
                songPlayer.controls.currentPosition = ms / 1000;
                songPlayer.controls.play();
                lastms = ms;
            }
        }
    }
}
