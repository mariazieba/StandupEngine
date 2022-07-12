using StandupEngine.Shared;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace StandupEngine.Services
{
    public class Engine : IEngine
    {
        private PromptObject _allPrompts;
        private Random _random;

        public Engine()
        {
            _random = new Random();
        }

        public string GetGreeting()
        {
            var index = _random.Next(_allPrompts.intro.Length);
            return _allPrompts.intro[index];
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
            return _allPrompts.modifiers[index].text;
        }

        public string GetWaitingText()
        {
            var index = _random.Next(_allPrompts.loading.Length);
            return _allPrompts.loading[index];
        }

        private void LoadAllPrompts()
        {
            using HttpClient httpClient = new();
            string jsonPrompts = httpClient.GetStringAsync(@"assets\sentences.json").Result;

            if (string.IsNullOrEmpty(jsonPrompts))
            {
                throw new ApplicationException("Oh no! There is no funny things to be said!");
            }            
        }

        public void LoadPrompts(string jsonPrompts)
        {
            _allPrompts = JsonSerializer.Deserialize<PromptObject>(jsonPrompts);
        }
    }
}
