using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenGL.Rendering.Display;
using StbiSharp;
using static OpenGL.GL;
namespace OpenGL;

public class Engine2D
{

    public uint LoadTexture2D(string filepath)
    {
        uint texture = 0;
        return texture;
    }
    
    public void DrawRectangle(int x, int y, int r, int g, int b)
    {
        
    }

    public void DrawRectangle(int x, int y, uint texture, string texWrapping)
    {
        
    }

    public void DrawCircle()
    {
        
    }

    public static void ClearScreen(int r, int g, int b, int a)
    {
        glClearColor(r, g, b, a);
        glClear(GL_COLOR_BUFFER_BIT);
    }

    public void CreateWindow(int w, int h, string title)
    {
        DisplayManager.CreateWindow(w, h, title);
    }
}