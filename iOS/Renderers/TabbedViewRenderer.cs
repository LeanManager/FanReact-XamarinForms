using System;
using System.Diagnostics;
using FanReact;
using FanReact.iOS;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedView), typeof(TabbedViewRenderer))]
namespace FanReact.iOS
{
    class TabbedViewRenderer : ViewRenderer<TabbedView, UISegmentedControl>
    {
        UISegmentedControl segmentedControl;

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedView> e)
        {
            base.OnElementChanged(e);

            segmentedControl = new UISegmentedControl();

			segmentedControl.Frame = new CGRect(20, 20, 280, 40);

			segmentedControl.InsertSegment("CURRENT", 0, true);
			//segmentedControl.InsertSegment("ARCHIVE", 1, true);
			segmentedControl.SelectedSegment = 0;

            segmentedControl.SetWidth(100f, 0);
            //segmentedControl.SetWidth(100f, 1);
            segmentedControl.BackgroundColor = UIColor.Clear;

            segmentedControl.Layer.CornerRadius = 0;
            segmentedControl.Layer.BorderColor = (UIColor.White).CGColor;
            segmentedControl.Layer.BorderWidth = 1.5f;

            //var redLine = new UIView(new CGRect(0, 0, 100, 7))
            //{
            //    BackgroundColor = UIColor.FromRGB(122, 0, 38)
            //};

            //
            var textAttributes = new UITextAttributes();
            textAttributes.TextColor = UIColor.FromRGB(122, 0, 38);
            textAttributes.Font = UIFont.BoldSystemFontOfSize(12);
            //
            segmentedControl.SetTitleTextAttributes(textAttributes, UIControlState.Selected);

			segmentedControl.ApportionsSegmentWidthsByContent = true;

			segmentedControl.Layer.MasksToBounds = true;

            segmentedControl.TintColor = UIColor.White;

			// The simplest approach is to have two views that you can toggle their visibility to indicate which view has been selected.
			segmentedControl.ValueChanged += (sender, eventArgs) =>
			{
				var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;

                // do something with either sender and/or eventArgs to swap colors dynamically at runtime

    //            if (selectedSegmentId == 0)
    //            {
    //                segmentedControl.SetTitleTextAttributes(textAttributes, UIControlState.Selected);
    //                segmentedControl.AddSubview(redLine);
    //                segmentedControl.BringSubviewToFront(redLine);

				//	// Or use MessagingCenter/interfaces to notify Xamarin.Forms main view

    //            }
    //            else
    //            {
    //                segmentedControl.SetTitleTextAttributes(textAttributes, UIControlState.Selected);

    //                var line = new UIView(new CGRect(100, 0, 100, 7))
    //                {
    //                    BackgroundColor = UIColor.FromRGB(122, 0, 38),
    //                };
    //                segmentedControl.AddSubview(line);
    //                segmentedControl.BringSubviewToFront(line);
				//}
            };

            SetNativeControl(segmentedControl);

			// After the native control is assigned, call MessagingCenter.Subscribe to subscribe 
			// to the tab message and set OnTabClicked to the Action callback.
			MessagingCenter.Subscribe<TabbedView>(this, "Tab", OnTabClicked);
        }

		// We want to ensure the native control is updated when properties are changed on the Xamarin.Forms element.
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

            //if (segmentedControl.)
            //{

            //}
        }

		// This method will be called when the tab message is received.
		void OnTabClicked(TabbedView sender)
		{
			// We want to ensure we only respond to messages from the element associated to this instance of the renderer. 
			// Check if sender matches Element. If it does, call the Clear method on Control.
			if (sender == Element)
            {
                // TODO
            }
		}
    }
}
