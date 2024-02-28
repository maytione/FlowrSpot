

namespace FlowrSpot.Application.Flowers.Dtos
{
    public class FlowerDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageRef { get; set; }
    }
}
