using System;

namespace ADHDGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ADHDGame game = new ADHDGame())
            {
                game.Run();
            }
        }
    }
#endif
}

