
namespace SimpleCompiler;

public class SymbolTable
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int Line { get; set; }

    public SymbolTable(string name, string type, int line)
    {
        Name = name;
        Type = type;
        Line = line;
    }

}