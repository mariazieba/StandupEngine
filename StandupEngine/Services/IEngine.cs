﻿using StandupEngine.Shared;

namespace StandupEngine.Services
{
    public interface IEngine
    {
        public Task<string> GetJokeAsync(bool firstTimeRender);
        public string GetSingleGreeting(string participant);
        public string GetMeetingOrder();
        public string GetTwist();
        public void LoadPrompts(string jsonPrompts);
    }
}
