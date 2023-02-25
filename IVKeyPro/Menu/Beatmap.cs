using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace IVKeyPro
{
    public class Beatmap
    {
        public string folderName;
        public string AudioFilename;
        public string Title;
        public string Creator; //mapper
        public string Source; //artist
        public int BeatmapID;
        public int BeatmapSetID;
        public bool[] HasDifficulty = new bool[6];
    }
    public partial class Window
    {

        List<Beatmap> loaded_beatmaps = new List<Beatmap>();
        int selectedBeatmapAt = 0;
        public void GetBeatmaps()
        {
            loaded_beatmaps.Clear();
            string Set(string b, string line)
            {
                if (line.Length > b.Length + 1 && line.Substring(0, b.Length + 1).Equals(b + ":"))
                {
                    Trace.WriteLine(line.Substring(b.Length + 1));
                    return line.Substring(b.Length + 1);
                    
                }
                return null;
            }
            string mode = "none";
            string[] path = Directory.GetDirectories(Environment.CurrentDirectory + @"\beatmap");
            for(int i = 0; i < path.Length; i++)
            {
                if(File.Exists(path[i] + @"\info.4k")){
                    Beatmap _beatmap = new Beatmap();
                    StreamReader reader = new StreamReader(path[i] + @"\info.4k");
                    _beatmap.folderName = path[i];
                    string line = reader.ReadLine();
                    if (File.Exists(path[i] + @"\easy.4k"))
                    {
                        _beatmap.HasDifficulty[0] = true;
                    }
                    if (File.Exists(path[i] + @"\normal.4k"))
                    {
                        _beatmap.HasDifficulty[1] = true;
                    }
                    if (File.Exists(path[i] + @"\hard.4k"))
                    {
                        _beatmap.HasDifficulty[2] = true;
                    }
                    if (File.Exists(path[i] + @"\expert.4k"))
                    {
                        _beatmap.HasDifficulty[3] = true;
                    }
                    if (File.Exists(path[i] + @"\insane.4k"))
                    {
                        _beatmap.HasDifficulty[4] = true;
                    }
                    if (File.Exists(path[i] + @"\master.4k"))
                    {
                        _beatmap.HasDifficulty[5] = true;
                    }
                    while (line != null) 
                    {
                        //Trace.WriteLine(path[i] + @"\info.b");
                        if (mode == "Meta")
                        {
                            //Set(_beatmap.AudioFilename, "AudioFilename", line);
                            if (Set("Title", line) != null) {
                                _beatmap.Title = Set("Title", line);
                            }
                            if (Set("Creator", line) != null)
                            {
                                _beatmap.Creator = Set("Creator", line);
                            }
                            if (Set("Source", line) != null)
                            {
                                _beatmap.Source = Set("Source", line);
                            }
                        }

                        if (line.Equals("[Metadata]")) mode = "Meta";
                        line = reader.ReadLine();
                    }
                    loaded_beatmaps.Add(_beatmap);
                }
            }
        }
    }

}
