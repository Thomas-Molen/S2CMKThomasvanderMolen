using System;

namespace PlatformerSpeedRunner
{
    public static class Program
    {
        public const int width = 1920;
        public const int height = 1080;

        static void Main()
        {
            using (var game = new MainGame(width, height))
                game.Run();
        }
    }
}
