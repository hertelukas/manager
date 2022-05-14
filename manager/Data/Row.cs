namespace manager.Data;

public class Row
{
    public Guid RowId { get; set; }
    public Guid BoardId { get; set; }
    public ICollection<ColumnEntry.ColumnEntry> Entries { get; set; }
}