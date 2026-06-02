namespace SimpleCompiler;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
       string sourceCode = @"
            int x;
            float y;
            x = 10;
            y = (x + 5) * 2;
            
            if (x) {
                while (y) {
                    print x;
                }
            }
        ";

        try
        {
            Lexer lexer = new(sourceCode);
            Parser parser = new(lexer);
            
            parser.Parse();
            /*Token token;
            do
            {
                token = lexer.NextToken();
                Console.WriteLine("Token: " + token.Lex);
            }
            while(token.Type != TokenType.END);*/
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[DERLEME HATASI] {ex.Message}");
            Console.ResetColor();
        }
        Console.ReadLine();
    }    
}