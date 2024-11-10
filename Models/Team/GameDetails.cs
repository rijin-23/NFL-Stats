namespace Project.Models{
    public class GameDetails{
        public string? away {get;set;}
        public string? home {get;set;}
        public string? teamIDAway {get;set;}
        public string? teamIDHome {get;set;}
        public string? gameID {get;set;}
        public string? awayPts {get;set;}
        public string? homePts {get;set;}
        public string? gameStatus {get;set;}
    }

    public class GameDetailsList{
        public Dictionary<string, GameDetails>? body { get; set; }
    }
}