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
		static public void RedBlackRedRampEvaluate( NSObject info,  float[] infloat, out float[] outfloat)
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
		public void MPIFunctionCallbacks () {

			// version
			// evaluate
			// releaseInfo
		}


		//static CGFunction createFunctionForRGB(CGFunctionEvaluateCallback evaluationFunction)
		static CGFunction createFunctionForRGB(CGFunction.CGFunctionEvaluate evaluationFunction)


		{
			CGFunction function;
			float[] domain;	// 1 input
			float[] range;	// 4 outputs
			//CGFunctionCallbacks shadingCallbacks; // this opaque object or structure not surfaced in code https://github.com/mono/maccore/pull/45/files

			MPIFunctionCallbacks shadingCallbacks = new MPIFunctionCallbacks();


			/*	This is a 1 in, 4 out function for drawing shadings 
		in a 3 component (plus alpha) color space. Shadings 
		parameterize the endpoints such that the starting point
		represents the function input value 0 and the ending point 
		represents the function input value 1. 
    */
			domain[0] = 0; domain[1] = 1;

			/*	The range is the range for the output colors. For an rgb
		color space the values range from 0-1 for the r,g,b, and a
		components. 
	*/

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
	}
}



