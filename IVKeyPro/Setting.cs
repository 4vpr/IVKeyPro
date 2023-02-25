using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace IVKeyPro
{
    public static class Setting
    {
        public static State state = State.SongSelct;
        // main , ingame , setting
        public static int delay = 0;
        public static bool fullscreen = false;
        public static int resolution_width = 1280;
        public static int resolution_height = 720;
        public static float noteVelocity = 2.4f;
        public static int noteWidth = 120;
        public static int noteHeight = 40;
        public static bool keyEffect = false;
        public static bool comboDisplay = true;
        public static Keys[] keySetting = new Keys[4];
        public static bool[] judgementDisplay = new bool [5];
        public static int voulume = 10;
        public static void Reset()
        {
            judgementDisplay[0] = true;
            judgementDisplay[1] = false;
            judgementDisplay[2] = false;
            judgementDisplay[3] = true;
            judgementDisplay[4] = true;
            keySetting[0] = Keys.Z;
            keySetting[1] = Keys.X;
            keySetting[2] = Keys.OemPeriod;
            keySetting[3] = Keys.OemQuestion;
        }
        public static void Save()
        {
        }
        public static void Load()
        {

            string SetString(string b, string line)
            {
                if (line.Length > b.Length + 1 && line.Substring(0, b.Length + 1).Equals(b + ":"))
                {
                    if (line.Substring(b.Length + 1) != null)
                    {
                        return line.Substring(b.Length + 1);
                    }
                    else
                    {
                        Trace.WriteLine( b + "missing");
                        return "[ missing ]";
                    }
                }
                return null;
            }
            int SetInt(string b, string line, int d, int origin)
            {
                if (line.Length > b.Length + 1 && line.Substring(0, b.Length + 1).Equals(b + ":"))
                {
                    try
                    {
                        return Convert.ToInt32(line.Substring(b.Length + 1));
                    }
                    catch
                    {
                        Trace.WriteLine(b + "missing");
                        return d;
                    }
                }
                return origin;
            }
            bool SetBool(string b, string line, bool d, bool origin)
            {
                if (line.Length > b.Length + 1 && line.Substring(0, b.Length + 1).Equals(b + ":"))
                {
                    try
                    {
                        return Convert.ToBoolean(line.Substring(b.Length + 1));
                    }
                    catch
                    {
                        return d;
                    }
                }
                return origin;
            }
            float SetFloat(string b, string line, float d, float origin)
            {
                Trace.WriteLine("<<<>>>");
                Trace.WriteLine(line.Length > b.Length + 1);
                if (line.Length > b.Length + 1 && line.Substring(0, b.Length + 1).Equals(b + ":"))
                {
                    try
                    {
                        return float.Parse(line.Substring(b.Length + 1));
                    }
                    catch
                    {
                        Trace.WriteLine(b + "missing");
                        return d;
                    }
                }
                return origin;
            }

            if (File.Exists(Environment.CurrentDirectory + @"\config.ini"))
            {
                StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\config.ini");
                string line = reader.ReadLine();
                while (line != null)
                {
                    Trace.WriteLine(line);
                    
                    delay = SetInt("SoundDealy", line, 0, delay);
                    fullscreen = SetBool("FullScreen", line, false, fullscreen);
                    resolution_width = SetInt("WindowWidth", line, 1280, resolution_width);
                    resolution_height = SetInt("WindowHeight", line, 720, resolution_height);
                    
                    keyEffect = SetBool("EnableKeyPressEffect", line, true, keyEffect);

                    noteHeight = SetInt("NoteHeight", line, 40, noteHeight);
                    noteWidth = SetInt("NoteWidth", line, 120, noteWidth);
                    noteVelocity = SetFloat("NoteVelocity", line, 2f, noteVelocity);

                    keySetting[0] = SetKey("Key1", line, Keys.D, keySetting[0]);
                    keySetting[1] = SetKey("Key2", line, Keys.F, keySetting[1]);
                    keySetting[2] = SetKey("Key3", line, Keys.J, keySetting[2]);
                    keySetting[3] = SetKey("Key4", line, Keys.K, keySetting[3]);

                    comboDisplay = SetBool("DisplayCombo", line, true, comboDisplay);
                    judgementDisplay[0] = SetBool("DisplayMiss", line, true, judgementDisplay[0]);
                    judgementDisplay[1] = SetBool("DisplayProPlus", line, true, judgementDisplay[1]);
                    judgementDisplay[2] = SetBool("DisplayPro", line, true, judgementDisplay[2]);
                    judgementDisplay[3] = SetBool("DisplayNoob", line, true, judgementDisplay[3]);
                    judgementDisplay[4] = SetBool("DisplayBad", line, true, judgementDisplay[4]);

                    line = reader.ReadLine();
                }
            }


            static Keys SetKey(string b,string line,Keys d,Keys Origin){
                if (line.Length > b.Length + 1 && line.Substring(0, b.Length + 1).Equals(b + ":"))
                {
                    if (line.Substring(b.Length + 1) != null)
                    {
                        string k = line.Substring(b.Length + 1);
                        if (k == "Z") return Keys.Z;
                        if (k == "X") return Keys.X;
                        if (k == "C") return Keys.C;
                        if (k == "V") return Keys.V;
                        if (k == "B") return Keys.B;
                        if (k == "N") return Keys.N;
                        if (k == "M") return Keys.M;

                        if (k == "A") return Keys.A;
                        if (k == "S") return Keys.S;
                        if (k == "D") return Keys.D;
                        if (k == "F") return Keys.F;
                        if (k == "G") return Keys.G;
                        if (k == "H") return Keys.H;
                        if (k == "J") return Keys.J;
                        if (k == "K") return Keys.K;
                        if (k == "L") return Keys.L;

                        if (k == "Q") return Keys.Q;
                        if (k == "W") return Keys.W;
                        if (k == "E") return Keys.E;
                        if (k == "R") return Keys.R;
                        if (k == "T") return Keys.T;
                        if (k == "Y") return Keys.Y;
                        if (k == "U") return Keys.U;
                        if (k == "I") return Keys.I;
                        if (k == "O") return Keys.O;
                        if (k == "P") return Keys.P;

                        if (k == "NumPad0") return Keys.NumPad0;
                        if (k == "NumPad1") return Keys.NumPad1;
                        if (k == "NumPad2") return Keys.NumPad2;
                        if (k == "NumPad3") return Keys.NumPad3;
                        if (k == "NumPad4") return Keys.NumPad4;
                        if (k == "NumPad5") return Keys.NumPad5;
                        if (k == "NumPad6") return Keys.NumPad6;
                        if (k == "NumPad7") return Keys.NumPad7;
                        if (k == "NumPad8") return Keys.NumPad8;
                        if (k == "NumPad9") return Keys.NumPad9;

                        if (k == "D0" || k == "0") return Keys.D0;
                        if (k == "D1" || k == "1") return Keys.D1;
                        if (k == "D2" || k == "2") return Keys.D2;
                        if (k == "D3" || k == "3") return Keys.D3;
                        if (k == "D4" || k == "4") return Keys.D4;
                        if (k == "D5" || k == "5") return Keys.D5;
                        if (k == "D6" || k == "6") return Keys.D6;
                        if (k == "D7" || k == "7") return Keys.D7;
                        if (k == "D8" || k == "8") return Keys.D8;
                        if (k == "D9" || k == "9") return Keys.D9;

                        if (k == "NumPad1") return Keys.NumPad1;
                        if (k == "NumPad2") return Keys.NumPad2;
                        if (k == "NumPad3") return Keys.NumPad3;
                        if (k == "NumPad4") return Keys.NumPad4;
                        if (k == "NumPad5") return Keys.NumPad5;
                        if (k == "NumPad6") return Keys.NumPad6;
                        if (k == "NumPad7") return Keys.NumPad7;
                        if (k == "NumPad8") return Keys.NumPad8;
                        if (k == "NumPad9") return Keys.NumPad9;

                        if (k == "OemQuotes") return Keys.OemQuotes;
                        if (k == "OemPlus") return Keys.OemPlus;
                        if (k == "OemMinus") return Keys.OemMinus;
                        if (k == "OemQuestion") return Keys.OemQuestion;
                        if (k == "OemComma") return Keys.OemComma;
                        if (k == "OemPeriod") return Keys.OemPeriod;
                        if (k == "OemSemicolon") return Keys.OemSemicolon;
                        if (k == "OemOpenBrackets") return Keys.OemOpenBrackets;


                    }
                    else
                    {
                        return d;
                    }
                }
                    return Origin;
            }
        }

    }
}
