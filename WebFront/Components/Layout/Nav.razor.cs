using Microsoft.AspNetCore.Components;
using WebFront.Services;

namespace WebFront.Components.Layout;

public partial class Nav
{
    // These fields caused the "already contains a definition" error
    private bool showNav = true;
    private string PollsUrl = "polls";
    private string NewPollUrl = "newpoll";
    private string LoginUrl = "login";

    private void ToggleNav()
    {
        showNav = !showNav;
    }

    private void NavigateTo(string url)
    {
        // 'nv' is available here because it is @injected in the .razor file
        nv.NavigateTo(url);
    }

    private async Task Logout()
    {
        if (AuthProvider is CustomAuthProvider customProvider)
        {
            await customProvider.Logout();
            nv.NavigateTo("login", true);
        }
    }
}