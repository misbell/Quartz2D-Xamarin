using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

// all the shadow and transparency code from gelphman


namespace Quartz2DCode
{
	public class ShadowAndTransparency : NSView
	{

		NSImage _image1, _image2;
		const string kGenericRGBProfile = "/System/Library/ColorSync/Profiles/Generic RGB Profile.icc";

		public ShadowAndTransparency ()
		{
		
			;
		}

		public override void DrawRect (CGRect dirtyRect)
		{

		
			//DrawColoredLogo ();
			//drawSimpleShadow ();

			//doShadowScaling();
			//drawFillAndStrokeWithShadow(); // shows a cool transparency

			//other methods
			//showComplexShadowIssues();
			//showComplexShadow();

			//shadowPDFDocument();

		}

	

		private float DegreesToRadians (float degrees)
		{
			return (float)(degrees * Math.PI) / 180.0f;
		}

		CGColorSpace getGenericRGBColorSpace ()
		{


			NSData ddata = NSData.FromFile ("/System/Library/ColorSync/Profiles/Generic RGB Profile.icc");

			byte[] bytedata = System.IO.File.ReadAllBytes ("/System/Library/ColorSync/Profiles/Generic RGB Profile.icc");

			NSData data = NSData.FromArray (bytedata);
			CGColorSpace colorspace = CGColorSpace.CreateICCProfile (data);

			return null; //colorspace;

		}

		CGColorSpace getTheCalibratedRGBColorSpace ()
		{
			CGColorSpace genericRGB = CGColorSpace.CreateWithName (CoreGraphics.CGColorSpaceNames.GenericRGB);


			CGColorSpace extraGenericRGB = this.getGenericRGBColorSpace ();


			return genericRGB;
		}

		CGColor getRGBOpaqueOrangeColor ()
		{
			CGColor rgbOrange = null;
			if (rgbOrange == null) {
				// { 0.965, 0.584, 0.059, 1. };
				nfloat[] opaqueOrange = { (nfloat)0.965, (nfloat)0.584, (nfloat)0.059, (nfloat)1.0 };    
				rgbOrange = new CGColor (getTheCalibratedRGBColorSpace (), opaqueOrange);
				int i = 0;
			}
			return rgbOrange;
		}

		CGColor getRGBOpaqueYellowColor ()
		{
			CGColor rgbYellow = null;
			if (rgbYellow == null) {
				//{ 1., 0.816, 0., 1. };  
				nfloat[] opaqueYellow = { (nfloat)1.0, (nfloat)0.816, (nfloat)0.0, (nfloat)1.0 };    
				rgbYellow = new CGColor (getTheCalibratedRGBColorSpace (), opaqueYellow);
				int i = 0;
			}
			return rgbYellow;
		}

		CGColor getRGBOpaquePurpleColor ()
		{
			CGColor rgbPurple = null;
			if (rgbPurple == null) {
				nfloat[] opaquePurple = { (nfloat)0.69, (nfloat)0.486, (nfloat)0.722, (nfloat)1.0 };    
				rgbPurple = new CGColor (getTheCalibratedRGBColorSpace (), opaquePurple);
				int i = 0;
			}
			return rgbPurple;
		}


		public void DrawColoredLogo ()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;
	
		
			CGRect r = new CGRect (0, 0, 100, 100);

			context.SaveState ();
				
			// Position the center of the rectangle on the left.
			context.TranslateCTM (250.0f, 140.0f);

			// Rotate so that the rectangles are rotated 45 degrees 
			// about the current coordinate origin.
			context.RotateCTM (DegreesToRadians (45));

			// Translate so that the center of the rect is at the previous origin.
			context.TranslateCTM (-r.Size.Width / 2, -r.Size.Height / 2);


			// Set the fill color to a purple color.
			context.SetFillColor (getRGBOpaquePurpleColor ());

			// Fill the first rectangle.
			context.FillRect (r);

