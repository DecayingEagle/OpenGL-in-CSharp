using System.Numerics;
using OpenGL.Rendering.Display;

namespace OpenGL.Rendering.Cameras;

public class Camera2D
{
    

    public Vector2 FocusPosition { get; set; }
    public float Zoom { get; set; }
    public Camera2D(Vector2 focusPosition, float zoom)
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
}