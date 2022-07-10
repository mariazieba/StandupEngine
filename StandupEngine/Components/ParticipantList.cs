using Microsoft.AspNetCore.Components;
using StandupEngine.Services;

namespace StandupEngine.Components
{
    public partial class ParticipantList : ComponentBase
    {
        [Inject]
        public IEngine _engine { get; set; }

        private string peopleList = "";
        private string selectedPeople = "";

        private List<string> ParticipantsReady = new();
        private List<string> ParticipantsAdded = new();

        public void ClearTheList(List<string> listToClear)
        {
            listToClear.Clear();
            StateHasChanged();
        }

        protected  override void OnAfterRender(bool firstRender = true)
        {

        }
    }
}
