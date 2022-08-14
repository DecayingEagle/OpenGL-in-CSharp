using System.Numerics;
using OpenGL.Rendering.Display;

namespace OpenGL.Rendering.Cameras;

public class OrthoCamera2D
{
    public Vector2 FocusPosition { get; set; }
    public float Zoom { get; set; }
    public OrthoCamera2D(Vector2 focusPosition, float zoom)
     {
         FocusPosition = focusPosition;
         Zoom = zoom;
     }

    public Matrix4x4 GetProjectionMatrix()
    {
        float left = FocusPosition.X - DisplayManager.WindowSize.X / 2f;
        float right = FocusPosition.X + DisplayManager.WindowSize.X / 2f;
        float top = FocusPosition.Y - DisplayManager.WindowSize.Y / 2f;
        float bottom = FocusPosition.Y + DisplayManager.WindowSize.Y / 2f;

        Matrix4x4 orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100f);
        Matrix4x4 zoomMatrix = Matrix4x4.CreateScale(Zoom);

        return orthoMatrix * zoomMatrix;
    }

    public Matrix4x4 GetViewMatrix()
    {
        Vector3 camPos;

        camPos = new Vector3(0f, 0f, 0.1f);

        Matrix4x4 mat4;
        mat4 = new Matrix4x4(   1f, 1f, 1f, 1f,
            1f, 1f, 1f, 1f,
            1f, 1f, 1f, 1f,
            1f, 1f, 1f, 1f);

        mat4 = Matrix4x4.CreateTranslation(camPos);
        Console.WriteLine(mat4);
        
        return mat4;

        
    }
}