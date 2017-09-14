#region

using System;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Xamarin.Forms;
using FanReact.Droid.Activities;
using Android.Support.V4.App;
using FanReact.Droid.Resources.layout;
using FanReact.ViewService;
using Newtonsoft.Json;
using Android.Net;
using FanReact.WebService;
using Xamarin.Forms.Platform.Android;
using Uri = Android.Net.Uri;
using FanReact.UWService;

#endregion

namespace FanReact.Droid {
	public static class IntentUtil {
		public static void GoToCreatePost(Context context, long boardId = -1) {
			var reactionIntent = new Intent(context, typeof(CreatePostActivity));
			reactionIntent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_CREATE_POST);
			if (boardId > -1)
				reactionIntent.PutExtra(CreatePostActivity.KEY_BOARD_ID, boardId);
			context.StartActivity(reactionIntent);
		}

		public static void GoToAddComment(Context context, int profId) {
			var intent = new Intent(context, typeof(CreatePostActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_CREATE_COMMENT);
			intent.PutExtra(Constants.KEY_PROFILE_ID, profId);
			context.StartActivity(intent);
		}

		public static void GoToBoard(Context context, long boardId) {
			var boardIntent = new Intent(context, typeof(BoardActivity));
			boardIntent.PutExtra(Constants.KEY_BOARD_ID, boardId);
			context.StartActivity(boardIntent);
		}

		public static void GoToProfile(Context context, long? profileId = null) {
			var intent = new Intent(context, typeof(ProfileActivity));
			intent.PutExtra(Constants.KEY_PROFILE_ID, profileId ?? -1);
			context.StartActivity(intent);
		}

		public static void GotToProfileTeams(Context context, int? profileId = null) {
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_PROFILE_ID, profileId ?? -1);
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_PROFILE_TEAMS);
			context.StartActivity(intent);
		}

		public static void GotToProfileFans(Context context, int? profileId = null) {
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_PROFILE_ID, profileId ?? -1);
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_PROFILE_FANS);
			context.StartActivity(intent);
		}

		public static void GotToProfileFansOf(Context context, int? profileId = null) {
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_PROFILE_ID, profileId ?? -1);
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_PROFILE_FANS_OF);
			context.StartActivity(intent);
		}

		public static void GoToLogin(Context context, bool loggingOut = false) {
			//TODO: figure out if we need to pop activity or add activity
			var intent = new Intent(context, typeof(LoginActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_LOGIN);
			intent.PutExtra(LoginActivity.KEY_HIDE_APPBAR, true);
			context.StartActivity(intent);
			//Finish the calling activity (Main Activity in this case) to prevent the user from being able to navigate back to MainActivity via the back button
			if (loggingOut) ((MainActivity) context).Finish();
		}

		//Example Xamarin Forms Integration
		public static void GoToLoginPage(Context context)
		{
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_LOGINPAGE_FRAGMENT);
			context.StartActivity(intent);
		}

		public static void GoToSignup(Context context) {
			var intent = new Intent(context, typeof(LoginActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_SIGNUP);
			intent.PutExtra(LoginActivity.KEY_HIDE_APPBAR, true);
			context.StartActivity(intent);
		}

		public static void GoToLoginForResult(Context context) {
			var intent = new Intent(context, typeof(LoginActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_LOGIN);
			((FormsAppCompatActivity) context).StartActivityForResult(intent, Constants.LOGIN_REQUEST_CODE);
		}

		public static void GoToMain(Context context) {
			//TODO: figure out if we need to pop activity or add activity
			context.StartActivity(new Intent(context, typeof(MainActivity)));
		}

		public static void GoToPost(Context context, PostView post) {
			var intent = new Intent(context, typeof(PostActivity));
			intent.PutExtra(Constants.KEY_POST_OBJECT, JsonConvert.SerializeObject(post));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_POST_FRAGMENT);
			context.StartActivity(intent);
		}

		public static void GotToAppInfo(Context context, int? profileId = null) {
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_APP_INFO_FRAGMENT);
			context.StartActivity(intent);
		}

		public static void GoToMllPlayStore(Context context) {
			var intent = new Intent(Intent.ActionView);
			intent.SetData(Uri.Parse("market://details?id=com.fanreact.mll"));
			context.StartActivity(intent);
		}

		public static void GoToFrPlayStore(Context context) {
			var intent = new Intent(Intent.ActionView);
			intent.SetData(Uri.Parse("market://details?id=com.fanreact.app"));
			context.StartActivity(intent);
		}

		public static void GoToAutoFollow(Context context) {
			var intent = new Intent(context, typeof(AutoFolowActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_AUTO_FOLLOW_FRAGMENT);
			context.StartActivity(intent);
		}

		public static void GoToFullScreenWebview(Context context, string url, bool isMedia = true, bool isImage = true) {
			var intent = new Intent(context, typeof(FullScreenWebviewActivity));
			intent.PutExtra(FullScreenWebviewActivity.KEY_URL, url);
			intent.PutExtra(FullScreenWebviewActivity.KEY_IS_MEDIA, isMedia);
			intent.PutExtra(FullScreenWebviewActivity.KEY_IS_IMAGE, isImage);
			context.StartActivity(intent);
		}

		public static void GoToSearch(Context context) {
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_SEARCH_FRAGMENT);
			context.StartActivity(intent);
		}

		public static void GoToEditProfile(Context context, bool isUpwardApp = false, UWProfileType uwProfileType = UWProfileType.Normal, long forcedProfileId = -1) {
			if (isUpwardApp) {
				var upIntent = new Intent(context, typeof(EditProfileActivity));
				upIntent.PutExtra(context.GetString(Resource.String.extra_uwProfileType), uwProfileType.ToString());
				upIntent.PutExtra(context.GetString(Resource.String.extra_uwProfileId), forcedProfileId);
				context.StartActivity(upIntent);
			}
			else {
				var intent = new Intent(context, typeof(SimpleActivity));
				intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_EDIT_PROFILE_FRAGMENT);
				context.StartActivity(intent);
			}
		}

		public static void GoToContactWizard(Context context) {
			var intent = new Intent(context, typeof(ContactWizardActivity));
			context.StartActivity(intent);
		}

		public static void GoToAboutProfile(Context context, long profileId, string type) {
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, type);
			intent.PutExtra(Constants.KEY_PROFILE_ID, profileId);
			context.StartActivity(intent);
		}

		public static void GoToAboutProfile(Context context, ProfileView profileView) {
			var intent = new Intent(context, typeof(SimpleActivity));
			intent.PutExtra(Constants.KEY_PROFILE_ID, (long)profileView.ProfileId);
			switch (UWEnumUtils.GetUWProfileType(profileView)) {
				case UWProfileType.Normal:
				case UWProfileType.Upward:
					intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_ABOUT_PROFILE_REGULAR);
					break;
				case UWProfileType.Partner:
					intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_ABOUT_PROFILE_PARTNER);
					break;
				case UWProfileType.Parent:
					intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_ABOUT_PROFILE_PARENT);
					break;
				default:
					intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.VALUE_TYPE_ABOUT_PROFILE_REGULAR);
					break;

			}
			context.StartActivity(intent);
		}

		public static void GoToAboutBoard(Context context, UWBoardView boardView) {
			var profileView = boardView.BoardProfileView;
			IntentUtil.GoToAboutProfile(context, profileView);
		}

		//XAMARIN FORMS
		public static void GoToAllPrograms(Context context, long boardId) {
			var intent = new Intent(context, typeof(FormsActivity));
			intent.PutExtra(Constants.KEY_FRAGMENT_TYPE, Constants.FRAG_TYPE_ALL_PROGRAMS);
			intent.PutExtra(Constants.EXTRA_LONG_ID, boardId);
			context.StartActivity(intent);
		}
	}
}