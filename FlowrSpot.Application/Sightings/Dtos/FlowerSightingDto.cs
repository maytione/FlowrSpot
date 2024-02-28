using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowrSpot.Application.Sightings.Dtos
{
    public class FlowerSightingDto
    {
        public int SightingId { get; set; }
        public int FlowerId { get; set; }
        public string? FlowerName { get; set; }
        public string? FlowerDescription { get; set; }
        public string? FlowerImage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int LikeCount { get; set; }
    }
}
