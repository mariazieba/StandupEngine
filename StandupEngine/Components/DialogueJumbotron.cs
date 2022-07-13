using Microsoft.AspNetCore.Components;
using StandupEngine.Services;

namespace StandupEngine.Components
{
    public partial class DialogueJumbotron : ComponentBase
    {
        [Inject]
        public IEngine Engine { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var greeting = await Engine.GetGreetingAsync();
        }
    }
}
