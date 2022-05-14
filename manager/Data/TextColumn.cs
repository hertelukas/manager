using manager.Data.ColumnEntry;

namespace manager.Data;

public class TextColumn : Column
{
    public TextColumn(string name)
    {
        Name = name;
    }

    public override string Name { get; set; }

    public string Text { get; set; }

    public override TextColumnEntry GetColumnEntry()
    {
        return new TextColumnEntry();
    }
}