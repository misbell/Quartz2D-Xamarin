using System;
using AppKit;
using CoreGraphics;
using Foundation;

namespace Quartz2DCode
{
	public class SimpleView : NSView
	{
		public SimpleView ()
		{
			//this.SetBackgroundColor (NSColor.Red);


		}

		public override void DrawRect (CGRect dirtyRect)
		{
			//
			// create a context from the current window
			// set rgb colorspace
			// set fill color
			// fill Rect with color at new CGRect coordinates

			CGContext mycontext = NSGraphicsContext.CurrentContext.GraphicsPort;
			mycontext.SetFillColorSpace (CGColorSpace.CreateDeviceRGB());

			mycontext.SetFillColor (NSColor.Red.CGColor);
			mycontext.FillRect (new CoreGraphics.CGRect (0, 0, 200, 100));

			mycontext.SetFillColor (NSColor.Blue.CGColor);
			mycontext.FillRect (new CoreGraphics.CGRect (0, 0, 100, 200));



		

		}
	}
}

