using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using nom.tam.fits;

namespace Gstc.Imaging.Fits {
    public class FitsData {

       
        public nom.tam.fits.Fits FitsDataStructure { get; set; }
        public ImageHDU PrimaryHdu { get; }
        public long BufferSize => PrimaryHdu.Header.DataSize;

        public int BitPix => PrimaryHdu.BitPix;
        public int[] Axes => PrimaryHdu.Axes;
        public IpcPixelFormat IpcPixelFormat { get; }
        public int PixelSize {get;}
        public int Channels { get; }
        public int Width { get; }
        public int Height { get; }

        public FitsData(Uri uri) {
            var fitsData = new nom.tam.fits.Fits(uri.OriginalString);
            PrimaryHdu = (ImageHDU)fitsData.ReadHDU();
            
            if (BufferSize > 4000000000) throw new NotSupportedException("Image Size greater than 4GB aren't supported.");

            if (BitPix != 8 && BitPix != 16 && BitPix != 32 && BitPix != -32 && BitPix != 64)
                throw new NotSupportedException("Bitpix field in pix is not 8,16,32,-32 or 64. BitSize " + BitPix + " is an unsupported value.");

            IpcChannelDataType dataType = IpcChannelDataType.Custom;
            if (BitPix > 0) dataType = IpcChannelDataType.UInt;
            if (BitPix < 0) dataType = IpcChannelDataType.Float;           
            PixelSize = Math.Abs(BitPix);


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

            IpcPixelFormat = IpcPixelFormats.GetMatchingFormat(Channels, BitPix, dataType);
       
        }
    }
}
