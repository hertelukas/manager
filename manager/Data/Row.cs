namespace manager.Data;

public class Row
{
    public Row(string name)
    {
        Name = name;
    }

    public Guid RowId { get; set; }
    public Guid BoardId { get; set; }
    public string Name { get; set; }
    public ICollection<ColumnEntry.ColumnEntry> Entries { get; set; }
}