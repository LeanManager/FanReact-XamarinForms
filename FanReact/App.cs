#region

using Splat;
using ModernHttpClient;
using System.Net.Http;
using Xamarin.Forms;
using System.Reflection;
using XamSvg.Shared;

#endregion

namespace FanReact 
{
	public class App : Application 
	{
		public App() 
		{
			Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));
			Config.ResourceAssembly = typeof(App).GetTypeInfo().Assembly;

			ViewModel = new MyTeamsViewModel(); // constructor parameter => Properties

			//this.Resources.Add("viewModel", ViewModel);
		}

		public MyTeamsViewModel ViewModel
		{
			private set;

			get;
		}

		protected override void OnStart() {
			// Handle when your app starts
		}

		protected override void OnSleep() {
			// Handle when your app sleeps
		}

		protected override void OnResume() {
			// Handle when your app resumes
		}
	}
}