using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenGL.Rendering.Display;
using OpenGL.Rendering.Shaders;
using OpenGL.Rendering.Texture;
using OpenGL.Sprite;
using StbiSharp;
using static OpenGL.GL;
namespace OpenGL;

public class Engine2D
{
    private string ProjectPath { get; } = System.IO.Directory.GetCurrentDirectory();
    // private List<Sprite> ObjList = new List<Sprite>();
    //
    // public void AddObjectFromList(Sprite obj){
    //     ObjList.Add(obj);
    // }
    //     
    // public void RemoveObjectFromList(int id){
    //     Sprite objectToRemove = ObjList.FirstOrDefault(x => x.id == id); 
    //     ObjList.Remove(objectToRemove);
    // }

    public void DrawRectangle(int x, int y, int r, int g, int b){
        Shader _shader;

        _shader = new Shader(ProjectPath + @"/Rendering/Shaders/vertex.glsl", ProjectPath + @"/Rendering/Shaders/fragment.glsl");
        _shader.Load();
         

        uint _vbo;
        uint _vao;

        _vao = glGenVertexArray();
        _vbo = glGenBuffer();
        glBindVertexArray(_vao);
        glBindBuffer(GL_ARRAY_BUFFER, _vbo);

    }
        

    public void DrawRectangle(int x, int y, string filepathTex, string texWrapping)
    {
        Shader _shader;
    }

    public void DrawCircle()
    {
        //TODO: Implement circle drawing
    }

    public static void ClearScreen(float r, float g, float b, float a)
    {
        glClearColor(r, g, b, a);
        glClear(GL_COLOR_BUFFER_BIT);
    }

    public void CreateWindow(int w, int h, string title)
    {
        DisplayManager.CreateWindow(w, h, title);
    }
}