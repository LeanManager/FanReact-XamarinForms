using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using FanReact;
using FanReact.Droid.Activities;
using Xamarin.Forms;

[assembly: Dependency(typeof(FanReact.Droid.AppNavigation))]
namespace FanReact.Droid
{

	public class AppNavigation : IAppNavigation
	{
		public static Activity Activity { get; set; }
		public static FragmentManager Manager { get; set; }
		public static int ResourceId { get; set; }

		public void Pop()
		{
			//Not needed just have the user use the back button
		}

		public void PushNNM(NativeNavigationModel nnm){
			NavigateByString nbs = new NavigateByString(nnm);
			nbs.NavigateDROID(nnm.fragmentType, nnm.fragmentValue);
		}

		public void Push(Page page)
		{

			NavigateByString nbs = new NavigateByString();
			nbs.NavigatePage(page);

		}

		void SetTitle()
		{
			if (Activity != null && Activity is AppCompatActivity)
				((AppCompatActivity)Activity).SupportActionBar.Title = GetTitle();
		}

		void SetHomeEnabled()
		{
			if (Activity != null && Activity is AppCompatActivity)
				((AppCompatActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(Manager.BackStackEntryCount > 1);
		}

		string GetTitle()
		{
			if (Manager.BackStackEntryCount == 0)
				return string.Empty;

			return Manager.GetBackStackEntryAt(Manager.BackStackEntryCount - 1)?.BreadCrumbTitleFormatted?.ToString() ?? string.Empty;
		}

	}


}
