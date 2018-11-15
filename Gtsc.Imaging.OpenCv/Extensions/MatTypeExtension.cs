using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Gstc.Imaging;
using OpenCvSharp;

namespace Gtsc.Imaging.OpenCv.Extensions {
    public static class MatTypeExtension {

        /// Gets the number of bytes the image uses in memory. 
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static long DataLength(this Mat mat) => mat.DataEnd.ToInt64() - mat.DataStart.ToInt64();


        /// <summary>
        /// Gets a compatible System.Window.Media.PixelFormat type from an OpenCV mat type.
        /// 
        /// </summary>
        /// <param name="matType"></param>
        /// <returns></returns>
        public static PixelFormat GetWindowsMediaPixelFormat(this MatType matType) => MatPixelFormatDictionary[matType];

        /// <summary>
        /// Gets a compatible OpenCV MatType from a System.Window.Media.PixelFormat type. 
        /// </summary>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        public static MatType GetOpenCvMatType(this PixelFormat pixelFormat) => PixelFormatMatDictionary[pixelFormat];

        public static MatType GetOpenCvMatType(this IpcPixelFormat pixelFormatDefault) {
            try { return MatTypeToIpcPixelFormatDictionary.First(x => x.Value == pixelFormatDefault).Key; } 
            catch { throw new NotSupportedException("Type: " + pixelFormatDefault + " is not supported by " + typeof(IpcPixelFormatDefault).FullName); }
        }



        public static Dictionary<PixelFormat, MatType> PixelFormatMatDictionary =
            new Dictionary<PixelFormat, MatType> {
                [PixelFormats.Gray8] = MatType.CV_8UC1,
                [PixelFormats.Gray16] = MatType.CV_16UC1,
                [PixelFormats.Gray32Float] = MatType.CV_32F,

                [PixelFormats.Bgr24] = MatType.CV_8UC3,
                [PixelFormats.Bgr32] = MatType.CV_8UC4,
                [PixelFormats.Bgra32] = MatType.CV_8UC4,
                [PixelFormats.Rgb48] = MatType.CV_16UC3,
                [PixelFormats.Rgba64] = MatType.CV_16UC4,
                [PixelFormats.Rgba64] = MatType.CV_16UC4,
                [PixelFormats.Rgb128Float] = MatType.CV_32FC3,
                [PixelFormats.Rgba128Float] = MatType.CV_32FC4,

            };


        public static Dictionary<MatType, IpcPixelFormatDefault> MatTypeToIpcPixelFormatDictionary = new Dictionary<MatType, IpcPixelFormatDefault>() {
            [MatType.CV_8UC1] = IpcPixelFormats.Gray8,
            [MatType.CV_16UC1] = IpcPixelFormats.Gray16,
            [MatType.CV_32SC1] = IpcPixelFormats.Gray32,


            [MatType.CV_32FC1] = IpcPixelFormats.GrayFloat32,
            [MatType.CV_64FC1] = IpcPixelFormats.GrayFloat64,


            [MatType.CV_8UC3] = IpcPixelFormats.Rgb24,
            [MatType.CV_16UC3] = IpcPixelFormats.Rgb48,
            [MatType.CV_32SC3] = IpcPixelFormats.Rgb96,
            [MatType.CV_8UC4] = IpcPixelFormats.Rgba32,
            [MatType.CV_16UC4] = IpcPixelFormats.Rgba128,
            [MatType.CV_32SC4] = IpcPixelFormats.Rgba256,

            [MatType.CV_32FC3] = IpcPixelFormats.Rgb96,
            [MatType.CV_32FC4] = IpcPixelFormats.Rgba128,

            [MatType.CV_64FC3] = IpcPixelFormats.RgbFloat192,
            [MatType.CV_64FC4] = IpcPixelFormats.RgbaFloat256,
        };

        /// <summary>
        /// Contains a map between a MatType and a compatible WPF type
        /// </summary>
        public static Dictionary<MatType, PixelFormat> MatPixelFormatDictionary =
            new Dictionary<MatType, PixelFormat> {
                [MatType.CV_8UC1] = PixelFormats.Gray8,
                [MatType.CV_8SC1] = PixelFormats.Gray8,
                [MatType.CV_8UC3] = PixelFormats.Bgr24,
                [MatType.CV_8SC3] = PixelFormats.Bgr24,
                [MatType.CV_8UC4] = PixelFormats.Bgr32,
                [MatType.CV_8UC4] = PixelFormats.Bgra32,
                [MatType.CV_8SC4] = PixelFormats.Bgra32,
                [MatType.CV_16UC1] = PixelFormats.Gray16,
                [MatType.CV_16SC1] = PixelFormats.Gray16,
                [MatType.CV_16UC3] = PixelFormats.Rgb48,
                [MatType.CV_16SC3] = PixelFormats.Rgb48,
                [MatType.CV_16UC4] = PixelFormats.Rgba64,
                [MatType.CV_16SC4] = PixelFormats.Rgba64,
                [MatType.CV_32SC4] = PixelFormats.Prgba64,
                [MatType.CV_32F] = PixelFormats.Gray32Float,
                [MatType.CV_32FC1] = PixelFormats.Gray32Float,
                //TODO: Fix, this may or may not actually be scRGB but the stride is correct
                [MatType.CV_32FC3] = PixelFormats.Rgb128Float,
                [MatType.CV_32FC4] = PixelFormats.Rgba128Float
            };


        /// <summary>
        /// Contains a map between an OpenCV type and a FITS type.
        /// </summary>
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
