using System;
using System.IO;
using System.Reflection;

namespace Gstc.Imaging.UnitTest.Images {
    public static class ImageUris {

        internal static string RootDirectory = Path.GetDirectoryName( Assembly.GetExecutingAssembly().CodeBase );

        internal static Uri GetUri(string filename) => new Uri(RootDirectory + filename, UriKind.Absolute);

        public static Uri LargeBitmapBgr = GetUri(@"\2048x2048.bmp");
        public static Uri FitsSingleChannel1024X1024Float32 = GetUri(@"\FOCx38i0101t_c0f.fits");
        public static Uri FitsMultiChannel200X200Float32 = GetUri(@"\WFPC2u5780205r_c0fx.fits");
        public static Uri Bgr32 = GetUri(@"\TestPatternBGR32.bmp");
        public static Uri Bgr24 = GetUri(@"\TestPatternBGR24.bmp");      
        public static Uri Grayscale8Bpp = GetUri(@"\TestPatternGrayscale8bpp.png");
        public static Uri LargeBmp = GetUri(@"\2048x2048.bmp");
    }
}
