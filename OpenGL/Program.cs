using OpenGL.GameLoop;
using StbiSharp;

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

