using StandupEngine.Shared;

namespace StandupEngine.Services
{
    public interface IEngine
    {
        public Task<string> GetGreetingAsync();
        public string GetSingleGreeting(string participant);
        public Task<string> GetWaitingTextAsync();
        public string GetMeetingOrder(List<string> registeredParticipants);
        public string GetTwist();
        public void LoadPrompts(string jsonPrompts);
    }
}
