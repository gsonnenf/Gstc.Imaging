using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using nom.tam.fits;

namespace Gstc.Imaging.Fits {
    public partial class FitsData {


        public nom.tam.fits.Fits Fits { get; set; }
        public ImageHDU PrimaryHdu { get; }
        
        public long BufferSize => PrimaryHdu.Header.DataSize;

        public int BitPix => PrimaryHdu.BitPix;
        public int[] Axes => PrimaryHdu.Axes;
        public IpcPixelFormat IpcPixelFormat { get; }
        public int BitsPerPixel { get; }
        public int BytesPerPixel { get; }
        public int Channels { get; }
        public int Width { get; }
        public int Height { get; }

        public readonly List<HeaderCard> HeaderCardDictionary = new List<HeaderCard>();

        public FitsData(Uri uri, bool isInterleave = false) {

            var fitsData = new nom.tam.fits.Fits(uri.OriginalString);

            PrimaryHdu = (ImageHDU)fitsData.ReadHDU();

            if (Axes.Length == 2) {
                Channels = 1;
                Width = Axes[0];
                Height = Axes[1];
            }
            else if (Axes.Length == 3) {
                Channels = Axes[0];
                Width = Axes[1];
                Height = Axes[2];
            }

            if (BufferSize > 4294967296) throw new NotSupportedException("Image Size greater than 4GB aren't supported.");

            IpcChannelDataType dataType;
            if (BitPix == 8 || BitPix == 16 || BitPix == 32) dataType = IpcChannelDataType.UInt;
            else if (BitPix == -32 || BitPix == -64) dataType = IpcChannelDataType.Float;
            else throw new NotSupportedException("Bitpix field in pix is not 8,16,32,-32 or 64. BitSize " + BitPix + " is an unsupported value.");

            BitsPerPixel = Math.Abs(BitPix);
            BytesPerPixel = BitsPerPixel / 8;

            IpcPixelFormat = isInterleave ? 
                IpcPixelFormats.GetMatchingFormat(Channels, BitsPerPixel, dataType) : 
                IpcPixelFormats.GetMatchingFormat(1, BitsPerPixel, dataType);

            //Construct header card dictionary.
            foreach (DictionaryEntry card in PrimaryHdu.Header) HeaderCardDictionary.Add( (HeaderCard) card.Value);
         
        }

        public object GetImageArray() => PrimaryHdu.Kernel;

        //TODO: Implement interleave and make public
        internal void CopyImageInterleave(IntPtr bufferPtr) {         
                if (Channels != 1 && Channels != 3 && Channels != 4) throw new NotSupportedException("Interleave is only support for 1, 3 or 4 channels. Fits image has " + Channels + " channels.");
                throw new NotImplementedException("Interleave not implemented yet."); 
        }

        /*
        public void CopyImageMultipleChannels(List<IntPtr> bufferPtrList) {
            var imageArrayList = (Array[][])PrimaryHdu.Kernel;
            if (imageArrayList.Length < bufferPtrList.Count) throw new ArgumentOutOfRangeException("More image buffers than available channels.");
            for (var i = 0; i < bufferPtrList.Count; i++) CopyImageArray(bufferPtrList[i], imageArrayList[i] );         
        }
        */

        public void CopyImageSingleChannel(IntPtr bufferPtr, int channel = 0) {
            if (channel >= Channels) throw new ArgumentOutOfRangeException("Specified channel " + channel + " exceeds available channels. (" + Channels + ")");

            if (Channels != 1) {
                var imageArrays = (Array[]) PrimaryHdu.Kernel;
                CopyImageArray(bufferPtr, (Array[]) imageArrays[channel]);
            }
            else CopyImageArray(bufferPtr, (Array[]) PrimaryHdu.Kernel);
        }

        protected void CopyImageArray(IntPtr bufferPtr, Array[] imageArray ) {
            for (int rowIndex = 0; rowIndex < Height; rowIndex++) {
                int offset = rowIndex * Height * BytesPerPixel;
                IntPtr ptr = IntPtr.Add(bufferPtr, offset); //TODO: Width instead of height?

                if (imageArray[rowIndex] is Single[] array) Marshal.Copy(array, 0, ptr, Width);
                else if (imageArray[rowIndex] is Double[] array2) Marshal.Copy(array2, 0, ptr, Width);
                else if (imageArray[rowIndex] is Int32[] array3) Marshal.Copy(array3, 0, ptr, Width);
                else if (imageArray[rowIndex] is Int64[] array4) Marshal.Copy(array4, 0, ptr, Width);
                else throw new NotSupportedException("Image array is of an unexpected type: " + imageArray.GetType().Name );
            }
        }
    }
}

