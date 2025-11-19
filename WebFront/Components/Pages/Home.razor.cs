using ClassLibrary;

namespace WebFront.Components.Pages;

public partial class Home
{
    private List<Polls> PollList = [];

    private async Task PopulatePolls()
    {
        PollList.Clear();
        var tmp = await PollService.GetAllPolls();
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
        Votes vote = new Votes()
        {
            UserId = null,
            VoteOptionId = vo.VoteOptionId
        };
        await VoteService.CreateVote(vote);
    }

    protected override async Task OnInitializedAsync()
    {
        await PopulatePolls();
    }
}
