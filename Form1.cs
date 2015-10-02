using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GdiPlusTest {
	public partial class Form1 : Form {

		private readonly Bitmap imgv;
		private readonly Bitmap imgh;

		public Form1() {
			InitializeComponent();
			this.imgv = new Bitmap(3, 6, PixelFormat.Format32bppPArgb);
			this.imgh = new Bitmap(6, 3, PixelFormat.Format32bppPArgb);
			using(var g = Graphics.FromImage(this.imgv)) {
				g.Clear(Color.Black);
				g.DrawLine(Pens.Red, 1, 0, 1, 6);
			}
			using(var g = Graphics.FromImage(this.imgh)) {
				g.Clear(Color.Black);
				g.DrawLine(Pens.Red, 0, 1, 6, 1);
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			var g = e.Graphics;
			g.PixelOffsetMode = PixelOffsetMode.Half;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.CompositingMode = CompositingMode.SourceOver;
			g.CompositingQuality = CompositingQuality.HighQuality;
			var modes = new[] {
				InterpolationMode.Default,  // OK
				InterpolationMode.Low,      // OK
				InterpolationMode.High,     // Buggy
				InterpolationMode.Bilinear, // OK
				InterpolationMode.Bicubic,  // OK
				InterpolationMode.NearestNeighbor,     // OK
				InterpolationMode.HighQualityBilinear, // Buggy
				InterpolationMode.HighQualityBicubic,  // Buggy
			};
			var srcv = new RectangleF(0, 0, this.imgv.Width, this.imgv.Height);
			var srch = new RectangleF(0, 0, this.imgh.Width, this.imgh.Height);
			var dstv = srcv;
			var dsth = srch;
			var n = 0;
			foreach(var mode in modes) {
				g.InterpolationMode = mode;
				for(var i = 0; i <= 20; ++i) {
					dstv.X = 20 + i / 20f + n * 24;
					dstv.Y = 20 + i * (this.imgv.Height + 1);
					g.DrawImage(this.imgv, dstv, srcv, GraphicsUnit.Pixel);
					dsth.X = 240 + i * (this.imgh.Width + 1);
					dsth.Y = 20 + i / 20f + n * 20;
					g.DrawImage(this.imgh, dsth, srch, GraphicsUnit.Pixel);
				}
				++n;
				g.DrawString(n.ToString(), DefaultFont, Brushes.Black, dstv.X - 4, dstv.Y + 20);
				g.DrawString(n.ToString(), DefaultFont, Brushes.Black, dsth.X + 20, dsth.Y - 4);
				g.DrawString(string.Format("{0}: {1}", n, mode), DefaultFont, Brushes.Black, 460, 8 + n * 12);
			}
		}
	}
}
