namespace SimpleCompiler;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnCompile_Click(object sender, EventArgs e)
    {
        // Her yeni derlemede ekranları sıfırla
        lstTokens.Items.Clear();
        txtConsole.Clear();
        txtAST.Clear();

        string sourceCode = txtSourceCode.Text;

        if (string.IsNullOrWhiteSpace(sourceCode))
        {
            txtConsole.SelectionColor = Color.Yellow;
            txtConsole.AppendText("[UYARI] Lütfen derlemek için geçerli bir kaynak kod yazın.\n");
            return;
        }

        try
        {
            txtConsole.AppendText("Derleme süreci başlatılıyor...\n\n");

            // ------------------------------------------------------------
            // PASS 1: LEXER & ARAYÜZ LİSTELEME
            // ------------------------------------------------------------
            txtConsole.AppendText("[PASS 1] Sözcük Analizi (Lexical Analysis) yapılıyor...\n");
            
            Lexer uiLexer = new Lexer(sourceCode);
            Token uiToken;

            do
            {
                uiToken = uiLexer.NextToken();

                if (uiToken.Type == TokenType.ERROR)
                {
                    throw new Exception($"Lexical Error: Satır {uiToken.Line} -> Tanımlanamayan karakter veya hatalı yapı: '{uiToken.Lex}'");
                }

                if (uiToken.Type != TokenType.END)
                {
                    // Ekranda hizalı durması için PadRight kullandık
                    string listItem = $"Line {uiToken.Line.ToString().PadRight(3)} | {uiToken.Type.ToString().PadRight(15)} : {uiToken.Lex}";
                    lstTokens.Items.Add(listItem);
                }

            } while (uiToken.Type != TokenType.END);

            txtConsole.SelectionColor = Color.LightGreen;
            txtConsole.AppendText("[PASS 1] Başarılı: Tüm sözcükler (token) başarıyla ayrıştırıldı.\n\n");


            // ------------------------------------------------------------
            // PASS 2: PARSER & SEMANTIC KONTROL & AST INŞASI
            // ------------------------------------------------------------
            txtConsole.SelectionColor = Color.White;
            txtConsole.AppendText("[PASS 2] Sözdizimi ve Anlamsal Analiz (Parser) başlatılıyor...\n");

            Lexer parserLexer = new Lexer(sourceCode);
            Parser parser = new Parser(parserLexer);

            // 1. Parser'ı çalıştırıp ağacın kök düğümünü (ProgramNode) teslim alıyoruz
            // Not: Eğer senin ana metodunun adı parser.Parse() ise burayı projene göre güncelle.
            ProgramNode astRoot = parser.Parse(); 

            // 2. Ağaç metnini üretip tamamen yeni oluşturduğumuz txtAST kutusuna basıyoruz
            string astText = astRoot.Print("");
            txtAST.Text = astText;

            // ------------------------------------------------------------
            // TÜM SÜREÇ BAŞARILI
            // ------------------------------------------------------------
            txtConsole.SelectionColor = Color.Lime;
            txtConsole.AppendText("\n==================================================\n");
            txtConsole.AppendText(">>> DERLEME BAŞARILI <<<\n");
            txtConsole.AppendText("Kod sözdizimi (Syntax) ve anlamsal (Semantic) açıdan kusursuz.");
            txtConsole.AppendText("\n==================================================\n");

        }
        catch (Exception ex)
        {
            // ------------------------------------------------------------
            // HATA YAKALAMA PANELİ
            // ------------------------------------------------------------
            txtConsole.SelectionColor = Color.Red;
            txtConsole.AppendText("\n==================================================\n");
            txtConsole.AppendText(">>> DERLEME BAŞARISIZ (HATA YAKALANDI) <<<\n\n");
            txtConsole.AppendText(ex.Message);
            txtConsole.AppendText("\n==================================================\n");
        }
    }
}
