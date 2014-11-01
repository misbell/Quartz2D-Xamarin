using System;
using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

// all the DrawingBasics.c code translated from gelphman


namespace Quartz2DCode
{
	public class DrawingBasics : NSView
	{
		// 
		NSImage _image1, _image2;
		const string kGenericRGBProfile = "/System/Library/ColorSync/Profiles/Generic RGB Profile.icc";

		public DrawingBasics ()
		{


		}

		public override void DrawRect (CGRect dirtyRect)
		{

			//DoEgg();

		}



		public void doSimpleRect ()
		{
			CGRect ourRect = new CGRect ();

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// Set the fill color to opaque red.
			context.SetFillColor (1.0f, 0.0f, 0.0f, 1.0f);
			// Set up the rectangle for drawing.
			ourRect.X = ourRect.Y = 20.0f;
			ourRect.Width = 130.0f;
			ourRect.Height = 100.0f;
			// Draw the filled rectangle.
			context.FillRect (ourRect);
		}

		public void doStrokedRect ()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGRect ourRect = new CGRect ();
			// Set the stroke color to a light opaque blue.
			context.SetStrokeColor (0.482f, 0.62f, 0.871f, 1f);
			// Set up the rectangle for drawing.
			ourRect.X = ourRect.Y = 20.0f;
			ourRect.Width = 130.0f;
			ourRect.Height = 100.0f;
			// Draw the stroked rectangle with a line width of 3.
			context.StrokeRectWithWidth (ourRect, 3.0f);
		}

		public void doStrokedAndFilledRect ()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// Define a rectangle to use for drawing.
			CGRect ourRect = new CGRect (20, 220, 130, 100); 

