using Blazored.LocalStorage;
using Blazorise;
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

        private const string ParticipantsReadyKey = "ParticipantsReady";
               
        protected override void OnInitialized()
        {
            string storedParticipantsReady = LocalStorage.GetItem<string>("ParticipantsReady");

            if (!string.IsNullOrEmpty(storedParticipantsReady))
            {
                ParticipantsReady = JsonSerializer.Deserialize<List<string>>(storedParticipantsReady);
            }
            else
            {
                LocalStorage.SetItem(ParticipantsReadyKey, "");
            }

            selectedReadyItem = ParticipantsReady.FirstOrDefault();
        }

        public void ClearReadyList()
        {
            ParticipantsReady.Clear();
            LocalStorage.RemoveItem(ParticipantsReadyKey);
            StateHasChanged();
        }

        public void AddParticipantReady(string participant)
        {
            if (participant is null || ParticipantsReady.Contains(participant))
            {
                newTeamMember = string.Empty;
                return;
            }               

            ParticipantsReady.Add(participant);
            var participantsReadyJson = JsonSerializer.Serialize(ParticipantsReady);
            LocalStorage.SetItem(ParticipantsReadyKey, participantsReadyJson);
            newTeamMember = string.Empty;
            StateHasChanged();
        }

        public void AddAllParticipants()
        {
            Participants.AddRange(ParticipantsReady);
            StateHasChanged();
        }

        public void RemoveParticipantReady(string participant, bool all = false)
        {            
            ParticipantsReady.Remove(participant);
            LocalStorage.SetItem(ParticipantsReadyKey, JsonSerializer.Serialize(ParticipantsReady));            
            StateHasChanged();
        }

        public void ClearParticipantsReady()
        {
            ParticipantsReady.Clear();
            LocalStorage.RemoveItem(ParticipantsReadyKey);
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

        public void RemoveAllParticipants()
        {
            Participants.Clear();
            StateHasChanged();
        }

        private ListGroupMode GetListMode()
        {
            if (string.IsNullOrEmpty(selectedReadyItem))
            {
                return ListGroupMode.Static;
            }
            else
            {
                return ListGroupMode.Selectable;
            }
        }
    }
}
