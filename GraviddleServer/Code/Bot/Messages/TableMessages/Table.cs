using ConsoleTables;

namespace GraviddleServer.Code.Bot.Messages.TableMessages;

public class Table
{
    private readonly ConsoleTable _table;

    public Table(params string[] columns)
    {
        _table = new ConsoleTable(columns)
        {
            Options =
            {
                EnableCount = false
            }
        };
    }

    public void Add(params object[] row)
    {
        _table.AddRow(row);
    }

    public string Build()
    {
        return "<pre>" + _table + "</pre>";
    }
}