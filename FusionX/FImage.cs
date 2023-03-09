using CTFAK.MMFParser.Shared.Banks;
using CTFAK.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace FusionX;

public class FImage
{
    public int Handle;
    public Image fileImage;
    public Texture2D TextureCache=null;
    
    public unsafe Texture2D GetTexture()
    {
        if (TextureCache == null)
        {
            TextureCache = new Texture2D(MainWindow.inst.GraphicsDevice,fileImage.Width,fileImage.Height);
            var data = new byte[fileImage.Width * fileImage.Height * 4];
            fixed (byte* imgPtr = data)
            {
                fixed (byte* dataPtr = fileImage.imageData)
                {
                    NativeLib.TranslateToRGBA(new IntPtr(imgPtr), fileImage.Width, fileImage.Height, fileImage.Flags["Alpha"] ? 1 : 0, fileImage.imageData.Length,
                        new IntPtr(dataPtr), fileImage.Transparent, 0);
                    TextureCache.SetData(data);
                }
            }
            
            
        }

        return TextureCache;
    }
}