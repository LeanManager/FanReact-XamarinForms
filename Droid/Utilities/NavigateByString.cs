using System;
using Android.Content;
using Android.Views;
using FanReact.Droid.Activities;
using Xamarin.Forms;

namespace FanReact.Droid
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
			Context context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(nnm.fragmentType, nnm.fragmentValue);
			context.StartActivity(intent);
		}

		public void NavigateDROID(string fragType, string fragValue)
		{
			Context context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(fragType, fragValue);
			context.StartActivity(intent);
		}

		public void NavigatePage(Page page){

			string fragValue = page.ToString();
			Context context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(FormsActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, fragValue);
			context.StartActivity(intent);
	
		}

		public void NavigatePage(String fragType) {
			Context context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(FormsActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, fragType);
			context.StartActivity(intent);
		}

		public void Pop()
		{
			//Not needed right now due to back button
		}

	}
}

