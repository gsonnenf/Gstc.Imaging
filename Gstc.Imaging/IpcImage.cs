using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Gstc.Imaging.Extensions;

namespace Gstc.Imaging {
    public abstract class IpcImage {
      

        public abstract IntPtr DataPtr { get; }
        public abstract IntPtr SectionPtr { get; }
        public abstract int BufferSize { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract int PaddingBytes { get; }
        public abstract int Stride { get; }
        public abstract int BitsPerPixel { get; }
        public abstract IpcPixelFormat IpcPixelFormat { get; }
        public IpcPixelFormatType IpcPixelFormatType => IpcPixelFormat.IpcPixelFormatType;
  

        public abstract object Clone(); //TODO: Do i really need this?
        public abstract IpcImage CloneIpcImage();

        public event Action OnUpdate;
        public event Action OnDispose;

        public static IpcImage LoadFromFile(Uri filePathUri) {
            return IpcImageFileMapped.LoadFromFile(filePathUri);
        }

        public void Update() => OnUpdate?.Invoke();
  

        public string Info() {
            var info = "";
            info += nameof(IpcPixelFormat) + ": " + IpcPixelFormat.IpcPixelFormatType + "\n";
            info += nameof(Height) + ": " + Height + "\n";
            info += nameof(Width) + ": " + Width + "\n";
            info += nameof(BufferSize) + ": " + BufferSize + "\n";
            info += nameof(Stride) + ": " + Stride + "\n";
            info += nameof(PaddingBytes) + ": " + PaddingBytes + "\n";
            info += nameof(BitsPerPixel) + ": " + BitsPerPixel + "\n";
            return info;
        }

        /// <summary>
        ///     Copies one ipc image memory block to a destination ipc image provided the buffers are of the same size.
        /// </summary>
        /// <param name="destIpcImage">The destination Ipc image.</param>
        public void CopyImageTo(IpcImage destIpcImage) {
            if (BufferSize != destIpcImage.BufferSize) throw new Exception("Images must have the same size buffer.");
            NativeMethods.CopyMemory(destIpcImage.DataPtr, DataPtr, (uint) destIpcImage.BufferSize);
        }

        ~IpcImage() {
            OnDispose?.Invoke();           
        }
    }
}