using ClassLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WebFront.Services;

namespace WebFront.Components.Pages;

public partial class Home
{
    [Inject] private ProtectedSessionStorage SessionStorage { get; set; } = default!;
    
    private List<Polls> PollList = [];

    private async Task PopulatePolls()
    {
        PollList.Clear();
        var tmp = await PollService.GetAllPolls(VoteOptionService);
        List<Polls> list = tmp.ToList();
        if (list != null)
        {
            PollList = list;
        }
        else
        {
            PollList = [];
        }
    }
    
    private async Task Vote(VoteOptions vo)
    {
        var sessionToken = SessionStorage.GetAsync<string>("sessionToken").ToString();
        
        Users? user = await LoginService.GetLoggedinnUser(sessionToken ?? "");
        Votes vote = new Votes()
        {
            UserId = user?.UserId ?? null,
            VoteOptionId = vo.VoteOptionId
        };
        vo.Votes?.Add(vote);
        await VoteService.CreateVote(vote);
    }

    protected override async Task OnInitializedAsync()
    {
        await PopulatePolls();
        await VoteService.GetVotes();
    }
}
