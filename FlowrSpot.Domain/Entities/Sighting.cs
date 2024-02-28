using FlowrSpot.Domain.Auditables;

namespace FlowrSpot.Domain.Entities
{
    public class Sighting: BaseAuditable
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int FlowerId { get; set; }
        public string? QuoteOfTheDay { get; set; }

        // Navigation properties
        public Flower Flower { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
