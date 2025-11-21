using ClassLibrary;
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WebFront.Components.Pages;

public partial class NewPoll
{
    [Inject] private ProtectedSessionStorage SessionStorage { get; set; } = default!;
    
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

        var sessionToken = SessionStorage.GetAsync<string>("sessionToken").ToString();
        
        Users? loggedIn = await LoginService.GetLoggedinnUser(sessionToken ?? "");
        if (loggedIn != null)
        {
            NPoll.UserId = loggedIn.UserId;
        }
    }
}