

using FlowrSpot.Domain.Auditables;

namespace FlowrSpot.Domain.Entities
{
    public class Flower: BaseAuditable
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageRef { get; set; }
        public required string UserId { get; set; }

        // Navigation property
        public ICollection<Sighting>? Sightings { get; set; }
    }
}
