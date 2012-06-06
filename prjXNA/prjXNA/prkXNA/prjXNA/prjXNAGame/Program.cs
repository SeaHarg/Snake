using System;

namespace prjXNAGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Snake game = new Snake())
            {
                game.Run();
            }
        }
    }
}

