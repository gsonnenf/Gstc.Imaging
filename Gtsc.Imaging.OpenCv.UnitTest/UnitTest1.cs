using System;
using Gstc.Imaging;
using Gtsc.Imaging.OpenCv.Extensions;
using Gtsc.Imaging.OpenCv.UnitTest.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace Gtsc.Imaging.OpenCv.UnitTest {
    [TestClass]
    public class LoadMatImage {
        [TestMethod]
        public void TestMatImageLoad() {
            var image = IpcImage.LoadFromFile(ImageUris.BGR32);
            var mat = image.GetOpenCv();
            Assert.AreEqual( image.Width, mat.Cols, "Width mismatch.");
            Assert.AreEqual(image.Height, mat.Height, "Height mismatch.");

            for (int rowIndex = 0; rowIndex < mat.Height; rowIndex++) {
                for (int columnIndex = 0; columnIndex < mat.Width; columnIndex++) {

                    var value = mat.Get<uint>(rowIndex, columnIndex);
                    Console.Write(value.ToString("x") + " ");

                    if (rowIndex == 0) Assert.AreEqual(0xff000000, value);
                    if (rowIndex == 1) Assert.AreEqual(0xffffffff, value);
                    if (rowIndex == 4) Assert.AreEqual(0xff01017f, value);
                    if (rowIndex == 6) Assert.AreEqual(0xff017f01, value);
                    if (rowIndex == 8) Assert.AreEqual(0xff7f0101, value);

                }
                Console.WriteLine("");
            }
        }

        [TestMethod]
        public void TestMatImageOperation() {
            var image = IpcImage.LoadFromFile(ImageUris.BGR32);
            var mat = image.GetOpenCv();

            //In place operations may not work for convolutions as the row/column data changes with each pass
            var temp = mat.GaussianBlur(new Size(3,3), 4, 4,BorderTypes.Replicate);
            temp.CopyTo(mat);

            for (int rowIndex = 0; rowIndex < mat.Height; rowIndex++) {
                for (int columnIndex = 0; columnIndex < mat.Width; columnIndex++) {

                    var value = mat.Get<uint>(rowIndex, columnIndex);
                    Console.Write(value.ToString("x") + " ");

                    if (rowIndex == 0) Assert.AreEqual(0xfd535353, value);
                    if (rowIndex == 1) Assert.AreEqual(0xfd565656, value);
                    if (rowIndex == 4) Assert.AreEqual(0xfda7a7d2, value);

                }
                Console.WriteLine("");
            }

        }
    }

}
    

