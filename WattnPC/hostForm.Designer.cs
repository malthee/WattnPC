namespace WattnPC
{
    partial class hostForm
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
            this.labelSpieler2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxSpieler2IP = new System.Windows.Forms.TextBox();
            this.labelSpieler1 = new System.Windows.Forms.Label();
            this.textBoxSpielerIP = new System.Windows.Forms.TextBox();
            this.textBoxSpieler3IP = new System.Windows.Forms.TextBox();
            this.labelSpieler3 = new System.Windows.Forms.Label();
            this.textBoxSpieler4IP = new System.Windows.Forms.TextBox();
            this.labelSpieler4 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelSpieler2
            // 
            this.labelSpieler2.AutoSize = true;
            this.labelSpieler2.Location = new System.Drawing.Point(9, 48);
            this.labelSpieler2.Name = "labelSpieler2";
            this.labelSpieler2.Size = new System.Drawing.Size(48, 13);
            this.labelSpieler2.TabIndex = 2;
            this.labelSpieler2.Text = "Spieler 2";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(9, 202);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(156, 35);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxSpieler2IP
            // 
            this.textBoxSpieler2IP.Location = new System.Drawing.Point(9, 64);
            this.textBoxSpieler2IP.MaxLength = 15;
            this.textBoxSpieler2IP.Name = "textBoxSpieler2IP";
            this.textBoxSpieler2IP.Size = new System.Drawing.Size(277, 20);
            this.textBoxSpieler2IP.TabIndex = 1;
            // 
            // labelSpieler1
            // 
            this.labelSpieler1.AutoSize = true;
            this.labelSpieler1.Location = new System.Drawing.Point(9, 9);
            this.labelSpieler1.Name = "labelSpieler1";
            this.labelSpieler1.Size = new System.Drawing.Size(48, 13);
            this.labelSpieler1.TabIndex = 10;
            this.labelSpieler1.Text = "Spieler 1";
            // 
            // textBoxSpielerIP
            // 
            this.textBoxSpielerIP.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxSpielerIP.Location = new System.Drawing.Point(9, 25);
            this.textBoxSpielerIP.MaxLength = 15;
            this.textBoxSpielerIP.Name = "textBoxSpielerIP";
            this.textBoxSpielerIP.ReadOnly = true;
            this.textBoxSpielerIP.Size = new System.Drawing.Size(277, 20);
            this.textBoxSpielerIP.TabIndex = 10;
            // 
            // textBoxSpieler3IP
            // 
            this.textBoxSpieler3IP.BackColor = System.Drawing.Color.White;
            this.textBoxSpieler3IP.Location = new System.Drawing.Point(9, 118);
            this.textBoxSpieler3IP.MaxLength = 15;
            this.textBoxSpieler3IP.Name = "textBoxSpieler3IP";
            this.textBoxSpieler3IP.Size = new System.Drawing.Size(277, 20);
            this.textBoxSpieler3IP.TabIndex = 2;
            // 
            // labelSpieler3
            // 
            this.labelSpieler3.AutoSize = true;
            this.labelSpieler3.Location = new System.Drawing.Point(6, 102);
            this.labelSpieler3.Name = "labelSpieler3";
            this.labelSpieler3.Size = new System.Drawing.Size(48, 13);
            this.labelSpieler3.TabIndex = 14;
            this.labelSpieler3.Text = "Spieler 3";
            // 
            // textBoxSpieler4IP
            // 
            this.textBoxSpieler4IP.BackColor = System.Drawing.Color.White;
            this.textBoxSpieler4IP.Location = new System.Drawing.Point(9, 157);
            this.textBoxSpieler4IP.MaxLength = 15;
            this.textBoxSpieler4IP.Name = "textBoxSpieler4IP";
            this.textBoxSpieler4IP.Size = new System.Drawing.Size(277, 20);
            this.textBoxSpieler4IP.TabIndex = 3;
            // 
            // labelSpieler4
            // 
            this.labelSpieler4.AutoSize = true;
            this.labelSpieler4.Location = new System.Drawing.Point(6, 141);
            this.labelSpieler4.Name = "labelSpieler4";
            this.labelSpieler4.Size = new System.Drawing.Size(48, 13);
            this.labelSpieler4.TabIndex = 12;
            this.labelSpieler4.Text = "Spieler 4";
            // 
            // labelError
            // 
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(12, 240);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(277, 13);
            this.labelError.TabIndex = 15;
            this.labelError.Text = "Error: xxxxxx";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelError.Visible = false;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.Color.White;
            this.textBoxPassword.Location = new System.Drawing.Point(175, 217);
            this.textBoxPassword.MaxLength = 15;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(114, 20);
            this.textBoxPassword.TabIndex = 16;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(172, 202);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 17;
            this.labelPassword.Text = "Password";
            // 
            // hostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 265);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.textBoxSpieler3IP);
            this.Controls.Add(this.labelSpieler3);
            this.Controls.Add(this.textBoxSpieler4IP);
            this.Controls.Add(this.labelSpieler4);
            this.Controls.Add(this.textBoxSpielerIP);
            this.Controls.Add(this.labelSpieler1);
            this.Controls.Add(this.textBoxSpieler2IP);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelSpieler2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "hostForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spiel Hosten";
            this.Load += new System.EventHandler(this.hostForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelSpieler2;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxSpieler2IP;
        private System.Windows.Forms.Label labelSpieler1;
        private System.Windows.Forms.TextBox textBoxSpielerIP;
        private System.Windows.Forms.TextBox textBoxSpieler3IP;
        private System.Windows.Forms.Label labelSpieler3;
        private System.Windows.Forms.TextBox textBoxSpieler4IP;
        private System.Windows.Forms.Label labelSpieler4;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
    }
}