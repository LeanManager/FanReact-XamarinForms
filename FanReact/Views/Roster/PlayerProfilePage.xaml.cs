using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FanReact
{
	public partial class PlayerProfilePage : ContentPage
	{
		Player currentPlayer;

		public PlayerProfilePage(Player player)
		{
			InitializeComponent();

			currentPlayer = player;

			this.BindingContext = player;

			grayLabel.BackgroundColor = Color.FromRgb(236, 233, 233);

			exitButton.BackgroundColor = Color.BlueViolet;
		}

		async void OnToolbarEditClicked(object sender, System.EventArgs e)
		{
			// When do we need if (e != null) ?

			// await Navigation.PushModalAsync(new EditPlayerPage(currentPlayer));

			DependencyService.Get<IAppNavigation>().Push(new EditPlayerPage(currentPlayer));
		}

		async void OnExitClicked(object sender, System.EventArgs e)
		{
			if (await DisplayAlert("Exit", "Are you sure you want to exit Coach Tools?", "No", "Yes"))
			{
				// TODO: Navigate out
			}
		}
	}
}
