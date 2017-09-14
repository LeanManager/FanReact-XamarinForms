using System;
using System.Collections.Generic;
using FanReact.Models;
using FanReact;
using Xamarin.Forms;

namespace FanReact
{
    public partial class MyTeamsPage : ContentPage
    {
        // create a reference to the instance of the page and make it a static class variable
        public static ContentPage myTeamsPage;

		public static MyTeamsViewModel viewModel;

        public MyTeamsPage()
        {
			NavigationPage.SetHasNavigationBar(this, true);

            InitializeComponent();

			// get data

			LoadTeamsData();

			this.BindingContext = viewModel;

            this.Padding = new Thickness(0, 20, 0, 0);
            this.BackgroundColor = Color.FromRgb(236, 233, 233);

            NavigationPage.SetBackButtonTitle(this, "Back");

            myTeamsPage = this.FindByName<ContentPage>("myTeamsPage");

			grayLabel.BackgroundColor = Color.FromRgb(236, 233, 233);

			exitButton1.BackgroundColor = Color.BlueViolet;

			// Create Effect(s) to change the rendering of the ListView expansion
		}

		private void LoadTeamsData()
		{
			// get ViewModel instance from App
			var vm = new MyTeamsViewModel();

			viewModel = vm;
		}

        void OnTeamItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var team = e.Item as Team;

            var vm = BindingContext as MyTeamsViewModel;

            vm?.HideOrShowTeamDetails(team);
        }

		public async void OnTeamItemButtonClicked(object sender, EventArgs e)
		{
			var item = (Xamarin.Forms.Button)sender;

			var team = (Team)item.CommandParameter;

			//if (team.DetailsLabelVisibility == true)
			//{
			//	team.DetailsLabelVisibility = !team.DetailsLabelVisibility;
			//}

			//await MyTeamsPage.myTeamsPage.Navigation.PushAsync(new TeamRosterPage(team));

			//await Navigation.PushAsync(new CoachToolsTabbedPage
			//{
			//	BindingContext = team
			//});

			DependencyService.Get<IAppNavigation>().Push(new CoachToolsTabbedPage
			{
				BindingContext = team
			});
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
