using System;
using AppKit;
using CoreGraphics;
using Foundation;

namespace Quartz2DCode
{
	public class SimpleView2 : NSView
	{

		NSImage _image1, _image2;

		public SimpleView2 ()
		{
			//this.SetBackgroundColor (NSColor.Red);

			// all of these work

			_image1 = NSImage.ImageNamed("cover.JPG");
			NSImageView anImageView = new NSImageView(_image1.AlignmentRect);
			Image1 = NSImage.ImageNamed("cover.JPG");
			anImageView.Image = NSImage.ImageNamed("cover.JPG");
			this.AddSubview(anImageView);

			CGContext c1 = 	MyCreateBitmapContext (100,200);
			CGContext c2 =  MyCreateBitmapContextWithImage ();

			int i = 0;


		}

		public override void DrawRect (CGRect dirtyRect)
		{

			CGContext mycontext = NSGraphicsContext.CurrentContext.GraphicsPort;
			mycontext.SetFillColorSpace (CGColorSpace.CreateDeviceRGB());

			mycontext.SetFillColor (NSColor.Red.CGColor);
			mycontext.FillRect (new CoreGraphics.CGRect (0, 0, 200, 100));

			mycontext.SetFillColor (NSColor.Blue.CGColor);
			mycontext.FillRect (new CoreGraphics.CGRect (0, 0, 100, 200));

			mycontext.SaveState();

			// do something here

			mycontext.RestoreState ();

		}

		public NSImage Image1 { 
			get {
				return _image1;
			}
			set { 
				_image1  = value;
				int i = 0;
			}

		}


		CGContext MyCreateBitmapContext (int pixelsWide,
			int pixelsHigh)
		{
	
			// create context from just width and height, the rest hardcoded

			CGContext context = null;
			CGColorSpace colorSpace;
			Byte[] bitmapData;
			int bitmapByteCount;
			int bitmapBytesPerRow;
			int EightBitsPerComponent = 8;

			// alpha red blue green is ARGB
			const int ARGB = 4;

			bitmapBytesPerRow   = (pixelsWide * ARGB);// row length * 4 bytes, 1 each for ARGB
			bitmapByteCount     = (bitmapBytesPerRow * pixelsHigh); // width * height

			colorSpace = CGColorSpace.CreateWithName (CGColorSpaceNames.GenericRGB); // CGColorSpaceCreateWithName(kCGColorSpaceGenericRGB);// 2
			bitmapData = new byte[ bitmapByteCount];// 3 

			if (bitmapData == null)
				return null;

			CGBitmapContext bitmapcontext = new CGBitmapContext(bitmapData,
				pixelsWide,pixelsHigh,
				EightBitsPerComponent,
				bitmapBytesPerRow,
				colorSpace,
				CGImageAlphaInfo.PremultipliedLast);

			return context;
		}


		CGContext MyCreateBitmapContextWithImage ()
		{

			//create bitmap context from image


			CGContext context = null;
			CGImage image = _image1.CGImage;//

			byte[] rawImageData =new byte[image.Height*(image.Width*4)];

			System.Drawing.RectangleF rectDraw = new System.Drawing.RectangleF();
			rectDraw.X =0;
			rectDraw.Y =0;
			rectDraw.Width= image.Width;
			rectDraw.Height= image.Height;

			if (rawImageData == null)
				return null;

			 context = new CGBitmapContext(rawImageData,    
				(int) image.Width, 
				(int) image.Height, 
				(int) image.BitsPerComponent, 
				(int) image.BytesPerRow,    
				image.ColorSpace,
				CGBitmapFlags.PremultipliedLast | CGBitmapFlags.ByteOrder32Big);

			// draw the image into a CGImage with a defined rectangle
			context.DrawImage(rectDraw, image);

			//CGBitmapFlags.PremultipliedLast | CGBitmapFlags.ByteOrder32Big
			// don't know what that 32big flag does

			return context;
		}


	}
}

