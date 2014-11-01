using System;

using Foundation;
using AppKit;

namespace Quartz2DCode
{
	public partial class MainWindow : NSWindow
	{
		public MainWindow (IntPtr handle) : base (handle)
		{
		}

		[Export ("initWithCoder:")]
		public MainWindow (NSCoder coder) : base (coder)
		{
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
		}
	}
}
