using System;
using System.Collections.Generic;
using FanReact.Models;
using Xamarin.Forms;

namespace FanReact
{
	/*
	   When the user clicks the REORDER button, the ListView items become draggable and can be rearranged.
	   Upon clicking Save, the new ListView order is saved, and the ObservableCollection<Player> is updated.
	   Upon clicking Cancel, the ListView returns to its original state.
	 */

    public partial class TeamRosterPage : ContentPage
    {
        public TeamRosterPage()
        {
            InitializeComponent();

			//this.BindingContext = team;

			//playerListView.Items = team.Players;

			//playerListView.ItemsSource = team.Players;

			NavigationPage.SetBackButtonTitle(this, "Back");

			exitButton2.BackgroundColor = Color.BlueViolet;

			//backButton.Text = "< My Teams";

		}

		void OnReorderClicked(object sender, System.EventArgs e)
		{
			// TODO: Reorder ListView elements logic - Modify ObservableCollection<T> and update by TwoWay binding

			// Logic to show or hide the Save button based on Reorder button clicks. This is all for native UI, done in the renderer class

			// Give UITableView the ability to drag and drop cells. Interface/abstraction to implement? Consider Android too.
		}

		async void OnExitCoachToolsClicked(object sender, System.EventArgs e)
		{
			if(await DisplayAlert("Exit", "Are you sure you want to exit Coach Tools?", "No", "Yes"))
			{
				// TODO: Navigate out
			}
		}

		async void OnPlayerItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			if (e != null)
			{
				//((ListView)sender).SelectedItem = null; // de-select the row

				var player = e.Item as Player;

				//await Navigation.PushAsync(new PlayerProfilePage(player));

				DependencyService.Get<IAppNavigation>().Push(new PlayerProfilePage(player));
			}
		}

		void OnPlayerItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e != null)
			{
				((ListView)sender).SelectedItem = null; // de-select the row
			}
		}

		void OnCoachItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e != null) ((ListView)sender).SelectedItem = null; // de-select the row
		}

		void OnAddAssistantCoachClicked(object sender, System.EventArgs e)
		{
			// TODO: Add a Coach object to the ObservableCollection<Coach> - TwoWay binding
			// Create a new viewmodel and initialize it with the passed in Team instance?
		}

		async void OnDeleteAssistantCoachClicked(object sender, System.EventArgs e)
		{
			if (await DisplayAlert("Delete Assistant Coach", "Are you sure you want to remove this assistant coach?", "No", "Yes"))
			{
				// TODO: Navigate out
			}
		}

		void SaveAction()
		{
			// TODO: Save changes - update list? Ask Stagg
		}

		//async void OnBackClicked(object sender, System.EventArgs e)
		//{
		//	Navigation.InsertPageBefore(new MyTeamsPage(), this);

		//	await Navigation.PopAsync();
		//}
	}
}
