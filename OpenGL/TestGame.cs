using OpenGL.Engines;
using OpenGL.GameLoop;

//To be abstracted away
using System.Numerics;
using GLFW;
using OpenGL.Rendering.Cameras;
using OpenGL.Rendering.Display;
using OpenGL.Rendering.Shaders;
using OpenGL.Rendering.Texture;
using static OpenGL.GL;

namespace OpenGL
{
    class TestGame : Game
    {
        //Abstract away in the engine
        public string projectPath = System.IO.Directory.GetCurrentDirectory();
        uint _vbo;
        uint _vao;

        Shader _shader;
        Texture _tex;
        Vector2 pos = new Vector2(300, 300);

        Shader _shader2;
        Texture _tex2;
        Vector2 pos2 = new Vector2(0, 0);

        private Matrix4x4 _mat4;

        private OrthoCamera2D _cam;
        
        //To keep
        public Vector3 CamPos;

#pragma warning disable CS8618
        public TestGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
#pragma warning restore CS8618
        {
            
        }
        protected override void Init()
        {
            CamPos = new Vector3(0f, 0f, 0.1f);   
        }

        

        protected override unsafe void LoadContent()
        {
            /*
            LoadObjList();
            */
            
            _shader = new Shader("/home/lena/RiderProjects/OpenGL-in-CSharp/OpenGL/Rendering/Shaders/vertex.glsl", "/home/lena/RiderProjects/OpenGL-in-CSharp/OpenGL/Rendering/Shaders/fragment.glsl");
            _shader.Load();
            
            _tex = new Texture("/home/lena/RiderProjects/OpenGL-in-CSharp/OpenGL/sprites/block.png");
            _tex.Load();
            

            _vao = glGenVertexArray();
            _vbo = glGenBuffer();

            glBindVertexArray(_vao);
            glBindBuffer(GL_ARRAY_BUFFER, _vbo);
            
            float[] vertices =
            {
              //    aPos   ,    aCol   , texPos
              //  x  ,  y  , r , g , b , tx, ty
                -0.5f, 0.5f, 1f, 0f, 0f, 0f, 1f, // top left
                0.5f, 0.5f, 0f, 1f, 0f, 1f, 1f,// top right
                -0.5f, -0.5f, 0f, 0f, 1f, 0f, 0f, // bottom left

                0.5f, 0.5f, 0f, 1f, 0f, 1f, 1f,// top right
                0.5f, -0.5f, 1f, 1f, 0f, 1f, 0f,// bottom right
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


            _shader2 = new Shader("/home/lena/RiderProjects/OpenGL-in-CSharp/OpenGL/Rendering/Shaders/vertex.glsl", "/home/lena/RiderProjects/OpenGL-in-CSharp/OpenGL/Rendering/Shaders/fragment.glsl");
            _shader2.Load();
            
            _tex2 = new Texture("/home/lena/RiderProjects/OpenGL-in-CSharp/OpenGL/sprites/maro.png");
            _tex2.Load();
            
            float[] vertices2 =
            {
              //    aPos   ,    aCol   , texPos
              //  x  ,  y  , r , g , b , tx, ty
                -0.5f, 0.5f, 1f, 0f, 0f, 0f, 1f, // top left
                0.5f, 0.5f, 0f, 1f, 0f, 1f, 1f,// top right
                -0.5f, -0.5f, 0f, 0f, 1f, 0f, 0f, // bottom left

                0.5f, 0.5f, 0f, 1f, 0f, 1f, 1f,// top right
                0.5f, -0.5f, 1f, 1f, 0f, 1f, 0f,// bottom right
                -0.5f, -0.5f, 0f, 0f, 1f, 0f, 0f// bottom left
            };

            fixed (float* v2 = &vertices2[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices2.Length, v2, GL_STATIC_DRAW);
            }
            
            glVertexAttribPointer(3, 2, GL_FLOAT, false, 7 * sizeof(float), (void*)(7 * sizeof(float)));
            glEnableVertexAttribArray(3);
            
            glVertexAttribPointer(4, 3, GL_FLOAT, false, 7 * sizeof(float), (void*)(10 * sizeof(float)));
            glEnableVertexAttribArray(4);
            
            glVertexAttribPointer(5, 2, GL_FLOAT, false, 7 * sizeof(float), (void*)(12 * sizeof(float)));
            glEnableVertexAttribArray(5);
            
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);



            _cam = new OrthoCamera2D(DisplayManager.WindowSize / 2f, 2.5f);
        }

