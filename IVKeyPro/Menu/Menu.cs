using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IVKeyPro
{
    public partial class Window
    {
        KeyChecker _keyChecker = new KeyChecker();
        int selectedDifficulty = 0;
        
        public void UpdateDIff()
        {
            if (!loaded_beatmaps[selectedBeatmapAt].HasDifficulty[selectedDifficulty])
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (loaded_beatmaps[selectedBeatmapAt].HasDifficulty[i])
                    {
                        selectedDifficulty = i;
                        Trace.WriteLine("D");
                        break;
                    }
                }
            }
        }
        public void ResultSceenUpdate(Result r)
        {
            _keyChecker.keyState_current = Keyboard.GetState();
            if(_keyChecker.keyPress(Keys.Enter) || _keyChecker.keyPress(Keys.Escape))
            {
                Setting.state = State.SongSelct;
            }

            _keyChecker.keyState_prev = Keyboard.GetState();
        }
        public void SongSelcetUpdate()
        {
            _keyChecker.keyState_current = Keyboard.GetState();
            if (_keyChecker.keyPress(Keys.F5))
            {
                Setting.Load();
                GetBeatmaps();
            }
            if (_keyChecker.keyPress(Keys.Left))
            {
                for (int i = selectedDifficulty - 1; i >= 0; i--)
                {
                    if (selectedBeatmap.HasDifficulty[i])
                    {

                        selectedDifficulty = i;
                        break;
                    }
                }
            }
            if (_keyChecker.keyPress(Keys.Right))
            {
                for (int i = selectedDifficulty + 1; i <= 5; i++)
                {
                    if (selectedBeatmap.HasDifficulty[i])
                    {
                        selectedDifficulty = i;
                        break;
                    }
                }
            }
            if (_keyChecker.keyPress(Keys.Down))
            {
                if(selectedBeatmapAt < loaded_beatmaps.Count - 1)
                {
                    selectedBeatmapAt +=1;
                    UpdateDIff();
                }
            }
            if (_keyChecker.keyPress(Keys.Up))
            {
                if (selectedBeatmapAt > 0)
                {
                    selectedBeatmapAt -=1;
                    UpdateDIff();
                }
            }
            if (_keyChecker.keyPress(Keys.Enter))
            {
                _inGame = new InGame();
                _inGame.PlayBeatMap(selectedBeatmap, selectedDifficulty);
            }
            //Trace.WriteLine(selectedBeatmapAt);
            selectedBeatmap = loaded_beatmaps.ElementAt(selectedBeatmapAt);
            _keyChecker.keyState_prev = Keyboard.GetState();

        }
    }
}
