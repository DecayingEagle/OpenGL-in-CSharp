using System.Numerics;
using System.Text;
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
        uint vbo;
        uint vao;

        Shader shader;
        Texture tex;

        private Camera2D cam;
        
        public TestGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {
            
        }
        protected override void Init()
        {
            
        }

        protected unsafe override void LoadContent()
        {
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    layout (location = 2) in vec2 aTexCoord;

                                    out vec4 vertexColor;
                                    out vec2 TexCoord;

                                    uniform mat4 projection;
                                    uniform mat4 model;
    
                                    void main() 
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                        TexCoord = aTexCoord;
                                    }";

            string fragmentShader = @"#version 330 core
                                    out vec4 FragColor;

                                    in vec4 vertexColor;
                                    in vec2 TexCoord;

                                    uniform sampler2D ourTexture;

                                    

                                    void main() 
                                    {
                                        FragColor = texture(ourTexture, TexCoord);
                                    }";

            shader = new Shader(vertexShader, fragmentShader);
            shader.Load();
            
            tex = new Texture("sprites/block.png");
            tex.Load();
            

            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);
            
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

            cam = new Camera2D(DisplayManager.WindowSize / 2f, 2.5f);
        }

        protected override void Update()
        {
            
        }

        protected override void Render()
        {
            glClearColor(0, 0, 0, 0);
            glClear(GL_COLOR_BUFFER_BIT);

            Vector2 position = new Vector2(300, 300);
            Vector2 scale = new Vector2(16, 16);
            float rotation = MathF.Sin(GameTime.TotalElapsedSec) * MathF.PI * 2f;

            Matrix4x4 trans = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);
            
            shader.SetMatrix4x4("model", sca * rot * trans);
            
            shader.Use();
            shader.SetMatrix4x4("projection", cam.GetProjectionMatrix());
            
            
            glBindTexture(GL_TEXTURE_2D, tex.texture_copy);
            glBindVertexArray(vao);
            // This is a nice debug feature for later
            // Draws wireframe of all vertexes
            // glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);
            
            Glfw.SwapBuffers(DisplayManager.Window);
        }
    }
}

