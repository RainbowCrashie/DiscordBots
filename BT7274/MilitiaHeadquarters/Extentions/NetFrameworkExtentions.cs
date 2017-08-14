using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BT7274.MilitiaHeadquarters.Extentions
{
    public static class NetFrameworkExtentions
    {
        public static Stream ToStream(this Image image, ImageFormat format)
        {
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }
}