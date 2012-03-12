using System;

namespace Pinflower
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PinflowerGame game = new PinflowerGame())
            {
                game.Run();
            }
        }
    }
#endif
}

