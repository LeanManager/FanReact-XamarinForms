using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FanReact
{
	public class Player : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate {};

		// Player Name
		private string playerName;
		public string PlayerName
		{
			get { return playerName; }
			set { SetProperty(ref playerName, value); }
		}

		// Rank
		private string rank;
		public string Rank
		{
			get { return rank; }
			set { SetProperty(ref rank, value); }
		}

		// Jersey No.
		private string jerseyNumber;
		public string JerseyNumber
		{
			get { return jerseyNumber; }
			set { SetProperty(ref jerseyNumber, value); }
		}

		// Player Notes
		private string notes;
		public string Notes
		{
			get { return notes; }
			set { SetProperty(ref notes, value); }
		}

		// Stars
		private int stars;
		public int Stars
		{
			get { return stars; }
			set { SetProperty(ref stars, value); }
		}

		bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
		{
			if (!object.Equals(field, value))
			{
				field = value;
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				return true;
			}
			return false;
		}
	}
}
