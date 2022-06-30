using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using GLFW;
using OpenGL.GameLoop;
using OpenGL.Rendering.Cameras;
using OpenGL.Rendering.Display;
using OpenGL.Rendering.Shaders;
using OpenGL.Rendering.Texture;
using static OpenGL.GL;

namespace OpenGL
{
    class TestGame : Game
    {
        uint _vbo;
        uint _vao;

        Shader _shader;
        Texture _tex;

        private OrthoCamera2D _cam;
        
#pragma warning disable CS8618
        public TestGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
#pragma warning restore CS8618
        {
            
        }
        protected override void Init()
        {
            
        }

        protected override unsafe void LoadContent()
        {
            
            _shader = new Shader("../../../Rendering/Shaders/vertex.glsl", "../../../Rendering/Shaders/fragment.glsl");
            _shader.Load();
            
            _tex = new Texture("sprites/block.png");
            _tex.Load();
            

            _vao = glGenVertexArray();
            _vbo = glGenBuffer();

            glBindVertexArray(_vao);
            glBindBuffer(GL_ARRAY_BUFFER, _vbo);
            
            float[] vertices =
            {
                -0.5f, 0.5f, 1f, 0f, 0f, 0f, 1f, // top left
                0.5f, 0.5f, 0f, 1f, 0f, 1f, 1f,// top right
                -0.5f, -0.5f, 0f, 0f, 1f, 0f, 0f, // bottom left

                0.5f, 0.5f, 0f, 1f, 0f, 1f, 1f,// top right
                0.5f, -0.5f, 0f, 1f, 1f, 1f, 0f,// bottom right
                -0.5f, -0.5f, 0f, 0f, 1f, 0f, 0f// bottom left
            };
            fixed (float* v = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
            }
            
            glVertexAttribPointer(0, 2, GL_FLOAT, false, 7 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);
            
            glVertexAttribPointer(1, 3, GL_FLOAT, false, 7 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);
            
            glVertexAttribPointer(2, 2, GL_FLOAT, false, 7 * sizeof(float), (void*)(5 * sizeof(float)));
            glEnableVertexAttribArray(2);
            
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            _cam = new OrthoCamera2D(DisplayManager.WindowSize / 2f, 2.5f);
        }

        protected override void Update()
        {
            
        }

        protected override void Render()
        {
            glClearColor(0, 0, 0, 0);
            glClear(GL_COLOR_BUFFER_BIT);

            Vector2 position = new Vector2(300, 300);
            Vector2 scale = new Vector2(100, 150);
            float rotation = MathF.Sin(GameTime.TotalElapsedSec) * MathF.PI * 2f;

            Matrix4x4 trans = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);
            
            _shader.SetMatrix4X4("model", sca * rot * trans);
            
            _shader.Use();
            _shader.SetMatrix4X4("projection", _cam.GetProjectionMatrix());
            
            
            glBindTexture(GL_TEXTURE_2D, _tex.TextureCopy);
            glBindVertexArray(_vao);
            // This is a nice debug feature for later
            // Draws wireframe of all vertexes
            // glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);
            
            Glfw.SwapBuffers(DisplayManager.Window);
        }
    }
}

