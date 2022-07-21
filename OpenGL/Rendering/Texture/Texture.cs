using System.Diagnostics;
using StbiSharp;
using System.Runtime.InteropServices;
using static OpenGL.GL;

namespace OpenGL.Rendering.Texture;

public class Texture
{
    private readonly string _filepath;
    public uint TextureCopy;

    public Texture(string filepath)
    {
        _filepath = filepath;
    }

    public unsafe void Load(){
        uint texture;
        glGenTextures(1, &texture);
        glBindTexture(GL_TEXTURE_2D, texture);
        
        // Repeat image in both directions
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
        // When stretching the image, pixelate
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST); 
        // When shrinking the image, pixelate
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

        Stream stream = File.OpenRead(_filepath);
        MemoryStream ms = new MemoryStream();
        stream.CopyTo(ms);

        StbiImage image = Stbi.LoadFromMemory(ms, 4);
        byte[] data = image.Data.ToArray();

        int w = image.Width;
        int h = image.Height;

        GCHandle pinnedData = GCHandle.Alloc(data, GCHandleType.Pinned);
        IntPtr dataPtr = pinnedData.AddrOfPinnedObject();
        pinnedData.Free();

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (data != null)
        {
            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, w, h, 0, GL_RGBA, GL_UNSIGNED_BYTE, dataPtr);
            glGenerateMipmap(GL_TEXTURE_2D);
        }
        else
        {
            Debug.WriteLine("Failed to load texture");
        }

        TextureCopy = texture;
    }
}