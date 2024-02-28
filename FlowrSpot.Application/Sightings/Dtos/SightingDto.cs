
namespace FlowrSpot.Application.Sightings.Dtos
{
    public class SightingDto
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int FlowerId { get; set; }
        public string? UserId { get; set; }
        public string? FlowerName { get; set; }
        public string? FlowerImageRef{ get; set; }
        public string? QuoteOfTheDay { get;  set; }

        public int Likes { get; internal set; }


    }
}
