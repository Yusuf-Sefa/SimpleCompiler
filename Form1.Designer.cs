namespace SimpleCompiler
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtSourceCode = new System.Windows.Forms.RichTextBox();
            this.btnCompile = new System.Windows.Forms.Button();
            this.lstTokens = new System.Windows.Forms.ListBox();
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.txtAST = new System.Windows.Forms.RichTextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblTokens = new System.Windows.Forms.Label();
            this.lblConsole = new System.Windows.Forms.Label();
            this.lblAST = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtSourceCode
            this.txtSourceCode.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSourceCode.Location = new System.Drawing.Point(12, 32);
            this.txtSourceCode.Name = "txtSourceCode";
            this.txtSourceCode.Size = new System.Drawing.Size(450, 350);
            this.txtSourceCode.TabIndex = 0;
            this.txtSourceCode.Text = "int x;\nx = 10;\nif (x) {\n    print(x);\n}";

            // btnCompile
            this.btnCompile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCompile.Location = new System.Drawing.Point(12, 395);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(450, 45);
            this.btnCompile.TabIndex = 1;
            this.btnCompile.Text = "KODU DERLE (COMPILE)";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);

            // lstTokens
            this.lstTokens.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstTokens.FormattingEnabled = true;
            this.lstTokens.ItemHeight = 15;
            this.lstTokens.Location = new System.Drawing.Point(480, 32);
            this.lstTokens.Name = "lstTokens";
            this.lstTokens.Size = new System.Drawing.Size(450, 349);
            this.lstTokens.TabIndex = 2;

            // txtConsole
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtConsole.ForeColor = System.Drawing.Color.White;
            this.txtConsole.Location = new System.Drawing.Point(12, 475);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(450, 240);
            this.txtConsole.TabIndex = 3;
            this.txtConsole.Text = "";

            // txtAST
            this.txtAST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtAST.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtAST.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.txtAST.Location = new System.Drawing.Point(480, 475);
            this.txtAST.Name = "txtAST";
            this.txtAST.ReadOnly = true;
            this.txtAST.Size = new System.Drawing.Size(450, 240);
            this.txtAST.TabIndex = 4;
            this.txtAST.Text = "";
            this.txtAST.WordWrap = false;

            // lblSource (Etiketler)
            this.lblSource.AutoSize = true;
            this.lblSource.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSource.Location = new System.Drawing.Point(12, 9);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(155, 17);
            this.lblSource.Text = "Kaynak Kod (Source Code)";

            // lblTokens
            this.lblTokens.AutoSize = true;
            this.lblTokens.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTokens.Location = new System.Drawing.Point(480, 9);
            this.lblTokens.Name = "lblTokens";
            this.lblTokens.Size = new System.Drawing.Size(152, 17);
            this.lblTokens.Text = "Pass 1: Token Listesi (Lexer)";

            // lblConsole
            this.lblConsole.AutoSize = true;
            this.lblConsole.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConsole.Location = new System.Drawing.Point(12, 455);
            this.lblConsole.Name = "lblConsole";
            this.lblConsole.Size = new System.Drawing.Size(127, 17);
            this.lblConsole.Text = "Derleyici Çıktı Paneli";

            // lblAST
            this.lblAST.AutoSize = true;
            this.lblAST.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAST.Location = new System.Drawing.Point(480, 455);
            this.lblAST.Name = "lblAST";
            this.lblAST.Size = new System.Drawing.Size(199, 17);
            this.lblAST.Text = "Pass 2: Soyut Sözdizim Ağacı (AST)";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 727);
            this.Controls.Add(this.lblAST);
            this.Controls.Add(this.lblConsole);
            this.Controls.Add(this.lblTokens);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.txtAST);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.lstTokens);
            this.Controls.Add(this.btnCompile);
            this.Controls.Add(this.txtSourceCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Compiler IDE";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RichTextBox txtSourceCode;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.ListBox lstTokens;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.RichTextBox txtAST;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblTokens;
        private System.Windows.Forms.Label lblConsole;
        private System.Windows.Forms.Label lblAST;
    }
}
