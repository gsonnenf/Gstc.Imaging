using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media.Imaging;
using Gstc.Imaging.Demo.Images;
using Gstc.Imaging.Net;

namespace Gstc.Imaging.Demo {
    /// <summary>
    /// Interaction logic for WriteableBitmapTest.xaml
    /// </summary>
    public partial class WriteableBitmapTest : UserControl {
        private IpcImage _ipcImage;
        private WriteableBitmap _writeableBitmap;
        private Graphics _graphics;
        private readonly Random _rand = new Random();

        public WriteableBitmapTest() {
            InitializeComponent();
            _ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.LargeBitmapBgr);
            _writeableBitmap = _ipcImage.GetWriteableBitmap();
            _graphics = _ipcImage.GetGdiGraphics();
            Draw(null, null);
            TestImage.Source = _writeableBitmap;
        }

        private void Draw(object sender, RoutedEventArgs e) {
            var brush = new System.Drawing.SolidBrush(Color.FromArgb(255, 0, 0, 0));

            var num1 = _rand.Next(_ipcImage.Width - 100);
            var num2 = _rand.Next(_ipcImage.Height - 100);
            _graphics.FillRectangle(brush, new System.Drawing.Rectangle(num1, num2, 100, 100));

            _ipcImage.Update();
        }

    }
}
