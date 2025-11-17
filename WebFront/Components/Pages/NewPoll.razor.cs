using ClassLibrary;

namespace WebFront.Components.Pages;

public partial class NewPoll
{
    private Polls NPoll;

    private async Task AddOption()
    {
        VoteOptions vo = new VoteOptions()
        {
            Poll = NPoll,
            Caption = ""
        };
        NPoll.Options?.Add(vo);
    }

    private async Task CreatePoll()
    {
        //TODO
        nv.NavigateTo("");
    }

    protected override async Task OnInitializedAsync()
    {
        NPoll = new Polls()
        {
            Creator = null,
            Question = "",
            Options = []
        };
        await AddOption();
    }
}