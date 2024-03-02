namespace TheIsleEvrimaRcon
{
    partial class RconConnectionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RconConnectionDialog));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            TxtHost = new TextBox();
            TxtPort = new TextBox();
            TxtPassword = new TextBox();
            BtnConnect = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(71, 93);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 0;
            label1.Text = "Host";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(383, 93);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 1;
            label2.Text = "Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 124);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 2;
            label3.Text = "Password";
            // 
            // TxtHost
            // 
            TxtHost.Location = new Point(109, 90);
            TxtHost.Name = "TxtHost";
            TxtHost.Size = new Size(268, 23);
            TxtHost.TabIndex = 1;
            TxtHost.Text = "127.0.0.1";
            // 
            // TxtPort
            // 
            TxtPort.Location = new Point(418, 90);
            TxtPort.Name = "TxtPort";
            TxtPort.Size = new Size(61, 23);
            TxtPort.TabIndex = 2;
            TxtPort.Text = "8888";
            // 
            // TxtPassword
            // 
            TxtPassword.Location = new Point(109, 121);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.Size = new Size(370, 23);
            TxtPassword.TabIndex = 3;
            TxtPassword.UseSystemPasswordChar = true;
            // 
            // BtnConnect
            // 
            BtnConnect.Location = new Point(12, 174);
            BtnConnect.Name = "BtnConnect";
            BtnConnect.Size = new Size(508, 23);
            BtnConnect.TabIndex = 6;
            BtnConnect.Text = "Connect";
            BtnConnect.UseVisualStyleBackColor = true;
            BtnConnect.Click += BtnConnect_Click;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 9);
            label4.Name = "label4";
            label4.Size = new Size(508, 55);
            label4.TabIndex = 7;
            label4.Text = "Connect to your The Isle Evrima RCON";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RconConnectionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(532, 209);
            Controls.Add(label4);
            Controls.Add(BtnConnect);
            Controls.Add(TxtPassword);
            Controls.Add(TxtPort);
            Controls.Add(TxtHost);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RconConnectionDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RCON Connection";
            Load += RconConnectionDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox TxtHost;
        private TextBox TxtPort;
        private TextBox TxtPassword;
        private Button BtnConnect;
        private Label label4;
    }
}