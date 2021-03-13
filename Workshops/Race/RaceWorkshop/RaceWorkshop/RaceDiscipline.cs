using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop
{
    class RaceDiscipline
    {
        List<Season> seasons = new List<Season>();

        private string name;
        private int since;
        private bool active;

        public void AddSeason(Season season)
        {
            seasons.Add(season);
        }

        public void RemoveSeason(Season season)
        {
            seasons.Remove(season);
        }

        public void UpdateRaceDiscipline(string Name, int Since, bool Active)
        {
            name = Name;
            since = Since;
            active = Active;
        }
    }
}
