using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FanReact
{
	public class NativeNavigation
	{
		public NativeNavigationModel nnm { get; set; }
		public string navMsg { get; set; }

		public NativeNavigation(string nMsg, NativeNavigationModel NNM)
		{
			navMsg = nMsg;
			nnm = NNM;
		}

		public void Navigate()
		{
			string arg = JsonConvert.SerializeObject(nnm);
			MessagingCenter.Send<NativeNavigation, string>(this, navMsg, arg);
		}
	}
}
