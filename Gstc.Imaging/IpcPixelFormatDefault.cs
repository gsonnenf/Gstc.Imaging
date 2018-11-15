using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Imaging {

    public class IpcPixelFormatDefault : IpcPixelFormat {
        public IpcChannelDataType IpcChannelDataType { get; set; }

        internal IpcPixelFormatDefault(int channelsPerPixel, int bitsPerChannel, IpcChannelDataType ipcChannelDataType,
            IpcPixelFormatType type) {
            ChannelsPerPixel = channelsPerPixel;
            BitsPerChannel = bitsPerChannel;
            IpcPixelFormatType = type;
            BitsPerPixel = channelsPerPixel * bitsPerChannel;
            BytesPerPixel = (int)Math.Round((double)BitsPerPixel / 8);
        }

        public int ChannelsPerPixel { get; }
        public int BitsPerChannel { get; }
        public int BitsPerPixel { get; }
        public int BytesPerPixel { get; }
        public IpcPixelFormatType IpcPixelFormatType { get; }

        public object OriginalFormat { get; set; }
        public Type OriginalFormatType { get; set; }
        public string OriginalFormatName { get; set; }

    }

    public static class IpcPixelFormats {

        public static readonly IpcPixelFormatDefault Gray8 = new IpcPixelFormatDefault(1, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Gray8);
        public static readonly IpcPixelFormatDefault Gray16 = new IpcPixelFormatDefault(1, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Gray16);
        public static readonly IpcPixelFormatDefault Gray32 = new IpcPixelFormatDefault(1, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Gray32);
        public static readonly IpcPixelFormatDefault Gray64 = new IpcPixelFormatDefault(1, 64, IpcChannelDataType.UInt, IpcPixelFormatType.Gray64);

        public static readonly IpcPixelFormatDefault GrayFloat8 = new IpcPixelFormatDefault(1, 8, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat8);
        public static readonly IpcPixelFormatDefault GrayFloat16 = new IpcPixelFormatDefault(1, 16, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat16);
        public static readonly IpcPixelFormatDefault GrayFloat32 = new IpcPixelFormatDefault(1, 32, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat32);
        public static readonly IpcPixelFormatDefault GrayFloat64 = new IpcPixelFormatDefault(1, 64, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat64);

        public static readonly IpcPixelFormatDefault Rgb24 = new IpcPixelFormatDefault(3, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb24);
        public static readonly IpcPixelFormatDefault Rgb32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb32);
        public static readonly IpcPixelFormatDefault Rgb48 = new IpcPixelFormatDefault(3, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb48);
        public static readonly IpcPixelFormatDefault Rgb64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb64);
        public static readonly IpcPixelFormatDefault Rgb96 = new IpcPixelFormatDefault(3, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb96);
        public static readonly IpcPixelFormatDefault Rgb128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb128);

        public static readonly IpcPixelFormatDefault Rgba32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba32);
        public static readonly IpcPixelFormatDefault Rgba64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba64);
        public static readonly IpcPixelFormatDefault Rgba128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba128);
        public static readonly IpcPixelFormatDefault Rgba256 = new IpcPixelFormatDefault(4, 64, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba256);


        public static readonly IpcPixelFormatDefault RgbFloat32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat32);
        public static readonly IpcPixelFormatDefault RgbFloat64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat64);
        public static readonly IpcPixelFormatDefault RgbFloat96 = new IpcPixelFormatDefault(3, 32, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat96);
        public static readonly IpcPixelFormatDefault RgbFloat128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat128);
        public static readonly IpcPixelFormatDefault RgbFloat192 = new IpcPixelFormatDefault(3, 64, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat192);

        public static readonly IpcPixelFormatDefault RgbaFloat32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat32);
        public static readonly IpcPixelFormatDefault RgbaFloat64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat64);
        public static readonly IpcPixelFormatDefault RgbaFloat128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat128);
        public static readonly IpcPixelFormatDefault RgbaFloat256 = new IpcPixelFormatDefault(4, 64, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat256);
    }
}

