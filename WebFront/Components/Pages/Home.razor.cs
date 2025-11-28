using ClassLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WebFront.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WebFront.Components.Pages;

public partial class Home
{
    [Inject] private ProtectedSessionStorage SessionStorage { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    
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
        // 1. Get the current user from the Auth State
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var userPrincipal = authState.User;

        int? userId = null;

        // 2. Extract the ID from the claims (saved in the JWT)
        if (userPrincipal.Identity is { IsAuthenticated: true })
        {
            // "nameid" is the standard claim for ID
            var idClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int parsedId))
            {
                userId = parsedId;
            }
        }

        // 3. Create the vote
        Votes vote = new Votes()
        {
            UserId = userId,
            VoteOptionId = vo.VoteOptionId
        };

        if (vo.Votes == null) vo.Votes = new List<Votes>();
        vo.Votes.Add(vote);

        await VoteService.CreateVote(vote);
    }

    protected override async Task OnInitializedAsync()
    {
        await PopulatePolls();
        await VoteService.GetVotes();
    }
}
