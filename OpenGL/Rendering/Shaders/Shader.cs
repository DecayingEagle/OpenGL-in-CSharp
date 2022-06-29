using System.Diagnostics;
using System.Numerics;
using static OpenGL.GL;

namespace OpenGL.Rendering.Shaders;

public class Shader
{
    private readonly string _vertexCode;
    private readonly string _fragmentCode;

    private uint ProgramId { get; set; }
    
    public Shader(string vertexCode, string fragmentCode)
        {
            this._vertexCode = vertexCode;
            this._fragmentCode = fragmentCode;
        }

    public void Load()
    {
        uint vs = glCreateShader(GL_VERTEX_SHADER);
        glShaderSource(vs, _vertexCode);
        glCompileShader(vs);

        int[] status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);

        if (status[0] == 0)
        {
            // Failed to compile
            string error = glGetShaderInfoLog(vs);
            Debug.WriteLine("ERROR COMPILING VERTEX SHADER" + error);
        }
        
        uint fs = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(fs, _fragmentCode);
        glCompileShader(fs);
        
        status = glGetShaderiv(fs, GL_COMPILE_STATUS, 1);

        if (status[0] == 0)
        {
            // Failed to compile
            string error = glGetShaderInfoLog(fs);
            Debug.WriteLine("ERROR COMPILING VERTEX SHADER" + error);
        }
        
        ProgramId = glCreateProgram();
        glAttachShader(ProgramId, vs);
        glAttachShader(ProgramId, fs);
        
        glLinkProgram(ProgramId);
        
        // Delete shaders
        
        glDetachShader(ProgramId, vs);
        glDetachShader(ProgramId, fs);
        glDeleteShader(vs);
        glDeleteShader(fs);
        
    }

    public void Use()
    {
        glUseProgram(ProgramId);
    }

    public void SetMatrix4X4(string uniformName, Matrix4x4 mat)
    {
        int location = glGetUniformLocation(ProgramId, uniformName);
        glUniformMatrix4fv(location, 1, false, GetMatrix4X4Values(mat));
    }
    
    private float[] GetMatrix4X4Values(Matrix4x4 m)
    {
        return new[]
        {
            m.M11, m.M12, m.M13, m.M14,
            m.M21, m.M22, m.M23, m.M24,
            m.M31, m.M32, m.M33, m.M34,
            m.M41, m.M42, m.M43, m.M44
        };
    }
}