			// Position to draw the right-most rectangle.
			context.TranslateCTM (-60.0f, +60.0f);

			// Set the fill color to a yellow color.
			context.SetFillColor (getRGBOpaqueYellowColor ());
			context.FillRect (r);

			context.TranslateCTM (-30.0f, +30.0f);

			// Set the stroke color to an orange color.
			context.SetStrokeColor (getRGBOpaqueOrangeColor ());
			// Stroke the rectangle with a linewidth of 12.
			context.StrokeRectWithWidth (r, 12);


			context.RestoreState ();


		}


		public void CreateTrianglePath (CGContext context)
		{

		
			context.BeginPath ();
			context.MoveTo (0, 0);
			context.AddLineToPoint (50, 0);
			context.AddLineToPoint (25, 50);
			context.ClosePath ();


		}

		public void drawSimpleShadow ()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGSize offset = new CGSize ();
			CGRect r = new CGRect (20, 20, 100, 200);
			float blur;
			CGColor shadowColor;


			context.TranslateCTM (20, 300);

			// A blur of 0 is a hard edge blur.
			blur = 0;
			// An offset where both components are negative casts a shadow to the
			// left and down from the object. The coordinate system for the offset
			// is base space, not current user space.
			offset.Width = -7;
			offset.Height = -7;

			offset = scaleShadowOffset (offset, r);

			// Set the shadow in the context. 
			context.SetShadow (offset, blur);

			// Object 1. 
			// Paint a rectangle.
			context.FillRect (r);

			// Object 2.
			context.TranslateCTM (150, 0);
			// A blur of 3 is a soft blur more
			// appropriate for a shadow effect.
			blur = 3;
			context.SetShadow (offset, blur);

			// Fill an ellipse to the right of the rect.
			context.BeginPath ();
			context.AddEllipseInRect (r);
			context.FillPath ();


			// Object 3.
			context.TranslateCTM (-130, -140);
			// Scale the coordinate system but the shadow is not affected. The offset
			// is in the base space of the context. Typically it looks best if the shapes
			// have a uniform shadow regardless of how the shapes were created, scaled,
			// rotated, or otherwise transformed.
			context.TranslateCTM (2, 2);
			CreateTrianglePath (context);
			context.SetStrokeColor (getRGBOpaquePurpleColor ());

			context.SetLineWidth (5);
			// Stroking produces a shadow as well.
			context.StrokePath ();

			// Object 4.
			context.TranslateCTM (75, 0);
			CreateTrianglePath (context);
			// Cast the shadow to the left and up from
			// the shape painted.
			offset.Width = -5;
			offset.Height = +7;

			offset = scaleShadowOffset (offset, r);

			// The shadow can be colored. Create a CGColorRef
			// that represents a red color with opacity of 0.3333...

			shadowColor = new CGColor (getRGBOpaqueOrangeColor (), (nfloat)1.0f / 3.0f);

			context.SetShadow (offset, blur, shadowColor);
			// Release the color now that the shadow color is set.
			//CGColorRelease(shadowColor); //****************************** assume I don't have to do this
			context.StrokePath ();

			// Object 5. Three stroked circles.
			context.TranslateCTM (-75, -65);
			// Set a black shadow offset at -7,-7.
			offset.Width = -7;
			offset.Height = -7;

			offset = scaleShadowOffset (offset, r);

			context.SetShadow (offset, blur);
			// Draw a set of three circles side by side.
			context.BeginPath ();
			context.SetLineWidth (3);
			r = new CGRect (30, 20, 20, 20);
			context.AddEllipseInRect (r);

			// change origin 
			r.X = r.X + 20;
			//r = new CGRect(r, 20, 0);
			context.AddEllipseInRect (r);

