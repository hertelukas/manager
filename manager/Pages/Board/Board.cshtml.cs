#nullable disable

using manager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace manager.Pages.Board;

[Authorize]
public class Board : PageModel
{
    public Data.Board UserBoard;

    private ILogger<Board> _logger;
    private UserManager<ApplicationUser> _userManager;
    private ApplicationDbContext _context;

    public Board(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<Board> logger)
    {
        _userManager = userManager;
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var result = await _userManager.GetUserAsync(User);


        var tmp = await _context
            .Boards
            .Include(b => b.Columns)
            .Include(b => b.Rows)
            .FirstOrDefaultAsync(
                b => b.BoardId == Guid.Parse(id) && b.ApplicationUserId == result.Id
            );

        if (tmp == null)
        {
            _logger.LogWarning("{Username} could not load Board {Id}", result.Email, id);
            return RedirectToPage("/Error");
        }

        UserBoard = tmp;

        return Page();
    }
}