using System.Diagnostics;
using System.Numerics;
using static OpenGL.GL;

namespace OpenGL.Rendering.Shaders;

public class Shader
{

    private readonly string _vsFilepath;
    private readonly string _fsFilepath;

    private float[] MatrixVal;

    private uint ProgramId { get; set; }
    
    public Shader(string vsFilepath, string fsFilepath)
        {
            this._vsFilepath = vsFilepath;
            this._fsFilepath = fsFilepath;
        }

    public void Load()
    {
        string vertexCode = File.ReadAllText(_vsFilepath);
        string fragmentCode = File.ReadAllText(_fsFilepath);
        
        uint vs = glCreateShader(GL_VERTEX_SHADER);
        glShaderSource(vs, vertexCode);
        glCompileShader(vs);

        int[] status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);

        if (status[0] == 0)
        {
            // Failed to compile
            string error = glGetShaderInfoLog(vs);
            Debug.WriteLine("ERROR COMPILING VERTEX SHADER" + error);
        }
        
        uint fs = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(fs, fragmentCode);
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
        float[] matrixValues =
        {
            mat.M11, mat.M12, mat.M13, mat.M14,
            mat.M21, mat.M22, mat.M23, mat.M24,
            mat.M31, mat.M32, mat.M33, mat.M34,
            mat.M41, mat.M42, mat.M43, mat.M44
        };
        glUniformMatrix4fv(location, 1, false, matrixValues);
    }
}