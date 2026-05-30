
namespace SimpleCompiler;

class Parser
{
    private readonly Lexer _lexer;
    private Token _currentToken;

    public Parser(Lexer lexer)
    {
        _lexer = lexer;
        _currentToken = _lexer.NextToken();
    }

    public void Parse()
    {
        while(_currentToken.Type != TokenType.END)
        {
            ParseStatements();
        }
    }

    public void ParseStatements()
    {
        if(_currentToken.Type == TokenType.KEYWORD)
            ParseVeriableDeclaration();

        else if(_currentToken.Type == TokenType.IDENTIFIERS)
            ParseAssigment();
        else
            throw new Exception($"Syntax Error: Line {_currentToken.Line} -> Invalid statement: '{_currentToken.Lex}'");
    }

    private void CheckTokenType(TokenType expectedType)
    {
        if(_currentToken.Type == expectedType)
            _currentToken = _lexer.NextToken();
        else
            throw new Exception($"Syntax Error: Line {_currentToken.Line} -> Expected: {expectedType}, Current: {_currentToken.Type} ('{_currentToken.Lex}')");
    }

    public void ParseVeriableDeclaration()
    {
        Token tokenType = _currentToken;
        CheckTokenType(TokenType.KEYWORD);

        Token tokenId = _currentToken;
        CheckTokenType(TokenType.IDENTIFIERS);

        CheckTokenType(TokenType.SEMICOLON);
    }
    public void ParseAssigment()
    {
        Token tokenId = _currentToken;
        CheckTokenType(TokenType.IDENTIFIERS);

        CheckTokenType(TokenType.ASSIGN);

        ParseExpression();

        CheckTokenType(TokenType.SEMICOLON);
    }
    public void ParseFactor()
    {
        if (_currentToken.Type == TokenType.INT_LITERAL)
            CheckTokenType(TokenType.INT_LITERAL);

        else if (_currentToken.Type == TokenType.FLOAT_LITERAL)
            CheckTokenType(TokenType.FLOAT_LITERAL);

        else if (_currentToken.Type == TokenType.IDENTIFIERS)
            CheckTokenType(TokenType.IDENTIFIERS);
            
        else if (_currentToken.Type == TokenType.L_PAR)
        {
            CheckTokenType(TokenType.L_PAR);
            ParseExpression();
            CheckTokenType(TokenType.R_PAR);
        }
        else
        {
            throw new Exception($"Syntax Error: Line {_currentToken.Line} -> Invalid characters or missing parentheses in mathematical expressions: '{_currentToken.Lex}'");
        }      
    }
    public void ParseTerm()
    {
        ParseFactor();

        while (_currentToken.Type == TokenType.ARITHMETIC && 
            (_currentToken.Lex == "*" || _currentToken.Lex == "/"))
        {
            CheckTokenType(TokenType.ARITHMETIC);
            ParseFactor();
        }
    }
    private void ParseExpression()
    {
        ParseTerm();

        while (_currentToken.Type == TokenType.ARITHMETIC && 
            (_currentToken.Lex == "+" || _currentToken.Lex == "-"))
        {
            CheckTokenType(TokenType.ARITHMETIC);
            ParseTerm();
        }
    }
}