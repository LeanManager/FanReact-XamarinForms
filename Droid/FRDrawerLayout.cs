#region

using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;

#endregion

namespace FanReact.Droid {
	[Register("fanreact.droid.FRDrawerLayout")]
	public class FRDrawerLayout : DrawerLayout {
		public FRDrawerLayout(Context context) : base(context) { }

		public FRDrawerLayout(Context context, IAttributeSet attrs) : base(context, attrs) { }


		public override bool OnInterceptTouchEvent(MotionEvent ev) {
			try {
				return base.OnInterceptTouchEvent(ev);
			}
			catch (Exception e) {
				return false;
			}
		}
	}
}