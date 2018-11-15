using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Imaging.Extensions {
    public static class ExtUtil {
        public static T MapFromIpcPixelFormat<T>(IpcPixelFormat ipcPixelFormat, Dictionary<IpcPixelFormatDefault, T> dictionary) {
            try {
                if (!(ipcPixelFormat is IpcPixelFormatDefault)) throw new Exception();
                return dictionary[ipcPixelFormat as IpcPixelFormatDefault];
            } catch { throw new NotSupportedException("Type: " + ipcPixelFormat.IpcPixelFormatType + " is not supported by " + typeof(T).FullName); }
        }
    }
}
