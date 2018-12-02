using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using Gstc.Imaging.Fits;
using Gstc.Imaging.Net;
using Gstc.Imaging.UnitTest.Images;
using Gtsc.Imaging.OpenCv.Extensions;
using OpenCvSharp;

namespace Gstc.Imaging.Demo {
    /// <summary>
    /// Interaction logic for FitsControl.xaml
    /// </summary>
    public partial class FitsControl : UserControl {
       
        public FitsControl() {
            InitializeComponent();
            LoadSingleChannel();
            LoadMultiChannel();
        }

        private void LoadSingleChannel() {
            var ipcImage = IpcImageFits.LoadFromFile(ImageUris.FitsSingleChannel1024X1024Float32);

            ListViewSingle.ItemsSource = ipcImage.FitsData.HeaderCardDictionary;

            ProcessImage(ipcImage);
            var mat = ipcImage.GetOpenCv();
            mat.MinMaxLoc(out double min, out double max);
            
            ImageSingle.Source = ipcImage.GetInteropBitmap();

        }

        private void LoadMultiChannel() {
            var ipcImageList = IpcImageFits.LoadFromFileMultiChannel(ImageUris.FitsMultiChannel200X200Float32);

            foreach (var ipcImage in ipcImageList) ProcessImage(ipcImage);
           
            Image1.Source = ipcImageList[0].GetInteropBitmap();
            Image2.Source = ipcImageList[1].GetInteropBitmap();
            Image3.Source = ipcImageList[2].GetInteropBitmap();
            Image4.Source = ipcImageList[3].GetInteropBitmap();
        }

        private void ProcessImage(IpcImage ipcImage)
        {
            var mat = ipcImage.GetOpenCv();
            mat.MinMaxLoc(out double min, out double max);



            //Console.WriteLine("1-ipc:" + _ipcImage.GetPixelValue(0,10));
            //Cv2.Divide(mat, -1, mat);
            Console.WriteLine("Pixel:" + mat.Get<Single>(0, 10));
            Cv2.Subtract(mat, -1 * min, mat);
            Cv2.Log(mat, mat);
            Cv2.Normalize(mat, mat);

        }

        private void ImageSingle_OnMouseMove(object sender, MouseEventArgs e) {

            e.GetPosition(ImageSingle);

        }
    }
}
