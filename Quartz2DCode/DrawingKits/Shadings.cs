using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;

namespace Quartz2DCode
{
	public class Shadings
	{
		public Shadings ()
		{
		}


		//static void RedBlackRedRampEvaluate(void *info, const float *in, float *out)
		static public unsafe void RedBlackRedRampEvaluate( CGFunction.CGFunctionEvaluate info,  nfloat *infloat, nfloat *outfloat)
		{
			/*	The domain of this function is 0 - 1. For an input value of 0
		this function returns the color to paint at the start point
		of the shading. For an input value of 1 this function returns
		the color to paint at the end point of the shading. This
		is a 1 in, 4 out function where the output values correspond
		to an r,g,b,a color.
		
		For an RGB color space as the shading color space, this
		function evaluates to produce a blend from pure, opaque
		red at the start point to a pure opaque black at the 
		midpoint, and back to pure opaque red at the end point.
    */
			nfloat newff = 1.0f;
			outfloat = &newff;
			// The red component evaluates to 1 for an input value of 0
			// (the start point of the shading). It smoothly reduces
			// to zero at the midpoint of the shading (input value 0.5)  
			// and increases up to 1 at the endpoint of the shading (input
			// value 1.0).
			outfloat[0] = (float)(Math.Abs(1.0 - infloat[0]*2));
			// The green and blue components are always 0.
			outfloat[1] = outfloat[2] = 0;
			// The alpha component is 1 for the entire shading.
			outfloat[3] = 1;
		}


		// should look something like this


//		struct MPIFunctionCallbacks {

//				int version; 
//				CGFunction.CGFunctionEvaluate evaluate; 
//			unsafe * releaseInfo ;


//		} ;


		// dummy function, see the function below
		public unsafe CGFunction createFunctionForRGB(CGFunction.CGFunctionEvaluate evaluationFunction,nfloat *infloat, nfloat *outfloat) {
			nfloat[] domain = new nfloat[1];
			nfloat[] range = new nfloat[1];
			return new CGFunction(domain, range, evaluationFunction);
		}

		/* at this time CANNOT IMPLEMENT DUE TO ISSUES WITH CGFunctionCallbacks
		 * 
		 * 
		//static CGFunction createFunctionForRGB(CGFunctionEvaluateCallback evaluationFunction)
		static CGFunction createFunctionForRGB(CGFunction.CGFunctionEvaluate evaluationFunction)


		{

		
			CGFunction function;
			float[] domain;	// 1 input
			float[] range;	// 4 outputs
			//CGFunctionCallbacks shadingCallbacks; // this opaque object or structure not surfaced in code https://github.com/mono/maccore/pull/45/files

			MPIFunctionCallbacks shadingCallbacks = new MPIFunctionCallbacks();


		//	/*	This is a 1 in, 4 out function for drawing shadings 
		//in a 3 component (plus alpha) color space. Shadings 
		//parameterize the endpoints such that the starting point
		//represents the function input value 0 and the ending point 
		//represents the function input value 1. 
    
			domain[0] = 0; domain[1] = 1;

		//	/*	The range is the range for the output colors. For an rgb
		//color space the values range from 0-1 for the r,g,b, and a
		//components. 
	

			// The red component, min and max.    
			range[0] = 0; range[1] = 1;
			// The green component, min and max.    
			range[2] = 0; range[3] = 1;
			// The blue component, min and max.    
			range[4] = 0; range[5] = 1;
			// The alpha component, min and max.    
			range[6] = 0; range[7] = 1;

			// The callbacks structure version is
			// 0, the only defined version as of Tiger.
			shadingCallbacks.version = 0;
			// The routine Quartz should call to evaluate the function.
			shadingCallbacks.evaluate = evaluationFunction;
			// This code does not use a releaseInfo parameter since this 
			// CGFunction has no resources it uses that need to be released
			// when the function is released.
			shadingCallbacks.releaseInfo = NULL;

			// Dimension of domain is 1 and dimension of range is 4.    
			function = CGFunctionCreate(NULL, 1, domain, 4, range, &shadingCallbacks);
			if(function == NULL){
				fprintf(stderr, "Couldn't create the CGFunction!\n");
				return NULL;
			}
			return function;
		}
		*/

		/*
		public unsafe void doSimpleAxialShading()
		{
			CGFunction axialFunction;
			CGShading shading;
			CGPoint startPoint, endPoint;
			bool extendStart, extendEnd;


			// work with the current context, associated with view
			NSGraphicsContext ccontext = NSGraphicsContext.CurrentContext;
			CGContext context = NSGraphicsContext.CurrentContext.GraphicsPort;

			// This shading paints colors in the calibrated Generic RGB 
			// color space so it needs a function that evaluates 1 in to 4 out.
			nfloat a = 1.0f;
			nfloat b = 1.0f;

			nfloat* c = &a;
			nfloat* d = &b;

			// not working
			axialFunction = null;
	//		axialFunction = createFunctionForRGB(RedBlackRedRampEvaluate, &c, &d);
			if(axialFunction == null){
				return;
			}

			// Start the shading at the point (20,20) and
			// end it at (420,20). The axis of the shading
			// is a line from (20,20) to (420,20).
			startPoint.X = 20;
			startPoint.Y = 20;
			endPoint.X = 420;
			endPoint.Y = 20;

			// Don't extend this shading.
			extendStart = extendEnd = false;

		

			shading = CGShading.CreateAxial((getTheCalibratedRGBColorSpace(), 
				startPoint, endPoint, 
				axialFunction, 
				extendStart, extendEnd);
			// The shading retains the function and this code
			// is done with the function so it should release it.

			// doesn't exist
			// is this just taken care of?
			CGFunctionRelease(axialFunction);
			if(shading == NULL){
				fprintf(stderr, "Couldn't create the shading!\n");
				return;
			}
			// Draw the shading. This paints the shading to
			// the destination context, clipped by the
			// current clipping area.
			CGContextDrawShading(context, shading);
			// Release the shading once the code is done with
			// it.
			CGShadingRelease(shading);
		}
				*/
	}
}



