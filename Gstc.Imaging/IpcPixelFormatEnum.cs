using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Imaging {
    public enum IpcPixelFormatType {
        Custom,

        Gray8,
        Gray16,
        Gray32,
        Gray64,

        GrayFloat8,
        GrayFloat16,
        GrayFloat32,
        GrayFloat64,

        Rgb24,
        Rgb32,
        Rgb48,
        Rgb64,
        Rgb96,
        Rgb128,
        Rgb192,
        Rgb256,

        Rgba32,
        Rgba48,
        Rgba64,
        Rgba128,
        Rgba256,

        RgbFloat24,
        RgbFloat32,
        RgbFloat48,
        RgbFloat64,
        RgbFloat96,
        RgbFloat128,
        RgbFloat192,

        RgbaFloat32,
        RgbaFloat64,
        RgbaFloat128,
        RgbaFloat256,
    }

    public enum IpcChannelDataType {
        Custom,
        UInt,
        Int,
        Float,
    }
}
