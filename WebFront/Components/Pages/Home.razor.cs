using ClassLibrary;

namespace WebFront.Components.Pages;

public partial class Home
{
    private List<Polls> PollList = [];

    private async Task PopulatePolls()
    {
        PollList.Clear();
        var list = await PollService.GetAllPolls();
        if (list != null)
        {
            PollList = list.ToList();
        }
    }
    
    private async Task Vote(VoteOptions vo)
    {
        //TODO
    }

    protected override async Task OnInitializedAsync()
    {
        await PopulatePolls();
    }
}
