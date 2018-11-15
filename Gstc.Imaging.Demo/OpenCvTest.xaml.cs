using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Gstc.Imaging.Demo.Images;
using Gstc.Imaging.Net;
using Gtsc.Imaging.OpenCv.Extensions;
using OpenCvSharp;
using Size = OpenCvSharp.Size;

namespace Gstc.Imaging.Demo {
    /// <summary>
    /// Interaction logic for OpenCvTest.xaml
    /// </summary>
    public partial class OpenCvTest : UserControl {
        private IpcImage _ipcImage;
        private InteropBitmap _interopBitmap;
        private readonly Random _rand = new Random();

        public OpenCvTest() {
            InitializeComponent();
            _ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.LargeBitmapBgr);
            _interopBitmap = _ipcImage.GetInteropBitmap();
            ImageWpf.Source = _interopBitmap;
            //_ipcImage.Update();

        }

        private void ApplyFilter(object sender, RoutedEventArgs e) {

            try {
               
                var mat = _ipcImage.GetOpenCv();
                var c = mat.DataLength();
                //TODO: Find out why black bar is appearing on second pass.
                var temp = mat.GaussianBlur(new Size(101, 101), 50, 50);
                temp.CopyTo(mat); //Perhaps use Memorycopy instead.
               
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            _ipcImage.Update();
        }
    }
}
