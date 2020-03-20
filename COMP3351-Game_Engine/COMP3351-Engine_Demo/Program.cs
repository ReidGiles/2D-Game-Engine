using System;
using COMP3351_Game_Engine;

namespace COMP3351_Engine_Demo
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Demo game = new Demo();
            game.Run();
        }
    }
#endif
}
