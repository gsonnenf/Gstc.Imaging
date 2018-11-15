using System;
using System.ComponentModel;
using Gstc.Imaging;
using Gtsc.Imaging.UnitTest.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtsc.Imaging.UnitTest {
    [TestClass]
    public class TestLoadFile {
        [TestMethod]
        public void LoadImageTestBgr32() {

            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.BGR32);

            Console.WriteLine(ipcImage.Info());
            unsafe {
                byte* pointer = (byte*)ipcImage.DataPtr.ToPointer();
                for (int rowIndex = 0; rowIndex < ipcImage.Height; rowIndex++) {
                    for (int columnIndex = 0; columnIndex < ipcImage.Width*ipcImage.BytesPerPixel ; columnIndex++) {
                        var value = *(pointer + columnIndex + rowIndex * ipcImage.Stride);
                        if ((columnIndex % (int)ipcImage.BytesPerPixel) == 0) Console.Write(" - ");
                        Console.Write(value + " ");
                        
                        if (rowIndex == 0 && (columnIndex % 4) == 0) Assert.AreEqual(0, value,"Black row failed.");
                        if (rowIndex == 1) Assert.AreEqual(255, value, "White row failed.");
                        if (rowIndex == 4 && (columnIndex % 4) == 0) Assert.AreEqual(127, value,"Blue row failed.");
                        if (rowIndex == 6 && ((columnIndex-1) % 4) == 0) Assert.AreEqual(127, value, "Green row failed.");
                        if (rowIndex == 8 && ((columnIndex-2) % 4) == 0) Assert.AreEqual(127, value, "Redd row failed.");
                        if (rowIndex == 9) Assert.AreEqual(255, value,"Closing white row failed");
                    }
                    Console.Write("\n");                   
                }
            }
        }

        [TestMethod]
        public void LoadImageTestBgr24() {

            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.BGR24);

            Console.WriteLine(ipcImage.Info());

            unsafe {
                byte* pointer = (byte*)ipcImage.DataPtr.ToPointer();
                for (int rowIndex = 0; rowIndex < ipcImage.Height; rowIndex++) {
                    for (int columnIndex = 0; columnIndex < ipcImage.Width * ipcImage.BytesPerPixel; columnIndex++) {
                        var value = *(pointer + columnIndex + rowIndex * ipcImage.Stride);
                        if ((int)(columnIndex % ipcImage.BytesPerPixel) == 0) Console.Write(" - ");
                        Console.Write(value + " ");

                        if (rowIndex == 0 && (columnIndex % 3) == 0) Assert.AreEqual(0, value, "Black row failed.");
                        if (rowIndex == 1) Assert.AreEqual(255, value, "White row failed.");
                        if (rowIndex == 4 && (columnIndex % 3) == 0) Assert.AreEqual(127, value, "Blue row failed.");
                        if (rowIndex == 6 && ((columnIndex - 1) % 3) == 0) Assert.AreEqual(127, value, "Green row failed.");
                        if (rowIndex == 8 && ((columnIndex - 2) % 3) == 0) Assert.AreEqual(127, value, "Redd row failed.");
                        if (rowIndex == 9) Assert.AreEqual(255, value, "Closing white row failed");
                    }
                    Console.Write("\n");
                }
            }
        }

        [TestMethod]
        public void LoadImageTestGrayScale8Bpp() {

            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile( ImageUris.Grayscale8bpp );

            Console.WriteLine(ipcImage.Info());
            unsafe {
                byte* pointer = (byte*)ipcImage.DataPtr.ToPointer();
                for (int rowIndex = 0; rowIndex < ipcImage.Height; rowIndex++) {
                    for (int columnIndex = 0; columnIndex < ipcImage.Width * ipcImage.BytesPerPixel; columnIndex++) {
                        var value = *(pointer + columnIndex + rowIndex * ipcImage.Stride);
                        if ((columnIndex % (int)ipcImage.BytesPerPixel) == 0) Console.Write(" - ");
                        Console.Write(value + " ");

                        if (rowIndex == 0) Assert.AreEqual(0, value, "Black row failed.");
                        if (rowIndex == 1) Assert.AreEqual(50, value, "Grey50 row failed.");
                        if (rowIndex == 2) Assert.AreEqual(100, value, "Grey100 row failed.");
                        if (rowIndex == 3) Assert.AreEqual(150, value, "Grey150 row failed.");
                        if (rowIndex == 4) Assert.AreEqual(200, value, "Grey200 row failed.");
                        if (rowIndex == 5) Assert.AreEqual(255, value, "White row failed.");

                    }
                    Console.Write("\n");
                }
            }
        }
    }


}
