using StandupEngine.Shared;

namespace StandupEngine.Services
{
    public interface IEngine
    {
        public string GetGreeting();
        public string GetSingleGreeting(string participant);
        public string GetWaitingText();
        public string GetMeetingOrder(List<string> registeredParticipants);
        public string GetTwist();
        public void LoadPrompts(string jsonPrompts);
    }
}
