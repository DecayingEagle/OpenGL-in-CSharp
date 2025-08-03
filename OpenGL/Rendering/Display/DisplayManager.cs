using System.Drawing;
using System.Numerics;
using GLFW;
using static OpenGL.GL;
using Exception = System.Exception;
using Monitor = GLFW.Monitor;

namespace OpenGL.Rendering.Display
{
    static class DisplayManager
    {
        public static Window Window { get; set; }
        public static Vector2 WindowSize { get; set; }
        public static void CreateWindow(int width, int height, string title)
        {
            // setting the window configs
            WindowSize = new Vector2(width, height);

            Glfw.Init();
            
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            
            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, true);

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

            if (Window == Window.None)
            {
                throw new Exception("Window has not been created");
            }

            Rectangle screen = Glfw.PrimaryMonitor.WorkArea;
            int x = (screen.Width - width) / 2;
            int y = (screen.Height - height) / 2;

            if (Environment.GetEnvironmentVariable("WAYLAND_DISPLAY") == null) {
              Glfw.SetWindowPosition(Window, x, y);
            }

            Glfw.MakeContextCurrent(Window);
            Import(Glfw.GetProcAddress);
            
            glViewport(0, 0, width, height);
            Glfw.SwapInterval(0); // vsync 0 = off, 1 = on
        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
        }
    }
}

