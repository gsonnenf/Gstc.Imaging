using System;
using System.Collections.Generic;

namespace Gstc.Imaging {
    public static class IpcPixelFormats {


        public static IpcPixelFormat GetMatchingFormat(int channels, int bits, IpcChannelDataType dataType) {
            foreach (var formatItem in DefaultFormatDictionary) {
                var format = formatItem.Value;
                if (format.BitsPerChannel == bits &&
                    format.ChannelsPerPixel == channels &&
                    format.IpcChannelDataType == dataType) return format;
                if (format.IpcPixelFormatType == IpcPixelFormatType.RgbFloat24) {
                    continue;
                }
            }

            throw new NotImplementedException("GetMatchingFormat does not currently implement custom types.");
        }

        public static Dictionary<IpcPixelFormatType,IpcPixelFormatDefault> DefaultFormatDictionary { get; } = 
            new Dictionary<IpcPixelFormatType, IpcPixelFormatDefault>();

        #region Default Pixel Formats

        public static readonly IpcPixelFormatDefault Gray8 = new IpcPixelFormatDefault(1, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Gray8);
        public static readonly IpcPixelFormatDefault Gray16 = new IpcPixelFormatDefault(1, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Gray16);
        public static readonly IpcPixelFormatDefault Gray32 = new IpcPixelFormatDefault(1, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Gray32);
        public static readonly IpcPixelFormatDefault Gray64 = new IpcPixelFormatDefault(1, 64, IpcChannelDataType.UInt, IpcPixelFormatType.Gray64);

        public static readonly IpcPixelFormatDefault GrayFloat8 = new IpcPixelFormatDefault(1, 8, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat8);
        public static readonly IpcPixelFormatDefault GrayFloat16 = new IpcPixelFormatDefault(1, 16, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat16);
        public static readonly IpcPixelFormatDefault GrayFloat32 = new IpcPixelFormatDefault(1, 32, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat32);
        public static readonly IpcPixelFormatDefault GrayFloat64 = new IpcPixelFormatDefault(1, 64, IpcChannelDataType.Float, IpcPixelFormatType.GrayFloat64);

        public static readonly IpcPixelFormatDefault Rgb24 = new IpcPixelFormatDefault(3, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb24);
        public static readonly IpcPixelFormatDefault Rgb48 = new IpcPixelFormatDefault(3, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb48);
        public static readonly IpcPixelFormatDefault Rgb96 = new IpcPixelFormatDefault(3, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb96);
        public static readonly IpcPixelFormatDefault Rgb192 = new IpcPixelFormatDefault(3, 64, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb192);

        public static readonly IpcPixelFormatDefault Rgb32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb32);
        public static readonly IpcPixelFormatDefault Rgb64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb64);
        public static readonly IpcPixelFormatDefault Rgb128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb128);
        public static readonly IpcPixelFormatDefault Rgb256 = new IpcPixelFormatDefault(4, 64, IpcChannelDataType.UInt, IpcPixelFormatType.Rgb256);


        public static readonly IpcPixelFormatDefault Rgba32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba32);
        public static readonly IpcPixelFormatDefault Rgba64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba64);
        public static readonly IpcPixelFormatDefault Rgba128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba128);
        public static readonly IpcPixelFormatDefault Rgba256 = new IpcPixelFormatDefault(4, 64, IpcChannelDataType.UInt, IpcPixelFormatType.Rgba256);

      
        public static readonly IpcPixelFormatDefault RgbFloat24 = new IpcPixelFormatDefault(3, 8, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat24);
        public static readonly IpcPixelFormatDefault RgbFloat48 = new IpcPixelFormatDefault(3, 16, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat48);
        public static readonly IpcPixelFormatDefault RgbFloat96 = new IpcPixelFormatDefault(3, 32, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat96);
        public static readonly IpcPixelFormatDefault RgbFloat192 = new IpcPixelFormatDefault(3, 64, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat192);

        public static readonly IpcPixelFormatDefault RgbFloat32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat32);
        public static readonly IpcPixelFormatDefault RgbFloat64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat64);
        public static readonly IpcPixelFormatDefault RgbFloat128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat128);
        public static readonly IpcPixelFormatDefault RgbFloat256 = new IpcPixelFormatDefault(4, 64, IpcChannelDataType.Float, IpcPixelFormatType.RgbFloat256);

        public static readonly IpcPixelFormatDefault RgbaFloat32 = new IpcPixelFormatDefault(4, 8, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat32);
        public static readonly IpcPixelFormatDefault RgbaFloat64 = new IpcPixelFormatDefault(4, 16, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat64);
        public static readonly IpcPixelFormatDefault RgbaFloat128 = new IpcPixelFormatDefault(4, 32, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat128);
        public static readonly IpcPixelFormatDefault RgbaFloat256 = new IpcPixelFormatDefault(4, 64, IpcChannelDataType.Float, IpcPixelFormatType.RgbaFloat256);
        
        
        #endregion

        static IpcPixelFormats() {

            //Builds a dictionary from pixel format types defined in class.
            Type myType = typeof(IpcPixelFormats);
            var myFields = myType.GetFields();
            foreach (var field in myFields) {              
                if (field.GetValue(null) is IpcPixelFormatDefault item) DefaultFormatDictionary.Add(item.IpcPixelFormatType, item);
            }
        }
    }
}
