using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop
{
    class TeamContainer
    {
        private List<Team> teams;

        public TeamContainer()
        {
            teams = new List<Team>();
        }

        public void AddTeam(Team TeamToAdd)
        {
            teams.Add(TeamToAdd);
        }

        public void RemoveTeam(Team TeamToRemove)
        {
            if (!teams.Contains(TeamToRemove))
            {
                throw new ArgumentOutOfRangeException("Team does not exist in current context: " + TeamToRemove.Name);
            }
            teams.Remove(team);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return teams.AsReadOnly();
        }
    }
}
