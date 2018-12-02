using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Gstc.Imaging;
using Gstc.Imaging.Net;
using Gstc.Imaging.UnitTest.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtsc.Imaging.Net.UnitTest {
    [TestClass]
    public class TestBitmapSource {

        [TestMethod]
        public void InteropBitmapTest() {
            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.Bgr32);
            var bitmap = ipcImage.GetInteropBitmap();
            var pixelArray = new UInt32[50];
            bitmap.CopyPixels(new Int32Rect(0,0,5,10), pixelArray, ipcImage.Stride,0);

            for (var i = 0; i < 50; i++) {
                var value = pixelArray[i];
                if (i >=0 && i < 5) Assert.AreEqual(0xFF000000,value);    //row 0
                if (i >= 5 && i < 10) Assert.AreEqual(0xFFFFFFFF, value); //row 1
                if (i >= 20 && i < 25) Assert.AreEqual(0xFF01017F, value); // row 4
                if (i >= 30 && i < 35) Assert.AreEqual(0xFF017F01, value); // row 6
                if (i >= 40 && i < 45) Assert.AreEqual(0xFF7F0101, value); // row 8
                if (i % 5 == 0) Console.WriteLine("\n");
                Console.Write(value.ToString("X") + "-");               
            }
        }

        [TestMethod]
        public void WriteableBitmapTest() {
            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile(ImageUris.Bgr32);
            var bitmap = ipcImage.GetWriteableBitmap();
            var pixelArray = new UInt32[50];
            bitmap.CopyPixels(new Int32Rect(0, 0, 5, 10), pixelArray, ipcImage.Stride, 0);

            for (var i = 0; i < 50; i++) {
                var value = pixelArray[i];
                if (i >= 0 && i < 5) Assert.AreEqual(0xFF000000, value);    //row 0
                if (i >= 5 && i < 10) Assert.AreEqual(0xFFFFFFFF, value); //row 1
                if (i >= 20 && i < 25) Assert.AreEqual(0xFF01017F, value); // row 4
                if (i >= 30 && i < 35) Assert.AreEqual(0xFF017F01, value); // row 6
                if (i >= 40 && i < 45) Assert.AreEqual(0xFF7F0101, value); // row 8
                if (i % 5 == 0) Console.WriteLine("\n");
                Console.Write(value.ToString("X") + "-");
            }
        }

        [TestMethod]
        public void InteropBitmapMemoryDiagnostic() {

            List<double> memoryList = new List<double>();
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);
            
            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile( ImageUris.LargeBmp );

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            var pixelArray = new UInt32[2048 * 2048];
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            var random = new Random();
            for (uint i = 0; i < pixelArray.Length; i++) pixelArray[i] = (uint)random.Next(Int32.MaxValue);
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);


            var bitmap = ipcImage.GetInteropBitmap();
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            bitmap.CopyPixels(new Int32Rect(0, 0, 2048, 2048), pixelArray, ipcImage.Stride, 0);
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            bitmap.Invalidate();
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            for (var i = 0; i < 10; i++) {
                bitmap.Invalidate();
                bitmap.CopyPixels(new Int32Rect(0, 0, 2048, 2048), pixelArray, ipcImage.Stride, 0);
                memoryList.Add(Process.GetCurrentProcess().WorkingSet64);
            }

            Console.WriteLine("Initial: " + memoryList[0]);
            Console.WriteLine("Ipc Image: " + memoryList[1] + " : " + (memoryList[1] - memoryList[0]) );
            Console.WriteLine("Array Init: " + memoryList[2] + " : " + (memoryList[2] - memoryList[1]));
            Console.WriteLine("ArrayFill: " + memoryList[3] + " : " + (memoryList[3] - memoryList[2]));
            Console.WriteLine("Bitmap: " + memoryList[4] + " : " + (memoryList[4] - memoryList[3]));
            Console.WriteLine("Copy: " + memoryList[5] + " : " + (memoryList[5] - memoryList[4]));
            Console.WriteLine("Invalidate: " + memoryList[6] + " : " + (memoryList[6] - memoryList[5]));
            Console.WriteLine("Loop: ");
            
            for (var i = 7; i < memoryList.Count; i++ ) Console.WriteLine(memoryList[i] + " : " + (memoryList[i]-memoryList[i-1]));

        }

        [TestMethod]
        public void WriteableBitmapMemoryDiagnostic() {
            List<double> memoryList = new List<double>();
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            IpcImageFileMapped ipcImage = IpcImageFileMapped.LoadFromFile( ImageUris.LargeBmp );

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForFullGCComplete();
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            var pixelArray = new UInt32[2048 * 2048];
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            var random = new Random();
            for (uint i = 0; i < pixelArray.Length; i++) pixelArray[i] = (uint)random.Next(Int32.MaxValue);
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);


            var bitmap = ipcImage.GetWriteableBitmap();
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            bitmap.CopyPixels(new Int32Rect(0, 0, 2048, 2048), pixelArray, ipcImage.Stride, 0);
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);
         
            bitmap.Lock();
            bitmap.AddDirtyRect(new Int32Rect(0, 0, 2048, 2048));
            bitmap.Unlock();
            memoryList.Add(Process.GetCurrentProcess().WorkingSet64);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForFullGCComplete();
            ;            for (var i = 0; i < 10; i++) {
                bitmap.Lock();
                bitmap.AddDirtyRect(new Int32Rect(0, 0, 2048, 2048));
                bitmap.Unlock();
                bitmap.CopyPixels(new Int32Rect(0, 0, 2048, 2048), pixelArray, ipcImage.Stride, 0);
                memoryList.Add(Process.GetCurrentProcess().WorkingSet64);
            }

            Console.WriteLine("Initial: " + memoryList[0]);
            Console.WriteLine("Ipc Image: " + memoryList[1] + " : " + (memoryList[1] - memoryList[0]));
            Console.WriteLine("Array Init: " + memoryList[2] + " : " + (memoryList[2] - memoryList[1]));
            Console.WriteLine("ArrayFill: " + memoryList[3] + " : " + (memoryList[3] - memoryList[2]));
            Console.WriteLine("Bitmap: " + memoryList[4] + " : " + (memoryList[4] - memoryList[3]));
            Console.WriteLine("Copy: " + memoryList[5] + " : " + (memoryList[5] - memoryList[4]));
            Console.WriteLine("Invalidate: " + memoryList[6] + " : " + (memoryList[6] - memoryList[5]));
            Console.WriteLine("Loop: ");

            for (var i = 7; i < memoryList.Count; i++) Console.WriteLine(memoryList[i] + " : " + (memoryList[i] - memoryList[i - 1]));


        }

    }
    
}
