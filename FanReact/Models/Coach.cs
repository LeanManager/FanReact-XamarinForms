using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FanReact
{
	public class Coach : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		// Coach name
		private string coachName;
		public string CoachName
		{
			get { return coachName; }
			set { SetProperty(ref coachName, value); }
		}

		private bool isAssistantCoach;
		public bool IsAssistantCoach
		{
			get { return isAssistantCoach; }
			set { SetProperty(ref isAssistantCoach, value); }
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
