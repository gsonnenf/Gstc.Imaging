using System;
using Gstc.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtsc.Imaging.UnitTest {
    [TestClass]
    public class PixelFormatTest {
        [TestMethod]
        public void DictionaryCheck() {
            Assert.AreNotEqual(0, IpcPixelFormats.DefaultFormatDictionary.Count);
            foreach (var item in IpcPixelFormats.DefaultFormatDictionary) {
                Console.WriteLine(item.Key + " : " + item.Value);
                Assert.AreEqual(typeof(IpcPixelFormatType), item.Key.GetType());
                Assert.AreEqual(typeof(IpcPixelFormatDefault), item.Value.GetType());
            }
        }
    }
}
