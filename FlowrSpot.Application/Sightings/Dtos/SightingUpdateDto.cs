

namespace FlowrSpot.Application.Sightings.Dtos
{
    public class SightingUpdateDto
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int FlowerId { get; set; }
        public string? QuoteOfTheDay { get; set; }
    }
}
