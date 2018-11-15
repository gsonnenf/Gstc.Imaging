using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Imaging {
    public class IpcPixelFormatCustom : IpcPixelFormat {

        public int ChannelsPerPixel { get; set; }
        public int BitsPerChannel { get; set; }
        public int BitsPerPixel { get; set; }
        public int BytesPerPixel { get; set; }
        public IpcPixelFormatType IpcPixelFormatType { get; set; }


        public IpcPixelFormatCustom() {
            IpcPixelFormatType = IpcPixelFormatType.Custom;
        }

        public IpcPixelFormatCustom(int channelsPerPixel, int bitsPerChannel, IpcChannelDataType ipcChannelDataType) {
            ChannelsPerPixel = channelsPerPixel;
            BitsPerChannel = bitsPerChannel;
            IpcPixelFormatType = IpcPixelFormatType.Custom;
            BitsPerPixel = channelsPerPixel * bitsPerChannel;
            BytesPerPixel = (int)Math.Round((double)BitsPerPixel / 8);
        }
    }
}
