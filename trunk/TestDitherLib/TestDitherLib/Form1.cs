using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDitherLib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Bitmap bmpSrc = (Bitmap)Image.FromFile("testSrc.jpg");
            Bitmap bmpDes = (Bitmap)((bmpSrc != null) ? new Bitmap(bmpSrc.Width, bmpSrc.Height) : null);

            if (bmpSrc != null)
            {
                Dither dither = new Dither();
                int iRet = dither.invoke(bmpSrc, bmpDes);
            }
        }
    }
}
