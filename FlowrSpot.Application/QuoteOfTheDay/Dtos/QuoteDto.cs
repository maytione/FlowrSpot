using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowrSpot.Application.QuoteOfTheDay.Dtos
{
    public class QuoteDto
    {
        public string? Id { get; set; }
        public string? Quote { get; set; }
        public int Length { get; set; }
        public string? Author { get; set; }
        public string? Language { get; set; }
        public List<string>? Tags { get; set; }
        public string? Sfw { get; set; }
        public string? Permalink { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Background { get; set; }
        public DateTime Date { get; set; }
    }
}
