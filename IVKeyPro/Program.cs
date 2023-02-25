using System;

namespace IVKeyPro
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Setting.Reset();
            using (var game = new Window())
                game.Run();
        }
    }
}
