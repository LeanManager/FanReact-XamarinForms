#region

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FanReact;
using Akavache;
using System.Threading.Tasks;
using XLabs.Ioc;

using Google.Analytics;
using System;
using Facebook.CoreKit;

using System.Reflection;
using Plugin.Iconize;
using Plugin.Iconize.Fonts;
using XamSvg;
using XamSvg.Shared;
using Newtonsoft.Json;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using WindowsAzure.Messaging;
using System.Collections.Generic;
using System.Globalization;
using FanReact.iOS;

#endregion

[assembly: ExportRenderer(typeof(MovableViewCell), typeof(MovableViewCellRenderer))]
namespace FanReact.iOS {
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate {
		public override UIWindow Window { get; set; }
		public SBNotificationHub Hub { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			#if ENABLE_TEST_CLOUD
				Xamarin.Calabash.Start();
			#endif

			Forms.Init();

			SQLitePCL.Batteries.Init();

			//Dependency enject
			LoadApplication(new App());
			DependencyService.Register<IAppNavigation, AppNavigation>();


			//start mobile center
			//MobileCenter.LogLevel = Microsoft.Azure.Mobile.LogLevel.Verbose;
			MobileCenter.Start("a699460a-0d1a-4bf3-ad97-814103e5a097",
				   typeof(Analytics), typeof(Crashes));

			//Set up Notifications
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
					   UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					   new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications();
			}
			else
			{
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			}


			Setup.InitSvgLib();
			Config.ResourceAssembly = typeof(App).GetTypeInfo().Assembly;





			//Load Fonts  - todo pull out
			Iconize.With(new FontAwesomeModule())
			       .With(new MaterialModule());

			//General apperance

			UINavigationBar.Appearance.TintColor = Color.White.ToUIColor();
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes {TextColor = UIColor.White});
			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);



			//Set cache size
			var cache = new NSUrlCache(6 * 1024 * 1024, 200 * 1024 * 1024, "iosCache"); // Size in bytes
			NSUrlCache.SharedCache = cache;
					
			ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
			return true;
		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation) {
			// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
			return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		}

		public override void OnResignActivation(UIApplication application) {
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application) {
			//invalidate my notification timer
			NSNotificationCenter.DefaultCenter.PostNotificationName("AppDidEnterBackground", this);

			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application) {
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application) {
			//restart Notification Listener Timer
			NSNotificationCenter.DefaultCenter.PostNotificationName("AppOnActivated", this);


			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application) {
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
			BlobCache.Shutdown().Wait();
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo,
		                                                  Action<UIBackgroundFetchResult> completionHandler) {
			Console.WriteLine("NOTIFICATION");
			ProcessNotification(userInfo, false);
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			if (Hub == null) {
				InitHub();
			}

			NSUserDefaults.StandardUserDefaults["deviceToken"] = deviceToken;
		}

		void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
		{
			// Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
			if (null != options && options.ContainsKey(new NSString("aps")))
			{
				//Get the aps dictionary
				NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

				string alert = string.Empty;

				//Extract the alert text
				// NOTE: If you're using the simple alert by just specifying
				// "  aps:{alert:"alert msg here"}  ", this will work fine.
				// But if you're using a complex alert with Localization keys, etc.,
				// your "alert" object from the aps dictionary will be another NSDictionary.
				// Basically the JSON gets dumped right into a NSDictionary,
				// so keep that in mind.
				if (aps.ContainsKey(new NSString("alert")))
					alert = (aps[new NSString("alert")] as NSString).ToString();

				//If this came from the ReceivedRemoteNotification while the app was running,
				// we of course need to manually process things like the sound, badge, and alert.
				if (!fromFinishedLaunching)
				{
					//Manually show an alert
					if (!string.IsNullOrEmpty(alert))
					{
						UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
						avAlert.Show();
					}
				}
			}
		}

		public void SetNotificationHubId(string profileId)
		{
			if (Hub == null) {
				InitHub();
			}

			var deviceToken = NSUserDefaults.StandardUserDefaults["deviceToken"] as NSData;
			if (deviceToken == null)
			{
				return;
			}

			var tag = new NSSet(profileId);
			var alertTemplate = "{\"aps\":{\"alert\":\"$(message)\"}}";
			var expire = DateTime.Now.AddDays(90).ToString(CultureInfo.CreateSpecificCulture("en-US"));
			//Hub.RegisterTemplateAsync(deviceToken, "apple", alertTemplate, expire, tag, (errorCallback) => {
			//	if (errorCallback != null)
			//		Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
			//});
			Hub.RegisterNativeAsync(deviceToken, tag, (errorCallback) => {
				if (errorCallback != null)
					Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
			});
		}

		private void InitHub()
		{
			Hub = new SBNotificationHub("Endpoint=sb://devupwardnotificiationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=t71V2qnyhe3f32Z6E+s1CWOe0i05BeKyk2b2+40hDSE=", "DevUpwardNotificiationHub");
		}
	}
}