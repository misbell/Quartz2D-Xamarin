using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

// all the path drawing.c code translated from gelphman

// original code NOT saved line by line, sorry, see other files 
// converted by Michael Prenez-Isbell
// Oct-Nov 2014

namespace Quartz2DCode
{
	public class PathDrawing : NSView
	{

		NSImage _image1, _image2;
		const string kGenericRGBProfile = "/System/Library/ColorSync/Profiles/Generic RGB Profile.icc";

		public PathDrawing ()
		{

			;
		}

		public override void DrawRect (CGRect dirtyRect)
		{

			DoEgg();

		}

		public void DoEgg ()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;
			CGContext context2 = ccontext.GraphicsPort; // extra step just to show what's what


			context.SaveState ();

			CGPoint p0 = new CGPoint (0.0f, 0.0f);
			CGPoint p1 = new CGPoint (0.0f, 200.0f);
			CGPoint c1 = new CGPoint (140.0f, 5.0f);
			CGPoint c2 = new CGPoint (80.0f, 198.0f);

			context.TranslateCTM (100.0f, 50.0f);
			context.BeginPath ();

			context.MoveTo (p0.X, p0.Y);

			// Create the Bézier path segment for the right side of the egg.
			context.AddCurveToPoint (c1.X, c1.Y, c2.X, c2.Y, p1.X, p1.Y);

			// Create the Bézier path segment for the left side of the egg.
			context.AddCurveToPoint (-c2.X, c2.Y, -c1.X, c1.Y, p0.X, p0.Y);
			context.ClosePath ();

			context.SetLineWidth (2);
			context.DrawPath (CGPathDrawingMode.Stroke);

			context.RestoreState (); // erase changes to context, restore its former state

		}

		public void addRoundedRectToPath(CGContext context, CGRect rect,
			float ovalWidth,
			float ovalHeight)
		{


			float fw, fh;
			// If either ovalWidth or ovalHeight is 0, draw a regular rectangle.
			if (ovalWidth == 0 || ovalHeight == 0) {
				context.AddRect ( rect);
			}else{
				context.SaveState();
				// Translate to lower-left corner of rectangle.
				context.TranslateCTM( rect.GetMinX(), rect.GetMinY());

				// Scale by the oval width and height so that
				// each rounded corner is 0.5 units in radius.
				context.ScaleCTM( ovalWidth, ovalHeight);
				// Unscale the rectangle width by the amount of the X scaling.
				fw = (float) rect.Width/ ovalWidth;
				// Unscale the rectangle height by the amount of the Y scaling.
				fh = (float)rect.Height/ ovalHeight;
				// Start at the right edge of the rect, at the midpoint in Y.
				context.MoveTo(fw, fh/2);
				// Segment 1
				context.AddArcToPoint((nfloat) fw,(nfloat) fh, (nfloat)fw/2, fh,(nfloat) 0.5);
				// Segment 2
				context.AddArcToPoint( (nfloat)0,(nfloat) fh, (nfloat)0, (nfloat)fh/2, (nfloat)0.5);
				// Segment 3
				context.AddArcToPoint( (nfloat)0,(nfloat) 0,(nfloat) fw/2, (nfloat)0, (nfloat)0.5);
				// Segment 4
				context.AddArcToPoint( (nfloat)fw,(nfloat) 0,(nfloat) fw,(nfloat) fh/2, (nfloat)0.5);
				// Closing the path adds the last segment.
				context.ClosePath();
				context.RestoreState();
			}
		}

		public  void doRoundedRects()
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGRect rect = new CGRect(10,10,210,150);

			float ovalWidth = 100, ovalHeight = 100;

