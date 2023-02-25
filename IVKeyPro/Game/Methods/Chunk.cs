using System;
using System.Collections.Generic;
using System.Text;

namespace IVKeyPro
{
    public partial class InGame
    {
        public void UpdateChunk()
        {
            if (songms + chunkLength > chunkLength * chunkCount)
            {
                chunkCount++;
                foreach (var item in note)
                {
                    if (item.timing < chunkCount * chunkLength)
                    {
                        spawnedNote.Add(item);
                    }
                }
                note.RemoveAll(spawnedNote.Contains);
                for (int i = 0; i < 4; i++)
                {
                    UpdateLine(i);
                }
            }
        }
        public void CheckLongNote(int i)
        {
            if (nextnote[i].length > 0 && !isLongNote[i])
            {
                isLongNote[i] = true;
            }
            else
            {
                UpdateLine(nextnote[i]);
                isLongNote[i] = false;
            }
        }
    }
}
