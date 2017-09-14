using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FanReact
{
	public partial class PracticesPage : ContentPage
	{
		public PracticesPage()
		{
			InitializeComponent();
		}

		async void OnExitClicked(object sender, System.EventArgs e)
		{
			if (await DisplayAlert("Exit", "Are you sure you want to exit Coach Tools?", "No", "Yes"))
			{
				// TODO: Navigate out
			}
		}

		async void OnMyLibraryClicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new MyLibraryPage());
		}

		async void OnPracticeNotesClicked(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new PracticeNotesPage());
		}

		void OnSearchClicked(object sender, System.EventArgs e)
		{
			
		}
	}
}
