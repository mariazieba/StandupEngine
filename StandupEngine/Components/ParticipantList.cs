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
        public Engine Engine { get; set; }

        [Inject]
        public ISyncLocalStorageService LocalStorage { get; set; }  

        [Parameter]
        public EventCallback<string> OnGreetingSet { get; set; }

        private string selectedReadyItem;
        private string? newTeamMember;

        private const string ParticipantsReadyKey = "ParticipantsReady";
               
        protected override void OnInitialized()
        {
            string storedParticipantsReady = LocalStorage.GetItem<string>("ParticipantsReady");

            if (!string.IsNullOrEmpty(storedParticipantsReady))
            {
                Engine.ParticipantsReady = JsonSerializer.Deserialize<List<string>>(storedParticipantsReady);
            }
            else
            {
                LocalStorage.SetItem(ParticipantsReadyKey, "");
            }

            selectedReadyItem = Engine.ParticipantsReady.FirstOrDefault();
        }

        public void ClearReadyList()
        {
            Engine.ParticipantsReady.Clear();
            LocalStorage.RemoveItem(ParticipantsReadyKey);
            StateHasChanged();
        }

        public void AddParticipantReady(string participant)
        {
            if (participant is null || Engine.ParticipantsReady.Contains(participant))
            {
                newTeamMember = string.Empty;
                return;
            }

            Engine.ParticipantsReady.Add(participant);

            var participantsReadyJson = JsonSerializer.Serialize(Engine.ParticipantsReady);
            LocalStorage.SetItem(ParticipantsReadyKey, participantsReadyJson);

            newTeamMember = string.Empty;
            StateHasChanged();
        }

        public void AddAllParticipants()
        {
            Engine.Participants.AddRange(Engine.ParticipantsReady);
            Engine.Participants = Engine.Participants.Distinct().ToList();
            StateHasChanged();
        }

        public void RemoveParticipantReady(string participant, bool all = false)
        {
            Engine.ParticipantsReady.Remove(participant);
            LocalStorage.SetItem(ParticipantsReadyKey, JsonSerializer.Serialize(Engine.ParticipantsReady));            
            StateHasChanged();
        }

        public void ClearParticipantsReady()
        {
            Engine.ParticipantsReady.Clear();
            LocalStorage.RemoveItem(ParticipantsReadyKey);
            StateHasChanged();
        }

        public void AddParticipant(string participant)
        {
            if (!Engine.Participants.Contains(participant))
            {
                Engine.Participants.Add(participant);
                OnGreetingSet.InvokeAsync(participant);
                StateHasChanged();
            }            
        }
        public void RemoveParticipant(string participant)
        {
            Engine.Participants.Remove(participant);
            StateHasChanged();
        }

        public void RemoveAllParticipants()
        {
            Engine.Participants.Clear();
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
