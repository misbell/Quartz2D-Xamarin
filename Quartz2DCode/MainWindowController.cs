using System;

using Foundation;
using AppKit;

namespace Quartz2DCode
{
	public partial class MainWindowController : NSWindowController
	{
		public MainWindowController (IntPtr handle) : base (handle)
		{
		}

		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
		}

		public MainWindowController () : base ("MainWindow")
		{
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			//new CoreGraphics.CGRect(
			// can i get coordinates of window frame?

			NSViewController sim = new SimpleViewController (new CoreGraphics.CGRect(0.0f, 0.0f, 800.0f,600.0f));
			this.Window.ContentView.AddSubview (sim.View);

		}

		public new MainWindow Window {
			get { return (MainWindow)base.Window; }
		}
	}
}
