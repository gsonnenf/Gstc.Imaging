using System;
using System.Collections;
using System.Diagnostics;
using nom.tam.fits;

namespace Gstc.Imaging.Fits
{

    public partial class FitsData {

     
        public DiagnosticsClass Diagnostics => new DiagnosticsClass(this);

        public class DiagnosticsClass
        {
            private readonly FitsData _fitsData;

            public DiagnosticsClass(FitsData fitsData) { _fitsData = fitsData; }

            [Conditional("DEBUG")]
            public void PrintHeaderData() {
                foreach ( var item in _fitsData.HeaderCardDictionary )
                    Console.WriteLine("Key: " + item.Key + "   Value: " + item.Value + "   Comment:" + item.Comment );
            }
        }
    }

}
