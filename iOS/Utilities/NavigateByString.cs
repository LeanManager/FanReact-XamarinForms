using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;

namespace FanReact.iOS
{
	public class NavigateByString
	{
		private NativeNavigationModel nnm { get; set; }

		public NavigateByString(NativeNavigationModel nm)
		{
			nnm = nm;
		}

		public NavigateByString()
		{
		}

		public void NavigateXF()
		{

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null)
				vc = vc.PresentedViewController;
			var navController = vc as UINavigationController;
			UIStoryboard board = UIStoryboard.FromName(nnm.storyboard, null);
			UIViewController ctrl = (UIViewController)board.InstantiateViewController(nnm.viewController);
			navController.PushViewController(ctrl, true);

		}

		public void NavigateIOS(string story, string viewCtrl)
		{

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null)
				vc = vc.PresentedViewController;
			var navController = vc as UINavigationController;
			UIStoryboard board = UIStoryboard.FromName(story, null);
			UIViewController ctrl = (UIViewController)board.InstantiateViewController(viewCtrl);
			navController.PushViewController(ctrl, true);

		}

		public void NavigateForm(Page xfPage){
			//Forms.Init();
			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			if (vc != null) {
				if (vc as UINavigationController != null)
				{
					var navController = vc as UINavigationController;
					navController.NavigationBar.Hidden = false;
					var iosViewController = xfPage.CreateViewController();
					navController.PushViewController(iosViewController, true);
				}
				else if (vc as UITabBarController != null)
				{
					var vct = vc as UITabBarController;
					var vcui = vct.SelectedViewController as UINavigationController;
					if (vcui != null)
					{
						vcui.NavigationBar.Hidden = false;
						var iosViewController = xfPage.CreateViewController();
						vcui.PushViewController(iosViewController, true);
					}
				}
			} 
		}

		//TODO: Make this method better. Might not be completely functional
		public void NavigateForm(Page xfPage, UINavigationController uiNavigationController) {
			uiNavigationController.NavigationBar.Hidden = false;
			var iosViewController = xfPage.CreateViewController();
			uiNavigationController.PushViewController(iosViewController, true);
		}

	}
}
