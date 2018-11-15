using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Gstc.Imaging.Extensions
{
    public static class IpcPixelFormatExtension {


        //Extensions

        public static PixelFormat ToMediaPixelFormat(this IpcPixelFormat ipcPixelFormat) =>
            ExtUtil.IpcPixelFormatToExternalFormat(ipcPixelFormat, IpcPixelFormatToMediaPixelFormatDictionary);

        public static IpcPixelFormat ToIpcPixelFormat(this PixelFormat pixelFormat) =>
            ExtUtil.ExternalFormatToIpcPixelFormatReverseLookup(pixelFormat, IpcPixelFormatToMediaPixelFormatDictionary);
       

        //Dictionaries

        public static Dictionary<IpcPixelFormatDefault, PixelFormat> IpcPixelFormatToMediaPixelFormatDictionary =
            new Dictionary<IpcPixelFormatDefault, PixelFormat>() {
                [IpcPixelFormats.Gray8] = PixelFormats.Gray8,
                [IpcPixelFormats.Gray16] = PixelFormats.Gray16,
                [IpcPixelFormats.GrayFloat32] = PixelFormats.Gray32Float,
                [IpcPixelFormats.Rgb24] = PixelFormats.Bgr24,
                [IpcPixelFormats.Rgb32] = PixelFormats.Bgr32,
                [IpcPixelFormats.Rgb48] = PixelFormats.Rgb48,
                [IpcPixelFormats.Rgba32] = PixelFormats.Bgra32,
                [IpcPixelFormats.Rgba64] = PixelFormats.Rgba64,
                [IpcPixelFormats.RgbFloat128] = PixelFormats.Rgb128Float,
                [IpcPixelFormats.RgbaFloat128] = PixelFormats.Rgba128Float,
            };




    }
}
