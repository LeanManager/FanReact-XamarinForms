#region

using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Android.App;
using Android.Content;
using Android.OS;
using FanReact;
using Akavache;
using XLabs.Ioc;
using Android.Content.PM;
using FanReact.WebService;
using FanReact.ViewService;
using FanReact.ViewService.Interfaces;
using FanReact.Droid.Utilities;
using Android.Widget;
using Android.Views;
using XamSvg;
using XamSvg.Internals;
using System.Threading;
using System.Reflection;
using Plugin.Iconize;
using Plugin.Iconize.Fonts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamSvg.Shared;
using Application = Xamarin.Forms.Application;

#endregion

namespace FanReact.Droid {
	[Activity(Theme = "@style/UpwardSports_Theme.Splash", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
		NoHistory = true, ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashActivity : FormsApplicationActivity {
		//public SplashActivity() : base()
		//{
		//	XamSvg.Setup.InitSvgLib();
		//}

		protected override void OnCreate(Bundle bundle) {
			Setup.InitSvgLib();
			Config.ResourceAssembly = typeof(App).GetTypeInfo().Assembly;
			base.OnCreate(bundle);

			Window.SetBackgroundDrawableResource(Resource.Drawable.UpwardLoginBackgroundImage);
		}

		protected override void OnStart() {
			base.OnStart();
			SetContentView(Resource.Layout.SplashActivity);

			//ImageView ut = new ImageView(this);
			//var dr = SvgFactory.GetDrawable(this, "upward_star_white", CancellationToken.None);
			//ut.SetImageDrawable(dr);
			var ut = ViewUtil.GetSVGImageView(this, "upward_star_white", 100, Resource.Color.white);
			//ut.Gravity = GravityFlags.Center;
			FindViewById<FrameLayout>(Resource.Id.splash).AddView(ut);
			Iconize
				.With(new MaterialModule())
				.With(new FontAwesomeModule());
		}

		protected override void OnResume() {
			base.OnResume();
			BlobCache.ApplicationName = "FanReact";
			BlobCache.EnsureInitialized();

			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {
				                                                          var isOk = true;
				                                                          // If there are errors in the certificate chain, look at each error to determine the cause.
				                                                          if (sslPolicyErrors != SslPolicyErrors.None)
					                                                          for (var i = 0; i < chain.ChainStatus.Length; i++)
						                                                          if (chain.ChainStatus[i].Status !=
						                                                              X509ChainStatusFlags.RevocationStatusUnknown) {
							                                                          chain.ChainPolicy.RevocationFlag =
								                                                          X509RevocationFlag.EntireChain;
							                                                          chain.ChainPolicy.RevocationMode =
								                                                          X509RevocationMode.Online;
							                                                          chain.ChainPolicy.UrlRetrievalTimeout =
								                                                          new TimeSpan(0, 1, 0);
							                                                          chain.ChainPolicy.VerificationFlags =
								                                                          X509VerificationFlags.AllFlags;
							                                                          var chainIsValid =
								                                                          chain.Build((X509Certificate2) certificate);
							                                                          if (!chainIsValid)
								                                                          isOk = false;
						                                                          }
				                                                          return isOk;
			                                                          };

			Forms.Init(this, new Bundle());
			Application app = new App(new DroidDependencySetup());
			LoadApplication(app);
			CheckUserAuth();
			var pInfo = PackageManager.GetPackageInfo(PackageName, 0);
			var trackingService = Resolver.Resolve<IViewTrackingService>();
			trackingService.InitializeTracking(pInfo.VersionName);
		}

		private async void CheckUserAuth() {
			var userService = Resolver.Resolve<IUserService>();
			var profileId = await userService.GetUserToken();
			if (profileId == null)
				IntentUtil.GoToMain(this);
			else
				IntentUtil.GoToMain(this);
		}
	}
}