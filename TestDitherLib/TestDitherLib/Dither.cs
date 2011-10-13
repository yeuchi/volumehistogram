// References:
//  http://stackoverflow.com/questions/4393669/c-calling-unmanaged-code
//  Marshal string http://stackoverflow.com/questions/2380594/passing-c-string-to-unmanaged-c-dll
//  Lock Bitmap http://msdn.microsoft.com/en-us/library/5ey6h79d.aspx
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace TestDitherLib
{
     unsafe class Dither
    {
         public const String FLOYD_STEINBERG = "floyd steinberg";

        [DllImport("DitherLibrary.dll")]
         public static extern int apply([MarshalAs(UnmanagedType.LPStr)]string szBuf, IntPtr bmpSrc, IntPtr bmpDes);

        public Dither()
        {
        }

        public int invoke(Bitmap bmpSrc, Bitmap bmpDes)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, bmpSrc.Width, bmpSrc.Height);
                BitmapData bmpSrcData = bmpSrc.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpSrc.PixelFormat);
                BitmapData bmpDesData = bmpDes.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpDes.PixelFormat);

                // Get the address of the first line.
                IntPtr ptrSrc = bmpSrcData.Scan0;
                IntPtr ptrDes = bmpDesData.Scan0;

                int iRet = apply(FLOYD_STEINBERG, ptrSrc, ptrDes);

                bmpSrc.UnlockBits(bmpSrcData);
                bmpDes.UnlockBits(bmpDesData);
                return iRet;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
