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
        Polls nPoll = new Polls()
        {
            UserId = NPoll.UserId,
            Question =  NPoll.Question
        };
        foreach (VoteOptions opt in NPoll.Options ?? [])
        {
            nPoll.Options?.Add(opt);
            await VoteOptionService.CreateVoteOption(opt);
        }
        await PollService.CreatePoll(nPoll);
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