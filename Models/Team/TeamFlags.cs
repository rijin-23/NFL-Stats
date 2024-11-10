namespace Project.Models{
    public class TeamFlags{
        public string? teamID {get;set;}
        public string? nflComLogo1 {get;set;}
    }

    public class FlagUrls{
        public List<TeamFlags>? body {get;set;}
    }
}