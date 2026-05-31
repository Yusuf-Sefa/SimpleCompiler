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
            y = (x @ 5) * 2;
            
            if (x {
                while (y) {
                    print (5 + 3);
                }
            }
        ";

        try
        {
            Lexer lexer = new Lexer(sourceCode);
            Parser parser = new Parser(lexer);
            
            parser.Parse();
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