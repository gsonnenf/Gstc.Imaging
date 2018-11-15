using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Imaging.Extensions {
    public static class ExtUtil {

        //Map IPC format-> External format
        public static T IpcPixelFormatToExternalFormat<T>(IpcPixelFormat ipcPixelFormat, Dictionary<IpcPixelFormatDefault, T> dictionary) {
            if (!(ipcPixelFormat is IpcPixelFormatDefault)) throw new NotSupportedException("Custom IpcPixelFormats are currently not supported for" + typeof(T));
            if (dictionary.TryGetValue((IpcPixelFormatDefault)ipcPixelFormat, out var pixelFormat)) return pixelFormat;
            throw new NotSupportedException("Type: " + ipcPixelFormat.IpcPixelFormatType + " is not supported by " + typeof(T).FullName);
        }

        public static T IpcPixelFormatToExternalFormatReverseLookup<T>(IpcPixelFormat ipcPixelFormat, Dictionary<T, IpcPixelFormatDefault> dictionary) where T : IEquatable<T> {
            try { return dictionary.First(x => x.Value.Equals(ipcPixelFormat)).Key; } catch {
                throw new NotSupportedException("Type: " + ipcPixelFormat.IpcPixelFormatType + " is not supported by " + typeof(T));
            }
        }

        //Map External format -> IPC format
        public static IpcPixelFormat ExternalFormatToIpcPixelFormat<T>(T pixelFormat, Dictionary<T,IpcPixelFormatDefault> dictionary) {
            if (dictionary.TryGetValue(pixelFormat, out var ipcPixelFormat)) return ipcPixelFormat;
            throw new NotSupportedException("Type: " + pixelFormat  + " is not supported by " + typeof(IpcPixelFormat).FullName);
        }
        
        public static IpcPixelFormat ExternalFormatToIpcPixelFormatReverseLookup<T>(T pixelFormat, Dictionary<IpcPixelFormatDefault,T> dictionary) where T : IEquatable<T> {
            try { return dictionary.First(x => x.Value.Equals(pixelFormat) ).Key; } catch {
                throw new NotSupportedException("Type: " + pixelFormat + " is not supported by " + typeof(IpcPixelFormatType).FullName);
            }
        }

        // Associates a member with an IpcImage using a dictionary. A poorman's Extension Property.
        public static bool GetOrCreateMember<T>(out T element, IpcImage ipcImage, Dictionary<IpcImage, T> dictionary, Func<IpcImage, T> createMethod) {
            if (dictionary.TryGetValue(ipcImage, out element)) return true;
            element = createMethod(ipcImage);
            dictionary[ipcImage] = element;
            ipcImage.OnDispose += () => dictionary.Remove(ipcImage);
            return false;
        }
    }
}
