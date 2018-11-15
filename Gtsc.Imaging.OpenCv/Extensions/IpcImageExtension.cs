using System.Collections.Generic;
using Gstc.Imaging;
using OpenCvSharp;
using OpenCvSharp.Cuda;

namespace Gtsc.Imaging.OpenCv.Extensions
{
    public static class IpcImageExtension {

        public static Dictionary<IpcImage, Mat> MatList = new Dictionary<IpcImage, Mat>();

        public static Mat GetOpenCv(this IpcImage ipcImage) {

            if (ipcImage is IpcImageMat imageMat) return imageMat.GetMat();
            if (MatList.ContainsKey(ipcImage)) return MatList[ipcImage];

            var format = ipcImage.IpcPixelFormat.GetOpenCvMatType();

            var mat = new Mat(
                ipcImage.Height, 
                ipcImage.Width,
                ipcImage.IpcPixelFormat.GetOpenCvMatType(), 
                ipcImage.DataPtr);

            //if (IpcImageMat.IsCvGpuEnabled) mat = new GpuMat(mat); //TODO: Test how GpuMat interacts with memory

            MatList.Add(ipcImage,mat);

            return mat;
        }
    }
}
