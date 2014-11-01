using System;
using AppKit;
using CoreGraphics;
using Foundation;

namespace Quartz2DCode
{
	public class SimpleViewController : NSViewController
	{
		public SimpleViewController ()
		{
		}

		public SimpleViewController(CGRect frame) {

			// set the view you want here
			// need some kind of menu system here, as in Gelphman's example

			this.View = new PathDrawing();
			this.View.SetFrameSize (frame.Size);
			this.View.SetFrameOrigin (frame.Location);



		}

		public override void AwakeFromNib ()
		{
			// not called here
			base.AwakeFromNib ();
		}

		public override void LoadView ()
		{
			base.LoadView ();
		}


	}
}



/*
 * 
 * Listing 2-1  Drawing to a window graphics context
@implementation MyQuartzView
 
- (id)initWithFrame:(NSRect)frameRect
{
    self = [super initWithFrame:frameRect];
    return self;
}
 
- (void)drawRect:(NSRect)rect
{
    CGContextRef myContext = [[NSGraphicsContext // 1
                                currentContext] graphicsPort];
                                
   // ********** Your drawing code here ********** // 2
CGContextSetRGBFillColor (myContext, 1, 0, 0, 1);// 3
CGContextFillRect (myContext, CGRectMake (0, 0, 200, 100 ));// 4
CGContextSetRGBFillColor (myContext, 0, 0, 1, .5);// 5
CGContextFillRect (myContext, CGRectMake (0, 0, 100, 200));// 6
}

@end

*/

