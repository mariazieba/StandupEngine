namespace StandupEngine.Services
{
    public interface IEngine
    {
        public string GetGreeting();
        public string GetSingleGreeting(string participant);
        public string GetWaitingText();
        public string GetMeetingOrder();
        public string GetTwist();

    }
}
