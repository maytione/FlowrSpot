using FlowrSpot.Application.QuoteOfTheDay.Dtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;


namespace FlowrSpot.Application.QuoteOfTheDay.Service
{
    public class QuoteOfTheDayService(HttpClient client, ILogger<QuoteOfTheDayService> logger)
    {
        private readonly HttpClient _client = client;
        private readonly ILogger<QuoteOfTheDayService> _logger = logger;

        private readonly string[] hikingQuotes = [
            "I go hiking because I like to practice random acts of disappearing.",
            "Hiking is the answer. Who cares what the question is?",
            "The best view comes after the hardest climb.",
            "Hiking is just walking where it’s okay to pee.",
            "Life is better when you’re hiking.",
            "There are no shortcuts to any place worth going.",
            "Hiking is the only way I can clear my head.",
            "The mountains are calling, and I must go.",
            "Hiking: because therapy is expensive.",
            "I'm on a seafood diet. I see food and I eat it, especially after a hike."
        ];

        public async Task<QodDto?> GetQuoteOfTheDay()
        {
            try
            {
                return await _client.GetFromJsonAsync<QodDto>("/qod?language=en'");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            Random random = new();
            int index = random.Next(hikingQuotes.Length);
            string randomQuote = hikingQuotes[index];

            return new QodDto()
            {
                Success = new SuccessDto()
                {
                    Total = 1
                },
                Contents = new ContentsDto()
                {
                    Quotes = [ new()
                    {
                        Quote = randomQuote,
                        Author = "Unknown author"
                    }]
                }
            };
            

        }
    }
}
