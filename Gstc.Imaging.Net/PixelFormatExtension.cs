using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Gstc.Imaging.Extensions;

namespace Gstc.Imaging.Net {
    public static class PixelFormatExtension {


        public static NativeMethods.BI ToGdiBiFormat(this PixelFormat pixelFormat) => PixelFormatToGdiDibDictionary[pixelFormat];
        /// <summary>
        /// Returns the data Type (float or Int) of PixelFormat Type.
        /// </summary>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        public static Type GetType(this System.Windows.Media.PixelFormat pixelFormat) {
            if (!DiscreteTypeDictionary.ContainsKey(pixelFormat))
                throw new NotSupportedException("Type: " + pixelFormat + " is not supported by DiscreteTypeDictionary");
            return DiscreteTypeDictionary[pixelFormat];
        }

        /// <summary>
        /// Returns the pixel length of the PixelFormat type.
        /// </summary>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        public static double GetBytesPerPixel(this System.Windows.Media.PixelFormat pixelFormat) {
            return pixelFormat.BitsPerPixel / 8.0;
            //if (!BytesPerPixelDictionary.ContainsKey(pixelFormatDefault)) throw new NotSupportedException("Type: " + pixelFormatDefault + " is not supported by BytesPerPixelDictionary");
            //return BytesPerPixelDictionary[pixelFormatDefault];
        }

        /// <summary>
        /// Returns the compatible type of System.Drawing.Imaging.PixelFormat for a given System.Windows.Media.PixelFormat.
        /// </summary>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        public static System.Drawing.Imaging.PixelFormat ToDrawingPixelFormat(
            this System.Windows.Media.PixelFormat pixelFormat) {
            if (!PixelFormatDictionary.ContainsKey(pixelFormat))
                throw new NotSupportedException("Type: " + pixelFormat + " is not supported by PixelFormatDictionary.");
            return PixelFormatDictionary[pixelFormat];
        }

        public static PixelFormat ToMediaPixelFormat(
            this System.Drawing.Imaging.PixelFormat pixelFormat) {
            try {
                return PixelFormatDictionary.First(x => x.Value == pixelFormat).Key;
            } catch {
                throw new NotSupportedException("Type: " + pixelFormat + " is not supported by PixelFormatDictionary.");
            }
        }

       
        /// <summary>
        /// Reference table from pixelFormatDefault to type of data used in the Pixel Format.
        /// </summary>
        public static Dictionary<PixelFormat, Type> DiscreteTypeDictionary =
            new Dictionary<PixelFormat, Type> {
                [PixelFormats.Gray8] = typeof(int),
                [PixelFormats.Bgr24] = typeof(int),
                [PixelFormats.Bgra32] = typeof(int),
                [PixelFormats.Gray16] = typeof(float),
                [PixelFormats.Rgb48] = typeof(int),
                [PixelFormats.Rgba64] = typeof(int),
                [PixelFormats.Prgba64] = typeof(int),
                [PixelFormats.Gray32Float] = typeof(float),
                [PixelFormats.Rgb128Float] = typeof(float),
                [PixelFormats.Rgba128Float] = typeof(float),
                [PixelFormats.Prgba128Float] = typeof(float),
                [PixelFormats.Default] = typeof(int)
            };

        /// <summary>
        /// Reference table for Stride used in each pixel Format.
        /// </summary>
        public static Dictionary<PixelFormat, int> BytesPerPixelDictionary =
            new Dictionary<PixelFormat, int> {
                [PixelFormats.Gray8] = 1,
                [PixelFormats.Bgr24] = 3,
                [PixelFormats.Bgra32] = 4,
                [PixelFormats.Bgr32] = 4,
                [PixelFormats.Gray16] = 2,
                [PixelFormats.Rgb48] = 6,
                [PixelFormats.Rgba64] = 8,
                [PixelFormats.Prgba64] = 8,
                [PixelFormats.Gray32Float] = 4,
                [PixelFormats.Rgb128Float] = 12,
                [PixelFormats.Rgba128Float] = 16,
                [PixelFormats.Prgba128Float] = 16,
                [PixelFormats.Default] = 4
            };

        /// <summary>
        /// Conversion table from System.Drawing PixelFormat to System.Windows PixelFormat.
        /// </summary>
        public static Dictionary<PixelFormat, System.Drawing.Imaging.PixelFormat> PixelFormatDictionary =
            new Dictionary<PixelFormat, System.Drawing.Imaging.PixelFormat> {
                [PixelFormats.Gray8] = System.Drawing.Imaging.PixelFormat.Format8bppIndexed,
                [PixelFormats.Bgr24] = System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                [PixelFormats.Bgra32] = System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                [PixelFormats.Bgr32] = System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                [PixelFormats.Gray16] = System.Drawing.Imaging.PixelFormat.Format16bppGrayScale,
                [PixelFormats.Rgb48] = System.Drawing.Imaging.PixelFormat.Format48bppRgb,
                [PixelFormats.Rgba64] = System.Drawing.Imaging.PixelFormat.Format64bppArgb,
                [PixelFormats.Prgba64] = System.Drawing.Imaging.PixelFormat.Format64bppPArgb,
                [PixelFormats.Gray32Float] = System.Drawing.Imaging.PixelFormat.Undefined,
                [PixelFormats.Rgb128Float] = System.Drawing.Imaging.PixelFormat.Undefined,
                [PixelFormats.Rgba128Float] = System.Drawing.Imaging.PixelFormat.Undefined,
                [PixelFormats.Prgba128Float] = System.Drawing.Imaging.PixelFormat.Undefined
            };

        public static Dictionary<PixelFormat, NativeMethods.BI> PixelFormatToGdiDibDictionary =
            new Dictionary<PixelFormat, NativeMethods.BI> {
                [PixelFormats.Bgr32] = NativeMethods.BI.RGB,
                [PixelFormats.Bgr24] = NativeMethods.BI.RGB,
                [PixelFormats.Bgra32] = NativeMethods.BI.RGB,
                [PixelFormats.Gray16] = NativeMethods.BI.BITFIELDS,
            };


    }
}
