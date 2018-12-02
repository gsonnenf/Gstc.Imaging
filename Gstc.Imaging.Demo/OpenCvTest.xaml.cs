using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Gstc.Imaging.Net;
using Gstc.Imaging.UnitTest.Images;
using Gtsc.Imaging.OpenCv.Extensions;
using OpenCvSharp;
using Size = OpenCvSharp.Size;
using Window = OpenCvSharp.Window;

namespace Gstc.Imaging.Demo
{
    /// <summary>
    /// Interaction logic for OpenCvTest.xaml
    /// </summary>
    public partial class OpenCvTest : UserControl
    {
        private IpcImage _ipcImage;
        private InteropBitmap _interopBitmap;
        private readonly Random _rand = new Random();

        public OpenCvTest()
        {
            InitializeComponent();
            _ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.LargeBitmapBgr);
            _interopBitmap = _ipcImage.GetInteropBitmap();
            ImageWpf.Source = _interopBitmap;
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            //Hack to create a 'modal' window.
           
            var window = new System.Windows.Window()
            { 
                Height = 300,
                Width = 200,
            };
            var isProcessing = true;
            window.Closing += (o, args) => args.Cancel = isProcessing;

          
            var button = new Button() { Content = "Please Wait....", Margin = new Thickness(100) };
            button.Click += (o, args) => window.Close();

            window.Content = button;
            
            //Execution of code
            Thread thread = new Thread(() => {
                try
                {
                    var mat = _ipcImage.GetOpenCv();
                    var c = mat.DataLength();
                    //TODO: Find out why black bar is appearing on second pass.
                    var temp = mat.GaussianBlur(new Size(101, 101), 50, 50);
                    temp.CopyTo(mat); //Perhaps use Memorycopy instead.
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Dispatcher.Invoke(() => {
                    _ipcImage.Update();
                    button.Content = "Ok";
                    isProcessing = false;
                });
            });

            thread.Start();
            window.ShowDialog();
        }
    }
}
