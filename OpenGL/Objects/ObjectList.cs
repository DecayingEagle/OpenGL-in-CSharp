using OpenGL.Rendering.Shaders;
using OpenGL.Rendering.Texture;
using System.Numerics;

namespace OpenGL.ObjectList{
    class Objects{
        public List<Sprite> ObjList;

        public void AddObject(Sprite obj){
            ObjList.Add(obj);
        }
    }
    

    public class Sprite{
        public Shader shader;
        public Texture tex;
        public int vaoSize;
        public Vector2 pos;
        public Vector2 scale;
        public float rot;
        public bool isActive;
        public int id;

        public Sprite(Shader shader, Texture tex, int vaoSize, Vector2 pos, Vector2 scale, float rot, bool isActive, int id){
            this.shader = shader;
            this.tex = tex;
            this.vaoSize = vaoSize;
            this.pos = pos;
            this.scale = scale;
            this.rot = rot;
            this.isActive = isActive;
            this.id = id;
        }
    }
}