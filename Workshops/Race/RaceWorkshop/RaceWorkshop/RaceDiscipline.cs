using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop
{
    class RaceDiscipline
    {
        List<Season> seasons;

        public string name { get; set; }
        public int since { get; set; }
        public bool isActive { get; set; }

        public RaceDiscipline()
        {
            seasons = new List<Season>();
        }

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
