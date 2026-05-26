
namespace SimpleCompiler;

class Token
{
    int Line { get; set; }
    string? Lex { get; set; }
    TokenType Type { get; set; }

    public Token(int Line, string Lex, TokenType Type)
    {
        this.Line = Line;
        this.Lex = Lex;
        this.Type = Type;        
    }

}