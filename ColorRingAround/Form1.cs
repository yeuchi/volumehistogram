// ============================================================================
// Module:		Form1.cs
//
// Description:	Parent window of the application
//				Color Ring Around - application for color adjustment
//
// Purpose:		Demonstration of dialog application with C#
//
// Input:		Image of type JPG, PNG, TIF, BMP, GIF (defined by C# library)
// Output:		Input image with desired color shift
//
// Author:		Chi Toung (C.T.) Yeung		- cty
//
// History: 
// 22Dec06		1st completion											cty
// 10Jan06		test statement for image available for color shift		cty
// ============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;




namespace ColorRingAround
{
	

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		Image m_Img_O, m_Img_T;								// Original image, thumbnail
		Image m_Img_E;										// Edited image (thumbnail size)

		Image m_Img_R, m_Img_G, m_Img_B;					// images with color shift ( thumbnail size)
		Image m_Img_Y, m_Img_M, m_Img_C;					// images with color shift ( thumbnail size)

		int AdjAmount			= 20;						// Default amount for color shift
		const int PIC_BOX_SIZE	= 110;						// Default picture box size

		// image display
		private System.Windows.Forms.PictureBox pictureBox_R;
		private System.Windows.Forms.PictureBox pictureBox_C;
		private System.Windows.Forms.PictureBox pictureBox_N;
		private System.Windows.Forms.PictureBox pictureBox_M;
		private System.Windows.Forms.PictureBox pictureBox_G;
		private System.Windows.Forms.PictureBox pictureBox_B;
		private System.Windows.Forms.PictureBox pictureBox_Y;
		private System.Windows.Forms.PictureBox pictureBox_O;
		private System.Windows.Forms.PictureBox pictureBox_E;

		// buttons
		private System.Windows.Forms.Button button_Browse;
		private System.Windows.Forms.Button button_SaveAs;
		private System.Windows.Forms.Button button_About;
		private System.Windows.Forms.Button button_BackClr;

		private System.Windows.Forms.GroupBox groupBox3;

		// Standar file dialog boxes
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.TrackBar trackBar1;

		// group box
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox_Adjust;
		private System.Windows.Forms.GroupBox groupBox_Progress;
		
		// text box
		private System.Windows.Forms.TextBox textBox1;

		// text labels
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label_R_R;
		private System.Windows.Forms.Label label_G_R;
		private System.Windows.Forms.Label label_B_R;
		private System.Windows.Forms.Label label_Y_R;
		private System.Windows.Forms.Label label_C_R;
		private System.Windows.Forms.Label label_M_R;
		private System.Windows.Forms.Label label_N_R;
		private System.Windows.Forms.Label label_R_G;
		private System.Windows.Forms.Label label_R_B;
		private System.Windows.Forms.Label label_M_G;
		private System.Windows.Forms.Label label_M_B;
		private System.Windows.Forms.Label label_B_G;
		private System.Windows.Forms.Label label_B_B;
		private System.Windows.Forms.Label label_N_G;
		private System.Windows.Forms.Label label_N_B;
		private System.Windows.Forms.Label label_C_G;
		private System.Windows.Forms.Label label_C_B;
		private System.Windows.Forms.Label label_Y_G;
		private System.Windows.Forms.Label label_Y_B;
		private System.Windows.Forms.Label label_G_G;
		private System.Windows.Forms.Label label_G_B;
		private System.Windows.Forms.Label label_O;
		private System.Windows.Forms.Label label_R;
		private System.Windows.Forms.Label label_N;
		private System.Windows.Forms.Label label_Y;
		private System.Windows.Forms.Label label_G;
		private System.Windows.Forms.Label label_B;
		private System.Windows.Forms.Label label_C;
		private System.Windows.Forms.Label label_M;
		private System.Windows.Forms.Label label_E;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Event handlers
			button_Browse.Click += new EventHandler ( File_Open );
			button_SaveAs.Click += new EventHandler ( File_SaveAs );
			button_BackClr.Click += new EventHandler ( Set_Back_Color );
			button_About.Click += new EventHandler ( About );

			pictureBox_R.MouseHover += new EventHandler ( Add_Red_Hoover );
			pictureBox_R.MouseLeave += new EventHandler ( Add_Red_Leave );
			pictureBox_G.MouseHover += new EventHandler ( Add_Green_Hoover );
			pictureBox_G.MouseLeave += new EventHandler ( Add_Green_Leave );
			pictureBox_B.MouseHover += new EventHandler ( Add_Blue_Hoover );
			pictureBox_B.MouseLeave += new EventHandler ( Add_Blue_Leave );
			pictureBox_Y.MouseHover += new EventHandler ( Add_Yellow_Hoover );
			pictureBox_Y.MouseLeave += new EventHandler ( Add_Yellow_Leave );
			pictureBox_M.MouseHover += new EventHandler ( Add_Magenta_Hoover );
			pictureBox_M.MouseLeave += new EventHandler ( Add_Magenta_Leave );
			pictureBox_C.MouseHover += new EventHandler ( Add_Cyan_Hoover );
			pictureBox_C.MouseLeave += new EventHandler ( Add_Cyan_Leave );
			pictureBox_O.MouseHover += new EventHandler ( Add_Original_Hoover );
			pictureBox_O.MouseLeave += new EventHandler ( Add_Original_Leave );

			pictureBox_R.Click += new EventHandler ( Add_Red );
			pictureBox_G.Click += new EventHandler ( Add_Green );
			pictureBox_B.Click += new EventHandler ( Add_Blue );
			pictureBox_M.Click += new EventHandler ( Add_Magenta );
			pictureBox_Y.Click += new EventHandler ( Add_Yellow );
			pictureBox_C.Click += new EventHandler ( Add_Cyan );
			pictureBox_O.Click += new EventHandler ( Revert );

			trackBar1.ValueChanged += new EventHandler ( NewAdjustAmount );
			textBox1.TextChanged += new EventHandler ( NewAdjustAmount2 );

			// Trackbar Initialization 
			trackBar1.Minimum = Convert.ToInt16(label10.Text);
			trackBar1.Maximum = Convert.ToInt16(label11.Text);
			trackBar1.Value = AdjAmount;

			// Default background color
			groupBox_Adjust.BackColor = Color.FromArgb(192, 192, 192);
			groupBox_Progress.BackColor = Color.FromArgb(192, 192, 192);

