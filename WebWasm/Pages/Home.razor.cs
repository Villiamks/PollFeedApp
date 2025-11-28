using WebWasm.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebWasm.Services;

namespace WebWasm.Pages;

public partial class Home
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

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
        var sessionToken = await JSRuntime.InvokeAsync<string?>("localStorage.getItem", new object[] { "sessionToken" });

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
