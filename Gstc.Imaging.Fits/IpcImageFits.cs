using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using nom.tam.fits;
using nom.tam.util;

namespace Gstc.Imaging.Fits {
    public class IpcImageFits : IpcImageFileMapped {

        public FitsData FitsData { get; }

        public IpcImageFits(int width, int height, IpcPixelFormatDefault pixelFormatDefault, int stride = 0) : base(width, height, pixelFormatDefault, stride) {  }

        public IpcImageFits(FitsData fitsData) : base(fitsData.Width, fitsData.Height, fitsData., )
            FitsData = fitsData;
        }

        public static void ExportFits(float[][] imageData) {
            var f = new nom.tam.fits.Fits();
            var h = FitsFactory.HDUFactory(imageData);
            f.AddHDU(h);
            var bufferedDataStream =
                new BufferedDataStream(new FileStream("Outputfile", FileMode.Create));
            f.Write(bufferedDataStream);
        }

        public static IpcImageFits LoadFromFile(Uri uri) {
            return ImportFits(uri.OriginalString);
        }

        public static FitsData GetFitsData(Uri uri) {
            return new FitsData(uri);
        }

        public static IpcImageFits ImportFits(string filePath, int frame = -1) {
            
            IpcImageFits ipcImage = new IpcImageFits(width, height, ipcPixelFormat);

            Array[] imgArray = (Array[])primaryHdu.Kernel;
            CopyArray(ipcImage, imgArray, width, height, ipcPixelFormat.BytesPerPixel);

            return ipcImage;
        }



       

        private static void CopyArray(IpcImage ipcImage, Array[] imgArray, int width, int height, int byteLength) {
            IntPtr ipcPtr = ipcImage.DataPtr;

            for (int rowIndex = 0; rowIndex < height; rowIndex++) {
                IntPtr ptr = IntPtr.Add(ipcPtr, rowIndex * height * byteLength); //TODO: Width instead of height?
                float[] floatArray = (float[])imgArray[rowIndex];
                
                Marshal.Copy(floatArray, 0, ptr, width);
            }
        }

        public static void NormalizeArray(Array[] imgArray, int width, int height) {

            var gMax = float.NegativeInfinity;
            var gMin = float.PositiveInfinity;
            for (var index = 0; index < height; index++) {
                float[] floatArray = (float[])imgArray[index];
                var lMax = floatArray.Max();
                var lMin = floatArray.Min();
                if (lMax > gMax) gMax = lMax;
                if (lMin < gMin) gMin = lMin;

            }
            //for (int a = 0; a < width; a++) floatArray[a] = (floatArray[a] - gMin) / (gMax + gMin); //For normalizing
        }

     }
}