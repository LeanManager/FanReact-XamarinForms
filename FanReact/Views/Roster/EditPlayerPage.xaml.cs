using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FanReact
{
	public partial class EditPlayerPage : ContentPage
	{
		public EditPlayerPage(Player player)
		{
			InitializeComponent();

			this.BindingContext = player;

			this.BackgroundColor = Color.FromRgb(236, 233, 233);
		}


		async void OnDismissClicked(object sender, System.EventArgs e)
		{
			await Navigation.PopModalAsync();
		}


		async void OnSavePlayerClicked(object sender, System.EventArgs e)
		{
			// TODO: Update ViewModel, which updates the Model

			await Navigation.PopModalAsync();
		}


		void OnNotesChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			// This performs the action of the Save Player button

			string newNotes = e.NewTextValue;

			editableNotes.Text = newNotes;
		}


		void OnNotesCompleted(object sender, System.EventArgs e)
		{
			// TODO: Update ViewModel --> Model
		}
	}
}