			// change origin
			r.X = r.X + 20;
			//r = new CGRect(r, 20, 0);
			context.AddEllipseInRect (r);
			context.StrokePath ();
		}


	
		// also no cgrectoffset

		public CGSize scaleShadowOffset (CGSize offset, CGRect r)
		{

			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			float shadowScaling = ScalingFactor;
			// Adjust the shadow offset if scaling to export as bits. This is equivalent to scaling base
			// space by the scaling factor.
			if (shadowScaling != 1.0) {



				CGAffineTransform atta = CGAffineTransform.MakeScale (shadowScaling, shadowScaling);

				CGRect newr = atta.TransformRect (r);

				offset = newr.Size;

	




				// Adjust the shadow offset if scaling to export as bits. This is equivalent to scaling base
				// space by the scaling factor.
				//if(shadowScaling != 1.0)
				//	offset = CGSizeApplyAffineTransform(offset, 
				//		CGAffineTransformMakeScale(shadowScaling, shadowScaling));

			}

			return offset;
		}



		// These routines are used for getting the correct results when
		// drawing shadows and patterns with Quartz and exporting the
		// results as bits. This is a hack; in principle the scaling
		// factor should be passed to the draw proc.


		private float gScalingFactor = 1.0f;

		public float ScalingFactor {

			get { return gScalingFactor; }
			set { gScalingFactor = value; }
		
		}



		public void doShadowScaling ()
		{

			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGSize offset = new CGSize ();
			offset.Height = -7;
			offset.Width = -7;

			float blur = 3;
			CGAffineTransform t;

			context.TranslateCTM (20, 220);

			CGRect r = new CGRect (0, 0, offset.Height, offset.Width);
			context.SetShadow (scaleShadowOffset (offset, r), blur);

			// Object 1
			// Draw a triangle filled with black and shadowed with black.
			CreateTrianglePath (context);
			context.FillPath ();

			// Object 2
			// Scaling without changing the shadow doesn't impact
			// the shadow offset or blur.

			t = CGAffineTransform.MakeScale (2, 2);
			context.ConcatCTM (t);
			context.TranslateCTM (40, 0);
			CreateTrianglePath (context);
			context.FillPath ();


			// Object 3
			// By transforming the offset you can transform the shadow. 
			// This may be desirable if you are drawing a zoomed view.

			CGRect r2 = new CGRect (0, 0, offset.Height, offset.Width);
			CGRect newr = t.TransformRect (r2);
			offset = newr.Size;
			context.SetShadow (scaleShadowOffset (offset, r2), blur);
			context.TranslateCTM (70, 0);
			CreateTrianglePath (context);
			context.FillPath ();

		}

	
		public void drawFillAndStrokeWithShadow ()
		{

			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGRect r = new CGRect (60, 60, 100, 100);
			CGSize offset = new CGSize ();
			offset.Height = -7;
			offset.Width = -7;
			float blur = 3;

			// Set the shadow.
			CGRect r2 = new CGRect (0, 0, offset.Height, offset.Width);
			context.SetShadow (scaleShadowOffset (offset, r2), blur);
			context.SetFillColor (getRGBOpaqueOrangeColor ());


			// Draw the graphic on the left.
			context.BeginPath ();
			context.AddEllipseInRect (r);
			context.DrawPath (CGPathDrawingMode.FillStroke);

			// Draw the graphic on the right.
			r.X = r.X + 125;

			// Begin the transparency layer.
			context.BeginTransparencyLayer (null);
			context.AddEllipseInRect (r);
			context.DrawPath (CGPathDrawingMode.FillStroke);
			// End the transparency layer.
			context.EndTransparencyLayer ();
		}
			

		public void showComplexShadowIssues ()
		{

			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGSize offset = new CGSize();
			offset.Height = -6;
			offset.Width = -6;
			float blur = 3;

			// Set the shadow.
			CGRect r = new CGRect (0, 0, offset.Height, offset.Width);
			context.SetShadow ( scaleShadowOffset (offset,r), blur);
			// Draw the colored logo.
			DrawColoredLogo ();
		}


		void showComplexShadow()
		{

			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGSize offset = new CGSize();
			offset.Height = -6;
			offset.Width = -6;
			float blur = 3;

			// Set the shadow.
			CGRect r = new CGRect (0, 0, offset.Height, offset.Width);
			context.SetShadow ( scaleShadowOffset (offset,r), blur);

			/*	Begin a transparency layer. A snapshot is made of the graphics state and the
		shadow parameter is temporarily reset to no shadow, the blend mode is set to
		Normal, and the global alpha parameter is set to 1.0.
		
		All drawing that occurs after CGContextBeginTransparencyLayer but before
		CGContextEndTransparencyLayer is collected together and when CGContextEndTransparencyLayer
		is called, Quartz composites the collected drawing to the context, using the global
		alpha, blend mode, and shadow that was in effect when CGContextBeginTransparencyLayer 
		was called.
    */
			context.BeginTransparencyLayer (null);
			DrawColoredLogo();


			/*  Ending the transparency layer causes all drawing in the transparency layer to be
	    composited with the global alpha, blend mode, and shadow in effect at the time 
	    CGContextBeginTransparencyLayer was called.
	    
	    The graphics state is restored to that in effect when CGContextBeginTransparencyLayer
	    was called.    
	*/
			// This restores the graphics state to that in effect
			// at the last call to CGContextBeginTransparencyLayer. 
			// End the transparency layer.
			context.EndTransparencyLayer ();
		}

		public void shadowPDFDocument()
		{

			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			CGSize offset = new CGSize();
			offset.Height = -7;
			offset.Width = -7;
			float blur = 3;

			NSData ddata = NSData.FromUrl (new NSUrl("http://manuals.info.apple.com/MANUALS/1000/MA1595/en_US/ipad_user_guide.pdf"));
			byte[] data = ddata.ToArray();

			CGDataProvider prov = new CGDataProvider(data, 0, data.Length);


			CGPDFDocument pdfDoc = new CGPDFDocument(prov);

			// or more simply

			CGPDFDocument pdfDoc2 = CGPDFDocument.FromUrl("http://manuals.info.apple.com/MANUALS/1000/MA1595/en_US/ipad_user_guide.pdf");

			if (pdfDoc2 == null)
				return;


			CGPDFDictionary cgpdfdict1 = pdfDoc2.GetInfo();
			CGPDFDictionary cgpdfdict2 = pdfDoc2.GetCatalog();
			CGPDFPage page =  pdfDoc2.GetPage(1);

			int major, minor;
			pdfDoc2.GetVersion(out major, out minor);

			CGRect r = page.GetBoxRect(CGPDFBox.Media); // also art, bleed etc
			r.X = 20;
			r.Y = 20;


	

			// Set the shadow.
			context.SetShadow( scaleShadowOffset(offset,r), 3);


			// On Tiger and later, there is no need to use
			// a transparency layer to draw a PDF document as
			// a grouped object. On Panther, you can do so
			// by using a transparency layer. Drawing collected in
			// this transparency layer is drawn with the shadow
			// when the layer is ended.
			context.BeginTransparencyLayer( null);

			using(CGContext g = NSGraphicsContext.CurrentContext.GraphicsPort){
				//g.TranslateCTM (0, Bounds.Height);
				//g.ScaleCTM (1, -1);

				// render the first page of the PDF
				using (CGPDFPage pdfPage = pdfDoc2.GetPage (1)) {

					//get the affine transform that defines where
					//the PDF is drawn
					CGAffineTransform t = pdfPage.GetDrawingTransform
						(CGPDFBox.Crop, r, 0, true);
					//concatenate the pdf transform with the CTM for
					//display in the view
					g.ConcatCTM (t);

					//draw the pdf page
					g.DrawPDFPage (pdfPage);
				}
			}
		

			context.EndTransparencyLayer();

			// On Panther, the PDF document must not be released before the context
			// is released if the context is a PDF or printing context. You
			// should release the document after you release the PDF context.
			//CGPDFDocumentRelease(pdfDoc);
			

				/*








				}
				*/
		}

	}
}

