using System.Collections.Generic;
using Gstc.Imaging;
using Gstc.Imaging.Extensions;
using OpenCvSharp;

namespace Gtsc.Imaging.OpenCv.Extensions {
    public static class IpcImageFormatExtensions {

        public static IpcPixelFormat GetIpcPixelFormat(this MatType matType) =>
            ExtUtil.ExternalFormatToIpcPixelFormat(matType, MatTypeToIpcPixelFormatDictionary);

        public static MatType GetOpenCvMatType(this IpcPixelFormat pixelFormat) =>
            ExtUtil.IpcPixelFormatToExternalFormat(pixelFormat, IpcPixelToFormatMatTypeDictionary);

        public static Dictionary<IpcPixelFormatDefault,MatType> IpcPixelToFormatMatTypeDictionary =
     new Dictionary<IpcPixelFormatDefault, MatType>() {
         [IpcPixelFormats.Gray8] = MatType.CV_8UC1,
         [IpcPixelFormats.Gray16] = MatType.CV_16UC1,
         [IpcPixelFormats.Gray32] = MatType.CV_32SC1,
 
         [IpcPixelFormats.GrayFloat32] = MatType.CV_32FC1,
         [IpcPixelFormats.GrayFloat64] = MatType.CV_64FC1,
         
         [IpcPixelFormats.Rgb24] = MatType.CV_8UC3,
         [IpcPixelFormats.Rgb32] = MatType.CV_8UC4,
         [IpcPixelFormats.Rgb48] = MatType.CV_16UC3,
         [IpcPixelFormats.Rgb64] = MatType.CV_16UC4,
         [IpcPixelFormats.Rgb96] = MatType.CV_32SC3,
         [IpcPixelFormats.Rgb128] = MatType.CV_32SC4,
       
         [IpcPixelFormats.Rgba32] = MatType.CV_8UC4,
         [IpcPixelFormats.Rgba64] = MatType.CV_16UC4,       
         [IpcPixelFormats.Rgba128] = MatType.CV_32SC4,

         [IpcPixelFormats.RgbaFloat128] = MatType.CV_32FC4,
         [IpcPixelFormats.RgbaFloat256] = MatType.CV_64FC4,
     };

        public static Dictionary<MatType, IpcPixelFormatDefault> MatTypeToIpcPixelFormatDictionary =
            new Dictionary<MatType, IpcPixelFormatDefault>() {
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
    }
}
