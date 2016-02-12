using System;

namespace Seagull
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Game1 game1 = new Game1(800, 600);
            game1.Run(60.0);
        }
    }
}
