using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using StandupEngine.Services;
using System.Text.Json;

namespace StandupEngine.Components
{
    public partial class ParticipantList : ComponentBase
    {
        [Inject]
        public IEngine Engine { get; set; }

        [Inject]
        public ISyncLocalStorageService LocalStorage { get; set; }  

        private string selectedReadyItem;
        private string? newTeamMember;

        private List<string> ParticipantsReady = new();
        private List<string> Participants = new();
               
        protected override void OnInitialized()
        {
            string storedParticipantsReady = LocalStorage.GetItem<string>("ParticipantsReady");

            if (!string.IsNullOrEmpty(storedParticipantsReady))
            {
                ParticipantsReady = JsonSerializer.Deserialize<List<string>>(storedParticipantsReady);
            }
            else
            {
                LocalStorage.SetItem("ParticipantsReady", "");
            }

            selectedReadyItem = ParticipantsReady.FirstOrDefault();
        }

        public void ClearReadyList()
        {
            ParticipantsReady.Clear();
            LocalStorage.RemoveItem("ParticipantsReady");
            StateHasChanged();
        }

        public void AddParticipantReady(string participant)
        {
            if (participant is null)
                return;

            ParticipantsReady.Add(participant);
            StateHasChanged();
        }

        public void RemoveParticipantReady(string participant)
        {
            ParticipantsReady.Remove(participant);            
            LocalStorage.SetItem("ParticipantsReady", JsonSerializer.Serialize(ParticipantsReady));
            StateHasChanged();
        }

        public void AddParticipant(string participant)
        {
            Participants.Add(participant);
            StateHasChanged();
        }
        public void RemoveParticipant(string participant)
        {
            Participants.Remove(participant);
            StateHasChanged();
        }
    }
}
