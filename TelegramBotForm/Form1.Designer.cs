namespace TelegramBotForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadFile = new System.Windows.Forms.Button();
            this.StartBot = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Export = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoadFile
            // 
            this.LoadFile.Location = new System.Drawing.Point(12, 12);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(175, 30);
            this.LoadFile.TabIndex = 0;
            this.LoadFile.Text = "Загрузка файла";
            this.LoadFile.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.LoadFile.UseVisualStyleBackColor = true;
            this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
            // 
            // StartBot
            // 
            this.StartBot.Location = new System.Drawing.Point(12, 59);
            this.StartBot.Name = "StartBot";
            this.StartBot.Size = new System.Drawing.Size(175, 30);
            this.StartBot.TabIndex = 1;
            this.StartBot.Text = "Запуск бота";
            this.StartBot.UseVisualStyleBackColor = true;
            this.StartBot.Visible = false;
            this.StartBot.Click += new System.EventHandler(this.StartBot_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(193, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(312, 550);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(12, 111);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(175, 30);
            this.Export.TabIndex = 3;
            this.Export.Text = "Выгрузить список";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Visible = false;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 578);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.StartBot);
            this.Controls.Add(this.LoadFile);
            this.Name = "Form1";
            this.Text = "ЧатБот";
            this.ResumeLayout(false);

        }

        #endregion

        private Button LoadFile;
        private Button StartBot;
        private RichTextBox richTextBox1;
        private Button Export;
    }
}