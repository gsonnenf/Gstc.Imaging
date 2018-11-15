using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Imaging.Extensions {
    public enum ImageFormatType {
        Bmp,
        Jpeg,
        Png,
        Gif,
        Tiff,
        Exif,
        Emf,
        Wmf,
        MemoryBMP,
        PhotoCD,
        FlashPIX,
        Icon,
        
    }

    public static class ImageFormatExtensions {

    /*
    using (Image sourceImage = Image.FromStream(stream, false, false)) {
    var imageFormatType = ImageFormatExtensions.ImageFormatGuidTypeDictionary[sourceImage.RawFormat.Guid];
    */
    public static Dictionary<Guid, ImageFormatType> ImageFormatGuidTypeDictionary = new Dictionary<Guid, ImageFormatType>() {
            [new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.MemoryBMP,
            [new Guid("{b96b3cab-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Bmp,
            [new Guid("{b96b3cac-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Emf,
            [new Guid("{b96b3cad-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Wmf,
            [new Guid("{b96b3cae-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Jpeg,
            [new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Png,
            [new Guid("{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Gif,
            [new Guid("{b96b3cb1-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Tiff,
            [new Guid("{b96b3cb2-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Exif,
            [new Guid("{b96b3cb3-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.PhotoCD,
            [new Guid("{b96b3cb4-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.FlashPIX,
            [new Guid("{b96b3cb5-0728-11d3-9d7b-0000f81ef32e}")] = ImageFormatType.Icon
        };


        public static Dictionary<byte[], string> ImageFormatDictionary = new Dictionary<byte[], string> {
            [new byte[] { 0x42, 0x4D }] = "bmp",
            [new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }] = "gif87a",
            [new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }] = "gif89a",
            [new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }] = "png",
            [new byte[] { 0x49, 0x49, 0x2A, 0x00 }] = "tiffI",
            [new byte[] { 0x4D, 0x4D, 0x00, 0x2A }] = "tiffM",
            [new byte[] { 0xFF, 0xD8, 0xFF }] = "jpeg",
            //[new byte[] { 0xFF, 0xD9 }] = "jpegEnd"
        };

        public static byte[] JpegEnd = new byte[] { 0xFF, 0xD9 };

        /// <summary>
        /// Reads the header of different image formats
        /// </summary>
        /// <param name="file">Image file</param>
        /// <returns>true if valid file signature (magic number/header marker) is found</returns>
        private static string ImageFileType(string file) {
            byte[] buffer = new byte[8];
            byte[] bufferEnd = new byte[2];


            using (System.IO.FileStream fs =
                new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read)) {
                if (fs.Length > buffer.Length) {
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Position = (int)fs.Length - bufferEnd.Length;
                    fs.Read(bufferEnd, 0, bufferEnd.Length);
                }

                fs.Close();
            }

            foreach (var format in ImageFormatDictionary) {
                if (!ByteArrayStartsWith(buffer, format.Key)) continue;
                if (format.Value == "jpeg" && !ByteArrayStartsWith(bufferEnd, JpegEnd)) continue;
                return format.Value;
            }

            throw new FileFormatException("A supported image header was not present.");
        }

        /// <summary>
        /// Returns a value indicating whether a specified subarray occurs within array
        /// </summary>
        /// <param name="a">Main array</param>
        /// <param name="b">Subarray to seek within main array</param>
        /// <returns>true if a array starts with b subarray or if b is empty; otherwise false</returns>
        private static bool ByteArrayStartsWith(byte[] a, byte[] b) {
            if (a.Length < b.Length) {
                return false;
            }

            for (int i = 0; i < b.Length; i++) {
                if (a[i] != b[i]) {
                    return false;
                }
            }

            return true;
        }
    }
}
