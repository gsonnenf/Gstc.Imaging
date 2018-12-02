using System;
using Gstc.Imaging;
using Gstc.Imaging.Net;
using Gstc.Imaging.UnitTest.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Color = System.Drawing.Color;

namespace Gtsc.Imaging.Net.UnitTest {
    [TestClass]
    public class TestGdi {

        [TestMethod]
        public void CreateMemorySectionTest() {
            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.Bgr32);
            

            var brush = new System.Drawing.SolidBrush(Color.FromArgb(255,2,2,2));
            ipcImage.GetGdiGraphics().FillRectangle(brush, new System.Drawing.Rectangle(0, 0, 5, 5));

            Console.WriteLine(ipcImage.Info());
            unsafe {
                byte* pointer = (byte*)ipcImage.DataPtr.ToPointer();
                for (int rowIndex = 0; rowIndex < ipcImage.Height; rowIndex++) {
                    for (int columnIndex = 0; columnIndex < ipcImage.Width * ipcImage.BytesPerPixel; columnIndex++) {
                        var value = *(pointer + columnIndex + rowIndex * ipcImage.Stride);
                        if ((columnIndex % (int)ipcImage.BytesPerPixel) == 0) Console.Write(" - ");
                        Console.Write(value + " ");

                        if (rowIndex >=0  && rowIndex <= 4 && ( (columnIndex+1) % 4) != 0) Assert.AreEqual(2, value, "Rows Failed");
                        if (rowIndex == 5) Assert.AreEqual(255, value, "White Row Failed");
                    }
                    Console.Write("\n");
                }
            }
        }
    }
}
