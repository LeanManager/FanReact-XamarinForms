using System;
using Xamarin.Forms;

namespace FanReact
{
	public class NativeNavigationModel
	{
		public string storyboard;       //iOS
		public string viewController;   //iOS
		public string fragmentType;     //Android
		public string fragmentValue;    //Android
		public Page page;				//Xamarin Forms
		public string payload;          //General information
	}
}
