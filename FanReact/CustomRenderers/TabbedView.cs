using System;
using Xamarin.Forms;

namespace FanReact
{
    public class TabbedView : View
    {
		// Create a BindableProperty named CurrentTabProperty. Set the propertyName as "CurrentTab", 
		// the return type as Int32 and the declaring type as the new element - TabbedView.
		public static readonly BindableProperty CurrentTabProperty = BindableProperty.Create("CurrentTab", typeof(Int32), typeof(TabbedView), 0);

		// Create an Int32 property named CurrentTab for the bindable property. 
		// Call the GetValue and SetValue methods, passing in CurrentTabProperty, in the getter/setter.
		public Int32 CurrentTab
		{
			get
			{
				return (Int32)GetValue(CurrentTabProperty);
			}
			set
			{
				SetValue(CurrentTabProperty, value);
			}
		}

		// Method to create a Xamarin.Forms TableView / ListView

        // We want to create a TableView/ListView (and populate it with static data) when the user clicks a particular tab button. 
		// However, we don't want to hold a reference from the element to the renderer, so we'll send a notification using the built-in Messaging Center.
		public void CreateTableView()
		{
			//MessagingCenter.Send(this, "Clear");
		}

        public event EventHandler TabUpdated;

       
    }
}
