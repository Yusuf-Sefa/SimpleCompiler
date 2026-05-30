
using System.ComponentModel.Design;

namespace SimpleCompiler;

class Parser
{
    private readonly Lexer _lexer;
    private Token _currentToken;
    private readonly Dictionary<string, SymbolTable> _symbolTable = [];
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
        else if(_currentToken.Type == TokenType.KEYWORD && _currentToken.Lex == "if")
            ParseIfStatement();
        else if(_currentToken.Type == TokenType.KEYWORD && _currentToken.Lex == "while")
            ParseWhileStatement();
        else if(_currentToken.Type == TokenType.KEYWORD && _currentToken.Lex == "print")
            ParsePrintStatement();
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
        string dataType = tokenType.Lex!;
        CheckTokenType(TokenType.KEYWORD);

        Token tokenId = _currentToken;
        string varName = tokenId.Lex!;
        CheckTokenType(TokenType.IDENTIFIERS);

        CheckTokenType(TokenType.SEMICOLON);

        if(_symbolTable.TryGetValue(varName, out SymbolTable? value))
            throw new Exception($"Semantic Error: Line {tokenId.Line}, '{varName}' veriable already exists in line {value.Line}");
        _symbolTable.Add(varName, new SymbolTable(varName, dataType, tokenId.Line));
    }
    public void ParseAssigment()
    {
        Token tokenId = _currentToken;
        string varName = tokenId.Lex!;
        CheckTokenType(TokenType.IDENTIFIERS);

        if(!_symbolTable.ContainsKey(varName))
            throw new Exception($"Semantic Error: Line {tokenId.Line}, '{varName} is undefined");

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
        {
            Token tokenId = _currentToken;
            if (!_symbolTable.ContainsKey(tokenId.Lex!))
            {
                throw new Exception($"Semantic Error: Line {tokenId.Line}, {tokenId.Lex} is undefined");
            }
            CheckTokenType(TokenType.IDENTIFIERS);
        }
            
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

    private void ParseStatementList()
    {
        while (_currentToken.Type != TokenType.R_BRACE && _currentToken.Type != TokenType.END)
            ParseStatements();
    }
    private void ParseIfStatement()
    {
        CheckTokenType(TokenType.KEYWORD);
        CheckTokenType(TokenType.L_PAR);

        ParseExpression();

        CheckTokenType(TokenType.R_PAR);
        CheckTokenType(TokenType.L_BRACE);

        ParseStatementList();

        CheckTokenType(TokenType.R_BRACE);
    }
    private void ParseWhileStatement()
    {
        CheckTokenType(TokenType.KEYWORD);
        CheckTokenType(TokenType.L_PAR);

        ParseExpression();

        CheckTokenType(TokenType.R_PAR);
        CheckTokenType(TokenType.L_BRACE);

        ParseStatementList();

        CheckTokenType(TokenType.R_BRACE);
    }

    private void ParsePrintStatement()
    {
        CheckTokenType(TokenType.KEYWORD);
        ParseExpression();
        CheckTokenType(TokenType.SEMICOLON);
    }
    
}