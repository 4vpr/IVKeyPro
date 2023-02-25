using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Data;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using WMPLib;

namespace IVKeyPro
{
    public partial class InGame
    {
        
        
        //keys
        public int[] keyEffect = { 0, 0, 0, 0 };
        public int[] judgementDisplay = { 0, 0 };

        public double maxscore = 0;
        //pause
        public bool isPause = false;
        public int combo = 0;
        public double hscore;

        int delay = 0; public double songms = 0;
        int noteCount = 0; public double ms = 0; double starttime;
        public double lastms = 0;
        //int[,] note = new int[50000,3];

        KeyChecker _keyChecker = new KeyChecker();

        public Beatmap beatmap_nowplaying = new Beatmap();

        Note nullNote = new Note();
        // true while playing game


        int chunkCount = 0; int chunkLength = 1000;
        //about chunk

        int[] longNoteTiming = { 0, 0, 0, 0 };
        public bool[] isLongNote = { false, false, false, false };
        // the length of 'in judgment' note

        List<Note> note = new List<Note>();
        public List<Note> spawnedNote = new List<Note>();
        public List<Note> failedLongNote = new List<Note>();
        public List<Note> failedLongNote_remove = new List<Note>();

        double[] acc = { 300, 300, 300, 300 }; double[] acc_long = { 300, 300, 300, 300 }; Note[] nextnote = new Note[4];

        int sync_lastms = 0;

        //StreamReader thing = new StreamReader(beatmap_lo + beatmap_play + @"\map.b");




        public void PlayBeatMap(Beatmap b,int d)
        {
            beatmap_nowplaying = b;
            _keyChecker.keyState_current = Keyboard.GetState();
            SetNotes(b,d);
            gameResult = new Result(beatmap_nowplaying.BeatmapID);
            Setting.state = State.Playing;
            NewGame(getns());
        }
        public void NewGame(double now)
        {
            songPlayer.controls.stop();
            isPause = false;
            starttime = getns() + 1000 * 1000000;
            songPlayer.controls.currentPosition = -1;
            ms = ((getns() - starttime) / 1000000);
        }

        public void Update()
        {
            songPlayer.settings.volume = Setting.voulume;
            _keyChecker.keyState_current = Keyboard.GetState();
            ms = ((getns() - starttime) / 1000000) - pauseTime;
            if(sync_lastms < ms)
            {
                Trace.WriteLine(((songPlayer.controls.currentPosition * 1000)) - ms);
                //songPlayer.controls.currentPosition = ms / 1000;
                sync_lastms += 1000;
            }
            if (lastms < 0 && ms >= 0)
            {
                songPlayer.controls.currentPosition = ms / 1000;
                songPlayer.controls.play();
                Trace.WriteLine(songPlayer.controls.currentPosition * 1000);
            }

            if (_keyChecker.keyPress(Keys.Escape))
            {
                Pause(!isPause, ms);
            }
            if (isPause)
                UpdatePause();
            if (!isPause)
            {
                songms = (songPlayer.controls.currentPosition * 1000) - delay;
                if (ms < 0)
                {
                    songms = ms;
                }
                if(finishTiming + 2000 < songms)
                {
                    Setting.state = State.Result;
                }
                UpdateChunk();
                Animate();
                UpdateAcc();
                if (gameResult.score >= maxscore)
                {
                    gameResult.acc = Math.Truncate(gameResult.score / maxscore * 100) + (Math.Truncate(gameResult.subscore / subMaxscore * 100) / 100);
                }
                else
                {
                    gameResult.acc = Math.Truncate(gameResult.score / maxscore * 10000) / 100;
                }
                if (gameResult.score == 0)
                {
                    gameResult.acc = 101;
                }
                

                DoLongNote();
            }

            _keyChecker.keyState_prev = Keyboard.GetState();
            lastms = ms;
        }

    }
}
