using System;

namespace Gtsc.Imaging.UnitTest.Images {
    public static class ImageUris {

        public static Uri BGR32 = new Uri(@"./Images/TestPatternBGR32.bmp", UriKind.Relative);
        public static Uri BGR24 = new Uri(@"./Images/TestPatternBGR24.bmp", UriKind.Relative);
        public static Uri Grayscale8bpp = new Uri(@"./Images/TestPatternGrayscale8bpp.png", UriKind.Relative);
        public static Uri LargeBmp = new Uri(@"./Images/2048x2048.bmp", UriKind.Relative);
    }
}