        protected override void DebugUpdate()
        {
            //Console.WriteLine(Input.MousePressed(MouseButton.Left));
        }

        protected override void Update()
        {
            float camSpeed = 40f;

            Vector2 camInput = Vector2.Zero;
            Vector2 posInput = Vector2.Zero;
            
            if (Input.KeyPressed(Keys.Left))  camInput.X -= 1f;
            if (Input.KeyPressed(Keys.Right)) camInput.X += 1f;
            if (Input.KeyPressed(Keys.Up))    camInput.Y -= 1f;
            if (Input.KeyPressed(Keys.Down))  camInput.Y += 1f;
            
            if (Input.KeyPressed(Keys.A)) posInput.X -= 1f;
            if (Input.KeyPressed(Keys.D)) posInput.X += 1f;
            if (Input.KeyPressed(Keys.W)) posInput.Y -= 1f;
            if (Input.KeyPressed(Keys.S)) posInput.Y += 1f;
            
            if (Input.KeyPressed(Keys.R)) {
                CamPos = new Vector3(0f, 0f, 0.1f);
                pos = new Vector2(300, 300);
            }
            
            CamPos += new Vector3(camInput * GameTime.DeltaTime * camSpeed, 0f);
            pos += posInput * GameTime.DeltaTime * camSpeed;

            pos2 = Input.MousePos();
            System.Console.WriteLine("pos2: " + pos2);

            _mat4 = new Matrix4x4(
                1f, 1f, 1f, 1f,
                1f, 1f, 1f, 1f,
                1f, 1f, 1f, 1f,
                1f, 1f, 1f, 1f);

            _mat4 = Matrix4x4.CreateTranslation(CamPos);
        }

        protected override void Render()
        {
            /*
            RenderObjList();
            */

            Engine2D.ClearScreen(0.4f, 0f, 0f, 0.1f);

            Vector2 position = pos;
            Vector2 scale = new Vector2(32, 32);
            float rotation = MathF.Sin(GameTime.TotalElapsedSec) * MathF.PI * 2f;

            Matrix4x4 trans = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);

            Vector2 position2 = pos2;
            Matrix4x4 trans2 = Matrix4x4.CreateTranslation(position2.X, position2.Y, 0);
            float rotation2 = 0f;
            Matrix4x4 rot2 = Matrix4x4.CreateRotationZ(rotation2);
            
            _shader.SetMatrix4X4("model", sca * rot * trans);
            
            _shader.Use();
            _shader.SetMatrix4X4("projection", _cam.GetProjectionMatrix());
            _shader.SetMatrix4X4("view", _mat4);
            glBindTexture(GL_TEXTURE_2D, _tex.TextureCopy);
            glBindVertexArray(_vao);
            glDrawArrays(GL_TRIANGLES, 0, 12);

            _shader2.SetMatrix4X4("model", sca * rot2 * trans2);
            
            _shader2.Use();
            _shader2.SetMatrix4X4("projection", _cam.GetProjectionMatrix());
            _shader2.SetMatrix4X4("view", _mat4);
            glEnable(GL_BLEND);
            glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
            glBindTexture(GL_TEXTURE_2D, _tex2.TextureCopy);
            

            glBindVertexArray(_vao);
            // This is a nice debug feature for later
            // Draws wireframe of all vertexes
            // glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
            glDrawArrays(GL_TRIANGLES, 0, 12);
            glBindVertexArray(0);
            
            Glfw.SwapBuffers(DisplayManager.Window);
        }
    }
}

