using manager.Data;
using manager.Pages.Shared;
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

    public async Task ReceiveAddRow(string id, string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            await SendNotificationAsync(Context.ConnectionId, NotificationModel.Level.Danger, "Row has to have a name");
            return;
        }

        var user = await _userManager.GetUserAsync(Context.User);

        if (user == null)
        {
            _logger.LogWarning("Could not find user while creating new row");
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
        await SendRowUpdateAsync(Context.ConnectionId, board.Rows.Count - 1, board.Rows[^1]);
    }

    #endregion

    #region Send

    private async Task SendNotificationAsync(string connectionId, NotificationModel.Level level, string message)
    {
        _logger.LogInformation(
            "Sending \"{Message}\" to {Connection} with level {Level}",
            message,
            connectionId,
            level);

        await Clients.Client(connectionId).SendAsync("Notification", level.ToString().ToLower(), message);
    }

    private async Task SendRowUpdateAsync(string connectionId, int rowIndex, Row row)
    {
        _logger.LogInformation("Sending row {Index} update to {Connection}", rowIndex, connectionId);
        await Clients.Client(connectionId).SendAsync("RowUpdate", rowIndex, row);
    }

    #endregion
}