			context.SetLineWidth( 2);
			context.BeginPath();
			addRoundedRectToPath(context, rect, ovalWidth, ovalHeight);
			context.SetStrokeColor(1, 0, 0, 1);
			context.DrawPath(CGPathDrawingMode.FillStroke);

		}

		public void doStrokeWithCTM()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			context.TranslateCTM( 150, 180);
			context.SetLineWidth( 10);
			// Draw ellipse 1 with a uniform stroke.
			context.SaveState();
			// Scale the CTM so the circular arc will be elliptical.
			context.ScaleCTM( 2, 1);
			context.BeginPath();
			// Create an arc that is a circle.
			context.AddArc((nfloat) 0.0f,(nfloat) 0.0f,(nfloat) 45.0f, (nfloat)0.0f, (nfloat)(2.0f*3.1416f),(bool) false); // one way to do it
			// Restore the context parameters prior to stroking the path.
			// CGContextRestoreGState does not affect the path in the context.
			context.RestoreState();
			context.StrokePath();

			// *** was 0, -120
			context.TranslateCTM( 220, 0);
			// Draw ellipse 2 with non-uniform stroke.
			context.SaveState();
			// Scale the CTM so the circular arc will be elliptical.
			context.ScaleCTM( 2, 1);
			context.BeginPath();
			// Create an arc that is a circle.
			context.AddArc( 0, 0, 45, 0, 2.0f*3.1416f, false); // the other way to do it
			// Stroke the path with the scaled coordinate system in effect.
			context.StrokePath();
			context.RestoreState();
		}
			
		public void doRotatedEllipsesWithCGPath()
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			int i, totreps = 144;

			CGPath path = null;
			//CGMutablePathRef path = NULL;
			float  tint = 1.0f, tintIncrement = 1.0f/totreps;

			// Create a new transform consisting of a 45 degree rotation.

			CGAffineTransform theTransform = CGAffineTransform.MakeRotation(3.1416f/4.0f);

			// Apply a scaling transformation to the transform just created.
			theTransform.Scale(1,2);
		
			// Create a mutable CGPath object.
			path = new CGPath();
			if(path != null){
				//fprintf(stderr, "Couldn't create path!\n");
				return;
			}
			// Add a circular arc to the CGPath object, transformed
			// by an affine transform.

			//path.AddArc                                                        // !!!!!!!!  NOT FINISHED !!!!!!!!!!!!!!!!
			//CGPathAddArc(path, &theTransform, 0., 0., 45., 0., 2*M_PI, false); // !!!!!!!!  NOT FINISHED !!!!!!!!!!!!!!!!
			// Close the CGPath object.
			path.CloseSubpath();

			// Place the first ellipse at a good location.	
			context.TranslateCTM (100.0f, 100.0f);
			for (i = 0 ; i < totreps ; i++){
				context.BeginPath();
				// Add the CGPath object to the current path in the context.
				context.AddPath( path);

				// Set the fill color for this instance of the ellipse.
				context.SetFillColor( tint, 0, 0, 1);
				// Filling the path implicitly closes it.
				context.FillPath();
				// Compute the next tint color.
				tint -= tintIncrement;
				// Move over for the next ellipse.
				context.TranslateCTM( 1, 0);
			}
			// Release the path when done with it.
			path = null;
		}


		public CGPoint alignPointToUserSpace( CGPoint p)
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// Compute the coordinates of the point in device space.
			CGRect r = new CGRect(p.X,p.Y, 1,1);
			r = context.ConvertRectToDeviceSpace( r);
			// Ensure that coordinates are at exactly the corner
			// of a device pixel.
			p.X = (nint) Math.Floor(r.X);
			p.Y = (nint) Math.Floor(r.Y);
			// Convert the device aligned coordinate back to user space.
			return context.ConvertPointToUserSpace(p);
		}


		public CGSize alignSizeToUserSpace( CGSize s)
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// Compute the size in device space.
			s = context.ConvertSizeToDeviceSpace( s);
			// Ensure that size is an integer multiple of device pixels.
			s.Width = (nint) Math.Floor(s.Width);
			s.Height = (nint) Math.Floor(s.Height);
			// Convert back to user space.
			return context.ConvertSizeToUserSpace( s);
		}

		public CGRect alignRectToUserSpace( CGRect r)
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// Compute the coordinates of the rectangle in device space.
			r = context.ConvertRectToUserSpace( r);
			// Ensure that the x and y coordinates are at a pixel corner.
			r.X = (nint)Math.Floor(r.X);
			r.Y = (nint) Math.Floor(r.Y);
			// Ensure that the width and height are an integer number of
			// device pixels. Note that this produces a width and height
			// that is less than or equal to the original width. Another
			// approach is to use ceil to ensure that the new rectangle
			// encloses the original one.
			r.Width =  (nint) Math.Floor(r.Width);
			r.Height =  (nint) Math.Floor(r.Height);

			// Convert back to user space.
			return context.ConvertRectToUserSpace(r);
		}


		public void doPixelAlignedFillAndStroke()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGPoint p1 = new CGPoint(16.7f, 17.8f);
			CGPoint p2 = new CGPoint(116.7f, 17.8f);
			CGRect r = new CGRect(16.7f, 20.8f, 100.6f, 100.6f);
			CGSize s;

			context.SetLineWidth( 2);
			context.SetFillColor( 1, 0, 0, 1);
			context.SetStrokeColor( 1, 0, 0, 1);

			// Unaligned drawing.
			context.BeginPath();
			context.MoveTo( p1.X, p1.Y);
			context.AddLineToPoint( p2.X, p2.Y);
			context.StrokePath();
			context.FillRect( r);

			// Translate to the right before drawing along
			// aligned coordinates.
			context.TranslateCTM(106, 0);

			// Aligned drawing.

			// Compute the length of the line in user space.
			s = new CGSize(p2.X - p1.X, p2.Y - p1.Y);

			context.BeginPath();
			// Align the starting point to a device
			// pixel boundary.
			p1 = alignPointToUserSpace( p1);
			// Establish the starting point of the line.
			context.MoveTo( p1.X, p1.Y);
			// Compute the line length as an integer
			// number of device pixels.
			s = alignSizeToUserSpace( s);
			context.AddLineToPoint( 
				p1.X + s.Width, 
				p1.Y + s.Height);
			context.StrokePath();
			// Compute a rect that is aligned to device
			// space with a width that is an integer
			// number of device pixels.
			r = alignRectToUserSpace( r);
			context.FillRect( r);
		}


	}
}
