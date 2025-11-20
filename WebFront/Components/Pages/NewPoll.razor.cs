using ClassLibrary;

namespace WebFront.Components.Pages;

public partial class NewPoll
{
    private Polls NPoll;

    private async Task AddOption()
    {
        VoteOptions vo = new VoteOptions()
        {
            Poll =  NPoll,
            Caption = ""
        };
        NPoll.Options?.Add(vo);
    }

    private async Task CreatePoll()
    {
        Polls nPoll = new Polls()
        {
            UserId = NPoll.UserId,
            Question =  NPoll.Question,
            Options = []
        };

        nPoll.Options = NPoll.Options?.Select(op => new VoteOptions()
        {
            Poll = nPoll,
            Caption = op.Caption,
        }).ToList();
        
        await PollService.CreatePoll(nPoll);
        nv.NavigateTo("");
    }

    protected override async Task OnInitializedAsync()
    {
        NPoll = new Polls()
        {
            Question = "",
            Options = []
        };
        await AddOption();
        
        Users? loggedIn = LoginService.GetLoggedinnUser();
        if (loggedIn != null)
        {
            NPoll.UserId = /*loggedIn.UserId*/ 0;
        }
    }
}