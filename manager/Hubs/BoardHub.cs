using manager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace manager.Hubs;

public class BoardHub : Hub
{
    private ILogger<BoardHub> _logger;
    private ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    #region Receive

    public BoardHub(ILogger<BoardHub> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public async Task ReceiveAddRow(string id, string name)
    {
        var user = await _userManager.GetUserAsync(Context.User);

        if (user == null)
        {
            _logger.LogWarning("Could not find user {User} while creating new row", Context.User);
            return;
        }

        var board = await _context.Boards.Include(b => b.Rows)
            .FirstOrDefaultAsync(b => b.BoardId.Equals(Guid.Parse(id)));

        if (board == null || !user.Boards.Contains(board))
        {
            _logger.LogWarning("{Username} has no access to the board {BoardId}", user.Email, id);
            return;
        }

        board.AddRow(name);
        await _context.SaveChangesAsync();
    }

    #endregion
}