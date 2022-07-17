using StandupEngine.Shared;
using System.Text.Json;

namespace StandupEngine.Services
{
    public class Engine : IEngine
    {
        public List<string> ParticipantsReady = new();
        public List<string> Participants = new();

        private PromptObject? _allPrompts;
        private Random _random;

        public Engine()
        {
            _random = new Random();
        }

        public async Task<string> GetJokeAsync(bool firstTimeRender)
        {
            return await FetchTheJoke(firstTimeRender);
        }

        public string GetMeetingOrder()
        {
            if (!Participants.Any())
            {
                return "No participants? Cool, let's start the weekend!";
            }

            var shuffledList = Participants.OrderBy(x => _random.Next()).ToList();

            string[] orders = { $"Let's try this: { string.Join(", ", shuffledList) }." ,
                                $"You can't tell, but this is backwards: { string.Join(", ", shuffledList) }.",
                                $"Wooohoooo! Let's make it clockwise! {shuffledList[0]} starts talking and then chooses who's next every time?",
                                $"Let's go counterclockwise, and {shuffledList[0]} goes first. Everyone chooses the next speaker."
            };

            return $"{orders[_random.Next(orders.Length)]} \n{GetTwist()}";
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

        public void LoadPrompts(string jsonPrompts)
        {
            _allPrompts = JsonSerializer.Deserialize<PromptObject>(jsonPrompts);
        }

        private async Task<string> FetchTheJoke(bool firstTimeRender)
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
                    return firstTimeRender ? "Hi people and welcome to your daily stand-up! No joke this time." : "Still no jokes for you!";
                }
            }
        }
    }
}
