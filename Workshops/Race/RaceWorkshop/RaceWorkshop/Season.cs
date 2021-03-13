using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop
{
    class Season
    {
        TeamContainer teamContainer = new TeamContainer();

        private int numberOfRaces;
        private string champion;
        private int year;

        public void AddTeam(Team team)
        {
            teamContainer.AddTeam(team);
        }

        public void RemoveTeam(Team team)
        {
            teamContainer.RemoveTeam(team);
        }

        public void AddChampion(string Champion)
        {
            champion = Champion;
        }
    }
}
