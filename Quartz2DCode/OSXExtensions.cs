
using System;
using AppKit;
using CoreGraphics;
using CoreImage;

namespace Quartz2DCode
{

	public static class OSXExtensionMethods
	{

		public static bool IsNumeric (this string s)
		{

			return true;
		}

		public static NSColor BackgroundColor (this NSView view)
		{

			return NSColor.FromCIColor (CIColor.FromCGColor (view.Layer.BackgroundColor));
		}

		public static void SetBackgroundColor (this NSView view, NSColor backgroundColor)
		{

			view.WantsLayer = true;
			view.Layer.BackgroundColor = backgroundColor.CGColor;

		}

		public static void SetBorderColor (this NSView view, NSColor borderColor)
		{
			view.WantsLayer = true;
			view.Layer.BorderColor = borderColor.CGColor;
		}

		public static void SetBorderWidth (this NSView view, nfloat borderWidth)
		{
			view.WantsLayer = true;
			view.Layer.BorderWidth = borderWidth;
		}

	}

}

