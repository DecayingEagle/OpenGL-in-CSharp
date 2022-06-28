using System.Data;
using System.Diagnostics;
using System.Net;
using StbiSharp;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static OpenGL.GL;

namespace OpenGL.Rendering.Texture;

public class Texture
{
    private string filepath;
    private int texId;

    public byte[] data_copy;
    public uint texture_copy;

    public unsafe Texture(string filepath)
    {
        this.filepath = filepath;
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

        int w, h,channels;
        
        Stream stream = File.OpenRead(filepath);
        MemoryStream ms = new MemoryStream();
        stream.CopyTo(ms);

        StbiImage image = Stbi.LoadFromMemory(ms, 4);
        byte[] data = image.Data.ToArray();
        data_copy = data;
        

        w = image.Width;
        h = image.Height;
        channels = image.NumChannels;

        GCHandle pinned_data = GCHandle.Alloc(data, GCHandleType.Pinned);
        IntPtr data_ptr = pinned_data.AddrOfPinnedObject();
        pinned_data.Free();

        if (data != null)
        {
            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, w, h, 0, GL_RGBA, GL_UNSIGNED_BYTE, data_ptr);
            glGenerateMipmap(GL_TEXTURE_2D);
        }
        else
        {
            Debug.WriteLine("Failed to load texture");
        }

        texture_copy = texture;
    }
}