namespace Project.Models
{
    public class Root
    {
        public int statusCode { get; set; }
        public Body body { get; set; }
    }

    public class Body
    {
        public string gameStatus { get; set; }
        public TeamStats teamStats { get; set; }
        public string gameDate { get; set; }
        public string teamIDHome { get; set; }
        public string homeResult { get; set; }
        public string away { get; set; }
        public LineScore lineScore { get; set; }
        public string home { get; set; }
        public string homePts { get; set; }
        public string awayResult { get; set; }
        public string teamIDAway { get; set; }
        public string awayPts { get; set; }
        public string gameID { get; set; }
    }

    public class TeamStats
    {
        public Away away { get; set; }
        public Home home { get; set; }
    }

    public class Away
    {
        public string passingYards {get;set;}
        public string totalYards { get; set; }
        public string rushingAttempts { get; set; }
        public string rushingYards { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string teamID { get; set; }
        public string currentlyInPossession { get; set; }
        public string totalPts { get; set; }
        public string teamAbv { get; set; }
    }

    public class Home
    {
        public string passingYards {get;set;}
        public string totalYards { get; set; }
        public string rushingAttempts { get; set; }
        public string rushingYards { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string teamID { get; set; }
        public string currentlyInPossession { get; set; }
        public string totalPts { get; set; }
        public string teamAbv { get; set; }
    }

    public class LineScore
    {
        public string period { get; set; }
        public string gameClock { get; set; }
        public Away away { get; set; }
        public Home home { get; set; }
    }

    public class NFLGameData
    {
        public string gameStatus { get; set; }
        public TeamStats teamStats { get; set; }
        public string gameDate { get; set; }
        public string teamIDHome { get; set; }
        public string homeResult { get; set; }
        public string away { get; set; }
        public LineScore lineScore { get; set; }
        public string home { get; set; }
        public string homePts { get; set; }
        public string awayResult { get; set; }
        public string teamIDAway { get; set; }
        public string awayPts { get; set; }
        public string gameID { get; set; }
    }
}
