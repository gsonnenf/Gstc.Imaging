using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Imaging {

    public class IpcPixelFormatDefault : IpcPixelFormat {
        public IpcChannelDataType IpcChannelDataType { get; set; }

        internal IpcPixelFormatDefault(int channelsPerPixel, int bitsPerChannel, IpcChannelDataType ipcChannelDataType, IpcPixelFormatType type) {
            ChannelsPerPixel = channelsPerPixel;
            BitsPerChannel = bitsPerChannel;
            IpcChannelDataType = ipcChannelDataType;
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

   
}

