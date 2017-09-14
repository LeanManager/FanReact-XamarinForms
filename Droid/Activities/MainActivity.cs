#region

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using V4Fragment = Android.Support.V4.App.Fragment;
using Plugin.Iconize.Droid.Controls;
using Android.Support.V4.View;
using System.Collections.Generic;
using Android.Runtime;
using System;
using FanReact.ViewService;
using System.Linq;
using Android.Content.PM;
using FanReact.Droid.Interfaces;
using XLabs.Ioc;
using Plugin.Connectivity;
using Android.Support.V4.Content;
using FanReact.WebService.Interfaces;
using FanReact.ViewService.Interfaces;
using Calligraphy;
using FanReact.Droid.Utilities;
using Square.Picasso;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using static Android.Views.View;
using FanReact.UWService;
using Android.Preferences;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using FanReact.WebService;
//using Android.Gms.Common;

#endregion

namespace FanReact.Droid
{
	[Activity(ConfigurationChanges = ConfigChanges.Orientation, MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : FormsAppCompatActivity, IPageScrollListener, ViewPager.IOnPageChangeListener,
								IOnClickListener
	{
		#region ctor

		public MainActivity()
		{
			_settingsService = Resolver.Resolve<ISettingsViewControllerService>();
			_trackingService = Resolver.Resolve<IViewTrackingService>();
			_profileDrawerControllerService = Resolver.Resolve<IProfileDrawerControllerService>();

		}

		#endregion

		public void OnClick(Android.Views.View v)
		{
			string link = null;
			//List<LinkObject> links = _profileDrawerControllerService.GetLinkObjects();
			var getInvolvedLinks = _profileDrawerControllerService.GetGetInvolvedItems();
			var moreLinks = _profileDrawerControllerService.GetMoreItems();
			var footerLinks = _profileDrawerControllerService.GetFooterItems();
			switch (v.Id)
			{
				case Resource.Id.find_a_league:
					link = getInvolvedLinks[0].Url;
					break;
				case Resource.Id.volunteer:
					//link = "http://www.upward.org/getinvolved/volunteer";
					link = getInvolvedLinks[1].Url;
					break;
				case Resource.Id.donate:
					//link = "http://www.upward.org/foundation";
					link = getInvolvedLinks[2].Url;
					break;
				case Resource.Id.about_upward:
					//link = "http://www.upward.org/about";
					link = moreLinks[0].Url;
					break;
				case Resource.Id.contact_us:
					//link = "http://www.upward.org/about/contactus";
					link = moreLinks[1].Url;
					break;
				case Resource.Id.drawerPrivacyPolicy:
					link = footerLinks[0].Url;
					break;
				case Resource.Id.drawerTermsAndConditions:
					link = footerLinks[1].Url;
					break;
				case Resource.Id.drawerFanreactTechnologies:
					//link = footerLinks[2].Url;
					//break;
					IntentUtil.GoToLoginPage(this);
					return;
				case Resource.Id.flDrawerNonAuthenticatedHeader_AvatarContainer:
					IntentUtil.GoToSignup(this);
					return;
				case Resource.Id.drawerAuthenticatedHeader:
					var profileId = (long)v.Tag;
					IntentUtil.GoToProfile(this, (int)profileId);
					break;
				default:
					link = null;
					break;
			}
			if (link != null) ViewUtil.GoToLink(this, link, true);
		}

		void ViewPager.IOnPageChangeListener.OnPageScrollStateChanged(int state) { }

		void ViewPager.IOnPageChangeListener.OnPageScrolled(int position, float positionOffset, int positionOffsetPixels) { }

		void ViewPager.IOnPageChangeListener.OnPageSelected(int position)
		{
			switch (position)
			{
				//case 0:
				//	NavView.Menu.FindItem(Resource.Id.navHome).SetChecked(true);
				//                _trackingService.HomeFeedPage();
				//	break;
				//case 1:
				//	NavView.Menu.FindItem(Resource.Id.navTrending).SetChecked(true);
				//                _trackingService.TrendingPage();
				//	break;
				//case 2:
				//	NavView.Menu.FindItem(Resource.Id.navBoards).SetChecked(true);
				//                _trackingService.BoardsPage();
				//	break;
				//case 3:
				//NavView.Menu.FindItem(Resource.Id.navNotificaitons).SetChecked(true);
				//               _trackingService.NotificationsPage();
				//break;
			}
		}

		void IPageScrollListener.PostFeedOnReachedBottom()
		{
			var fragment = (HomeFeedFragment)Fragments[INDEX_HOME_FEED_FRAGMENT];
			if (!fragment.ReachedEndOfPosts) fragment.LoadMoreData();
		}

		void IPageScrollListener.TrendingOnReachedBottom()
		{
			((TrendingFragment)Fragments[INDEX_TRENDING_FRAGMENT]).LoadMoreData();
		}

		void IPageScrollListener.NotificationsOnReachedBottom()
		{
			((NotificationFragment)Fragments[INDEX_NOTIFICATIONS_FRAGMENT]).LoadMoreData();
		}

		private void SetConnectivityMessage()
		{
			connectivityMessage = Snackbar.Make(MainViewPager, "No internet connection.", Snackbar.LengthIndefinite);

			if (!CrossConnectivity.Current.IsConnected) connectivityMessage.Show();
			CrossConnectivity.Current.ConnectivityChanged += (sender, e) =>
			{
				if (CrossConnectivity.Current.IsConnected)
					connectivityMessage.Dismiss();
				else connectivityMessage.Show();
			};
		}

		private void SetUpFragments()
		{
			Fragments = new List<V4Fragment>();

			Fragments.Add(new HomeFeedFragment());

			//Fragments.Add(new TrendingFragment());

			//Fragments.Add(new BoardsFragment());

			//Fragments.Add(NotificationFragment.NewInstance());
		}

		public void SetUpNavigationDrawer()
		{
			GetDrawerInfo();
			//If not logged in, show nonauthenticated section and hide authenticated
			//Otherwise, set up profile picture, profile full name, handle and favorites
			NavView.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.createAccountButton_Icon)
				   .AddView(ViewUtil.GetSVGImageView(this, "arrow_right_white_small", 24, Android.Resource.Color.White));
			NavView.FindViewById(Resource.Id.createAccountButton).Click += CreateAccount_Click;
			NavView.FindViewById(Resource.Id.alreadyHaveAccountButton).Click += SignIn_Click;
			NavView.FindViewById(Resource.Id.about_upward).Click += GoToAppInfo_Click;

			float iconSize = 60;

			var findALeague = ViewUtil.GetSVGImageView(this, "location_grey", iconSize);
			var volunteer = ViewUtil.GetSVGImageView(this, "volunteer_grey", iconSize);
			var donate = ViewUtil.GetSVGImageView(this, "donate_grey", iconSize);
			var aboutUpward = ViewUtil.GetSVGImageView(this, "about_grey", iconSize);
			var contact = ViewUtil.GetSVGImageView(this, "contact_grey", iconSize);
			var preferences = ViewUtil.GetSVGImageView(this, "settings_grey", iconSize);

			NavView.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.find_a_league_icon).AddView(findALeague);
			NavView.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.volunteer_icon).AddView(volunteer);
			NavView.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.donate_icon).AddView(donate);

