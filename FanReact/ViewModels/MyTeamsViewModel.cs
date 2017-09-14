using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FanReact.Models;
using Xamarin.Forms;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FanReact.ViewModels;
using System.Collections.Generic;

namespace FanReact
{
    public class MyTeamsViewModel : ViewModelBase
    {
        private Team oldTeam;

		public ObservableCollection<Coach> Coaches { get; set; }

		public ObservableCollectionEx<Player> Players { get; set; }

        public ObservableCollection<Team> Teams { get; set; }

		//public void SaveTeams(IDictionary<string, Team> properties)
		//{
		//	// Store teams using team.Name as the dictionary key.
		//	foreach (Team team in Teams)
		//	{
		//		properties[team.Name] = team;
		//	}
		//}

        public MyTeamsViewModel() // constructor parameter => IDictionary<string, object> properties
		{
			Teams = new ObservableCollection<Team>
			{
				new Team
				{
					Name = "Bulls",
					Years = "2017-2018",
					Division = "2rd - 3th",
					Program = "Fun Football with Chess League",
					Church = "Pine Trails MVP",
					Icon = "football1.png",
					IsVisible = false,
					DetailsLabelVisibility = true,
					CurrentWeek = "Week 4",
					Coaches = new ObservableCollection<Coach>
					{
						new Coach
						{
							CoachName = "Charlie Frazier",
							IsAssistantCoach = false
						},
						new Coach
						{
							CoachName = "James Frazier",
							IsAssistantCoach = true	
						}
					},
					Players = new ObservableCollectionEx<Player>
					{
						new Player
						{
							PlayerName = "Fanne S.",
							Rank = "A",
							JerseyNumber = "#13",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
								    "Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Lizzie H.",
							Rank = "B",
							JerseyNumber = "#99",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Lily A.C.",
							Rank = "C",
							JerseyNumber = "#4",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Sarah H.",
							Rank = "D",
							JerseyNumber = "#6",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Hallie C.",
							Rank = "E",
							JerseyNumber = "#8",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Bessie C.",
							Rank = "F",
							JerseyNumber = "#00",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Lillian O.",
							Rank = "G",
							JerseyNumber = "#1",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Connie V.",
							Rank = "H",
							JerseyNumber = "#2",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Marylou C.",
							Rank = "I",
							JerseyNumber = "#19",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Raechel M.",
							Rank = "J",
							JerseyNumber = "#14",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Vanessa H.",
							Rank = "K",
							JerseyNumber = "#21",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						}
					}
                },

                new Team
                {
                    Name = "Aggies",
                    Years = "2017-2018",
                    Division = "3rd - 4th",
                    Program = "Flag Football with Cheerleading League",
                    Church = "Cypress Trails UMC",
                    Icon = "football1.png",
                    IsVisible = false,
                    DetailsLabelVisibility = true,
					CurrentWeek = "Week 4",
					Coaches = new ObservableCollection<Coach>
					{
						new Coach
						{
							CoachName = "Lionel Messi",
							IsAssistantCoach = false
						},
						new Coach
						{
							CoachName = "Neymar Jr.",
							IsAssistantCoach = true
						}
					},
					Players = new ObservableCollectionEx<Player>
					{
						new Player
						{
							PlayerName = "Amanda S.",
							Rank = "A",
							JerseyNumber = "#31",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Jennifer H.",
							Rank = "B",
							JerseyNumber = "#98",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Conie A.C.",
							Rank = "C",
							JerseyNumber = "#5",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Kimber H.",
							Rank = "D",
							JerseyNumber = "#7",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Kourtie C.",
							Rank = "E",
							JerseyNumber = "#9",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Emily An C.",
							Rank = "F",
							JerseyNumber = "#02",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Vivianna O.",
							Rank = "G",
							JerseyNumber = "#86",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Nicole H.V.",
							Rank = "H",
							JerseyNumber = "#2",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Andreas C.",
							Rank = "I",
							JerseyNumber = "#19",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Kaleysa M.",
							Rank = "J",
							JerseyNumber = "#14",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						},
						new Player
						{
							PlayerName = "Selenna H.",
							Rank = "K",
							JerseyNumber = "#21",
							Notes = "Specifically good at handling the ball but jumper needs work. " +
									"Best rated on the team because she is an incredible athlete and is super tall, even though she misses mid-range shots."
						}
					}
                }
            };
        }

        public void HideOrShowTeamDetails(Team team)
        {
			// same ListView cell
            if (oldTeam == team)
            {
				// clicking twice on the same item will hide it
				team.DetailsLabelVisibility = !team.DetailsLabelVisibility;
                team.IsVisible = !team.IsVisible;
                UpdateTeams(team);
            }
			// new or different ListView cell
            else
            {
                if (oldTeam != null)
                {
					// hide previous selected item
					oldTeam.DetailsLabelVisibility = true;
                    oldTeam.IsVisible = false;
                    UpdateTeams(oldTeam);
                }
				// show selected item
				team.DetailsLabelVisibility = false;
                team.IsVisible = true;
                UpdateTeams(team);
            }

            oldTeam = team;
        }

        private void UpdateTeams(Team team)
        {
            var index = Teams.IndexOf(team);
            Teams.Remove(team);
            Teams.Insert(index, team);
        }
    }
}
