using GLFW;
using OpenGL.Rendering.Display;

namespace OpenGL.GameLoop
{
    abstract class Game
    {
        

        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string InitialWindowTitle { get; set; }

        protected Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
                {
                    this.InitialWindowWidth = initialWindowWidth;
                    this.InitialWindowHeight = initialWindowHeight;
                    this.InitialWindowTitle = initialWindowTitle;
                }
        
        public void Run()
        {
            Init();
            
            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);
            
            LoadContent();

            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedSec;
                GameTime.TotalElapsedSec = (float)Glfw.Time;

                DebugUpdate();
                Update();

                Glfw.PollEvents();
                 
                Render();
            }
            DisplayManager.CloseWindow();
        }

        protected abstract void Init();
        protected abstract void LoadContent();

        protected abstract void Update();
        protected abstract void DebugUpdate();
        protected abstract void Render();
    }
}
