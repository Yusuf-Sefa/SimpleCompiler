
namespace SimpleCompiler;

class Lexer
{
    private readonly string _source; // Hold the source code

    private int _position = 0; // Hold current _positions in soruce

    private int _currentLine = 1; // Hold current line of code

    private char _currentChar // Hold char of current _position
    {
        get
        {
            return _position >= _source.Length
                    ? '\0'
                    : _source[_position]; 
        }
    }
    
    private readonly Dictionary<string, TokenType> _keywordDict = new()
    {
        { "int", TokenType.KEYWORD },
        { "float", TokenType.KEYWORD },
        { "if", TokenType.KEYWORD },
        { "else", TokenType.KEYWORD },
        { "while", TokenType.KEYWORD },
        { "print", TokenType.KEYWORD } 
    };

    public Lexer(string source)
    {
        _source = source;
    }

    // Advance the next char
    private void Next() 
    {
        if(_currentChar == '\n')
            _currentLine++;
        _position++;
    }


    // Return the next char
    private char GetNext() 
    {
        return _position >= _source.Length
                ? '\0'
                : _source[_position + 1];
    }

    public Token NextToken()
    {
        ParseWhitespace();

        if(_currentChar == '\0')
            return new Token(_currentLine, "END", TokenType.END);

        if(char.IsDigit(_currentChar))
            return ParseIntOrFloatLiterals();

        if(char.IsLetter(_currentChar) || _currentChar == '_')
            return ParseIdentifiersOrKeywords();

        if(_currentChar == '"')
            return ParseStringLiterals();

        Token token = ParseOperators();
        if(token is not null)
            return token;

        char invalidChar = _currentChar;
        Next();
        return new Token(_currentLine, invalidChar.ToString(), TokenType.ERROR);
    }

    private void ParseWhitespace()
    {
        while(_currentChar == ' ' 
                || _currentChar == '\t'
                || _currentChar == '\n'
                || _currentChar == '\r')
            Next();
    }

    private Token ParseIntOrFloatLiterals()
    {
        string num = "";
        int pointCount = 0;

        while(char.IsDigit(_currentChar) || _currentChar == '.')
        {
            if(_currentChar == '.')
            {
                pointCount++;
                if(pointCount > 1)
                    break;
            }
            num += _currentChar;
            Next();
        }

        if(pointCount > 1 || num == ".")
            return new Token(_currentLine, num, TokenType.ERROR);
        
        return pointCount == 0 
                ? new Token(_currentLine, num, TokenType.INT_LITERAL)
                : new Token(_currentLine, num, TokenType.FLOAT_LITERAL);
    }

    private Token ParseIdentifiersOrKeywords()
    {
        string str = "";
        
        while(char.IsLetterOrDigit(_currentChar) ||  _currentChar == '_')
        {
            str+= _currentChar;
            Next();
        }

        return _keywordDict.ContainsKey(str)
            ? new Token(_currentLine, str, TokenType.KEYWORD)
            : new Token(_currentLine, str, TokenType.IDENTIFIERS);
    }

    private Token ParseStringLiterals()
    {
        Next();
        string str = "";
        
        while(char.IsAscii(_currentChar) && !char.IsControl(_currentChar) && _currentChar != '"')
        {
            str += _currentChar;
            Next();
        }

        if(_currentChar != '"')
            return new Token(_currentLine, str, TokenType.ERROR);

        Next();
        return new Token(_currentLine, str, TokenType.STRING_LITERAL);
    }

    private Token? ParseOperators()
    {
        char c = _currentChar;
        string str = c.ToString();

        switch(c)
        {
            case ';' :
                Next();
                return new Token(_currentLine, str, TokenType.SEMICOLON);
            case ',' :
                Next();
                return new Token(_currentLine, str, TokenType.COMMA);

            case '(' :
                Next();
                return new Token(_currentLine, str, TokenType.L_PAR);
            case ')' :
                Next();
                return new Token(_currentLine, str, TokenType.R_PAR);
            case '{' :
                Next();
                return new Token(_currentLine, str, TokenType.L_BRACE);
            case '}' :
                Next();
                return new Token(_currentLine, str, TokenType.R_BRACE);

            case '=' :
                Next();
                if(_currentChar == '=')
                {
                    Next();
                    return new Token(_currentLine, "==", TokenType.RELATIONAL);                
                }
                return new Token(_currentLine, "=", TokenType.ASSIGN);
            
            case '+':
            case '-':
            case '*':
            case '/':
                Next();
                return new Token(_currentLine, str, TokenType.ARITHMETIC);
            
            case '<':
            case '>':
                Next();
                if(_currentChar == '=')
                {
                    Next();
                    string t = str + "=";
                    return new Token(_currentLine, t, TokenType.RELATIONAL);
                }
                return new Token(_currentLine, str, TokenType.RELATIONAL);

            case '!' :
                Next();
                if(_currentChar == '=')
                {
                    Next();
                    return new Token(_currentLine, "!=",  TokenType.RELATIONAL);
                }
                return new Token(_currentLine, str, TokenType.ERROR);
            
            case '&' :
                Next();
                if(_currentChar == '&')
                {
                    Next();
                    return new Token(_currentLine, "&&", TokenType.LOGICAL);
                }
                return new Token(_currentLine, str, TokenType.ERROR);

            case '|' :
                Next();
                if(_currentChar == '|')
                {
                    Next();
                    return new Token(_currentLine, "||", TokenType.LOGICAL);
                }
                return new Token(_currentLine, str, TokenType.ERROR);
        }
        return null;
    }
}