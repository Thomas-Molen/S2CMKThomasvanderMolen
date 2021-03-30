using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop
{
    class Season
    {
        private List<Team> teamsInSeason;

        public int numberOfRaces { get; private set; }
        public string champion { get; private set; }
        public int year { get; private set; }

        public Season(int NumberOfRaces = 0, string Champion = "", int Year = 0)
        {
            numberOfRaces = NumberOfRaces;
            champion = Champion;
            year = Year;
            teamsInSeason = new List<Team>();
        }

        public void AddTeam(Team team)
        {
            teamsInSeason.Add(team);
        }

        public void RemoveTeam(Team TeamToRemove)
        {
            if (!teamsInSeason.Contains(TeamToRemove))
            {
                throw new ArgumentOutOfRangeException("Team does not exist in current context: " + TeamToRemove.Name);
            }
            teamsInSeason.Remove(TeamToRemove);
        }

        public void AddChampion(string Champion)
        {
            champion = Champion;
        }
    }
}
