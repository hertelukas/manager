using manager.Data.ColumnEntry;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace manager.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }

    // Columns
    public DbSet<Column> Columns { get; set; }
    public DbSet<TextColumn> TextColumns { get; set; }

    // Rows
    public DbSet<Row> Rows { get; set; }

    // Entry types
    public DbSet<ColumnEntry.ColumnEntry> ColumnEntries { get; set; }
    public DbSet<TextColumnEntry> TextColumnEntries { get; set; }
}