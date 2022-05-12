using manager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#nullable disable

namespace manager.Pages.Board;

[Authorize]
public class Board : PageModel
{
    public Data.Board UserBoard;
    private UserManager<ApplicationUser> _userManager;

    public Board(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var result = await _userManager.GetUserAsync(User);

        // If the user has no boards yet, return
        if (result.Boards == null)
        {
            return RedirectToPage("/Error");
        }

        var tmp = result.Boards.SingleOrDefault(b => b.BoardId == id);


        if (tmp == null)
        {
            return RedirectToPage("/Error");
        }

        UserBoard = tmp;

        return Page();
    }
}