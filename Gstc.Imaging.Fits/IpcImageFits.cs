using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media;
using nom.tam.fits;
using nom.tam.util;

namespace Gstc.Imaging.Fits {
    public class IpcImageFits : IpcImageFileMapped {
        public IpcImageFits(int width, int height, IpcPixelFormatDefault pixelFormatDefault, int stride = 0) : base(width, height, pixelFormatDefault, stride) {  }

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

        public static IpcImageFits ImportFits(string filePath) {
            var fitsData = new nom.tam.fits.Fits(filePath);
            var primaryHdu = (ImageHDU)fitsData.ReadHDU();
            var channels = -1;
            var width = -1;
            var height = -1;
            int bitSize = primaryHdu.BitPix;
            var bufferSize = primaryHdu.Header.DataSize;
            var axes = primaryHdu.Axes;

            //TODO: Set a good error size limit here.
            if (bufferSize > 4000000000) throw new NotSupportedException("Image Size greater than 4GB aren't supported.");

            if (axes.Length == 2) {
                channels = 1;
                width = axes[0];
                height = axes[1];
            }
            else if (axes.Length == 3) {
                channels = axes[0];
                width = axes[1];
                height = axes[2];
            }

            else throw new NotSupportedException("FITS data with more than 3 data dimensions is not supported.");

            var ipcPixelFormat = GetIpcFormatType(channels, bitSize);
            IpcImageFits ipcImage = new IpcImageFits(width, height, ipcPixelFormat);

            Array[] imgArray = (Array[])primaryHdu.Kernel;
            CopyArray(ipcImage, imgArray, width, height, ipcPixelFormat.BytesPerPixel);

            return ipcImage;
        }

        private static IpcPixelFormatDefault GetIpcFormatType(int channels, int bitSize) {
            if (bitSize != 8 ||
                bitSize != 16 ||
                bitSize != 32 ||
                bitSize != -32 ||
                bitSize != 64) throw new NotSupportedException("Bitpix field in pix is not 8,16,32,-32 or 64. This is an unsupported value.");


            if (channels == 1) {
                if (bitSize == 8) return IpcPixelFormats.Gray8;
                if (bitSize == 16) return IpcPixelFormats.Gray16;
                if (bitSize == 32) return IpcPixelFormats.Gray32;
                if (bitSize == -32) return IpcPixelFormats.GrayFloat32;
                if (bitSize == -64) return IpcPixelFormats.GrayFloat64;
                throw new NotSupportedException();
            }

            if (channels == 3) {
                IpcPixelFormatDefault pixelFormatDefault;
                if (bitSize == 8) return IpcPixelFormats.Rgb24;
                if (bitSize == 16) return IpcPixelFormats.Rgb48;
                if (bitSize == 32) return IpcPixelFormats.Rgb96;
                if (bitSize == -32) return IpcPixelFormats.RgbFloat96;
                if (bitSize == -64) return IpcPixelFormats.RgbFloat192;
                throw new NotSupportedException();

            }

            if (channels == 4) {
                IpcPixelFormatDefault pixelFormatDefault;
                if (bitSize == 8) return IpcPixelFormats.Rgba32;
                if (bitSize == 16) return IpcPixelFormats.Rgba64;
                if (bitSize == 32) return IpcPixelFormats.Rgba128;
                if (bitSize == -32) return IpcPixelFormats.RgbaFloat128;
                if (bitSize == -64) return IpcPixelFormats.RgbaFloat256;
                throw new NotSupportedException();
            }

            throw new NotSupportedException();
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