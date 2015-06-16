using System;

namespace DrunkenSoftUniWarrior
{
#if WINDOWS || XBOX
    static class GameMain
    {
        static void Main(string[] args)
        {
            using (DrunkenSoftUniWarrior game = new DrunkenSoftUniWarrior())
            {
                game.Run();
            }
        }
    }
#endif
}

