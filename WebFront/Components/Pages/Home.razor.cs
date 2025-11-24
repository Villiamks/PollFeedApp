using ClassLibrary;
using WebFront.Services;

namespace WebFront.Components.Pages;

public partial class Home
{
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
        Users? user = LoginService.GetLoggedinnUser();
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
