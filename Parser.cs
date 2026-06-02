
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

    public ProgramNode Parse()
    {
    ProgramNode program = new ProgramNode();
    while (_currentToken.Type != TokenType.END)
    {
        ASTNode stmt = ParseStatements();
        if (stmt != null)
        {
            program.Statements.Add(stmt);
        }
    }
    return program;
    }

    public ASTNode ParseStatements()
    {
        if (_currentToken.Type == TokenType.KEYWORD)
        {
            switch (_currentToken.Lex)
            {
                case "if":
                    return ParseIfStatement();
                case "while":
                    return ParseWhileStatement();
                case "print":
                    return ParsePrintStatement();
                default:
                    return ParseVeriableDeclaration();
            }
        }
        else if (_currentToken.Type == TokenType.IDENTIFIERS)
        {
            return ParseAssigment();
        }
        else
        {
            throw new Exception($"Syntax Error: Line {_currentToken.Line} -> Invalid statement: '{_currentToken.Lex}'");
        }
    }

    private void CheckTokenType(TokenType expectedType)
    {
        if(_currentToken.Type == expectedType)
            _currentToken = _lexer.NextToken();
        else
            throw new Exception($"Syntax Error: Line {_currentToken.Line} -> Expected: {expectedType}, Current: {_currentToken.Type} ('{_currentToken.Lex}')");
    }

    public ASTNode ParseVeriableDeclaration()
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

        return new VariableDeclNode { DataType = dataType, VarName = varName };
    }
    public ASTNode ParseAssigment()
    {
        Token tokenId = _currentToken;
        string varName = tokenId.Lex!;
        CheckTokenType(TokenType.IDENTIFIERS);

        if (!_symbolTable.ContainsKey(varName))
            throw new Exception($"Semantic Error: Line {tokenId.Line}, '{varName}' is undefined");

        CheckTokenType(TokenType.ASSIGN);

        ASTNode exprNode = ParseExpression();
        
        CheckTokenType(TokenType.SEMICOLON);

        return new AssignmentNode 
        { 
            VarName = varName, 
            Expression = exprNode 
        };
    }

    public ASTNode ParseFactor()
    {
        if (_currentToken.Type == TokenType.INT_LITERAL)
        {
            string val = _currentToken.Lex!;
            CheckTokenType(TokenType.INT_LITERAL);
            return new LiteralNode { Value = val };
        }

        else if (_currentToken.Type == TokenType.FLOAT_LITERAL)
        {
            string val = _currentToken.Lex!;
            CheckTokenType(TokenType.FLOAT_LITERAL);
            return new LiteralNode { Value = val };
        }

        else if (_currentToken.Type == TokenType.IDENTIFIERS)
        {
            Token tokenId = _currentToken;
            if (!_symbolTable.ContainsKey(tokenId.Lex!))
            {
                throw new Exception($"Semantic Error: Line {tokenId.Line}, {tokenId.Lex} is undefined");
            }
            
            string val = tokenId.Lex!;
            CheckTokenType(TokenType.IDENTIFIERS);
            return new LiteralNode { Value = val };
        }
            
        else if (_currentToken.Type == TokenType.L_PAR)
        {
            CheckTokenType(TokenType.L_PAR);
            
            ASTNode exprNode = ParseExpression();
            
            CheckTokenType(TokenType.R_PAR);
            
            return exprNode; 
        }
        else
        {
            throw new Exception($"Syntax Error: Line {_currentToken.Line} -> Invalid characters or missing parentheses in mathematical expressions: '{_currentToken.Lex}'");
        }      
    }
    public ASTNode ParseTerm()
    {
        ASTNode left = ParseFactor();

        while (_currentToken.Type == TokenType.ARITHMETIC && 
            (_currentToken.Lex == "*" || _currentToken.Lex == "/"))
        {
            string op = _currentToken.Lex!;
            CheckTokenType(TokenType.ARITHMETIC);
            
            ASTNode right = ParseFactor();
            
            left = new BinaryOpNode 
            { 
                Operator = op, 
                Left = left, 
                Right = right 
            };
        }

        return left;
    }
    public ASTNode ParseExpression()
    {
        ASTNode left = ParseMathExpression();

        if (_currentToken.Type == TokenType.RELATIONAL) 
        {
            string op = _currentToken.Lex!;
            CheckTokenType(TokenType.RELATIONAL);

            ASTNode right = ParseMathExpression();

            left = new RelationalOpNode { Operator = op, Left = left, Right = right };
        }

        return left;
    }
    private ASTNode ParseMathExpression()
    {
        ASTNode left = ParseTerm();

        while (_currentToken.Type == TokenType.ARITHMETIC && 
            (_currentToken.Lex == "+" || _currentToken.Lex == "-"))
        {
            string op = _currentToken.Lex!;
            CheckTokenType(TokenType.ARITHMETIC);
            
            ASTNode right = ParseTerm();
            
            left = new BinaryOpNode { Operator = op, Left = left, Right = right };
        }
        return left;
    }

    private List<ASTNode> ParseStatementList()
    {
        List<ASTNode> list = new List<ASTNode>();
        
        while (_currentToken.Type != TokenType.R_BRACE && _currentToken.Type != TokenType.END)
        {
            ASTNode stmt = ParseStatements();
            if (stmt != null)
            {
                list.Add(stmt);
            }
        }
        
        return list;
    }
    private ASTNode ParseIfStatement()
    {
        CheckTokenType(TokenType.KEYWORD);
        CheckTokenType(TokenType.L_PAR);
        ASTNode conditionNode = ParseExpression();
        CheckTokenType(TokenType.R_PAR);
        CheckTokenType(TokenType.L_BRACE);
        List<ASTNode> bodyNodes = ParseStatementList();
        CheckTokenType(TokenType.R_BRACE);

        List<ASTNode> elseBodyNodes = new List<ASTNode>();

        if (_currentToken.Type == TokenType.KEYWORD && _currentToken.Lex == "else")
        {
            CheckTokenType(TokenType.KEYWORD);
            CheckTokenType(TokenType.L_BRACE);
            elseBodyNodes = ParseStatementList();
            CheckTokenType(TokenType.R_BRACE);
        }

        return new IfStatementNode 
        { 
            Condition = conditionNode, 
            Body = bodyNodes,
            ElseBody = elseBodyNodes
        };
    }
    private ASTNode ParseWhileStatement()
    {
        CheckTokenType(TokenType.KEYWORD); 
        CheckTokenType(TokenType.L_PAR);

        ASTNode conditionNode = ParseExpression();

        CheckTokenType(TokenType.R_PAR);
        CheckTokenType(TokenType.L_BRACE);

        List<ASTNode> bodyNodes = ParseStatementList();

        CheckTokenType(TokenType.R_BRACE);

        return new WhileStatementNode 
        { 
            Condition = conditionNode, 
            Body = bodyNodes 
        };
    }

    private ASTNode ParsePrintStatement()
    {
        CheckTokenType(TokenType.KEYWORD); 
        CheckTokenType(TokenType.L_PAR);
        
        ASTNode printExpression = null;

        if (_currentToken.Type == TokenType.STRING_LITERAL)
        {
            string val = _currentToken.Lex!;
            CheckTokenType(TokenType.STRING_LITERAL);
            printExpression = new LiteralNode { Value = $"\"{val}\"" };
        }
        else if (_currentToken.Type == TokenType.R_PAR)
        {
            throw new Exception($"Syntax Error: Line {_currentToken.Line}, print() cannot be empty");
        }
        else
        {
            printExpression = ParseExpression();
        }

        CheckTokenType(TokenType.R_PAR);
        CheckTokenType(TokenType.SEMICOLON);

        return new PrintNode { Expression = printExpression };
    }
    
}