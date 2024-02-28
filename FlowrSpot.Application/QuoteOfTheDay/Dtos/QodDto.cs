using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowrSpot.Application.QuoteOfTheDay.Dtos
{
    public class QodDto
    {
        public SuccessDto? Success { get; set; }
        public ContentsDto? Contents { get; set; }
        public CopyrightDto? Copyright { get; set; }
    }
}
