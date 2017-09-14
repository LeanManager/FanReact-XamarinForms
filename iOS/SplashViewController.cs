#region

using Foundation;
using System;
using UIKit;
using Xamarin.Forms;

#endregion

namespace FanReact.iOS {
	public partial class SplashViewController : UIViewController {
		public SplashViewController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			//View.BackgroundColor = ColorCodes.UpwardsBlue.ToUIColor();
			SetNeedsStatusBarAppearanceUpdate();
		}

		partial void GoToFormsButton_TouchUpInside(UIButton sender)
		{
			//go to forms logic

			// Navigate to Coach Tools
			NavigateByString nbs = new NavigateByString();
			Page pg = new NavigationPage(new MyTeamsPage());
			nbs.NavigateForm(pg);
		}

		public override void ViewDidAppear(bool animated) {
			//SegueUtil.PresentMainTabBarController(this);
		}
	}
}