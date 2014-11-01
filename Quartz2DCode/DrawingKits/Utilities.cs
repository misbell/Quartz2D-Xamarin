using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

namespace Quartz2DCode
{
	public class Utilities
	{
		private const nfloat kTickLength= 5.0;
		private const nfloat kTickDistance= 72.0;
		private const nfloat kAxesLength =(20*kTickDistance);

		public Utilities ()
		{
		}

		//void drawCoordinateAxes(CGContextRef context)
		public void drawCoordinateAxes()
		{
			int i;
			float t;
			float tickLength = kTickLength;

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			//CGContextSaveGState(context);
			context.SaveState();

			//CGContextBeginPath(context);
			context.BeginPath();

			// Paint the x-axis in red.
			//CGContextSetRGBStrokeColor(context, 1, 0, 0, 1);
			context.SetStrokeColor(1,0,0,1);

			context.MoveTo( -kTickLength, 0.0f);
			CGContextAddLineToPoint(context, kAxesLength, 0.);
			CGContextDrawPath(context, kCGPathStroke);

			// Paint the y-axis in blue.
			CGContextSetRGBStrokeColor(context, 0, 0, 1, 1);
			CGContextMoveToPoint(context, 0, -kTickLength);
			CGContextAddLineToPoint(context, 0, kAxesLength);
			CGContextDrawPath(context, kCGPathStroke);

			// Paint the x-axis tick marks in red.
			CGContextSetRGBStrokeColor(context, 1, 0, 0, 1);
			for(i = 0; i < 2 ; i++)
			{
				for(t=0.; t < kAxesLength ; t += kTickDistance){
					CGContextMoveToPoint(context, t, -tickLength);
					CGContextAddLineToPoint(context, t, tickLength);
				}
				CGContextDrawPath(context, kCGPathStroke);
				CGContextRotateCTM(context, M_PI/2.);
				// Paint the y-axis tick marks in blue.
				CGContextSetRGBStrokeColor(context, 0, 0, 1, 1);
			}
			drawPoint(context, CGPointZero);
			CGContextRestoreGState(context);
		}

		public void drawPoint(CGPoint p)
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// CGContextSaveGState(context);
			context.SaveState();

			// Opaque black.
			CGContextSetRGBStrokeColor(context, 0, 0, 0, 1);
			CGContextSetLineWidth(context, 5);
			CGContextSetLineCap(context, kCGLineCapRound);
			CGContextMoveToPoint(context, p.x, p.y);
			CGContextAddLineToPoint(context, p.x, p.y);
			CGContextStrokePath(context);
			CGContextRestoreGState(context);


		}

	}
}

