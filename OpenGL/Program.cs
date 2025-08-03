using OpenGL.GameLoop;

namespace OpenGL;

internal class Program {
  public static void Main(string[] args) {
    Game game = new TestGame(1000, 1000, "test");
    game.Run();
  }
}