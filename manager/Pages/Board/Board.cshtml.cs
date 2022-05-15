#nullable disable

using manager.Data;
using manager.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace manager.Pages.Board;

[Authorize]
public class Board : PageModel
{
    public Data.Board UserBoard;

    [TempData] public string ErrorMessage { get; set; }

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

        if (!Guid.TryParse(id, out var guid))
        {
            ErrorMessage = JsonConvert.SerializeObject(
                new NotificationModel(NotificationModel.Level.Danger, "This link is invalid"));

            return RedirectToPage("/Board/Index");
        }

        var tmp = await _context
            .Boards
            .Include(b => b.Columns)
            .Include(b => b.Rows)
            .FirstOrDefaultAsync(
                b => b.BoardId == guid && b.ApplicationUserId == result.Id
            );

        if (tmp == null)
        {
            ErrorMessage = JsonConvert.SerializeObject(
                new NotificationModel(NotificationModel.Level.Danger, "Could not find your board")
            );

            _logger.LogWarning("{Username} could not load Board {Id}", result.Email, id);
            return RedirectToPage("/Board/Index");
        }

        UserBoard = tmp;

        return Page();
    }
}