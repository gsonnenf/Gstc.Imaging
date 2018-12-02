using System;
using Gstc.Imaging.UnitTest.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gstc.Imaging.Fits.UnitTest {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void LoadSingleFrame() {
            var ipcImage = IpcImageFits.LoadFromFileSingleChannel(ImageUris.FitsSingleChannel1024X1024Float32);
        }

        [TestMethod]
        public void LoadMultipleFrames() {
            var ipcImageList = IpcImageFits.LoadFromFileMultiChannel(ImageUris.FitsMultiChannel200X200Float32);           
        }
    }
}
