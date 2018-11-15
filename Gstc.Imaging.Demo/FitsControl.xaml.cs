using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gstc.Imaging.Demo.Images;
using Gstc.Imaging.Fits;
using Gstc.Imaging.Net;

namespace Gstc.Imaging.Demo {
    /// <summary>
    /// Interaction logic for FitsControl.xaml
    /// </summary>
    public partial class FitsControl : UserControl {
        private IpcImage _ipcImage;
        private InteropBitmap _interopBitmap;
        public FitsControl() {
            InitializeComponent();
            _ipcImage = IpcImageFits.LoadFromFile(ImageUris.FitsSingleChannel1024x1024_32f);
            _interopBitmap = _ipcImage.GetInteropBitmap();
            ImageWpf.Source = _interopBitmap;
        }
    }
}
