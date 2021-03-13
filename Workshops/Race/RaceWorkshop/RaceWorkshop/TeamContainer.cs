using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop
{
    class TeamContainer
    {
        private List<Team> teams = new List<Team>();
        public void AddTeam(Team team)
        {
            teams.Add(team);
        }

        public void RemoveTeam(Team team)
        {
            teams.Remove(team);
        }

        public Array GetAllTeams()
        {
            return teams.ToArray();
        }
    }
}
