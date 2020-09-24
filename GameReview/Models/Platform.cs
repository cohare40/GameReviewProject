namespace GameReview.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageAddress { get; set; }
        public int ApiPlatformId { get; set; }

        public string PlatformDescription { get; set; }
    }
}