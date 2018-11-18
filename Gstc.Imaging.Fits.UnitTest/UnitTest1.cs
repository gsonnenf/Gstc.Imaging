using System;
using Gstc.Imaging.Fits.UnitTest.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gstc.Imaging.Fits.UnitTest {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void LoadSingleFrame() {
            IpcImageFits ipcImage = IpcImageFits.LoadFromFile(ImageUris.FitsSingleChannel1024x1024_32f);
        }

        public void LoadMultipleFrames() {
            IpcImageFits ipcImage = IpcImageFits.LoadFromFile(ImageUris.FitsSingleChannel1024x1024_32f);
        }
    }
}
