using System.Diagnostics;
using static OpenGL.GL;

namespace OpenGL.Rendering.Shaders;

class Shader
{
    private string vertexCode;
    private string fragmentCode;
    
    public uint ProgramID { get; set; }
    
    public Shader(string vertexCode, string fragmentCode)
        {
            this.vertexCode = vertexCode;
            this.fragmentCode = fragmentCode;
        }

    public void Load()
    {
        uint vs, fs;
        
        vs = glCreateShader(GL_VERTEX_SHADER);
        glShaderSource(vs, vertexCode);
        glCompileShader(vs);

        int[] status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);

        if (status[0] == 0)
        {
            // Failed to compile
            string error = glGetShaderInfoLog(vs);
            Debug.WriteLine("ERROR COMPILING VERTEX SHADER" + error);
        }
        
        fs = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(fs, fragmentCode);
        glCompileShader(fs);
        
        status = glGetShaderiv(fs, GL_COMPILE_STATUS, 1);

        if (status[0] == 0)
        {
            // Failed to compile
            string error = glGetShaderInfoLog(fs);
            Debug.WriteLine("ERROR COMPILING VERTEX SHADER" + error);
        }
        
        ProgramID = glCreateProgram();
        glAttachShader(ProgramID, vs);
        glAttachShader(ProgramID, fs);
        
        glLinkProgram(ProgramID);
        
        // Delete shaders
        
        glDetachShader(ProgramID, vs);
        glDetachShader(ProgramID, fs);
        glDeleteShader(vs);
        glDeleteShader(fs);
        
    }

    public void Use()
    {
        glUseProgram(ProgramID);
    }
}