using System;
using AppKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;



namespace Quartz2DCode
{
	public class DataProvidersAndConsumers
	{
		public DataProvidersAndConsumers ()
		{
		}

		CGDataProvider createDataProviderFromPathName (string path)
		{
		

			// Create a CFURL for the supplied file system path.
	
			NSData ddata = NSData.FromFile (path);
			byte[] data = ddata.ToArray ();

			// Create a Quartz data provider for the URL.
			CGDataProvider dataProvider = new CGDataProvider (data, 0, data.Length);

			// Release the URL when done with it.
			//CFRelease(url);
			if (dataProvider == null) {
				//fprintf(stderr, "Couldn't create data provider!\n");
				return null;
			}
			return dataProvider;
		}


		//	static void rgbReleaseRampData(void *info, const void *data, size_t size)
		//	{
		//		free((char *)data);
		//	}



		public CGDataProvider createRGBRampDataProvider ()
		{
			CGDataProvider dataProvider = null;
			byte width = unchecked((byte) 256);
			byte height = unchecked((byte)256);

			byte[] imageData = new byte[width * height * 3];

			/*
	    Build an image that is RGB 24 bits per sample. This is a ramp
	    where the red component value increases in red from left to 
	    right and the green component increases from top to bottom.
	*/



			byte r, g;
			int component, idata = 0;
			for (g = 0; g < height; g++) {
				for (r = 0; r < width; r++) {
					for (component = 0; component < 3; component++) {
						if (component == 0)
							imageData [idata++] = r;
						if (component == 1)
							imageData [idata++] = g;
						if (component == 2)
							imageData [idata++] = 0; // no b

					}

				}
			}

			// Once this data provider is created, the data associated 
			// with dataP MUST be available until Quartz calls the data 
			// releaser function 'rgbReleaseRampData'.
			dataProvider = new CGDataProvider(imageData,0, width*height*3);
			return dataProvider;
		}


	}
}

