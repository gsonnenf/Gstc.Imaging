using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Gstc.Imaging.Extensions;

namespace Gstc.Imaging.Net {
    public static class IpcPixelFormatExtension {

        public static System.Drawing.Imaging.PixelFormat ToDrawingPixelFormat(this IpcPixelFormat ipcPixelFormat) =>
        ExtUtil.IpcPixelFormatToExternalFormat(ipcPixelFormat, IpcPixelFormatToDrawingPixelFormatDictionary);
        
        public static NativeMethods.BI ToGdiBiFormat(this IpcPixelFormat ipcPixelFormat) =>
            ExtUtil.IpcPixelFormatToExternalFormat(ipcPixelFormat, IpcPixelFormatToGdiBiDictionary);
            
        public static Dictionary<IpcPixelFormatDefault, System.Drawing.Imaging.PixelFormat> IpcPixelFormatToDrawingPixelFormatDictionary =
            new Dictionary<IpcPixelFormatDefault, System.Drawing.Imaging.PixelFormat>() {
                [IpcPixelFormats.Gray16] = System.Drawing.Imaging.PixelFormat.Format16bppGrayScale,
                [IpcPixelFormats.Rgb24] = System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                [IpcPixelFormats.Rgb32] = System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                [IpcPixelFormats.Rgb48] = System.Drawing.Imaging.PixelFormat.Format48bppRgb,
                [IpcPixelFormats.Rgba32] = System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                [IpcPixelFormats.Rgba64] = System.Drawing.Imaging.PixelFormat.Format64bppArgb,
            };

        public static Dictionary<IpcPixelFormatDefault, NativeMethods.BI> IpcPixelFormatToGdiBiDictionary =
            new Dictionary<IpcPixelFormatDefault, NativeMethods.BI> {
                [IpcPixelFormats.Rgb24] = NativeMethods.BI.RGB,
                [IpcPixelFormats.Rgb32] = NativeMethods.BI.RGB,
                [IpcPixelFormats.Rgba32] = NativeMethods.BI.RGB,
                [IpcPixelFormats.Gray16] = NativeMethods.BI.BITFIELDS,
            };
    }
}
