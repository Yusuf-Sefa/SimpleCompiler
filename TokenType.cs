
namespace SimpleCompiler;

enum TokenType
{
    KEYWORD, // int, float, if , else, print

    IDENTIFIERS, // Veriable names (x, y ...)
    INT_LITERAL, // Integer values (5, 17, 98 ...)
    FLOAT_LITERAL, // Float values (5.4, 21.25 ...)
    STRING_LITERAL, // String values ("Some string")

    ASSIGN, // =
    ARITHMETIC, // +, -, *, /
    LOGICAL, // &&, ||
    RELATIONAL, // ==, !=, <, >, <=, >=
    
    L_PAR, // (
    R_PAR, // )
    L_BRACE, // {
    R_BRACE, // }
    SEMICOLON, // ;
    COMMA, // ,

    END, // For end of the file
    ERROR, // For erorr
}