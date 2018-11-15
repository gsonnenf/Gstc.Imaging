using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Gstc.Imaging.Demo.Images;
using Gstc.Imaging.Extensions;
using Gstc.Imaging.Net;
using Image = System.Drawing.Image;

namespace Gstc.Imaging.Demo {
    /// <summary>
    /// Interaction logic for InteropGdi.xaml
    /// </summary>
    public partial class InteropGdi : UserControl {
        private IpcImage _ipcImage;
        private InteropBitmap _interopBitmap;
        private readonly Random _rand = new Random();

        public InteropGdi() {
            InitializeComponent();
            _ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.LargeBitmapBgr);
            _interopBitmap = _ipcImage.GetInteropBitmap();
            ImageWpf.Source = _interopBitmap;
            DrawGdiImage(null, null);
        }

        private void DrawGdiImage(object sender, RoutedEventArgs e) {
            var brush = new System.Drawing.SolidBrush(Color.FromArgb(255, 0, 0, 0));

            var num1 = _rand.Next(_ipcImage.Width - 100);
            var num2 = _rand.Next(_ipcImage.Height - 100);

            _ipcImage.GetGdiGraphics().FillRectangle(brush, new System.Drawing.Rectangle(num1, num2, 100, 100));
            _ipcImage.Update();
        }

      
    }
}
