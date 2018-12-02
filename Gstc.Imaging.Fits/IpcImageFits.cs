using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using nom.tam.fits;
using nom.tam.util;

namespace Gstc.Imaging.Fits {
    public class IpcImageFits : IpcImageFileMapped {
        //protected IpcImageFits(int width, int height, IpcPixelFormatDefault pixelFormatDefault, int stride = 0) : base(  width, height, pixelFormatDefault, stride) { }

        protected IpcImageFits(FitsData fitsData, int channel) : base(fitsData.Width, fitsData.Height,
            fitsData.IpcPixelFormat, 0) {
            FitsData = fitsData;
            ActiveChannel = channel;
        }

        protected IpcImageFits(FitsData fitsData) : base(fitsData.Width * fitsData.Channels, fitsData.Height,
            fitsData.IpcPixelFormat, 0) {
            FitsData = fitsData;
            ActiveChannel = -1;
        }

        public FitsData FitsData { get; }
        public int ActiveChannel { get; }

        #region Loading methods
        public new static IpcImageFits LoadFromFile(Uri uri) => LoadFromFileSingleChannel(uri);

        public static IpcImageFits LoadFromFileSingleChannel(Uri uri, int channel = 0) => LoadFromFitsDataSingleChannel( new FitsData(uri), channel );

        public static List<IpcImageFits> LoadFromFileMultiChannel(Uri uri) => LoadFromFitsDataMultiChannel( new FitsData(uri) );

        public static IpcImageFits LoadFromFitsDataSingleChannel(FitsData fitsData, int channel) {
            var ipcImage = new IpcImageFits(fitsData, channel);
            fitsData.CopyImageSingleChannel(ipcImage.DataPtr, channel);
            return ipcImage;
        }

        public static List<IpcImageFits> LoadFromFitsDataMultiChannel(FitsData fitsData) {
            var ipcImageList = new List<IpcImageFits>();
            var ipcImagePtrList = new List<IntPtr>();

            for (var i = 0; i < fitsData.Channels; i++) {
                var ipcImage = new IpcImageFits(fitsData, i);
                fitsData.CopyImageSingleChannel(ipcImage.DataPtr, i);
                ipcImageList.Add(ipcImage);
            }
            return ipcImageList;
        }

        //TODO: Make public when interleave is implemented
        internal static IpcImageFits LoadFromFileInterleave(Uri uri) {
            var fitsData = new FitsData(uri);
            if (fitsData.Channels == 1) return LoadFromFileSingleChannel(uri, 0);
            var ipcImage = new IpcImageFits(fitsData, -1);
            fitsData.CopyImageInterleave(ipcImage.DataPtr);
            return ipcImage;
        }
        #endregion

        public static void NormalizeArray(Array[] imgArray, int width, int height) {
            var gMax = float.NegativeInfinity;
            var gMin = float.PositiveInfinity;
            for (var index = 0; index < height; index++) {
                var floatArray = (float[]) imgArray[index];
                var lMax = floatArray.Max();
                var lMin = floatArray.Min();
                if (lMax > gMax) gMax = lMax;
                if (lMin < gMin) gMin = lMin;
            }

            //for (int a = 0; a < width; a++) floatArray[a] = (floatArray[a] - gMin) / (gMax + gMin); //For normalizing
        }


        public static void ExportFits(float[][] imageData) { //TODO: Check if this works
            var f = new nom.tam.fits.Fits();
            var h = FitsFactory.HDUFactory(imageData);
            f.AddHDU(h);
            var bufferedDataStream = new BufferedDataStream(new FileStream("Outputfile", FileMode.Create));
            f.Write(bufferedDataStream);
        }
    }
}