			// ***** Rectangle 1 *****
			// Set the fill color to a light opaque blue.
			context.SetFillColor (0.482f, 0.62f, 0.871f, 1);
			// Set the stroke color to an opaque green.
			context.SetStrokeColor (0.404f, 0.808f, 0.239f, 1);
			// Fill the rect.
			context.FillRect (ourRect);
			// ***** Rectangle 2 *****
			// Move the rectangle’s origin to the right by 200 units.
			ourRect.X += 200;
			// Stroke the rectangle with a line width of 10.
			context.StrokeRectWithWidth (ourRect, 10);
			// ***** Rectangle 3 *****
			// Move the rectangle’s origin to the left by 200 units
			// and down by 200 units.
			ourRect.X -= 200;
			ourRect.Y -= 200;
			// Fill then stroke the rect with a line width of 10.
			context.FillRect (ourRect);
			context.StrokeRectWithWidth (ourRect, 10);
			// ***** Rectangle 4 *****
			// Move the rectangle’s origin to the right by 200 units.
			ourRect.X += 200;
			// Stroke then fill the rect.
			context.StrokeRectWithWidth (ourRect, 10);
			context.FillRect (ourRect);
		}

		public void createRectPath (CGRect rect)
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// Create a path using the coordinates of the rect passed in.
			context.BeginPath ();
			context.MoveTo (rect.X, rect.Y);
			// ***** Segment 1 *****
			context.AddLineToPoint (rect.X + rect.Width,
				rect.Y);
			// ***** Segment 2 *****
			context.AddLineToPoint (rect.X + rect.Width,
				rect.Y + rect.Height);
			// ***** Segment 3 *****
			context.AddLineToPoint (rect.X, 
				rect.Y + rect.Height);
			// ***** Segment 4 is created by closing the path *****
			context.ClosePath ();
		}

		public void doPathRects ()
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// Define a rectangle to use for drawing.
			CGRect ourRect = new CGRect (20, 220, 130, 100); 

			// ***** Rectangle 1 *****
			// Create the rect path.
			createRectPath (ourRect);
			// Set the fill color to a light opaque blue.
			context.SetFillColor (0.482f, 0.62f, 0.871f, 1);
			// Fill the path.
			context.DrawPath (CGPathDrawingMode.Fill); // Clears the path.
			// ***** Rectangle 2 *****
			// Translate the coordinate system 200 units to the right.
			context.TranslateCTM (200, 0);
			// Set the stroke color to an opaque green.
			context.SetStrokeColor (0.404f, 0.808f, 0.239f, 1);
			createRectPath (ourRect);
			// Set the line width to 10 units.
			context.SetLineWidth (10);
			// Stroke the path.
			context.DrawPath (CGPathDrawingMode.Stroke);  // Clears the path.
			// ***** Rectangle 3 *****
			// Translate the coordinate system 
			// 200 units to the left and 200 units down.
			context.TranslateCTM (-200f, -200f);
			createRectPath (ourRect);
			//CGContextSetLineWidth(context, 10);	// This is redundant.
			// Fill, then stroke the path.
			context.DrawPath (CGPathDrawingMode.FillStroke);  // Clears the path.
			// ***** Rectangle 4 *****
			// Translate the coordinate system 200 units to the right.
			context.TranslateCTM (200, 0);
			createRectPath (ourRect);
			// Stroke the path.
			context.DrawPath (CGPathDrawingMode.Stroke);  // Clears the path.
			// Create the path again.
			createRectPath (ourRect);
			// Fill the path.
			context.DrawPath (CGPathDrawingMode.Fill); // Clears the path.

		}

		public void doAlphaRects ()
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// ***** Part 1 *****
			CGRect ourRect = new CGRect (0, 0, 130, 100); 
			int i, numRects = 6;
			float rotateAngle = (float)(2f * Math.PI / numRects);
			float tint, tintAdjust = 1 / numRects;

			// ***** Part 2 *****
			context.TranslateCTM (2 * ourRect.Width, 
				2 * ourRect.Height);

			// ***** Part 3 *****
			for (i = 0, tint = 1.0f; i < numRects; i++, tint -= tintAdjust) {
				context.SetFillColor (tint, 0.0f, 0.0f, tint);
				context.FillRect (ourRect);
				// These transformations are cummulative.
				context.RotateCTM (rotateAngle);
			} 

		}



		public void drawStrokedLine (CGPoint start, CGPoint end)
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			context.BeginPath ();
			context.MoveTo (start.X, start.Y);
			context.AddLineToPoint (end.X, end.Y);
			context.DrawPath (CGPathDrawingMode.Stroke);
		}

		public void doDashedLines ()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGPoint start = new CGPoint ();
			CGPoint end = new CGPoint ();
			nfloat[] lengths = { 12.0f, 6.0f, 5.0f, 6.0f, 5.0f, 6.0f };

			start.X = 20;
			start.Y = 270;
			end.X = 300;
			end.Y = 270;

			// ***** Line 1 solid line *****
			context.SetLineWidth (5);
			drawStrokedLine (start, end);
			// ***** Line 2 long dashes *****
			context.TranslateCTM (0, -50);
			context.SetLineDash (0.0f, lengths, (int)2);
			drawStrokedLine (start, end);
			// ***** Line 3 long short pattern *****
			context.TranslateCTM (0, -50);
			context.SetLineDash (0.0f, lengths, 4);
			drawStrokedLine (start, end);
			// ***** Line 4 long short short pattern *****
			context.TranslateCTM (0, -50);
			context.SetLineDash (0.0f, lengths, 6);
			drawStrokedLine (start, end);
			// ***** Line 5 short short long pattern *****
			context.TranslateCTM (0.0f, -50);
			context.SetLineDash (lengths [0] + lengths [1], lengths, 6);
			drawStrokedLine (start, end);
			// ***** Line 6 solid line *****
			context.TranslateCTM (0, -50);
			// Reset dash to solid line.
			context.SetLineDash (0.0f, null, 0);
			drawStrokedLine (start, end);
		}

		public void doClippedCircle ()
		{
			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGPoint circleCenter = new CGPoint (150, 150);
			float circleRadius = 100.0f;
			float startingAngle = 0.0f, endingAngle = (float)(2 * Math.PI);
			CGRect ourRect = new CGRect (65, 65, 170, 170);

			// ***** Filled Circle ***** )
			CGColor rgbColor = new CGColor (0.663f, .031f, 1.0f);
			context.SetFillColor (0.663f, 0f, 0.031f, 1.0f);
			context.BeginPath ();
			// Construct the circle path counterclockwise.
			context.AddArc (circleCenter.Y, 
				circleCenter.Y, circleRadius, 
				startingAngle, endingAngle, false);

			context.DrawPath (CGPathDrawingMode.Fill);

			// ***** Stroked Square ***** 
			context.StrokeRect ( ourRect);

			// Translate so that the next drawing doesn’t overlap what 
			// has already been drawn.
			context.TranslateCTM( ourRect.Size.Width + circleRadius + 5, 0);
			// Create a rectangular path and clip to that path.
			context.BeginPath();
			context.AddRect(ourRect);

			context.Clip();

			// ***** Clipped Circle *****
			context.BeginPath();
			// Construct the circle path counterclockwise.
			context.AddArc( circleCenter.X, 
				circleCenter.Y, circleRadius,
				startingAngle, endingAngle, false);
			context.DrawPath( CGPathDrawingMode.Fill);
		}


		public void doPDFDocument( NSUrl url)
		{
			CGRect pdfRect;

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;


			NSData ddata = NSData.FromUrl (url);
			byte[] data = ddata.ToArray();
			CGDataProvider prov = new CGDataProvider(data, 0, data.Length);
			CGPDFDocument pdfDoc = new CGPDFDocument(prov);

			if(pdfDoc != null){


				// The media box is the bounding box of the PDF document.

				// Draw page 1 of the PDF document.

				using(CGContext g = NSGraphicsContext.CurrentContext.GraphicsPort){
			
					// render the first page of the PDF
					using (CGPDFPage pdfPage = pdfDoc.GetPage (1)) {

						g.ScaleCTM(.5f, .5f);
						pdfRect = pdfPage.GetBoxRect(CGPDFBox.Media);

						// Set the destination rect origin to the Quartz origin.
						CGPoint loc = new CGPoint(0.0f,0.0f);
						pdfRect.Location = loc;
						g.DrawPDFPage (pdfPage);

						g.TranslateCTM( pdfRect.Size.Width*1.2f, 0f);
						// Scale non-uniformly making the y coordinate scale 1.5 times
						// the x coordinate scale.
						g.ScaleCTM( 1f, 1.5f);
						g.DrawPDFPage( pdfPage);

						g.TranslateCTM( pdfRect.Size.Width*1.2f, pdfRect.Size.Height);
						// Flip the y coordinate axis horizontally about the x axis.
						g.ScaleCTM( 1, -1);
						g.DrawPDFPage(pdfPage);

					}
				}


			}else {
			//	fprintf(stderr, "Can't create PDF document for URL!\n");
			}
		}

	

	}
}
