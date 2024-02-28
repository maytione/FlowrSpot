
namespace FlowrSpot.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required int SightingId { get; set; }

        // Navigation properties
        public Sighting? Sighting { get; set; }
    }
}
