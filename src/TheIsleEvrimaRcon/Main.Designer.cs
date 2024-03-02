namespace TheIsleEvrimaRcon
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            RtbConsole = new RichTextBox();
            TxtRconCommand = new TextBox();
            BtnSendCommand = new Button();
            label1 = new Label();
            CbQuickCommands = new ComboBox();
            TxtQuickCommandArg = new TextBox();
            BtnSendQuickCommand = new Button();
            SuspendLayout();
            // 
            // RtbConsole
            // 
            RtbConsole.BackColor = SystemColors.ControlLight;
            RtbConsole.Location = new Point(12, 56);
            RtbConsole.Name = "RtbConsole";
            RtbConsole.ReadOnly = true;
            RtbConsole.Size = new Size(770, 363);
            RtbConsole.TabIndex = 0;
            RtbConsole.Text = "";
            RtbConsole.TextChanged += RtbConsole_TextChanged;
            // 
            // TxtRconCommand
            // 
            TxtRconCommand.Location = new Point(12, 427);
            TxtRconCommand.Name = "TxtRconCommand";
            TxtRconCommand.Size = new Size(689, 23);
            TxtRconCommand.TabIndex = 4;
            TxtRconCommand.KeyPress += TxtRconCommand_KeyPress;
            // 
            // BtnSendCommand
            // 
            BtnSendCommand.Location = new Point(707, 427);
            BtnSendCommand.Name = "BtnSendCommand";
            BtnSendCommand.Size = new Size(75, 23);
            BtnSendCommand.TabIndex = 5;
            BtnSendCommand.Text = "Execute";
            BtnSendCommand.UseVisualStyleBackColor = true;
            BtnSendCommand.Click += BtnSendCommand_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 3;
            label1.Text = "Quick command:";
            // 
            // CbQuickCommands
            // 
            CbQuickCommands.DropDownStyle = ComboBoxStyle.DropDownList;
            CbQuickCommands.FormattingEnabled = true;
            CbQuickCommands.Location = new Point(12, 27);
            CbQuickCommands.Name = "CbQuickCommands";
            CbQuickCommands.Size = new Size(284, 23);
            CbQuickCommands.TabIndex = 1;
            CbQuickCommands.SelectedIndexChanged += CbQuickCommands_SelectedIndexChanged;
            // 
            // TxtQuickCommandArg
            // 
            TxtQuickCommandArg.Enabled = false;
            TxtQuickCommandArg.Location = new Point(302, 27);
            TxtQuickCommandArg.Name = "TxtQuickCommandArg";
            TxtQuickCommandArg.Size = new Size(399, 23);
            TxtQuickCommandArg.TabIndex = 2;
            TxtQuickCommandArg.KeyPress += TxtQuickCommandArg_KeyPress;
            // 
            // BtnSendQuickCommand
            // 
            BtnSendQuickCommand.Location = new Point(707, 26);
            BtnSendQuickCommand.Name = "BtnSendQuickCommand";
            BtnSendQuickCommand.Size = new Size(75, 23);
            BtnSendQuickCommand.TabIndex = 3;
            BtnSendQuickCommand.Text = "Execute";
            BtnSendQuickCommand.UseVisualStyleBackColor = true;
            BtnSendQuickCommand.Click += BtnSendQuickCommand_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(794, 462);
            Controls.Add(BtnSendQuickCommand);
            Controls.Add(TxtQuickCommandArg);
            Controls.Add(CbQuickCommands);
            Controls.Add(label1);
            Controls.Add(BtnSendCommand);
            Controls.Add(TxtRconCommand);
            Controls.Add(RtbConsole);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Evrima RCON";
            FormClosing += Main_FormClosing;
            Load += Main_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox RtbConsole;
        private TextBox TxtRconCommand;
        private Button BtnSendCommand;
        private Label label1;
        private ComboBox CbQuickCommands;
        private TextBox TxtQuickCommandArg;
        private Button BtnSendQuickCommand;
    }
}
