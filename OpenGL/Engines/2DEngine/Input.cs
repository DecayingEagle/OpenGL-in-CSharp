using System.Numerics;
using GLFW;
using OpenGL.Rendering.Display;

namespace OpenGL.Engines;

public static class Input
{
    public static bool KeyPressed(Keys key)
    {
        return Glfw.GetKey(DisplayManager.Window, key) == InputState.Press;
    }

    public static Vector2 MousePos()
    {
        Vector2 mousePos = new Vector2();
        //Glfw.GetCursorPosition(DisplayManager.Window, out mousePos.X, out mousePos.Y);
        return Vector2.One;
    }

    public static bool MousePressed(MouseButton button)
    {
        return Glfw.GetMouseButton(DisplayManager.Window, button) == InputState.Press;
    }
}