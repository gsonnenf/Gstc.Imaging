using System;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Gstc.Imaging;
using Gtsc.Imaging.OpenCv.Extensions;
using OpenCvSharp;
using OpenCvSharp.Cuda;
using OpenCvSharp.Extensions;

namespace Gtsc.Imaging.OpenCv {
    public class IpcImageMat : IpcImage {

        #region STATIC
        public static bool IsCvGpuEnabled;

        static IpcImageMat() {
            try {
                Cv2.ThrowIfGpuNotAvailable();
                IsCvGpuEnabled = true;
            } catch (Exception) {
                IsCvGpuEnabled = false;
            }
        }
        #endregion
        public IpcImageMat() { }

        protected InteropBitmap _interopBitmap;
        protected Mat _mat;
        protected WriteableBitmap _writeableBitmap;

        public IpcImageMat(Mat mat) {
            _mat = mat;
        }

        //TODO: Find out why object placeholder is required so no overload error because of "public IpcImageMat(Mat mat)" sharing one element signature
        public IpcImageMat(string filePath, object placeholder = null) {
            _mat = new Mat(filePath, ImreadModes.AnyColor);
            if (IsCvGpuEnabled) _mat = new GpuMat(_mat);
        }

        public override IntPtr DataPtr => _mat.DataStart;
        public override IntPtr MapPtr { get; }
        public override int Width => _mat.Cols;
        public override int Height => _mat.Rows;
        public override int PaddingBytes { get; }
        public override int BitsPerPixel => IpcPixelFormat.BitsPerPixel;
        public override int Stride => (int)_mat.Step();

        
        public override IpcPixelFormat IpcPixelFormat { get; }


        public Mat GetMat() => _mat;

        public override int BufferSize => (int)(_mat.DataEnd.ToInt64() - _mat.DataStart.ToInt64());


        public void LoadFromFile(Uri filePathUri) {
            _mat = new Mat(filePathUri.ToString(), ImreadModes.AnyColor);
        }

        public override IpcImage CloneIpcImage() { return new IpcImageMat(_mat.Clone()); }
       

        public override object Clone() { return new IpcImageMat(_mat.Clone()); }
       
    }

    public static class OpenCvExtension {
        public static long DataLength(this Mat mat) {
            return mat.DataEnd.ToInt64() - mat.DataStart.ToInt64();
        }
    }
}
