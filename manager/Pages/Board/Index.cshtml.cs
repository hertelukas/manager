using System.ComponentModel.DataAnnotations;
using manager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace manager.Pages.Board;

[Authorize]
public class Index : PageModel
{
    public ICollection<Data.Board> Boards;

    private ILogger<Index> _logger;
    private UserManager<ApplicationUser> _userManager;
    private ApplicationDbContext _context;

    [BindProperty] [Required] public string Name { get; set; }

    public Index(UserManager<ApplicationUser> userManager, ILogger<Index> logger, ApplicationDbContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _context = context;
        Boards = new List<Data.Board>();
    }


    public async Task OnGet()
    {
        var result = await _userManager.GetUserAsync(User);

        Boards = await _context.Boards.Where(b => b.ApplicationUserId.Equals(result.Id)).ToListAsync();

        if (result.Boards != null)
        {
            _logger.LogInformation("{Username} loaded {Amount} boards", result.Email, result.Boards.Count);
            Boards = result.Boards;
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userManager.GetUserAsync(User);

        var board = new Data.Board
        {
            Name = Name
        };

        _logger.LogInformation("{Username} created board {Name}", result.Email, board.Name);

        result.Boards ??= new List<Data.Board>();

        result.Boards.Add(board);
        await _userManager.UpdateAsync(result);

        return RedirectToPage("Board", new {id = board.BoardId});
    }
}