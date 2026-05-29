
namespace SimpleCompiler;

class Token
{
    public int Line { get; set; }
    public string? Lex { get; set; }
    public TokenType Type { get; set; }

    public Token(int Line, string Lex, TokenType Type)
    {
        this.Line = Line;
        this.Lex = Lex;
        this.Type = Type;        
    }

}