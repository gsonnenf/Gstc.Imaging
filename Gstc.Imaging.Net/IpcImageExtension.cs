using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Gstc.Imaging.Extensions;

namespace Gstc.Imaging.Net {
    public static class IpcImageExtension {

        private static readonly Dictionary<IpcImage, WriteableBitmap> WritableBitmapDictionary = new Dictionary<IpcImage, WriteableBitmap>();
        private static readonly Dictionary<IpcImage, InteropBitmap> InteropBitmapDictionary = new Dictionary<IpcImage, InteropBitmap>();
        private static readonly Dictionary<IpcImage, Bitmap> DrawingBitmapDictionary = new Dictionary<IpcImage, Bitmap>();
        private static readonly Dictionary<IpcImage, Graphics> GdiGraphicsDictionary = new Dictionary<IpcImage, Graphics>();

        #region Extensions

 

        public static WriteableBitmap GetWriteableBitmap(this IpcImage ipcImage) {
            if ( ExtUtil.GetOrCreateMember(out var bitmap, ipcImage, WritableBitmapDictionary, CreateWriteableBitmap) ) return bitmap;
       
            ipcImage.OnUpdate += () => {
                var rect = new Int32Rect(0, 0, ipcImage.Width, ipcImage.Height);
                bitmap.WritePixels(rect, ipcImage.DataPtr, ipcImage.BufferSize, ipcImage.Stride);
                bitmap.Lock();
                bitmap.AddDirtyRect(rect);
                bitmap.Unlock();
            };

            return bitmap;
        }

        public static InteropBitmap GetInteropBitmap(this IpcImage ipcImage) {
            if (ExtUtil.GetOrCreateMember(out var bitmap, ipcImage, InteropBitmapDictionary, CreateInteropBitmap)) return bitmap;
            ipcImage.OnUpdate += () => bitmap.Invalidate();         
            return bitmap;
        }

        public static Bitmap GetDrawingBitmap(this IpcImage ipcImage) {

            if (ExtUtil.GetOrCreateMember(out var bitmap, ipcImage, DrawingBitmapDictionary, CreateDrawingBitmap)) return bitmap;
            ipcImage.OnDispose += () => bitmap.Dispose();
            return bitmap;
        }

        public static Graphics GetGdiGraphics(this IpcImage ipcImage) {
            var dictionary = GdiGraphicsDictionary;
            if (dictionary.TryGetValue(ipcImage, out var graphics)) return graphics;
            graphics = CreateGdiGraphics(ipcImage, GetDrawingBitmap(ipcImage));
            dictionary[ipcImage] = graphics;

            ipcImage.OnDispose += ()=> {
                dictionary.Remove(ipcImage);
                graphics.Dispose();
            };
            return graphics;
        }
        #endregion

        #region Create Methods
        public static WriteableBitmap CreateWriteableBitmap(IpcImage ipcImage) {
            var writeableBitmap = new WriteableBitmap(ipcImage.Width, ipcImage.Height, 96.0, 96.0, ipcImage.IpcPixelFormat.ToMediaPixelFormat(), null);
            writeableBitmap.WritePixels(
                new Int32Rect(0, 0, ipcImage.Width, ipcImage.Height),
                ipcImage.DataPtr,
                ipcImage.BufferSize,
                ipcImage.Stride);

            return writeableBitmap;
        }

        public static InteropBitmap CreateInteropBitmap(IpcImage ipcImage) {
            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(
                ipcImage.MapPtr,
                ipcImage.Width,
                ipcImage.Height,
                ipcImage.IpcPixelFormat.ToMediaPixelFormat(),
                ipcImage.Stride,
                0);
            return (InteropBitmap)bitmapSource;
        }

        public static Bitmap CreateDrawingBitmap(IpcImage ipcImage) {
            return new Bitmap(ipcImage.Width, ipcImage.Height, ipcImage.Stride, ipcImage.IpcPixelFormat.ToDrawingPixelFormat(), ipcImage.DataPtr);
        }

        public static Graphics CreateGdiGraphics(this IpcImage ipcImage, Bitmap bitmap) {
            var bitmapInfo = new NativeMethods.BITMAPINFO {
                biWidth = ipcImage.Width,
                biHeight = ipcImage.Height,
                biBitCount = (short)ipcImage.BitsPerPixel,
                biPlanes = 1,
                biCompression = ipcImage.IpcPixelFormat.ToGdiBiFormat()
            };
            bitmapInfo.biSize = Marshal.SizeOf(bitmapInfo);
            return Graphics.FromImage(bitmap);
        }
        #endregion
    }
}


/*s
        //http://stackoverflow.com/questions/1546091/wpf-createbitmapsourcefromhbitmap-memory-leak
        //http://stackoverflow.com/questions/30727343/fast-converting-bitmap-to-bitmapsource-wpf
        private static BitmapSource ConvertBitmap(IntPtr sourcePtr)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                sourcePtr,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        private static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }
*/

/*
private void CreateHbitmap() {


_hBitmap = NativeMethods.CreateDIBSection(IntPtr.Zero,
ref bitmapInfo,
NativeMethods.DIB.RGB_COLORS,
out _ppvBits,
MapPtr,
0);

//IntPtr hdcScreen = NativeMethods.GetDC(HWND.NULL);
var a = Graphics.FromHwnd(IntPtr.Zero);
var b = a.GetHdc();
//NativeMethods.SelectObject(b, _hBitmap);


IntPtr hdcScreen = NativeMethods.GetDC(IntPtr.Zero);
_hMemoryDeviceContext = NativeMethods.CreateCompatibleDC(hdcScreen);
NativeMethods.SelectObject(_hMemoryDeviceContext, _hBitmap);

//NativeMethods.SelectObject(b, _hBitmap);
GraphicsGdi = Graphics.FromHdc(_hMemoryDeviceContext);

    OnDispose+=//if (_hBitmap != IntPtr.Zero) NativeMethods.DeleteGdiObject(_hBitmap);
}
*/
