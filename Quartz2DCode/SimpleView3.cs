using System;
using AppKit;
using CoreGraphics;
using Foundation;

namespace Quartz2DCode
{
	public class SimpleView3 : NSView
	{

		NSImage _image1, _image2;

		public SimpleView3 ()
		{
			//this.SetBackgroundColor (NSColor.Red);

			// all of these work

			_image1 = NSImage.ImageNamed("cover.JPG");
			NSImageView anImageView = new NSImageView(_image1.AlignmentRect);
			Image1 = NSImage.ImageNamed("cover.JPG");
			anImageView.Image = NSImage.ImageNamed("cover.JPG");
			this.AddSubview(anImageView);

			CGContext c1 = 	MyCreateBitmapContext (100,200);


			int i = 0;


		}

		public override void DrawRect (CGRect dirtyRect)
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext gp = NSGraphicsContext.CurrentContext.GraphicsPort;
			CGContext cc = ccontext.GraphicsPort;

			gp.SaveState();

			gp.SetLineWidth (20);

			gp.RestoreState(); // erase changes to context, restore its former state

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



			// work with offline bitmap context

			CGBitmapContext bitmapcontext = new CGBitmapContext(bitmapData,
				pixelsWide,pixelsHigh,
				EightBitsPerComponent,
				bitmapBytesPerRow,
				colorSpace,
				CGImageAlphaInfo.PremultipliedLast);

			return context;
		}


	
	}
}

