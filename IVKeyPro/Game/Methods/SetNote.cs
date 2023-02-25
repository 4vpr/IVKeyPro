using System;
using System.Diagnostics;
using System.IO;
using WMPLib;

namespace IVKeyPro
{
    public partial class InGame
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        int[] prevTiming = new int[4] {0, 0, 0, 0 };
        WMPLib.WindowsMediaPlayer songPlayer = new WMPLib.WindowsMediaPlayer();
        string difficulty;
        public int finishTiming = 0;

        public void SetNotes(Beatmap b, int d)
        {
            
            switch (d)
            {
                case 0:
                    difficulty = @"\easy.4k";
                    break;
                case 1:
                    difficulty = @"\normal.4k";
                    break;
                case 2:
                    difficulty = @"\hard.4k";
                    break;
                case 3:
                    difficulty = @"\expert.4k";
                    break;
                case 4:
                    difficulty = @"\insane.4k";
                    break;
                case 5:
                    difficulty = @"\master.4k";
                    break;
            }
            string? line, mode = "none";
            int point1; int point2; int point3; int point4; int point5; int point6;
            int note_length; int note_timing; int note_line;
            try
            {
                StreamReader reader = new StreamReader(b.folderName + difficulty);
                line = reader.ReadLine();
                while (line != null)
                {
                    if (mode == "osu" && line.Length > 3)
                    {
                        try
                        {
                            point1 = line.IndexOf(',');
                            point2 = line.IndexOf(',', point1 + 1);
                            point3 = line.IndexOf(',', point2 + 1);
                            point4 = line.IndexOf(',', point3 + 1);
                            point5 = line.IndexOf(',', point4 + 1);
                            point6 = line.IndexOf(':');
                            note_line = Convert.ToInt32(line.Substring(0, point1));
                            note_timing = Convert.ToInt32(line.Substring(point2 + 1, point3 - point2 - 1));
                            if (point6 != -1)
                            {
                                note_length = Convert.ToInt32(line.Substring(point5 + 1, point6 - point5 - 1));
                            }
                            else
                            {
                                note_length = Convert.ToInt32(line.Substring(point5 + 1));
                            }
                            if (note_length != 0) { note_length -= note_timing; }


                            switch (note_line)
                            {
                                case 64:
                                    note_line = 0;
                                    break;
                                case 192:
                                    note_line = 1;
                                    break;
                                case 320:
                                    note_line = 2;
                                    break;
                                case 448:
                                    note_line = 3;
                                    break;
                            }
                            if (note_timing > prevTiming[note_line] && note_length >= 0)
                            {
                                note.Add(CreateNote(note_timing, note_length, note_line));
                                noteCount++;
                                prevTiming[note_line] = note_timing + note_length;
                            }
                            
                            
                        }
                        catch
                        {
                            Trace.WriteLine("not Found");
                        }
                    for(int i = 0; i < 4; i++)
                        {
                            if (prevTiming[i] > finishTiming) {
                                finishTiming = prevTiming[i];
                            }
                        }
                    }
                    if (mode == "note" && line.Length > 3)
                    {
                        try
                        {
                            point1 = line.IndexOf(',');
                            point2 = line.IndexOf(',', point1 + 1);
                            note_timing = Convert.ToInt32(line.Substring(0, point1));
                            note_length = Convert.ToInt32(line.Substring(point1 + 1, (point2 - point1) - 1));
                            note_line = Convert.ToInt32(line.Substring(point2 + 1));
                            if (note_timing < 50 && note_timing > 0)
                            {
                                note_timing = 50;
                                Console.WriteLine("long note length must be > 50ms or 0ms");
                            }
                            prevTiming[note_line] = note_timing + note_length;
                            note.Add(CreateNote(note_timing, note_length, note_line));
                            noteCount++;
                        }
                        catch
                        {
                            Console.WriteLine("Note[" + noteCount + "] doesn't fit the standard");
                        }
                    }
                    if (line.Equals("[HitObjects]")) mode = "osu";
                    line = reader.ReadLine();
                }
                if (File.Exists(b.folderName + @"\song.wav"))
                {
                    Trace.WriteLine("set");
                    songPlayer.URL = b.folderName + @"\song.wav";
                }
                else if(File.Exists(b.folderName + @"\song.mp3"))
                {
                    Trace.WriteLine("set");
                    songPlayer.URL = b.folderName + @"\song.mp3";

                }
            }
            catch
            {
                Trace.WriteLine(Environment.CurrentDirectory);
            }
        }
    }
}
