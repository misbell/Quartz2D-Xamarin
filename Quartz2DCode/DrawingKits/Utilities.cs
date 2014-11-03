using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

// original code saved line by line
// original code saved line by line 
// converted by Michael Prenez-Isbell
// Oct-Nov 2014

namespace Quartz2DCode
{
	public class Utilities
	{
		private const float kTickLength= 5.0f;
		private const float kTickDistance= 72.0f;
		private const float kAxesLength =(20f*kTickDistance);

		public Utilities ()
		{
		}

		// no cfbundle call in xamarin
		NSBundle getAppBundle()
		{
			NSBundle appBundle = null;
			if(appBundle == null)
				appBundle = NSBundle.MainBundle;

			return appBundle;
		}



		/*
		This version of getTheRGBColorSpace returns
		the DeviceRGB color space.
		*/
		static CGColorSpace getTheRGBColorSpace()
		{
		
			CGColorSpace deviceRGB = null;
		// Set once, the first time this function is called.
		if(deviceRGB == null)
				deviceRGB = CGColorSpace.CreateDeviceRGB();

		return deviceRGB;
		}


		/*
		// The full path to the generic RGB ICC profile.
		const string kGenericRGBProfilePathStr = "/System/Library/ColorSync/Profiles/Generic RGB Profile.icc";

		public static CGColorSpace getTheRGBColorSpace()
		{
			 CGColorSpace genericRGBColorSpace = NULL;
			if(genericRGBColorSpace == NULL)
			{
				CMProfileRef genericRGBProfile = NULL;
				OSStatus err = noErr;
				CMProfileLocation loc;
				// Build up a profile location for ColorSync.
				loc.locType = cmPathBasedProfile;
				strcpy (loc.u.pathLoc.path, kGenericRGBProfilePathStr);
				// Open the profile with ColorSync.
				err = CMOpenProfile(&genericRGBProfile, &loc);
				if(err == noErr){
					genericRGBColorSpace =
						CGColorSpaceCreateWithPlatformColorSpace(genericRGBProfile);
					if(genericRGBColorSpace == NULL)
						fprintf(stderr, "couldn't create the generic RGB color space\n");
					// This code opened the profile so it is 
					// up to it to close it.
					CMCloseProfile(genericRGBProfile); 
				}else{
					// ColorSync could not open the profile so log a message 
					// to the console.
					fprintf(stderr, "couldn't open generic profile due to error %d\n",
						(int)err);
				}
			}
			return genericRGBColorSpace;
		}

		// This only builds on Tiger and later.
		CGColorSpace getTheCalibratedRGBColorSpace()
		{
			 CGColorSpace genericRGBColorSpace = NULL;
			if(genericRGBColorSpace == NULL)
			{
				// Test the symbol kCGColorSpaceGenericRGB to see if 
				// it is available. If so, use it.
				if(&kCGColorSpaceGenericRGB != NULL)
					genericRGBColorSpace = CGColorSpaceCreateWithName(kCGColorSpaceGenericRGB);

				// If genericRGBColorSpace is still NULL, use the technique
				// of using ColorSync to open the disk based profile by using 
				// getTheRGBColorSpace.
				if(genericRGBColorSpace == NULL){
					genericRGBColorSpace = getTheRGBColorSpace();
				}
			}
			return genericRGBColorSpace;
		}

		*/

		//void drawCoordinateAxes(CGContextRef context)
		static public void drawCoordinateAxes(CGContext context)
		{
			int i;
			float t;
			float tickLength = kTickLength;

			// work with the current context, associated with view


			//CGContextSaveGState(context);
			context.SaveState();

			//CGContextBeginPath(context);
			context.BeginPath();

			// Paint the x-axis in red.
			//CGContextSetRGBStrokeColor(context, 1, 0, 0, 1);
			context.SetStrokeColor(1,0,0,1);

			context.MoveTo( -kTickLength, 0.0f);

			context.AddLineToPoint(kAxesLength, 0.0f);
			//CGContextAddLineToPoint(context, kAxesLength, 0.);

			context.DrawPath(CGPathDrawingMode.Stroke);
			//CGContextDrawPath(context, kCGPathStroke);

			// Paint the y-axis in blue.
			//CGContextSetRGBStrokeColor(context, 0, 0, 1, 1);
			context.SetStrokeColor(0, 0, 1, 1);

			//CGContextMoveToPoint(context, 0, -kTickLength);
			context.MoveTo( 0, -kTickLength);

			//CGContextAddLineToPoint(context, 0, kAxesLength);
			context.AddLineToPoint(0, kAxesLength);

			//CGContextDrawPath(context, kCGPathStroke);
			context.DrawPath(CGPathDrawingMode.Stroke);

			// Paint the x-axis tick marks in red.
			//CGContextSetRGBStrokeColor(context, 1, 0, 0, 1);
			context.SetStrokeColor(1,0,0,1); // red, w alpha

			for(i = 0; i < 2 ; i++)
			{
				for(t=0.0f; t < kAxesLength ; t += kTickDistance){
					context.MoveTo(t, -tickLength);
					//CGContextMoveToPoint(context, t, -tickLength);
					context.AddLineToPoint(t, tickLength);
					//CGContextAddLineToPoint(context, t, tickLength);
				}
				context.DrawPath(CGPathDrawingMode.Stroke);
				//CGContextDrawPath(context, kCGPathStroke);
				context.RotateCTM((nfloat)(Math.PI/2f));
				//CGContextRotateCTM(context, M_PI/2.);
				// Paint the y-axis tick marks in blue.
				context.SetStrokeColor(0,0,1,1);
				//CGContextSetRGBStrokeColor(context, 0, 0, 1, 1);
			}
			CGPoint pointZero = new CGPoint(0,0);
			//Utilities.drawPoint(context, CGPointZero); // cgpointzero doesn't exist in xamarin?
			Utilities.drawPoint(context, pointZero);
			//CGContextRestoreGState(context);
			context.RestoreState();
		}

		static public void drawPoint(CGContext context, CGPoint p)
		{
			// work with the current context, associated with view


			// CGContextSaveGState(context);
			context.SaveState();

			// Opaque black.
			//CGContextSetRGBStrokeColor(context, 0, 0, 0, 1);
			context.SetStrokeColor(0,0,0,1);

			//CGContextSetLineWidth(context, 5);
			context.SetLineWidth(5);

			//CGContextSetLineCap(context, kCGLineCapRound);
			context.SetLineCap(CGLineCap.Round); 	

			//CGContextMoveToPoint(context, p.x, p.y);
			context.MoveTo(p.X, p.Y);

			//CGContextAddLineToPoint(context, p.x, p.y);
			context.AddLineToPoint(p.X,p.Y);

			//CGContextStrokePath(context);
			context.StrokePath();

			//CGContextRestoreGState(context);
			context.RestoreState();


		}




	}
}

