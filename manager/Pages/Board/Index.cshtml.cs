using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace manager.Pages.Board;

[Authorize]
public class Index : PageModel
{
    public void OnGet()
    {
        
    }
}