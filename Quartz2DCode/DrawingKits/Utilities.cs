using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

namespace Quartz2DCode
{
	public class Utilities
	{
		public Utilities ()
		{
		}

		void drawCoordinateAxes(CGContextRef context)
		{
			int i;
			float t;
			float tickLength = kTickLength;

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			//CGContextSaveGState(context);
			context.

			CGContextBeginPath(context);
			// Paint the x-axis in red.
			CGContextSetRGBStrokeColor(context, 1, 0, 0, 1);
			CGContextMoveToPoint(context, -kTickLength, 0.);
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

		void drawPoint(CGContextRef context, CGPoint p)
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGContextSaveGState(context);
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

