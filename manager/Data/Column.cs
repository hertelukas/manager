namespace manager.Data;

public abstract class Column
{
    public Guid ColumnId { get; set; }
    public abstract string Name { get; set; }

    public abstract ColumnEntry.ColumnEntry GetColumnEntry();
}