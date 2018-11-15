using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gstc.Imaging;

namespace Gstc.Imaging {

    public interface IpcPixelFormat {
        int ChannelsPerPixel { get; }
        int BitsPerChannel { get; }
        int BitsPerPixel { get;  }
        int BytesPerPixel { get; }
        IpcPixelFormatType IpcPixelFormatType { get; }
    }


    
}
