
namespace SimpleCompiler;
    public abstract class ASTNode 
    {
        public abstract string Print(string indent);
    }
    public class ProgramNode : ASTNode
    {
        public List<ASTNode> Statements { get; set; } = new List<ASTNode>();

        public override string Print(string indent)
        {
            string result = indent + "└── Program\n";
            foreach (var stmt in Statements)
            {
                result += stmt.Print(indent + "    ");
            }
            return result;
        }
    }

    public class VariableDeclNode : ASTNode
    {
        public string DataType { get; set; }
        public string VarName { get; set; }

        public override string Print(string indent)
        {
            return indent + $"└── VariableDecl: {DataType} {VarName}\n";
        }
    }

    public class AssignmentNode : ASTNode
    {
        public string VarName { get; set; }
        public ASTNode Expression { get; set; }

        public override string Print(string indent)
        {
            string result = indent + $"└── Assignment: {VarName} =\n";
            result += Expression.Print(indent + "    ");
            return result;
        }
    }
    public class BinaryOpNode : ASTNode
    {
        public string Operator { get; set; }
        public ASTNode Left { get; set; }
        public ASTNode Right { get; set; }

        public override string Print(string indent)
        {
            string result = indent + $"└── BinaryOp: {Operator}\n";
            result += Left.Print(indent + "    ");
            result += Right.Print(indent + "    ");
            return result;
        }
    }

    public class LiteralNode : ASTNode
    {
        public string Value { get; set; }

        public override string Print(string indent)
        {
            return indent + $"└── Literal/Id: {Value}\n";
        }
    }

    public class PrintNode : ASTNode
    {
        public ASTNode Expression { get; set; }

        public override string Print(string indent)
        {
            string result = indent + "└── Print\n";
            result += Expression.Print(indent + "    ");
            return result;
        }
    }

    public class IfStatementNode : ASTNode
    {
        public ASTNode Condition { get; set; }
        public List<ASTNode> Body { get; set; } = new();
        public List<ASTNode> ElseBody { get; set; } = new();

        public override string Print(string indent)
        {
            string result = indent + "└── IfStatement\n";
            result += indent + "    ├── Condition:\n";
            result += Condition.Print(indent + "    │   ");
            result += indent + "    ├── TrueBody:\n";
            foreach (var stmt in Body) result += stmt.Print(indent + "    │   ");
            
            if (ElseBody.Count > 0)
            {
                result += indent + "    └── ElseBody:\n";
                foreach (var stmt in ElseBody) result += stmt.Print(indent + "        ");
            }
            return result;
        }
    }

    public class WhileStatementNode : ASTNode
    {
        public ASTNode Condition { get; set; } 
        public List<ASTNode> Body { get; set; } = new();

        public override string Print(string indent)
        {
            string result = indent + "└── WhileStatement\n";
            result += indent + "    ├── Condition:\n";
            result += Condition.Print(indent + "    │   ");
            result += indent + "    └── Body:\n";
            foreach (var stmt in Body)
            {
                result += stmt.Print(indent + "        ");
            }
            return result;
        }
    }

    public class RelationalOpNode : ASTNode
    {
        public string Operator { get; set; }
        public ASTNode Left { get; set; }
        public ASTNode Right { get; set; }

        public override string Print(string indent)
        {
            string result = indent + $"└── RelationalOp: {Operator}\n";
            result += Left.Print(indent + "    ");
            result += Right.Print(indent + "    ");
            return result;
        }
    }