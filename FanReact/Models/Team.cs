using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FanReact.Models
{
    public class Team : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        // Name
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        // Years
        private string years;
        public string Years
        {
            get { return years; }
            set { SetProperty(ref years, value); }
        }

		// Division
		private string division;
		public string Division
		{
			get { return division; }
			set { SetProperty(ref division, value); }
		}

		// Program
		private string program;
		public string Program
		{
			get { return program; }
			set { SetProperty(ref program, value); }
		}

		// Church
		private string church;
		public string Church
		{
			get { return church; }
			set { SetProperty(ref church, value); }
		}

		// Players
		private ObservableCollectionEx<Player> players;
		public ObservableCollectionEx<Player> Players
		{
			get { return players; }
			set { SetProperty(ref players, value); }
		}

		// Current Week
		private string currentWeek;
		public string CurrentWeek
		{
			get { return currentWeek; }
			set { SetProperty(ref currentWeek, value); }
		}

		// Coaches
		private ObservableCollection<Coach> coaches;
		public ObservableCollection<Coach> Coaches
		{
			get { return coaches; }
			set { SetProperty(ref coaches, value); }
		}

        public string Icon { get; set; }

        public bool IsVisible { get; set; }

        public bool DetailsLabelVisibility { get; set; }

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

		public override string ToString()
		{
			return this.Name + "" + this.Years;
		}
    }
}