			// Initialize labels
			InitLabels();
		}

		/// //////////////////////////////////////////////////////////////////////////////////////
		/// // Initialize values
		
		// ====================================================================
		// Description:	Initialize all labels with zero
		// Return:		void
		void InitLabels()
		// ====================================================================
		{
			label_R_R.Text = label_G_R.Text = label_B_R.Text = label_C_R.Text = label_M_R.Text = label_Y_R.Text = label_N_R.Text = "R = 0";
			label_R_G.Text = label_G_G.Text = label_B_G.Text = label_C_G.Text = label_M_G.Text = label_Y_G.Text = label_N_G.Text = "G = 0";
			label_R_B.Text = label_G_B.Text = label_B_B.Text = label_C_B.Text = label_M_B.Text = label_Y_B.Text = label_N_B.Text = "B = 0";
		}
		
		// ====================================================================
		// Description:	"Never used" call back method for thumbnail creation
		// Return:		void
		public bool ThumbnailCallback()
		// ====================================================================
		{
			return false;
		}

		// ====================================================================
		// Description:	Initialize all the images, their picture boxes and
		//				color shift
		// Return:		void
		void InitAll ()
			// ====================================================================
		{
			this.Cursor = Cursors.WaitCursor;

			InitLabels();

			// Dispose images if exist
			if ( m_Img_E != null )
				m_Img_E.Dispose();

			if ( m_Img_R != null )
				m_Img_R.Dispose();

			if ( m_Img_G != null )
				m_Img_G.Dispose();

			if ( m_Img_B != null )
				m_Img_B.Dispose();

			if ( m_Img_Y != null )
				m_Img_Y.Dispose();

			if ( m_Img_M != null )
				m_Img_M.Dispose();

			if ( m_Img_C != null )
				m_Img_C.Dispose();
		
			int Height, Width;
			int XOff, YOff;

			Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

			// calculate thumbnail size
			if ( m_Img_O.Width <  m_Img_O.Height )
			{
				Height = PIC_BOX_SIZE;
				Width = (int)((double)PIC_BOX_SIZE * (double)m_Img_O.Width / (double)m_Img_O.Height );
				m_Img_E = (Image)m_Img_O.GetThumbnailImage( Width, Height, myCallback, IntPtr.Zero );
				XOff = ( PIC_BOX_SIZE - Width ) / 2;
				YOff = 0;
			}
			else 
			{
				Width = PIC_BOX_SIZE;
				Height = (int)((double)PIC_BOX_SIZE * (double)m_Img_O.Height / (double)m_Img_O.Width );
				m_Img_E = (Image)m_Img_O.GetThumbnailImage( Width, Height, myCallback, IntPtr.Zero );
				XOff = 0;
				YOff = ( PIC_BOX_SIZE - Height ) / 2;
			}
				
			m_Img_T = (Image)m_Img_E.Clone();

			// create ring around images
			m_Img_R = (Image)m_Img_E.Clone();
			AdjustImage ( (Bitmap)m_Img_R, AdjAmount, 0, 0 );

			m_Img_G = (Image)m_Img_E.Clone();
			AdjustImage ( (Bitmap)m_Img_G, 0, AdjAmount, 0 );

			m_Img_B = (Image)m_Img_E.Clone();
			AdjustImage ( (Bitmap)m_Img_B, 0, 0, AdjAmount );

			m_Img_Y = (Image)m_Img_E.Clone();
			//AdjustImage ( (Bitmap)m_Img_Y, AdjAmount, AdjAmount, 0 );
			AdjustImage ( (Bitmap)m_Img_Y, 0, 0, -1*AdjAmount );

			m_Img_M = (Image)m_Img_E.Clone();
			//AdjustImage ( (Bitmap)m_Img_M, AdjAmount, 0, AdjAmount );			
			AdjustImage ( (Bitmap)m_Img_M, 0, -1*AdjAmount, 0 );
			
			m_Img_C = (Image)m_Img_E.Clone();
			//AdjustImage ( (Bitmap)m_Img_C, 0, AdjAmount, AdjAmount );
			AdjustImage ( (Bitmap)m_Img_C, -1*AdjAmount, 0, 0 );
			
			// insert images into picture boxes
			// NOTE: magic number are pictureboxes' location(x,y)co-ordinates
			pictureBox_O.Image = m_Img_T;
			pictureBox_O.Bounds = new Rectangle ( 16+XOff, 40+YOff, Width, Height); 
			pictureBox_O.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_R.Image = m_Img_R;
			pictureBox_R.Bounds = new Rectangle ( 216+XOff, 24+YOff, Width, Height); 
			pictureBox_R.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_G.Image = m_Img_G;
			pictureBox_G.Bounds = new Rectangle ( 392+XOff, 296+YOff, Width, Height); 
			pictureBox_G.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_B.Image = m_Img_B;
			pictureBox_B.Bounds = new Rectangle ( 40+XOff, 296+YOff, Width, Height); 
			pictureBox_B.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_Y.Image = m_Img_Y;
			pictureBox_Y.Bounds = new Rectangle ( 392+XOff, 120+YOff, Width, Height); 
			pictureBox_Y.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_M.Image = m_Img_M;
			pictureBox_M.Bounds = new Rectangle ( 40+XOff, 120+YOff, Width, Height); 
			pictureBox_M.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_C.Image = m_Img_C;
			pictureBox_C.Bounds = new Rectangle ( 216+XOff, 384+YOff, Width, Height); 
			pictureBox_C.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_E.Image = m_Img_E;
			pictureBox_E.Bounds = new Rectangle ( 16+XOff, 184+YOff, Width, Height); 
			pictureBox_E.SizeMode = PictureBoxSizeMode.StretchImage;

			pictureBox_N.Image = m_Img_E;
			pictureBox_N.Bounds = new Rectangle ( 216+XOff, 208+YOff, Width, Height); 
			pictureBox_N.SizeMode = PictureBoxSizeMode.StretchImage;
			this.Cursor = Cursors.Default;
		}

		/// //////////////////////////////////////////////////////////////////////////////////////
		/// // picture Box borders
		
		// ====================================================================
		// Description:	user mover cursor over the picture
		//				change border to solid color to show selection
		// Return:		void
		void Add_Red_Hoover (object sender, EventArgs e)
		// ====================================================================
		{
			pictureBox_R.BorderStyle = BorderStyle.FixedSingle;
		}

		// ====================================================================
		// Description:	user mover cursor move away from picture
		//				change border back to 3-D to show de-selection
		// Return:		void
		void Add_Red_Leave (object sender, EventArgs e)
		// ====================================================================
		{
			pictureBox_R.BorderStyle = BorderStyle.Fixed3D;
		}

		// ====================================================================
		// Description:	user mover cursor over the picture
		//				change border to solid color to show selection
		// Return:		void
		void Add_Green_Hoover (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_G.BorderStyle = BorderStyle.FixedSingle;
		}

		// ====================================================================
		// Description:	user mover cursor move away from picture
		//				change border back to 3-D to show de-selection
		// Return:		void
		void Add_Green_Leave (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_G.BorderStyle = BorderStyle.Fixed3D;
		}

		// ====================================================================
		// Description:	user mover cursor over the picture
		//				change border to solid color to show selection
		// Return:		void
		void Add_Blue_Hoover (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_B.BorderStyle = BorderStyle.FixedSingle;
		}

		// ====================================================================
		// Description:	user mover cursor move away from picture
		//				change border back to 3-D to show de-selection
		// Return:		void
		void Add_Blue_Leave (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_B.BorderStyle = BorderStyle.Fixed3D;
		}

		// ====================================================================
		// Description:	user mover cursor over the picture
		//				change border to solid color to show selection
		// Return:		void
		void Add_Yellow_Hoover (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_Y.BorderStyle = BorderStyle.FixedSingle;
		}

		// ====================================================================
		// Description:	user mover cursor move away from picture
		//				change border back to 3-D to show de-selection
		// Return:		void
		void Add_Yellow_Leave (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_Y.BorderStyle = BorderStyle.Fixed3D;
		}

		// ====================================================================
		// Description:	user mover cursor over the picture
		//				change border to solid color to show selection
		// Return:		void
		void Add_Magenta_Hoover (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_M.BorderStyle = BorderStyle.FixedSingle;
		}

		// ====================================================================
		// Description:	user mover cursor move away from picture
		//				change border back to 3-D to show de-selection
		// Return:		void
		void Add_Magenta_Leave (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_M.BorderStyle = BorderStyle.Fixed3D;
		}

		// ====================================================================
		// Description:	user mover cursor over the picture
		//				change border to solid color to show selection
		// Return:		void
		void Add_Cyan_Hoover (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_C.BorderStyle = BorderStyle.FixedSingle;
		}

		// ====================================================================
		// Description:	user mover cursor move away from picture
		//				change border back to 3-D to show de-selection
		// Return:		void
		void Add_Cyan_Leave (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_C.BorderStyle = BorderStyle.Fixed3D;
		}

		// ====================================================================
		// Description:	user mover cursor over the picture
		//				change border to solid color to show selection
		// Return:		void
		void Add_Original_Hoover (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_O.BorderStyle = BorderStyle.FixedSingle;
		}

		// ====================================================================
		// Description:	user mover cursor move away from picture
		//				change border back to 3-D to show de-selection
		// Return:		void
		void Add_Original_Leave (object sender, EventArgs e)
			// ====================================================================
		{
			pictureBox_O.BorderStyle = BorderStyle.Fixed3D;
		}
		/// //////////////////////////////////////////////////////////////////////////////////////
		/// // Buttons OR slider

		// ====================================================================
		// Description:	1) Open File Dialog
		//				2) Create a new form
		//				3) Open file and bitblit onto form
		// Return:		void
		void File_Open (object sender, EventArgs e)
			// ====================================================================
		{
			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "All files (*.*)|*.*|JPEG files (*.jpg)|*.jpg|PNG files (*.png)|*.png |TIFF files (*.tif)|*.tif|Bitmap files (*.bmp)|*.bmp|GIF files (*.gif)|*.gif" ;
			openFileDialog1.FilterIndex = 1 ;

			if ( openFileDialog1.ShowDialog() == DialogResult.OK)
			{		
				if ( m_Img_O != null )
					m_Img_O.Dispose();

				m_Img_O = Image.FromFile ( openFileDialog1.FileName );

				this.Text = openFileDialog1.FileName;
				InitAll();
			}
		}

		// ====================================================================
		// Description:	1) Open File Dialog
		//				2) Save image to file
		// Return:		void
		void File_SaveAs (object sender, EventArgs e)
			// ====================================================================
		{
			// set up dialog box
			string sName, sPath;
			int pos;

			if ( m_Img_E != null )									// make sure there is an image opened
			{
				if ( this.Text.Length == 0 )
				{
					saveFileDialog1.InitialDirectory = "c:\\";
					saveFileDialog1.FileName = "Default";
				}
				else
				{
					pos = this.Text.LastIndexOf ('\\');
					sName = this.Text.Remove( 0, pos+1 );
					sPath = this.Text.Remove (pos, this.Text.Length - pos );
					saveFileDialog1.InitialDirectory = sPath;
					saveFileDialog1.FileName = sName;
				}
				
				saveFileDialog1.Filter = "JPEG files (*.jpg)|*.jpg|PNG files (*.png)|*.png |TIFF files (*.tif)|*.tif|Bitmap files (*.bmp)|*.bmp|GIF files (*.gif)|*.gif|All files (*.*)|*.*" ;
				saveFileDialog1.FilterIndex = 1;
				
				// get image and write to disk
				if ( saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					AdjustOriginal();
					m_Img_O.Save (saveFileDialog1.FileName);
				}
			}
		}
		
		// ====================================================================
		// Description:	Set color for the background of the group boxes
		// Return:		void
		void Set_Back_Color(object sender, EventArgs e)
		// ====================================================================
		{
			ColorDialog dlg = new ColorDialog();
			if(dlg.ShowDialog() == DialogResult.OK)
			{
				groupBox_Adjust.BackColor = dlg.Color;
				groupBox_Progress.BackColor = dlg.Color;

				Color clr = Color.FromArgb(255-(dlg.Color.R), 255-(dlg.Color.G), 255-(dlg.Color.B));
				label_R_R.ForeColor = label_G_R.ForeColor = label_B_R.ForeColor = label_C_R.ForeColor = label_M_R.ForeColor = label_Y_R.ForeColor = label_N_R.ForeColor = clr;
				label_R_G.ForeColor = label_G_G.ForeColor = label_B_G.ForeColor = label_C_G.ForeColor = label_M_G.ForeColor = label_Y_G.ForeColor = label_N_G.ForeColor = clr;
				label_R_B.ForeColor = label_G_B.ForeColor = label_B_B.ForeColor = label_C_B.ForeColor = label_M_B.ForeColor = label_Y_B.ForeColor = label_N_B.ForeColor = clr;
			
				label_R.ForeColor = label_G.ForeColor = label_B.ForeColor = label_Y.ForeColor = label_M.ForeColor = label_C.ForeColor = label_O.ForeColor = label_N.ForeColor = label_E.ForeColor = clr;
			}
		}

		// ====================================================================
		// Description:	Invoke about dialog box
		// Return:		void
		void About(object sender, EventArgs e)
		// ====================================================================
		{
			About dlg = new About();
			dlg.Show();
		}

		// ====================================================================
		// Description:	change trackbar value
		// Return:		void
		void NewAdjustAmount (object sender, EventArgs e)
		// ====================================================================
		{
			AdjAmount = trackBar1.Value;
			textBox1.Text = Convert.ToString(trackBar1.Value );
			
			if ( m_Img_E != null )
				AdjustRingAround ();
		}

		// ====================================================================
		// Description:	change text box associated with trackbar
		// Return:		void
		void NewAdjustAmount2 (object sender, EventArgs e)
			// ====================================================================
		{
			trackBar1.Value = Convert.ToInt16(textBox1.Text);
		}

		/// //////////////////////////////////////////////////////////////////////////////////////
		/// // User makes selection

		// ====================================================================
		// Description:	Red image has been selected, add red to all images
		// Return:		void
		void Add_Red (object sender, EventArgs e)
		// ====================================================================
		{
			if ( m_Img_E != null )
				AdjustAll ( AdjAmount, 0, 0 );
		}

		// ====================================================================
		// Description:	Green image has been selected, add green to all images
		// Return:		void
		void Add_Green (object sender, EventArgs e)
		// ====================================================================
		{
			if ( m_Img_E != null )
				AdjustAll ( 0, AdjAmount, 0 );
		}

		// ====================================================================
		// Description:	Blue image has been selected, add blue to all images
		// Return:		void
		void Add_Blue (object sender, EventArgs e)
		// ====================================================================
		{
			if ( m_Img_E != null )
				AdjustAll ( 0, 0, AdjAmount );
		}

		// ====================================================================
		// Description:	Yellow image has been selected, add yellow to all images
		// Return:		void
		void Add_Yellow (object sender, EventArgs e)
		// ====================================================================
		{
			//AdjustAll ( AdjAmount, AdjAmount, 0 );
			if ( m_Img_E != null )
				AdjustAll ( 0, 0, -1*AdjAmount );

		}

		// ====================================================================
		// Description:	Magenta image has been selected, add magenta to all images
		// Return:		void
		void Add_Magenta (object sender, EventArgs e)
		// ====================================================================
		{
			//AdjustAll ( AdjAmount, 0, AdjAmount );
			if ( m_Img_E != null )
				AdjustAll ( 0, -1*AdjAmount, 0 );
			
		}

		// ====================================================================
		// Description:	Cyan image has been selected, add cyan to all images
		// Return:		void
		void Add_Cyan (object sender, EventArgs e)
		// ====================================================================
		{
			//AdjustAll ( 0, AdjAmount, AdjAmount );
			if ( m_Img_E != null )
				AdjustAll ( -1*AdjAmount, 0, 0 );
						
		}

		// ====================================================================
		// Description:	Original image has been selected, revert to default
		// Return:		void
		void Revert (object sender, EventArgs e)
		// ====================================================================
		{
			if ( m_Img_E != null )
			{
				AdjAmount = trackBar1.Value;
				InitAll ();
			}			
		}

		/// //////////////////////////////////////////////////////////////////////////////////////
		/// // Adjust image color

		// ====================================================================
		// Description:	Adjust the full size original image to the amount of 
		//				color shift desired before save
		// Return:		void
		void AdjustOriginal()
		// ====================================================================
		{
			this.Cursor = Cursors.WaitCursor;

			Color sClr;
			int R, G, B;
			StringPlus	sPlus = new StringPlus();
			int iRed, iGrn, iBlu;

			iRed = sPlus.AfterEqual(label_N_R.Text );
			iGrn = sPlus.AfterEqual(label_N_G.Text );
			iBlu = sPlus.AfterEqual(label_N_B.Text );
			
			if ( ( iRed == 0 ) && ( iGrn == 0 ) && ( iBlu == 0 ) )
				return;

			Bitmap img = (Bitmap)m_Img_O;

			for ( int y = 0; y < m_Img_O.Height; y ++ )
			{
				for ( int x = 0; x < m_Img_O.Width; x ++ )
				{
					sClr = img.GetPixel ( x, y );

					R = Requant ( sClr.R + iRed );
					G = Requant ( sClr.G + iGrn );
					B = Requant ( sClr.B + iBlu );

					img.SetPixel ( x, y, Color.FromArgb (R, G, B));
				}
			}
			this.Cursor = Cursors.Default;
		}

		// ====================================================================
		// Description:	Trackbar has been changed, adjust the shift in color
		//				for all images in the ring around to reflect change.
		// Return:		void
		void AdjustRingAround ()
		// ====================================================================
		{
			this.Cursor = Cursors.WaitCursor;
			
			AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_R, AdjAmount, 0, 0 );
			AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_G, 0, AdjAmount, 0 );
			AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_B, 0, 0, AdjAmount );
			AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_C, -1*AdjAmount, 0, 0 );
			AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_M, 0, -1*AdjAmount, 0 );
			AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_Y, 0, 0, -1*AdjAmount );
			//AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_Y, AdjAmount, AdjAmount, 0 );
			//AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_M, AdjAmount, 0, AdjAmount );			
			//AdjustImage ( (Bitmap)m_Img_E, (Bitmap)m_Img_C, 0, AdjAmount, AdjAmount );
			
			pictureBox_R.Image = m_Img_R;
			pictureBox_G.Image = m_Img_G;
			pictureBox_B.Image = m_Img_B;
			pictureBox_Y.Image = m_Img_Y;
			pictureBox_M.Image = m_Img_M;
			pictureBox_C.Image = m_Img_C;
			pictureBox_N.Image = m_Img_E;
			pictureBox_E.Image = m_Img_E;
			
			this.Cursor = Cursors.Default;
		}

		// ====================================================================
		// Description:	user has selected a color shift, add the shift to all images
		// Return:		void
		void AdjustAll ( int iRed,
						int iGrn,
						int iBlu )
		// ====================================================================
		{
			this.Cursor = Cursors.WaitCursor;
			AdjustImage ( (Bitmap)m_Img_E, iRed, iGrn, iBlu );
			
			AdjustImage ( (Bitmap)m_Img_R, iRed, iGrn, iBlu );
			AdjustImage ( (Bitmap)m_Img_G, iRed, iGrn, iBlu );
			AdjustImage ( (Bitmap)m_Img_B, iRed, iGrn, iBlu );
			AdjustImage ( (Bitmap)m_Img_Y, iRed, iGrn, iBlu );
			AdjustImage ( (Bitmap)m_Img_M, iRed, iGrn, iBlu );
			AdjustImage ( (Bitmap)m_Img_C, iRed, iGrn, iBlu );
			
			pictureBox_R.Image = m_Img_R;
			pictureBox_G.Image = m_Img_G;
			pictureBox_B.Image = m_Img_B;
			pictureBox_Y.Image = m_Img_Y;
			pictureBox_M.Image = m_Img_M;
			pictureBox_C.Image = m_Img_C;
			pictureBox_N.Image = m_Img_E;
			pictureBox_E.Image = m_Img_E;
			
			this.Cursor = Cursors.Default;
		}

		// ====================================================================
		// Description:		Adjust an individual image
		// Return:		void
		void AdjustImage (  Bitmap imgDst,	// [in/out] image
							int iRed,		// [in] amount of red
							int iGrn,		// [in] amount of green
							int iBlu )		// [in] amount of blue
		// ====================================================================
		{
			AdjustImage ( imgDst, imgDst, iRed, iGrn, iBlu );
		}

		// ====================================================================
		// Description:		Adjust an individual image
		// Return:		void
		void AdjustImage (  Bitmap imgSrc,	// [in] source image
							Bitmap imgDst,	// [out] destination image
							int iRed,		// [in] amount of red
							int iGrn,		// [in] amount of green
							int iBlu )		// [in] amount of blue
		// ====================================================================
		{
			AdjustLabel ( imgDst, iRed, iGrn, iBlu );

			for ( int y = 0; y < m_Img_E.Height; y ++ )
			{
				for ( int x = 0; x < m_Img_E.Width; x ++ )
				{
					Color dClr, sClr;
					int R, G, B;
					
					sClr = imgSrc.GetPixel ( x, y );
					dClr = imgDst.GetPixel ( x, y );
					R = sClr.R + iRed;
					G = sClr.G + iGrn;
					B = sClr.B + iBlu;

					R = Requant ( R );
					G = Requant ( G );
					B = Requant ( B );

					imgDst.SetPixel ( x, y, Color.FromArgb (R, G, B));
				}
			}
		}

		// ====================================================================
		// Description:	change the text label that displays R, G, B values
		// Return:		void
		void AdjustLabel ( Bitmap imgDst,	// [in] label owner to be adjusted
							int iRed,		// [in] amount of red 
							int iGrn,		// [in] amount of green
							int iBlu )		// [in] amount of blue
		// ====================================================================
		{
			StringPlus	sPlus = new StringPlus();

			if ( ((Image) imgDst) == m_Img_R )
			{			
				label_R_R.Text = "R = " + Convert.ToString(AdjAmount + sPlus.AfterEqual(label_N_R.Text));
				label_R_G.Text = "G = " + Convert.ToString(sPlus.AfterEqual(label_N_G.Text));
				label_R_B.Text = "B = " + Convert.ToString(sPlus.AfterEqual(label_N_B.Text));
			}
			else if ( ((Image) imgDst) == m_Img_G )
			{
				label_G_R.Text = "R = " + Convert.ToString(sPlus.AfterEqual(label_N_R.Text));
				label_G_G.Text = "G = " + Convert.ToString(AdjAmount + sPlus.AfterEqual(label_N_G.Text));
				label_G_B.Text = "B = " + Convert.ToString(sPlus.AfterEqual(label_N_B.Text));
			}
			else if ( ((Image) imgDst) == m_Img_B )
			{
				label_B_R.Text = "R = " + Convert.ToString(sPlus.AfterEqual(label_N_R.Text));
				label_B_G.Text = "G = " + Convert.ToString(AdjAmount + sPlus.AfterEqual(label_N_G.Text));
				label_B_B.Text = "B = " + Convert.ToString(sPlus.AfterEqual(label_N_B.Text));
			}
			else if ( ((Image) imgDst) == m_Img_Y )
			{
				label_Y_R.Text = "R = " + Convert.ToString(sPlus.AfterEqual(label_N_R.Text));
				label_Y_G.Text = "G = " + Convert.ToString(sPlus.AfterEqual(label_N_G.Text));
				label_Y_B.Text = "B = " + Convert.ToString(-1*AdjAmount + sPlus.AfterEqual(label_N_B.Text));
			}
			else if ( ((Image) imgDst) == m_Img_M )
			{
				label_M_R.Text = "R = " + Convert.ToString(sPlus.AfterEqual(label_N_R.Text));
				label_M_G.Text = "G = " + Convert.ToString(-1*AdjAmount + sPlus.AfterEqual(label_N_G.Text));
				label_M_B.Text = "B = " + Convert.ToString(sPlus.AfterEqual(label_N_B.Text));
			}
			else if ( ((Image) imgDst) == m_Img_C )
			{
				label_C_R.Text = "R = " + Convert.ToString(-1*AdjAmount + sPlus.AfterEqual(label_N_R.Text));
				label_C_G.Text = "G = " + Convert.ToString(sPlus.AfterEqual(label_N_G.Text));
				label_C_B.Text = "B = " + Convert.ToString(sPlus.AfterEqual(label_N_B.Text));
			}
			else
			{
				label_N_R.Text = "R = " + Convert.ToString(iRed + sPlus.AfterEqual(label_N_R.Text));
				label_N_G.Text = "G = " + Convert.ToString(iGrn + sPlus.AfterEqual(label_N_G.Text));
				label_N_B.Text = "B = " + Convert.ToString(iBlu + sPlus.AfterEqual(label_N_B.Text));
			}
		}

		// ====================================================================
		// Description:	Contain the RGB values without 0 to 255
		// Return:		void
		int Requant ( int iNum )
		// ====================================================================
		{
			if ( iNum < 0 )
				return 0;

			else if ( iNum > 255 )
				return 255;

			else
				return iNum;
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox_R = new System.Windows.Forms.PictureBox();
			this.pictureBox_C = new System.Windows.Forms.PictureBox();
			this.pictureBox_N = new System.Windows.Forms.PictureBox();
			this.pictureBox_M = new System.Windows.Forms.PictureBox();
			this.pictureBox_G = new System.Windows.Forms.PictureBox();
			this.pictureBox_B = new System.Windows.Forms.PictureBox();
			this.pictureBox_Y = new System.Windows.Forms.PictureBox();
			this.label_R_R = new System.Windows.Forms.Label();
			this.label_G_R = new System.Windows.Forms.Label();
			this.label_B_R = new System.Windows.Forms.Label();
			this.label_Y_R = new System.Windows.Forms.Label();
			this.label_C_R = new System.Windows.Forms.Label();
			this.label_M_R = new System.Windows.Forms.Label();
			this.label_N_R = new System.Windows.Forms.Label();
			this.groupBox_Adjust = new System.Windows.Forms.GroupBox();
			this.label_M = new System.Windows.Forms.Label();
			this.label_C = new System.Windows.Forms.Label();
			this.label_B = new System.Windows.Forms.Label();
			this.label_G = new System.Windows.Forms.Label();
			this.label_Y = new System.Windows.Forms.Label();
			this.label_N = new System.Windows.Forms.Label();
			this.label_R = new System.Windows.Forms.Label();
			this.label_G_B = new System.Windows.Forms.Label();
			this.label_G_G = new System.Windows.Forms.Label();
			this.label_Y_B = new System.Windows.Forms.Label();
			this.label_Y_G = new System.Windows.Forms.Label();
			this.label_C_B = new System.Windows.Forms.Label();
			this.label_C_G = new System.Windows.Forms.Label();
			this.label_N_B = new System.Windows.Forms.Label();
			this.label_N_G = new System.Windows.Forms.Label();
			this.label_B_B = new System.Windows.Forms.Label();
			this.label_B_G = new System.Windows.Forms.Label();
			this.label_M_B = new System.Windows.Forms.Label();
			this.label_M_G = new System.Windows.Forms.Label();
			this.label_R_B = new System.Windows.Forms.Label();
			this.label_R_G = new System.Windows.Forms.Label();
			this.groupBox_Progress = new System.Windows.Forms.GroupBox();
			this.label_E = new System.Windows.Forms.Label();
			this.label_O = new System.Windows.Forms.Label();
			this.pictureBox_E = new System.Windows.Forms.PictureBox();
			this.pictureBox_O = new System.Windows.Forms.PictureBox();
			this.button_Browse = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button_SaveAs = new System.Windows.Forms.Button();
			this.button_About = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.button_BackClr = new System.Windows.Forms.Button();
			this.groupBox_Adjust.SuspendLayout();
			this.groupBox_Progress.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox_R
			// 
			this.pictureBox_R.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_R.Location = new System.Drawing.Point(216, 24);
			this.pictureBox_R.Name = "pictureBox_R";
			this.pictureBox_R.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_R.TabIndex = 0;
			this.pictureBox_R.TabStop = false;
			// 
			// pictureBox_C
			// 
			this.pictureBox_C.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_C.Location = new System.Drawing.Point(216, 384);
			this.pictureBox_C.Name = "pictureBox_C";
			this.pictureBox_C.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_C.TabIndex = 1;
			this.pictureBox_C.TabStop = false;
			// 
			// pictureBox_N
			// 
			this.pictureBox_N.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_N.Location = new System.Drawing.Point(216, 208);
			this.pictureBox_N.Name = "pictureBox_N";
			this.pictureBox_N.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_N.TabIndex = 2;
			this.pictureBox_N.TabStop = false;
			// 
			// pictureBox_M
			// 
			this.pictureBox_M.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_M.Location = new System.Drawing.Point(40, 120);
			this.pictureBox_M.Name = "pictureBox_M";
			this.pictureBox_M.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_M.TabIndex = 3;
			this.pictureBox_M.TabStop = false;
			// 
			// pictureBox_G
			// 
			this.pictureBox_G.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_G.Location = new System.Drawing.Point(392, 296);
			this.pictureBox_G.Name = "pictureBox_G";
			this.pictureBox_G.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_G.TabIndex = 4;
			this.pictureBox_G.TabStop = false;
			// 
			// pictureBox_B
			// 
			this.pictureBox_B.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_B.Location = new System.Drawing.Point(40, 296);
			this.pictureBox_B.Name = "pictureBox_B";
			this.pictureBox_B.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_B.TabIndex = 5;
			this.pictureBox_B.TabStop = false;
			// 
			// pictureBox_Y
			// 
			this.pictureBox_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_Y.Location = new System.Drawing.Point(392, 120);
			this.pictureBox_Y.Name = "pictureBox_Y";
			this.pictureBox_Y.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_Y.TabIndex = 6;
			this.pictureBox_Y.TabStop = false;
			// 
			// label_R_R
			// 
			this.label_R_R.AutoSize = true;
			this.label_R_R.Location = new System.Drawing.Point(216, 136);
			this.label_R_R.Name = "label_R_R";
			this.label_R_R.Size = new System.Drawing.Size(25, 16);
			this.label_R_R.TabIndex = 7;
			this.label_R_R.Text = "Red";
			// 
			// label_G_R
			// 
			this.label_G_R.AutoSize = true;
			this.label_G_R.Location = new System.Drawing.Point(392, 408);
			this.label_G_R.Name = "label_G_R";
			this.label_G_R.Size = new System.Drawing.Size(36, 16);
			this.label_G_R.TabIndex = 8;
			this.label_G_R.Text = "Green";
			// 
			// label_B_R
			// 
			this.label_B_R.AutoSize = true;
			this.label_B_R.Location = new System.Drawing.Point(40, 408);
			this.label_B_R.Name = "label_B_R";
			this.label_B_R.Size = new System.Drawing.Size(27, 16);
			this.label_B_R.TabIndex = 9;
			this.label_B_R.Text = "Blue";
			// 
			// label_Y_R
			// 
			this.label_Y_R.AutoSize = true;
			this.label_Y_R.Location = new System.Drawing.Point(392, 232);
			this.label_Y_R.Name = "label_Y_R";
			this.label_Y_R.Size = new System.Drawing.Size(38, 16);
			this.label_Y_R.TabIndex = 10;
			this.label_Y_R.Text = "Yellow";
			// 
			// label_C_R
			// 
			this.label_C_R.AutoSize = true;
			this.label_C_R.Location = new System.Drawing.Point(216, 496);
			this.label_C_R.Name = "label_C_R";
			this.label_C_R.Size = new System.Drawing.Size(31, 16);
			this.label_C_R.TabIndex = 11;
			this.label_C_R.Text = "Cyan";
			// 
			// label_M_R
			// 
			this.label_M_R.AutoSize = true;
			this.label_M_R.Location = new System.Drawing.Point(40, 232);
			this.label_M_R.Name = "label_M_R";
			this.label_M_R.Size = new System.Drawing.Size(48, 16);
			this.label_M_R.TabIndex = 12;
			this.label_M_R.Text = "Magenta";
			// 
			// label_N_R
			// 
			this.label_N_R.AutoSize = true;
			this.label_N_R.Location = new System.Drawing.Point(216, 320);
			this.label_N_R.Name = "label_N_R";
			this.label_N_R.Size = new System.Drawing.Size(41, 16);
			this.label_N_R.TabIndex = 13;
			this.label_N_R.Text = "Neutral";
			// 
			// groupBox_Adjust
			// 
			this.groupBox_Adjust.Controls.Add(this.label_M);
			this.groupBox_Adjust.Controls.Add(this.label_C);
			this.groupBox_Adjust.Controls.Add(this.label_B);
			this.groupBox_Adjust.Controls.Add(this.label_G);
			this.groupBox_Adjust.Controls.Add(this.label_Y);
			this.groupBox_Adjust.Controls.Add(this.label_N);
			this.groupBox_Adjust.Controls.Add(this.label_R);
			this.groupBox_Adjust.Controls.Add(this.label_G_B);
			this.groupBox_Adjust.Controls.Add(this.label_G_G);
			this.groupBox_Adjust.Controls.Add(this.label_Y_B);
			this.groupBox_Adjust.Controls.Add(this.label_Y_G);
			this.groupBox_Adjust.Controls.Add(this.label_C_B);
			this.groupBox_Adjust.Controls.Add(this.label_C_G);
			this.groupBox_Adjust.Controls.Add(this.label_N_B);
			this.groupBox_Adjust.Controls.Add(this.label_N_G);
			this.groupBox_Adjust.Controls.Add(this.label_B_B);
			this.groupBox_Adjust.Controls.Add(this.label_B_G);
			this.groupBox_Adjust.Controls.Add(this.label_M_B);
			this.groupBox_Adjust.Controls.Add(this.label_M_G);
			this.groupBox_Adjust.Controls.Add(this.label_R_B);
			this.groupBox_Adjust.Controls.Add(this.label_R_G);
			this.groupBox_Adjust.Controls.Add(this.label_Y_R);
			this.groupBox_Adjust.Controls.Add(this.label_G_R);
			this.groupBox_Adjust.Controls.Add(this.pictureBox_N);
			this.groupBox_Adjust.Controls.Add(this.label_C_R);
			this.groupBox_Adjust.Controls.Add(this.pictureBox_R);
			this.groupBox_Adjust.Controls.Add(this.pictureBox_M);
			this.groupBox_Adjust.Controls.Add(this.pictureBox_Y);
			this.groupBox_Adjust.Controls.Add(this.label_R_R);
			this.groupBox_Adjust.Controls.Add(this.label_M_R);
			this.groupBox_Adjust.Controls.Add(this.label_N_R);
			this.groupBox_Adjust.Controls.Add(this.pictureBox_C);
			this.groupBox_Adjust.Controls.Add(this.label_B_R);
			this.groupBox_Adjust.Controls.Add(this.pictureBox_G);
			this.groupBox_Adjust.Controls.Add(this.pictureBox_B);
			this.groupBox_Adjust.Location = new System.Drawing.Point(16, 16);
			this.groupBox_Adjust.Name = "groupBox_Adjust";
			this.groupBox_Adjust.Size = new System.Drawing.Size(544, 576);
			this.groupBox_Adjust.TabIndex = 14;
			this.groupBox_Adjust.TabStop = false;
			this.groupBox_Adjust.Text = "Adjust";
			// 
			// label_M
			// 
			this.label_M.Location = new System.Drawing.Point(24, 120);
			this.label_M.Name = "label_M";
			this.label_M.Size = new System.Drawing.Size(16, 16);
			this.label_M.TabIndex = 34;
			this.label_M.Text = "M";
			// 
			// label_C
			// 
			this.label_C.Location = new System.Drawing.Point(200, 384);
			this.label_C.Name = "label_C";
			this.label_C.Size = new System.Drawing.Size(16, 16);
			this.label_C.TabIndex = 33;
			this.label_C.Text = "C";
			// 
			// label_B
			// 
			this.label_B.Location = new System.Drawing.Point(24, 296);
			this.label_B.Name = "label_B";
			this.label_B.Size = new System.Drawing.Size(16, 16);
			this.label_B.TabIndex = 32;
			this.label_B.Text = "B";
			// 
			// label_G
			// 
			this.label_G.Location = new System.Drawing.Point(376, 296);
			this.label_G.Name = "label_G";
			this.label_G.Size = new System.Drawing.Size(16, 16);
			this.label_G.TabIndex = 31;
			this.label_G.Text = "G";
			// 
			// label_Y
			// 
			this.label_Y.Location = new System.Drawing.Point(376, 120);
			this.label_Y.Name = "label_Y";
			this.label_Y.Size = new System.Drawing.Size(16, 16);
			this.label_Y.TabIndex = 30;
			this.label_Y.Text = "Y";
			// 
			// label_N
			// 
			this.label_N.Location = new System.Drawing.Point(200, 208);
			this.label_N.Name = "label_N";
			this.label_N.Size = new System.Drawing.Size(16, 16);
			this.label_N.TabIndex = 29;
			this.label_N.Text = "N";
			// 
			// label_R
			// 
			this.label_R.Location = new System.Drawing.Point(200, 32);
			this.label_R.Name = "label_R";
			this.label_R.Size = new System.Drawing.Size(16, 16);
			this.label_R.TabIndex = 28;
			this.label_R.Text = "R";
			// 
			// label_G_B
			// 
			this.label_G_B.AutoSize = true;
			this.label_G_B.Location = new System.Drawing.Point(392, 440);
			this.label_G_B.Name = "label_G_B";
			this.label_G_B.Size = new System.Drawing.Size(36, 16);
			this.label_G_B.TabIndex = 27;
			this.label_G_B.Text = "Green";
			// 
			// label_G_G
			// 
			this.label_G_G.AutoSize = true;
			this.label_G_G.Location = new System.Drawing.Point(392, 424);
			this.label_G_G.Name = "label_G_G";
			this.label_G_G.Size = new System.Drawing.Size(36, 16);
			this.label_G_G.TabIndex = 26;
			this.label_G_G.Text = "Green";
			// 
			// label_Y_B
			// 
			this.label_Y_B.AutoSize = true;
			this.label_Y_B.Location = new System.Drawing.Point(392, 264);
			this.label_Y_B.Name = "label_Y_B";
			this.label_Y_B.Size = new System.Drawing.Size(38, 16);
			this.label_Y_B.TabIndex = 25;
			this.label_Y_B.Text = "Yellow";
			// 
			// label_Y_G
			// 
			this.label_Y_G.AutoSize = true;
			this.label_Y_G.Location = new System.Drawing.Point(392, 248);
			this.label_Y_G.Name = "label_Y_G";
			this.label_Y_G.Size = new System.Drawing.Size(38, 16);
			this.label_Y_G.TabIndex = 24;
			this.label_Y_G.Text = "Yellow";
			// 
			// label_C_B
			// 
			this.label_C_B.AutoSize = true;
			this.label_C_B.Location = new System.Drawing.Point(216, 528);
			this.label_C_B.Name = "label_C_B";
			this.label_C_B.Size = new System.Drawing.Size(31, 16);
			this.label_C_B.TabIndex = 23;
			this.label_C_B.Text = "Cyan";
			// 
			// label_C_G
			// 
			this.label_C_G.AutoSize = true;
			this.label_C_G.Location = new System.Drawing.Point(216, 512);
			this.label_C_G.Name = "label_C_G";
			this.label_C_G.Size = new System.Drawing.Size(31, 16);
			this.label_C_G.TabIndex = 22;
			this.label_C_G.Text = "Cyan";
			// 
			// label_N_B
			// 
			this.label_N_B.AutoSize = true;
			this.label_N_B.Location = new System.Drawing.Point(216, 352);
			this.label_N_B.Name = "label_N_B";
			this.label_N_B.Size = new System.Drawing.Size(41, 16);
			this.label_N_B.TabIndex = 21;
			this.label_N_B.Text = "Neutral";
			// 
			// label_N_G
			// 
			this.label_N_G.AutoSize = true;
			this.label_N_G.Location = new System.Drawing.Point(216, 336);
			this.label_N_G.Name = "label_N_G";
			this.label_N_G.Size = new System.Drawing.Size(41, 16);
			this.label_N_G.TabIndex = 20;
			this.label_N_G.Text = "Neutral";
			// 
			// label_B_B
			// 
			this.label_B_B.AutoSize = true;
			this.label_B_B.Location = new System.Drawing.Point(40, 440);
			this.label_B_B.Name = "label_B_B";
			this.label_B_B.Size = new System.Drawing.Size(27, 16);
			this.label_B_B.TabIndex = 19;
			this.label_B_B.Text = "Blue";
			// 
			// label_B_G
			// 
			this.label_B_G.AutoSize = true;
			this.label_B_G.Location = new System.Drawing.Point(40, 424);
			this.label_B_G.Name = "label_B_G";
			this.label_B_G.Size = new System.Drawing.Size(27, 16);
			this.label_B_G.TabIndex = 18;
			this.label_B_G.Text = "Blue";
			// 
			// label_M_B
			// 
			this.label_M_B.AutoSize = true;
			this.label_M_B.Location = new System.Drawing.Point(40, 264);
			this.label_M_B.Name = "label_M_B";
			this.label_M_B.Size = new System.Drawing.Size(48, 16);
			this.label_M_B.TabIndex = 17;
			this.label_M_B.Text = "Magenta";
			// 
			// label_M_G
			// 
			this.label_M_G.AutoSize = true;
			this.label_M_G.Location = new System.Drawing.Point(40, 248);
			this.label_M_G.Name = "label_M_G";
			this.label_M_G.Size = new System.Drawing.Size(48, 16);
			this.label_M_G.TabIndex = 16;
			this.label_M_G.Text = "Magenta";
			// 
			// label_R_B
			// 
			this.label_R_B.AutoSize = true;
			this.label_R_B.Location = new System.Drawing.Point(216, 168);
			this.label_R_B.Name = "label_R_B";
			this.label_R_B.Size = new System.Drawing.Size(41, 16);
			this.label_R_B.TabIndex = 15;
			this.label_R_B.Text = "label13";
			// 
			// label_R_G
			// 
			this.label_R_G.AutoSize = true;
			this.label_R_G.Location = new System.Drawing.Point(216, 152);
			this.label_R_G.Name = "label_R_G";
			this.label_R_G.Size = new System.Drawing.Size(41, 16);
			this.label_R_G.TabIndex = 14;
			this.label_R_G.Text = "label12";
			// 
			// groupBox_Progress
			// 
			this.groupBox_Progress.Controls.Add(this.label_E);
			this.groupBox_Progress.Controls.Add(this.label_O);
			this.groupBox_Progress.Controls.Add(this.pictureBox_E);
			this.groupBox_Progress.Controls.Add(this.pictureBox_O);
			this.groupBox_Progress.Location = new System.Drawing.Point(576, 16);
			this.groupBox_Progress.Name = "groupBox_Progress";
			this.groupBox_Progress.Size = new System.Drawing.Size(144, 312);
			this.groupBox_Progress.TabIndex = 15;
			this.groupBox_Progress.TabStop = false;
			this.groupBox_Progress.Text = "Progress";
			// 
			// label_E
			// 
			this.label_E.AutoSize = true;
			this.label_E.Location = new System.Drawing.Point(24, 168);
			this.label_E.Name = "label_E";
			this.label_E.Size = new System.Drawing.Size(36, 16);
			this.label_E.TabIndex = 17;
			this.label_E.Text = "Edited";
			// 
			// label_O
			// 
			this.label_O.AutoSize = true;
			this.label_O.Location = new System.Drawing.Point(24, 24);
			this.label_O.Name = "label_O";
			this.label_O.Size = new System.Drawing.Size(43, 16);
			this.label_O.TabIndex = 16;
			this.label_O.Text = "Original";
			// 
			// pictureBox_E
			// 
			this.pictureBox_E.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_E.Location = new System.Drawing.Point(16, 184);
			this.pictureBox_E.Name = "pictureBox_E";
			this.pictureBox_E.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_E.TabIndex = 15;
			this.pictureBox_E.TabStop = false;
			// 
			// pictureBox_O
			// 
			this.pictureBox_O.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox_O.Location = new System.Drawing.Point(16, 40);
			this.pictureBox_O.Name = "pictureBox_O";
			this.pictureBox_O.Size = new System.Drawing.Size(110, 110);
			this.pictureBox_O.TabIndex = 14;
			this.pictureBox_O.TabStop = false;
			// 
			// button_Browse
			// 
			this.button_Browse.Location = new System.Drawing.Point(32, 24);
			this.button_Browse.Name = "button_Browse";
			this.button_Browse.TabIndex = 16;
			this.button_Browse.Text = "Browse";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button_SaveAs);
			this.groupBox3.Controls.Add(this.button_Browse);
			this.groupBox3.Location = new System.Drawing.Point(576, 336);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(144, 96);
			this.groupBox3.TabIndex = 17;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "File";
			// 
			// button_SaveAs
			// 
			this.button_SaveAs.Location = new System.Drawing.Point(32, 56);
			this.button_SaveAs.Name = "button_SaveAs";
			this.button_SaveAs.TabIndex = 17;
			this.button_SaveAs.Text = "Save As...";
			// 
			// button_About
			// 
			this.button_About.Location = new System.Drawing.Point(608, 520);
			this.button_About.Name = "button_About";
			this.button_About.TabIndex = 19;
			this.button_About.Text = "About";
			// 
			// trackBar1
			// 
			this.trackBar1.AllowDrop = true;
			this.trackBar1.LargeChange = 10;
			this.trackBar1.Location = new System.Drawing.Point(32, 24);
			this.trackBar1.Maximum = 30;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(456, 45);
			this.trackBar1.TabIndex = 5;
			this.trackBar1.Value = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textBox1);
			this.groupBox4.Controls.Add(this.trackBar1);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Location = new System.Drawing.Point(16, 608);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(704, 72);
			this.groupBox4.TabIndex = 20;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Adjust Amount";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(576, 32);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(56, 20);
			this.textBox1.TabIndex = 22;
			this.textBox1.Text = "textBox_Amount";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 40);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(24, 23);
			this.label10.TabIndex = 21;
			this.label10.Text = "0";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(488, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(24, 23);
			this.label11.TabIndex = 21;
			this.label11.Text = "30";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// button_BackClr
			// 
			this.button_BackClr.Location = new System.Drawing.Point(608, 488);
			this.button_BackClr.Name = "button_BackClr";
			this.button_BackClr.TabIndex = 21;
			this.button_BackClr.Text = "Back Color";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 718);
			this.Controls.Add(this.button_BackClr);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox_Progress);
			this.Controls.Add(this.groupBox_Adjust);
			this.Controls.Add(this.button_About);
			this.Name = "Form1";
			this.Text = "Color Ring Around";
			this.groupBox_Adjust.ResumeLayout(false);
			this.groupBox_Progress.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
	}
}
