using Microsoft.AspNetCore.Components;
using StandupEngine.Services;

namespace StandupEngine.Components
{
    public partial class DialogueJumbotron : ComponentBase
    {
        [Inject]
        public Engine Engine { get; set; }
        [CascadingParameter]
        public string CurrentText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentText = await Engine.GetJokeAsync(true);
        }
    }
}
