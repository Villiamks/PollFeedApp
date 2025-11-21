using ClassLibrary;
using ClassLibrary.DTOs;

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
        PollDTO dtoPoll = new PollDTO()
        {
            UserId = NPoll.UserId,
            Question = NPoll.Question,
        };
        dtoPoll.Options = NPoll.Options?.Select(op => new VoteOptionDTO()
        {
            Caption = op.Caption
        }).ToList(); 
            
        await PollService.CreatePoll(dtoPoll);
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