﻿using OpenGL.GameLoop;

namespace OpenGL
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new TestGame(600, 600, "test");
            game.Run();
        }
    }
}

