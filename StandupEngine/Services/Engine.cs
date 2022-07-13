using StandupEngine.Shared;
using System.Text.Json;

namespace StandupEngine.Services
{
    public class Engine : IEngine
    {
        private PromptObject? _allPrompts;
        private Random _random;

        public Engine()
        {
            _random = new Random();
        }

        public async Task<string> GetGreetingAsync()
        {
            return await FetchTheJoke(true);
        }

        public string GetMeetingOrder(List<string> registeredParticipants)
        {
            var shuffledList = registeredParticipants.OrderBy(x => _random.Next()).ToList();
            return $"Allright! I dag bestemmer jeg rekkefølgen! Hva med ${ string.Join(" ,så ", shuffledList)}?";
        }

        public string GetSingleGreeting(string participant)
        {
            var index = _random.Next(_allPrompts.added.Length);
            return _allPrompts.added[index].Replace("{name}", participant);
        }

        public string GetTwist()
        {
            var index = _random.Next(_allPrompts.modifiers.Length);
            return _allPrompts.modifiers[index];
        }

        public async Task<string> GetWaitingTextAsync()
        {
            return await FetchTheJoke(false);
        }

        public void LoadPrompts(string jsonPrompts)
        {
            _allPrompts = JsonSerializer.Deserialize<PromptObject>(jsonPrompts);
        }

        private async Task<string> FetchTheJoke(bool firstGreeting)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "text/plain");
                var newJoke = await client.GetAsync("https://icanhazdadjoke.com/");

                if (newJoke.IsSuccessStatusCode && newJoke is not null)
                {
                    return await newJoke.Content.ReadAsStringAsync();
                }
                else
                {
                    return firstGreeting ? "Hi people and welcome to your daily stand-up! No joke this time." : "Still no jokes for you!";
                }
            }
        }
    }
}
