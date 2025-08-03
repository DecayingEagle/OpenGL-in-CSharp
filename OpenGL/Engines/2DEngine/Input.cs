using System.Numerics;
using GLFW;
using OpenGL.Rendering.Display;

namespace OpenGL.Engines;

public static class Input {
  public static bool KeyPressed(Keys key) {
    return Glfw.GetKey(DisplayManager.Window, key) == InputState.Press;
  }

  public static Vector2 MousePos() {
    double x;
    double y;
    Glfw.GetCursorPosition(DisplayManager.Window, out x, out y);
    Vector2 vec = new((float)x, (float)y);
    return vec;
  }

  public static bool MousePressed(MouseButton button) {
    return Glfw.GetMouseButton(DisplayManager.Window, button) == InputState.Press;
  }
}