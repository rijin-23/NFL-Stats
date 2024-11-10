namespace Project.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserComment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}