using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using nom.tam.fits;
using OpenCvSharp;

namespace Gstc.Imaging.Fits.Extensions {
    public static class FitsExtension {


        public static PixelFormat getPixelFormat(this ImageHDU imageHdu) {
            return BitpixMediaPixelFormatDictionary[imageHdu.BitPix];
        }

        public static Dictionary<int, PixelFormat> BitpixMediaPixelFormatDictionary =
            new Dictionary<int, PixelFormat> {
                [-32] = PixelFormats.Gray32Float,
                //[-64] = PixelFormats.Default,
                [8] = PixelFormats.Gray8,
                [16] = PixelFormats.Gray16,
                //[32] = PixelFormats.Default
            };

        public static Dictionary<int, MatType> BitpixMatTypeDictionary =
            new Dictionary<int, MatType> {
                [-32] = MatType.CV_32F,
                [-64] = MatType.CV_64F,
                [8] = MatType.CV_8U,
                [16] = MatType.CV_16S,
                [32] = MatType.CV_32S
            };
    }
}
