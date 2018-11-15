using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Gstc.Imaging.Extensions
{
    public static class IpcPixelFormatExtension {




        public static PixelFormat ToMediaPixelFormat(this IpcPixelFormat ipcPixelFormat) {
            try {
                if (!(ipcPixelFormat is IpcPixelFormatDefault)) throw new Exception();
                return IpcPixelFormatToMediaPixelFormatDictionary[(IpcPixelFormatDefault)ipcPixelFormat];
            } catch (NotSupportedException e) { throw e; } catch { throw new NotSupportedException("Type: " + ipcPixelFormat.IpcPixelFormatType + " is not supported by " + typeof(PixelFormat).FullName); }
        }

        public static IpcPixelFormat ToIpcPixelFormat(this PixelFormat pixelFormat) {
            try { return IpcPixelFormatToMediaPixelFormatDictionary.First(x => x.Value == pixelFormat).Key; } 
            catch { throw new NotSupportedException("Type: " + pixelFormat + " is not supported by " + typeof(IpcPixelFormatType).FullName);  }
        }

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
