﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using Gstc.Imaging.Extensions;

namespace Gstc.Imaging {
    public class IpcImageFileMapped : IpcImage {
        private readonly int _bufferSize;

        private IntPtr _mapPtr = IntPtr.Zero;
        private IntPtr _viewPtr = IntPtr.Zero;

        public IpcImageFileMapped(int width, int height, IpcPixelFormat ipcPixelFormat, int stride = 0) {
            Height = height;
            Width = width;
            IpcPixelFormat = ipcPixelFormat;

            //Calculates stride
            if (stride <= 0) Stride = (int) Math.Ceiling(width * BytesPerPixel);
            else if (stride > 0 && stride < width * ipcPixelFormat.BytesPerPixel)
                throw new ArgumentOutOfRangeException(@"Stride must be greater or equal to Width in memory.");
            else if (stride <= width * ipcPixelFormat.BytesPerPixel) Stride = stride;

            _bufferSize = height * Stride;
            CreateFileMappedMemory(_bufferSize);
        }

        public IntPtr ViewPtr => _viewPtr;
        public override IntPtr DataPtr => _viewPtr;
        public override IntPtr MapPtr => _mapPtr;
        public override int BufferSize => _bufferSize;
        public override int Width { get; }
        public override int Height { get; }

        public override int PaddingBytes => Stride - (int) Math.Ceiling(Width * IpcPixelFormat.BitsPerPixel / 8.0);
        public override int BitsPerPixel => IpcPixelFormat.BitsPerPixel;
        public double BytesPerPixel => IpcPixelFormat.BitsPerPixel / 8.0;
        public sealed override int Stride { get; }

        public override IpcPixelFormat IpcPixelFormat { get; }

        public new static IpcImageFileMapped LoadFromFile(Uri filePathUri) {
            using (Stream stream = File.OpenRead(filePathUri.LocalPath)) {
                var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                var bitmapImage = decoder.Frames[0];

                var ipcImage = new IpcImageFileMapped(bitmapImage.PixelWidth, bitmapImage.PixelHeight, bitmapImage.Format.ToIpcPixelFormat());
                for (var i = 0; i < ipcImage.BufferSize; i++) Marshal.WriteByte(ipcImage.DataPtr, i, 100);

                bitmapImage.CopyPixels(
                    new Int32Rect(0, 0, bitmapImage.PixelWidth, bitmapImage.PixelHeight),
                    ipcImage.DataPtr,
                    ipcImage.BufferSize,
                    ipcImage.Stride);
                return ipcImage;
            }
        }

        public override IpcImage CloneIpcImage() {
            var ipcImageFileMapped = new IpcImageFileMapped(Width, Height, IpcPixelFormat);
            CopyImageTo(ipcImageFileMapped);
            return ipcImageFileMapped;
        }

        public override object Clone() {
            return CloneIpcImage();
        }

        /*     
        void LoadFileWinforms() {     
        //Winforms bitmap
             using (var bitmap = new Bitmap(filePathUri.OriginalString)) {
                 var ipcImage = new IpcImageFileMapped(bitmap.Width, bitmap.Height, bitmap.PixelFormat.ToMediaPixelFormat());
                 var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                 NativeMethods.CopyMemory(ipcImage.DataPtr, bitmapData.Scan0, (uint)ipcImage.BufferSize);
             }
        }
         */

        #region Private

        private void CreateFileMappedMemory(int numberOfBytes) {
            //MemoryMappedFile mmf = MemoryMappedFile.CreateNew("test", 100, MemoryMappedFileAccess.ReadWrite); //Uses safe buffer with bounds checking, slow.
            //string name = null; // Guid.NewGuid().ToString();
            _mapPtr = NativeMethods.CreateFileMapping(
                NativeMethods.INVALID_HANDLE_VALUE, 
                IntPtr.Zero,
                NativeMethods.FileMapProtection.PageReadWrite, 
                0, 
                (uint) numberOfBytes, 
                null);

            _viewPtr = NativeMethods.MapViewOfFile(
                _mapPtr, 
                NativeMethods.FileMapAccess.FileMapAllAccess, 
                0, 0, 0);
            //if (_viewPtr == IntPtr.Zero) { throw new Exception("MapViewOfFile", new Win32Exception(Marshal.GetLastWin32Error())); }
        }

        ~IpcImageFileMapped() {
            if (_viewPtr != IntPtr.Zero) NativeMethods.UnmapViewOfFile(_viewPtr);
            if (_mapPtr != IntPtr.Zero) NativeMethods.CloseHandle(_mapPtr);
            _viewPtr = _mapPtr = IntPtr.Zero;
            //TODO: ? UnmapViewOfFile()
        }

        #endregion
    }
}