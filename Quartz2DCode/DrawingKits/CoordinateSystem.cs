using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

namespace Quartz2DCode
{
	public class CoordinateSystem
	{
		public CoordinateSystem ()
		{
		}

	//	void doRotatedEllipses(CGContextRef context) //
		public void doRotatedEllipses()
		{
			int i, totreps = 144;
			float  tint = 1.0f, tintIncrement = 1.0f/totreps;
			// Create a new transform consisting of a 45 degrees rotation.

			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGAffineTransform theTransform = CGAffineTransform.MakeRotation((nfloat)(Math.PI/4.0f));
			//CGAffineTransform theTransform = CGAffineTransformMakeRotation(M_PI/4);

			// Apply a scale to the transform just created.

			//theTransform = CGAffineTransformScale(theTransform, 1, 2);
			theTransform = CGAffineTransform.MakeScale(1.0f, 2.0f);

			// Place the first ellipse at a good location.
			//CGContextTranslateCTM(context, 100., 100.);
			context.TranslateCTM(100.0f, 100.0f);

			for(i=0 ; i < totreps ; i++){
				// Make a snapshot the coordinate system.

				//CGContextSaveGState(context);
				context.SaveState();

				// Set up the coordinate system for the rotated ellipse.

				//CGContextConcatCTM(context, theTransform);
				context.ConcatCTM(theTransform);

				//CGContextBeginPath(context);
				context.BeginPath();


				//CGContextAddArc(context, 0., 0., 45., 0., 2*M_PI, 0);
				context.AddArc(0.0f, 0.0f, 45.0f, 0.0f, (nfloat)(2.0f * Math.PI), false);


				// Set the fill color for this instance of the ellipse.
				//CGContextSetRGBFillColor(context, tint, 0., 0., 1.0);
				context.SetFillColor(tint,0.0f, 0.0f, 1.0f);


				//CGContextDrawPath(context, kCGPathFill);
				context.DrawPath(CGPathDrawingMode.Fill);

				// Restore the coordinate system to that of the snapshot.
				//CGContextRestoreGState(context);
				context.RestoreState();

				// Compute the next tint color.
				tint -= tintIncrement;
				// Move over by 1 unit in x for the next ellipse.
				//CGContextTranslateCTM(context, 1.0, 0.0);
				context.TranslateCTM(1.0f, 0.0f);
			}
		}

		//void drawSkewedCoordinateSystem(CGContextRef context)
		public void drawSkewedCoordinateSystem()
		{

			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// alpha is 22.5 degrees and beta is 15 degrees.
			//float alpha =  M_PI/8, beta = M_PI/12;
			float alpha = Math.PI / 8.0f;
			float beta = Math.PI / 12.0f;
			CGAffineTransform skew;
			// Create a rectangle that is 72 units on a side
			// with its origin at (0,0).
			//CGRect r = CGRectMake(0, 0, 72, 72);

			//CGContextTranslateCTM(context, 144, 144);
			context.TranslateCTM(144.0f, 144.0f);

			// Draw the coordinate axes untransformed.
			drawCoordinateAxes(context);
			// Fill the rectangle.
			CGContextFillRect(context, r);

			// Create an affine transform that skews the coordinate system,
			// skewing the x-axis by alpha radians and the y-axis by beta radians. 
			skew = CGAffineTransformMake(1, tan(alpha), tan(beta), 1, 0, 0);
			// Apply that transform to the context coordinate system.
			CGContextConcatCTM(context, skew);

			// Set the fill and stroke color to a dark blue.
			CGContextSetRGBStrokeColor(context, 0.11, 0.208, 0.451, 1);
			CGContextSetRGBFillColor(context, 0.11, 0.208, 0.451, 1);

			// Draw the coordinate axes again, now transformed.
			drawCoordinateAxes(context);
			// Set the fill color again but with a partially transparent alpha.
			CGContextSetRGBFillColor(context, 0.11, 0.208, 0.451, 0.7);
			// Fill the rectangle in the transformed coordinate system.
			CGContextFillRect(context, r);
		}
	}
}

