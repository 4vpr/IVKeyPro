using System;
using System.Collections.Generic;
using System.Text;

namespace IVKeyPro
{
    public partial class InGame
    {
        public Note CreateNote(int a, int b, int c)
        {
            return new Note
            {
                timing = a,
                length = b,
                line = c,
            };
        }
    }
    public class Note
    {
        public int timing;
        public int line;
        public int length;
    }
}