			NavView.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.about_upward_icon).AddView(aboutUpward);
			NavView.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.contact_us_icon).AddView(contact);
			NavView.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.preferences_icon).AddView(preferences);

			NavView.FindViewById(Resource.Id.drawerLogout).Click += Logout_Click;

			NavView.FindViewById(Resource.Id.find_a_league).SetOnClickListener(this);
			NavView.FindViewById(Resource.Id.volunteer).SetOnClickListener(this);
			NavView.FindViewById(Resource.Id.contact_us).SetOnClickListener(this);
			NavView.FindViewById(Resource.Id.donate).SetOnClickListener(this);
			NavView.FindViewById(Resource.Id.about_upward).SetOnClickListener(this);
			NavView.FindViewById(Resource.Id.drawerPrivacyPolicy).SetOnClickListener(this);
			NavView.FindViewById(Resource.Id.drawerFanreactTechnologies).SetOnClickListener(this);
			NavView.FindViewById(Resource.Id.drawerTermsAndConditions).SetOnClickListener(this);
		}

		public void FinishNavigationDrawer()
		{
			NavView.FindViewById(Resource.Id.createAccountButton).Click -= CreateAccount_Click;
			NavView.FindViewById(Resource.Id.alreadyHaveAccountButton).Click -= SignIn_Click;
			NavView.FindViewById(Resource.Id.about_upward).Click -= GoToAppInfo_Click;

			NavView.FindViewById(Resource.Id.drawerLogout).Click -= Logout_Click;
		}

		private void Logout_Click(object sender, EventArgs e)
		{
			Logout();
			//IntentUtil.GoToLogin(this);
		}

		private async void Logout()
		{
			await _profileDrawerControllerService.LogOut();

			RunOnUiThread(() =>
			{
				var i = new Intent(this, typeof(MainActivity));
				i.AddFlags(ActivityFlags.ClearTop);
				Finish();
				StartActivity(i);
			});
		}

		private void CreateAccount_Click(object sender, EventArgs e)
		{
			IntentUtil.GoToSignup(this);
		}

		private void SignIn_Click(object sender, EventArgs e)
		{
			IntentUtil.GoToLogin(this);
		}

		private void GoToAppInfo_Click(object sender, EventArgs e)
		{
			IntentUtil.GotToAppInfo(this);
		}

		private void SetTabIcons()
		{
			var tabIconSize = 42;
			var navIconSize = 14;
			var tabColor = ContextCompat.GetColor(this, Resource.Color.white);
			var navColor = ContextCompat.GetColor(this, Resource.Color.primary);

			var homeIcon = "fa-home";
			MainTabLayout.GetTabAt(0).SetIcon(ViewUtil.GetSVGDrawable(this, "home_tab_bar", tabIconSize, tabColor, "#FFFFFF"));
			//NavView.Menu.FindItem(Resource.Id.navHome).SetIcon(new IconDrawable(this, homeIcon).Color(navColor).SizeDp(navIconSize));

			//var trendingIcon = "md-trending-up";
			//MainTabLayout.GetTabAt(1).SetIcon(new IconDrawable(this, trendingIcon).Color(tabColor).SizeDp(tabIconSize));
			//NavView.Menu.FindItem(Resource.Id.navTrending).SetIcon(new IconDrawable(this, trendingIcon).Color(navColor).SizeDp(navIconSize));

			//var boardIcon = "fa-users";
			//MainTabLayout.GetTabAt(2).SetIcon(new IconDrawable(this, boardIcon).Color(tabColor).SizeDp(tabIconSize));
			//NavView.Menu.FindItem(Resource.Id.navBoards).SetIcon(new IconDrawable(this, boardIcon).Color(navColor).SizeDp(navIconSize));

			if (Fragments.Count > 1)
			{
				var bellIcon = "fa-bell";
				MainTabLayout.GetTabAt(1).SetIcon(ViewUtil.GetSVGDrawable(this, "notifications_white", tabIconSize, tabColor));


				MainTabLayout.GetTabAt(2).SetIcon(ViewUtil.GetSVGDrawable(this, "messaging_white", tabIconSize, tabColor));
				//UpwardFontTextView v = ViewUtil.GetUpwardFontTextView(this, "O", navIconSize, Resource.Color.white);
				//v.DrawingCacheEnabled = true;
				//v.Measure(MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified), MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
				//v.Layout(0, 0, v.MeasuredWidth, v.MeasuredHeight);

				//v.BuildDrawingCache(true);
				//Bitmap b = Bitmap.CreateBitmap(v.DrawingCache);
				//v.DrawingCacheEnabled = false;
				//MainTabLayout.GetTabAt(1).SetIcon(new BitmapDrawable(b));
			}
			//NavView.Menu.FindItem(Resource.Id.navNotificaitons).SetIcon(new IconDrawable(this, bellIcon).Color(navColor).SizeDp(navIconSize));

			//NavView.Menu.FindItem(Resource.Id.navMyProfile).SetIcon(new IconDrawable(this, "md-person").Color(navColor).SizeDp(navIconSize));
			//NavView.Menu.FindItem(Resource.Id.navLogout).SetIcon(new IconDrawable(this, "fa-sign-out").Color(navColor).SizeDp(navIconSize));
			//NavView.Menu.FindItem(Resource.Id.navSearch).SetIcon(new IconDrawable(this, "md-search").Color(navColor).SizeDp(navIconSize));
			//NavView.Menu.FindItem(Resource.Id.navAppInfo).SetIcon(new IconDrawable(this, "md-info-outline").Color(navColor).SizeDp(navIconSize));
		}

		private void DrawerItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			switch (e.MenuItem.ItemId)
			{
				case Resource.Id.navHome:
					MainViewPager.SetCurrentItem(0, false);
					e.MenuItem.SetChecked(true);
					break;
				case Resource.Id.navTrending:
					e.MenuItem.SetChecked(true);
					MainViewPager.SetCurrentItem(1, false);
					break;
				case Resource.Id.navBoards:
					e.MenuItem.SetChecked(true);
					MainViewPager.SetCurrentItem(2, false);
					break;
				case Resource.Id.navNotificaitons:
					e.MenuItem.SetChecked(true);
					MainViewPager.SetCurrentItem(3, false);
					break;
				case Resource.Id.navSearch:
					IntentUtil.GoToSearch(this);
					break;
				case Resource.Id.navMyProfile:
					IntentUtil.GoToProfile(this);
					break;
				case Resource.Id.navAppInfo:
					IntentUtil.GotToAppInfo(this);
					_trackingService.AppInfoPage();
					break;
				case Resource.Id.navLogout:
					Logout();
					break;
			}

			DrawerLayout.CloseDrawers();
		}

		private async void GetDrawerInfo()
		{
			var profile = await _profileDrawerControllerService.GetUserProfile();

			RunOnUiThread(() =>
			{
				if (profile != null)
				{
					//set up profile picture, logout button and favorites if the user is logged in
					LoggedIn = true;
					NavView.FindViewById(Resource.Id.drawerNonAuthenticatedHeader).Visibility = ViewStates.Gone;
					NavView.FindViewById(Resource.Id.drawerLogout).Visibility = ViewStates.Visible;
					var authHeader = NavView.FindViewById(Resource.Id.drawerAuthenticatedHeader);
					authHeader.Visibility = ViewStates.Visible;
					Picasso.With(this).Load(profile.ProfileImageUrl)
						   .Into(authHeader.FindViewById<ImageView>(Resource.Id.drawerAuthenticatedHeader_Avatar));

					authHeader.Tag = profile.ProfileId;
					authHeader.SetOnClickListener(this);

					authHeader.FindViewById<TextView>(Resource.Id.drawerUser_Fullname).Text =
						profile.ProfileFirstName + " " + profile.ProfileLastName;
					authHeader.FindViewById<TextView>(Resource.Id.drawerUser_Handle).Text = profile.ProfileHandle;

					//Setting up auth tab layout
					//MainTabLayout.Visibility = ViewStates.Visible;
					Fab.Visibility = ViewStates.Visible;
					//TODO: Change these to real fragments later
					if (Fragments.Count == 1)
					{
						Fragments.Add(NotificationFragment.NewInstance());
						Fragments.Add(NotificationFragment.NewInstance());
					}
					MainFragAdapter.NotifyDataSetChanged();
					SetTabIcons();
				}
				else
				{
					//set up icon and hide tab layout when user is not logged in
					LoggedIn = false;
					Fab.Visibility = ViewStates.Gone;
					NavView.FindViewById(Resource.Id.drawerLogout).Visibility = ViewStates.Gone;
					NavView.FindViewById(Resource.Id.drawerNonAuthenticatedHeader).Visibility = ViewStates.Visible;
					NavView.FindViewById(Resource.Id.drawerAuthenticatedHeader).Visibility = ViewStates.Gone;

					NavView.FindViewById<ImageView>(Resource.Id.drawerNonAuthenticatedHeader_Avatar)
						   .SetImageDrawable(
							   ViewUtil.GetSVGDrawable(this, "profile_empty", 200, Resource.Color.Upward_dark_grey));


					MainTabLayout.Visibility = ViewStates.Gone;
				}
				PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).Edit()
								 .PutBoolean(Constants.PREF_SIGNED_IN_KEY, LoggedIn).Commit();
			});
		}

		#region attributes

		public V4FragmentManager fm;
		private readonly ISettingsViewControllerService _settingsService;
		private readonly IViewTrackingService _trackingService;
		private readonly IProfileDrawerControllerService _profileDrawerControllerService;

		private List<V4Fragment> Fragments;
		private Snackbar connectivityMessage;
		private DrawerLayout DrawerLayout;

		public NavigationView NavView;

		//private FloatingActionButton Fab;
		private Android.Widget.RelativeLayout Fab;

		private V7Toolbar MainToolBar;
		private ViewPager MainViewPager;
		private TabLayout MainTabLayout;

		private MainAdapter MainFragAdapter;

		private const int INDEX_HOME_FEED_FRAGMENT = 0;
		private const int INDEX_TRENDING_FRAGMENT = 1;
		private const int INDEX_BOARDS_FRAGMENT = 2;
		private const int INDEX_NOTIFICATIONS_FRAGMENT = 3;

		//TODO: Manipulate this boolean once authentication is online
		public static bool LoggedIn;

		#endregion

		#region methods

		protected override void AttachBaseContext(Context @base)
		{
			ViewUtil.SetFont(true, @base.GetString(Resource.String.AppFont));
			base.AttachBaseContext(CalligraphyContextWrapper.Wrap(@base));
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			fm = SupportFragmentManager;

			Forms.Init(this, null);

			MobileCenter.Start("d19450d5-6882-4bea-9b6e-0b3f2e4afe02",
				   typeof(Analytics), typeof(Crashes));
			
			MessagingCenter.Subscribe<NativeNavigation, string>(this, "NativeNav", (sender, arg) =>
				{
					System.Console.WriteLine("Got the message from Xamarin Forms to load Global Search");
					NativeNavigationModel nnm = JsonConvert.DeserializeObject<NativeNavigationModel>(arg);
					NavigateByString nav = new NavigateByString(nnm);
					nav.NavigateXF();
				}
			);

			SetContentView(Resource.Layout.Main);

			MainToolBar = FindViewById<V7Toolbar>(Resource.Id.mainToolBar);

			//UpwardFontTextView upwardLogo = ViewUtil.GetUpwardFontTextView(this, "U", 60, Resource.Color.white);
			//MainToolBar.FindViewById<RelativeLayout>(Resource.Id.upwardLogo_container).AddView(upwardLogo);

			ViewUtil.AddUpwardLogoToView(this, MainToolBar.FindViewById<Android.Widget.RelativeLayout>(Resource.Id.upwardLogo_container),
										 Resources.GetInteger(Resource.Integer.upward_toolbar_logo_size));

			SetSupportActionBar(MainToolBar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeAsUpIndicator(new IconDrawable(this, "md-menu").SizeDp(28).Color(Android.Graphics.Color.White));

			MainTabLayout = FindViewById<TabLayout>(Resource.Id.mainTabLayout);
			MainViewPager = FindViewById<ViewPager>(Resource.Id.mainViewPager);

			//NOTE: The following group of lines restores the old tab layout. These were removed in favor of the singular home page fragment approach for Upward
			SetUpFragments();
			MainFragAdapter = new MainAdapter(this, fm, Fragments);
			MainViewPager.Adapter = MainFragAdapter;
			MainTabLayout.SetupWithViewPager(MainViewPager);
			MainViewPager.OffscreenPageLimit = Fragments.Count - 1;
			MainViewPager.AddOnPageChangeListener(this);

			//HomeFeedFragment fragment = new HomeFeedFragment();
			//FragmentUtil.LoadFragment(SupportFragmentManager, Resource.Id.staticFragmentContainer, fragment, Constants.VALUE_TYPE_HOME_FEED);

			NavView = FindViewById<NavigationView>(Resource.Id.nav_view);
			//NavView.NavigationItemSelected += DrawerItemSelected;
			DrawerLayout = FindViewById<FRDrawerLayout>(Resource.Id.drawer_layout);


			//Fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			////var icon = new IconDrawable(this, "md-create").Color(Android.Graphics.Color.White).SizeDp(20);
			//Fab.Click += (sender, e) =>
			//{
			//    IntentUtil.GoToCreatePost(this);
			//    //add layout and tint to layout

			//};
			//Fab.SetImageDrawable(icon);
			SetUpFab();

			SetTabIcons();
			SetConnectivityMessage();

			//The following 6 lines of code will set up the drawer for a non-logged in state to start, and change it later as required
			Fab.Visibility = ViewStates.Gone;
			NavView.FindViewById(Resource.Id.drawerLogout).Visibility = ViewStates.Gone;
			NavView.FindViewById(Resource.Id.drawerNonAuthenticatedHeader).Visibility = ViewStates.Visible;
			NavView.FindViewById(Resource.Id.drawerAuthenticatedHeader).Visibility = ViewStates.Gone;
			NavView.FindViewById<ImageView>(Resource.Id.drawerNonAuthenticatedHeader_Avatar)
				   .SetImageDrawable(ViewUtil.GetSVGDrawable(this, "profile_empty", 200, Resource.Color.Upward_dark_grey));
			MainTabLayout.Visibility = ViewStates.Gone;
			FindViewById(Resource.Id.flDrawerNonAuthenticatedHeader_AvatarContainer).SetOnClickListener(this);

			//Setup MainView
			var linear = new LinearLayout(this);
			linear.LayoutParameters = new Android.Views.ViewGroup.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.MatchParent);
			linear.Orientation = Orientation.Vertical;

			var frame = new FrameLayout(this);
			frame.LayoutParameters = linear.LayoutParameters = new Android.Views.ViewGroup.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.MatchParent);

			frame.Id = 100;

			//linear.AddView(frame);

			//SetContentView(linear);

			//Setup navigation
			if (!Forms.IsInitialized)
				Forms.Init(this, savedInstanceState);

			DependencyService.Register<IAppNavigation, AppNavigation>();

			//AppNavigation.Manager = FragmentManager;
			//AppNavigation.ResourceId = 100;

			var appNavigation = DependencyService.Get<IAppNavigation>();

			//appNavigation.Push(App.StartPage);

		}

		private void SetUpFab()
		{
			Fab = FindViewById<Android.Widget.RelativeLayout>(Resource.Id.fab);
			var cross = ViewUtil.GetUpwardFAB(this);

			//cross.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Android.Resource.Color.White)));

			Fab.AddView(cross);
			Fab.Click += (sender, e) =>
			{
				IntentUtil.GoToCreatePost(this);
				//add layout and tint to layout
			};
		}

		protected override void OnStart()
		{
			base.OnStart();

			//IntentUtil.GoToBoard(this, 1165);
			//IntentUtil.GoToAllPrograms(this, 1172);

			switch (MainViewPager.CurrentItem)
			{
				case 0:
					_trackingService.HomeFeedPage();
					break;
				case 1:
					_trackingService.TrendingPage();
					break;
				case 2:
					_trackingService.BoardsPage();
					break;
				case 3:
					_trackingService.NotificationsPage();
					break;
			}
			SetUpNavigationDrawer();
			//NavView.Menu.GetItem(MainViewPager.CurrentItem).SetChecked(true);
		}

		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			base.OnPostCreate(savedInstanceState);
		}

		protected override void OnResume()
		{
			base.OnResume();
			//_navControllerService.FillCacheOnLogin(true);
		}

		protected override void OnStop()
		{
			MessagingCenter.Unsubscribe<NativeNavigation, string>(this, "NativeNav");
			base.OnStop();
			FinishNavigationDrawer();
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					DrawerLayout.OpenDrawer(GravityCompat.Start);
					return true;
				case Resource.Id.searchMenuButton:
					IntentUtil.GoToSearch(this);
					return true;
			}
			return base.OnOptionsItemSelected(item);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.SearchMenu, menu);
			menu.GetItem(0).SetIcon(new IconDrawable(this, "md-search").Color(Android.Graphics.Color.White).SizeDp(24));
			return base.OnCreateOptionsMenu(menu);
		}

		#endregion
	}
}