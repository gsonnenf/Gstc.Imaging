using System;
using System.Collections.Generic;
using System.Globalization;
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

        [TestMethod]
        public void DictionarySearch() {
            List<int> pixelChannelList = new List<int> {1, 3, 4};
            List<int> bitLengthList = new List<int> {8, 16, 32, 64};
            List<IpcChannelDataType> DataTypeList =
                new List<IpcChannelDataType>() {IpcChannelDataType.Float, IpcChannelDataType.UInt};
            foreach (var dataType in DataTypeList) {
                foreach (var bitLength in bitLengthList) {
                    foreach (var pixelChannel in pixelChannelList) {
                        try {
                            IpcPixelFormats.GetMatchingFormat(pixelChannel, bitLength, dataType);
                            Console.WriteLine("Success on Channel:" + pixelChannel + "  bitsize: " + bitLength + "  Datatype: " + dataType);
                        } 
                        catch { Assert.Fail("Fail on Channel:" + pixelChannel + "  bitsize: "  + bitLength + "  Datatype: " + dataType);}
                    }
                }
            }
        }
    }
}
