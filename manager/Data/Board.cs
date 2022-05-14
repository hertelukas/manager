namespace manager.Data;

#nullable disable

public class Board
{
    public Guid BoardId { get; set; }
    public string ApplicationUserId { get; set; }
    public string Name { get; set; }

    public ICollection<Column> Columns { get; set; }

    public ICollection<Row> Rows { get; set; }

    public void AddColumn(ColumnType type, string name)
    {
        switch (type)
        {
            case ColumnType.TextColumn:
                Columns.Add(new TextColumn(name));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public void AddRow(string name)
    {
        Rows ??= new List<Row>();

        Rows.Add(new Row(name));
    }
}