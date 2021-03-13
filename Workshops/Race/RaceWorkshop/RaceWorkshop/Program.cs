using System;

namespace RaceWorkshop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RaceDiscipline Formula1 = new RaceDiscipline();
                Formula1.UpdateRaceDiscipline("Formula1", 1950, true);
                Season season2018 = new Season();

            Team Redbull = new Team();
            Team Ferrari = new Team();
            Team Haas = new Team();
            Team Mercedes = new Team();

            Formula1.AddSeason(season2018 = new Season());
            season2018.AddTeam(Redbull);
            season2018.AddTeam(Ferrari);
            season2018.AddTeam(Haas);
            season2018.AddTeam(Mercedes);
        }
    }
}
