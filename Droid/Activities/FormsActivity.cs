#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using FanReact.Droid.Fragments;
using FanReact.ViewService;
using Android.Support.Design.Widget;
using FanReact.Droid.Utilities;
using Calligraphy;
using Xamarin.Forms.Platform.Android;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using FragmentTransaction = Android.App.FragmentTransaction;
using Xamarin.Forms;
using FanReact.UWService;
using XLabs.Ioc;

#endregion

namespace FanReact.Droid.Activities
{
	[Activity(ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class FormsActivity : FormsAppCompatActivity
	{
		protected FloatingActionButton FAB;
		private FragmentManager FM;
		public Fragment FragmentToLoad;

		public FragmentTransaction lft { get; set; }
		public Android.App.Fragment lpFragment { get; set; }

		private readonly IUWBoardControllerService _uwBoardControllerService;

		public FormsActivity() {
			_uwBoardControllerService = Resolver.Resolve<IUWBoardControllerService>();
		}

		protected override void AttachBaseContext(Context @base)
		{
			ViewUtil.SetFont(true, @base.GetString(Resource.String.AppFont));
			base.AttachBaseContext(CalligraphyContextWrapper.Wrap(@base));
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SimpleActivity);

			SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.simpleActivityToolBar));

			FAB = (FloatingActionButton)FindViewById(Resource.Id.simpleActivityFAB);
			FAB.Hide();


			FM = SupportFragmentManager;
			var ft = FM.BeginTransaction();

			var fragString = Intent.GetStringExtra(Constants.KEY_FRAGMENT_TYPE);
			var longId = Intent.GetLongExtra(Constants.EXTRA_LONG_ID, -1);

			switch (fragString)
			{
				case "FanReact.LoginPage":
					lpFragment = new LoginPage().CreateFragment(this);
					lft = FragmentManager.BeginTransaction();
					lft.AddToBackStack(null);
					lft.Replace(Resource.Id.simpleActivityContent, lpFragment, "Login Page");
					lft.Commit();
					return;
				case "FanReact.SecondPage":
					lpFragment = new SecondPage().CreateFragment(this);
					lft = FragmentManager.BeginTransaction();
					lft.AddToBackStack(null);
					lft.Replace(Resource.Id.simpleActivityContent, lpFragment, "Second Page");
					lft.Commit();
					return;
				case Constants.FRAG_TYPE_ALL_PROGRAMS:
					//GoToAllProgramsPage(longId);
					lpFragment = new AllProgramsPage(longId).CreateFragment(this);
					lft = FragmentManager.BeginTransaction();
					lft.AddToBackStack(null);
					lft.Replace(Resource.Id.simpleActivityContent, lpFragment, Constants.FRAG_TYPE_ALL_PROGRAMS);
					lft.Commit();
					return;
			}

		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:

					OnBackPressed();
					break;
			}
			return false;
		}

		private async void GoToAllProgramsPage(long boardId){
			var uwBoardView = await _uwBoardControllerService.GetUpwardBoardView(boardId);

			if (uwBoardView != null) {
				var allProgramsFragment = new AllProgramsPage(uwBoardView).CreateFragment(this);
				var ft = FragmentManager.BeginTransaction();
				ft.AddToBackStack(null);
				ft.Replace(Resource.Id.simpleActivityContent, allProgramsFragment, Constants.FRAG_TYPE_ALL_PROGRAMS);
				ft.Commit();
			}
		}
	}
}