namespace bitlc_do1
{
    partial class AssistSonja
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssistSonja));
            this.PnlHead = new System.Windows.Forms.Panel();
            this.btnFromKlein = new System.Windows.Forms.Button();
            this.btnFromOut = new System.Windows.Forms.Button();
            this.TirUpDate = new System.Windows.Forms.Timer(this.components);
            this.txtSockBox = new System.Windows.Forms.TextBox();
            this.listSocketBox = new System.Windows.Forms.ListBox();
            this.txtInputbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panelSocket = new System.Windows.Forms.Panel();
            this.LblSocket = new System.Windows.Forms.Label();
            this.PnlHead.SuspendLayout();
            this.panelSocket.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlHead
            // 
            this.PnlHead.BackColor = System.Drawing.Color.Cyan;
            this.PnlHead.Controls.Add(this.btnFromKlein);
            this.PnlHead.Controls.Add(this.btnFromOut);
            this.PnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHead.Location = new System.Drawing.Point(0, 0);
            this.PnlHead.Name = "PnlHead";
            this.PnlHead.Size = new System.Drawing.Size(916, 27);
            this.PnlHead.TabIndex = 0;
            this.PnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlHead_MouseDown);
            this.PnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PnlHead_MouseMove);
            this.PnlHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PnlHead_MouseUp);
            // 
            // btnFromKlein
            // 
            this.btnFromKlein.Location = new System.Drawing.Point(871, 3);
            this.btnFromKlein.Name = "btnFromKlein";
            this.btnFromKlein.Size = new System.Drawing.Size(18, 19);
            this.btnFromKlein.TabIndex = 2;
            this.btnFromKlein.Text = "-";
            this.btnFromKlein.UseVisualStyleBackColor = true;
            this.btnFromKlein.Click += new System.EventHandler(this.btnFromKlein_Click);
            // 
            // btnFromOut
            // 
            this.btnFromOut.Location = new System.Drawing.Point(895, 3);
            this.btnFromOut.Name = "btnFromOut";
            this.btnFromOut.Size = new System.Drawing.Size(18, 19);
            this.btnFromOut.TabIndex = 3;
            this.btnFromOut.Text = "x";
            this.btnFromOut.UseVisualStyleBackColor = true;
            this.btnFromOut.Click += new System.EventHandler(this.btnFromOut_Click);
            // 
            // TirUpDate
            // 
            this.TirUpDate.Enabled = true;
            this.TirUpDate.Tick += new System.EventHandler(this.TirUpDate_Tick);
            // 
            // txtSockBox
            // 
            this.txtSockBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtSockBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSockBox.Font = new System.Drawing.Font("Tempus Sans ITC", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSockBox.ForeColor = System.Drawing.Color.Cyan;
            this.txtSockBox.Location = new System.Drawing.Point(3, 3);
            this.txtSockBox.Multiline = true;
            this.txtSockBox.Name = "txtSockBox";
            this.txtSockBox.ReadOnly = true;
            this.txtSockBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSockBox.Size = new System.Drawing.Size(124, 199);
            this.txtSockBox.TabIndex = 2;
            this.txtSockBox.TextChanged += new System.EventHandler(this.txtSockBox_TextChanged);
            // 
            // listSocketBox
            // 
            this.listSocketBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.listSocketBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listSocketBox.Font = new System.Drawing.Font("Perpetua Titling MT", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listSocketBox.ForeColor = System.Drawing.Color.Aqua;
            this.listSocketBox.FormattingEnabled = true;
            this.listSocketBox.Location = new System.Drawing.Point(133, 3);
            this.listSocketBox.Name = "listSocketBox";
            this.listSocketBox.Size = new System.Drawing.Size(124, 156);
            this.listSocketBox.TabIndex = 3;
            // 
            // txtInputbox
            // 
            this.txtInputbox.Location = new System.Drawing.Point(133, 182);
            this.txtInputbox.Name = "txtInputbox";
            this.txtInputbox.Size = new System.Drawing.Size(124, 20);
            this.txtInputbox.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Send ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelSocket
            // 
            this.panelSocket.BackColor = System.Drawing.Color.Cyan;
            this.panelSocket.Controls.Add(this.LblSocket);
            this.panelSocket.Controls.Add(this.listSocketBox);
            this.panelSocket.Controls.Add(this.button1);
            this.panelSocket.Controls.Add(this.txtSockBox);
            this.panelSocket.Controls.Add(this.txtInputbox);
            this.panelSocket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelSocket.Location = new System.Drawing.Point(12, 333);
            this.panelSocket.Name = "panelSocket";
            this.panelSocket.Size = new System.Drawing.Size(260, 205);
            this.panelSocket.TabIndex = 6;
            // 
            // LblSocket
            // 
            this.LblSocket.AutoSize = true;
            this.LblSocket.Location = new System.Drawing.Point(209, 163);
            this.LblSocket.Name = "LblSocket";
            this.LblSocket.Size = new System.Drawing.Size(45, 13);
            this.LblSocket.TabIndex = 6;
            this.LblSocket.Text = "Member";
            // 
            // AssistSonja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(916, 550);
            this.Controls.Add(this.panelSocket);
            this.Controls.Add(this.PnlHead);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AssistSonja";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssistSonja_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AssistSonja_MouseMove);
            this.PnlHead.ResumeLayout(false);
            this.panelSocket.ResumeLayout(false);
            this.panelSocket.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel PnlHead;
        private System.Windows.Forms.Timer TirUpDate;
        private System.Windows.Forms.Button btnFromKlein;
        private System.Windows.Forms.Button btnFromOut;
        public System.Windows.Forms.TextBox txtSockBox;
        public System.Windows.Forms.ListBox listSocketBox;
        public System.Windows.Forms.TextBox txtInputbox;
        public System.Windows.Forms.Panel panelSocket;
        private System.Windows.Forms.Label LblSocket;
        public System.Windows.Forms.Button button1;
    }
}

