namespace WattnPC
{
    partial class connectForm
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
            this.groupBoxHostJoin = new System.Windows.Forms.GroupBox();
            this.radioButtonJoin = new System.Windows.Forms.RadioButton();
            this.radioButtonHost = new System.Windows.Forms.RadioButton();
            this.labelJoin = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.buttonJoin = new System.Windows.Forms.Button();
            this.comboBoxHost = new System.Windows.Forms.ComboBox();
            this.buttonHost = new System.Windows.Forms.Button();
            this.labelHost = new System.Windows.Forms.Label();
            this.checkBoxLatinisch = new System.Windows.Forms.CheckBox();
            this.checkBoxKrittenimSpiel = new System.Windows.Forms.CheckBox();
            this.checkBoxGuadeundBese = new System.Windows.Forms.CheckBox();
            this.checkBoxWeliFarbe = new System.Windows.Forms.CheckBox();
            this.checkBoxKrittenUber = new System.Windows.Forms.CheckBox();
            this.groupBoxSpielEinstellungen = new System.Windows.Forms.GroupBox();
            this.labelJoinStatus = new System.Windows.Forms.Label();
            this.textBoxJoin = new System.Windows.Forms.TextBox();
            this.buttonStartNorm = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.comboBoxSetting = new System.Windows.Forms.ComboBox();
            this.groupBoxHostJoin.SuspendLayout();
            this.groupBoxSpielEinstellungen.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxHostJoin
            // 
            this.groupBoxHostJoin.Controls.Add(this.radioButtonJoin);
            this.groupBoxHostJoin.Controls.Add(this.radioButtonHost);
            this.groupBoxHostJoin.Enabled = false;
            this.groupBoxHostJoin.Location = new System.Drawing.Point(14, 23);
            this.groupBoxHostJoin.Name = "groupBoxHostJoin";
            this.groupBoxHostJoin.Size = new System.Drawing.Size(171, 71);
            this.groupBoxHostJoin.TabIndex = 1;
            this.groupBoxHostJoin.TabStop = false;
            this.groupBoxHostJoin.Text = "Spiel";
            this.groupBoxHostJoin.Visible = false;
            // 
            // radioButtonJoin
            // 
            this.radioButtonJoin.AutoSize = true;
            this.radioButtonJoin.Location = new System.Drawing.Point(16, 42);
            this.radioButtonJoin.Name = "radioButtonJoin";
            this.radioButtonJoin.Size = new System.Drawing.Size(56, 17);
            this.radioButtonJoin.TabIndex = 1;
            this.radioButtonJoin.TabStop = true;
            this.radioButtonJoin.Text = "Joinen";
            this.radioButtonJoin.UseVisualStyleBackColor = true;
            this.radioButtonJoin.CheckedChanged += new System.EventHandler(this.radioButtonCheckedChanged);
            // 
            // radioButtonHost
            // 
            this.radioButtonHost.AutoSize = true;
            this.radioButtonHost.Location = new System.Drawing.Point(16, 19);
            this.radioButtonHost.Name = "radioButtonHost";
            this.radioButtonHost.Size = new System.Drawing.Size(59, 17);
            this.radioButtonHost.TabIndex = 0;
            this.radioButtonHost.TabStop = true;
            this.radioButtonHost.Text = "Hosten";
            this.radioButtonHost.UseVisualStyleBackColor = true;
            this.radioButtonHost.CheckedChanged += new System.EventHandler(this.radioButtonCheckedChanged);
            // 
            // labelJoin
            // 
            this.labelJoin.AutoSize = true;
            this.labelJoin.Enabled = false;
            this.labelJoin.Location = new System.Drawing.Point(22, 97);
            this.labelJoin.Name = "labelJoin";
            this.labelJoin.Size = new System.Drawing.Size(64, 13);
            this.labelJoin.TabIndex = 2;
            this.labelJoin.Text = "Spiel Joinen";
            this.labelJoin.Visible = false;
            // 
            // labelIP
            // 
            this.labelIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIP.Location = new System.Drawing.Point(12, 370);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(310, 13);
            this.labelIP.TabIndex = 3;
            this.labelIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonJoin
            // 
            this.buttonJoin.Enabled = false;
            this.buttonJoin.Location = new System.Drawing.Point(196, 135);
            this.buttonJoin.Name = "buttonJoin";
            this.buttonJoin.Size = new System.Drawing.Size(126, 21);
            this.buttonJoin.TabIndex = 4;
            this.buttonJoin.Text = "Join";
            this.buttonJoin.UseVisualStyleBackColor = true;
            this.buttonJoin.Visible = false;
            this.buttonJoin.Click += new System.EventHandler(this.buttonJoin_Click);
            // 
            // comboBoxHost
            // 
            this.comboBoxHost.AllowDrop = true;
            this.comboBoxHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHost.Enabled = false;
            this.comboBoxHost.FormattingEnabled = true;
            this.comboBoxHost.Items.AddRange(new object[] {
            "2-Spieler",
            "4-Spieler"});
            this.comboBoxHost.Location = new System.Drawing.Point(14, 171);
            this.comboBoxHost.Name = "comboBoxHost";
            this.comboBoxHost.Size = new System.Drawing.Size(176, 21);
            this.comboBoxHost.TabIndex = 0;
            this.comboBoxHost.Visible = false;
            this.comboBoxHost.SelectedValueChanged += new System.EventHandler(this.comboBoxHost_SelectedValueChanged);
            // 
            // buttonHost
            // 
            this.buttonHost.Enabled = false;
            this.buttonHost.Location = new System.Drawing.Point(196, 170);
            this.buttonHost.Name = "buttonHost";
            this.buttonHost.Size = new System.Drawing.Size(126, 21);
            this.buttonHost.TabIndex = 2;
            this.buttonHost.Text = "Host";
            this.buttonHost.UseVisualStyleBackColor = true;
            this.buttonHost.Visible = false;
            this.buttonHost.Click += new System.EventHandler(this.buttonHost_Click);
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Enabled = false;
            this.labelHost.Location = new System.Drawing.Point(22, 154);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(67, 13);
            this.labelHost.TabIndex = 7;
            this.labelHost.Text = "Spiel Hosten";
            this.labelHost.Visible = false;
            // 
            // checkBoxLatinisch
            // 
            this.checkBoxLatinisch.Checked = true;
            this.checkBoxLatinisch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLatinisch.Enabled = false;
            this.checkBoxLatinisch.Location = new System.Drawing.Point(196, 64);
            this.checkBoxLatinisch.Name = "checkBoxLatinisch";
            this.checkBoxLatinisch.Size = new System.Drawing.Size(130, 18);
            this.checkBoxLatinisch.TabIndex = 8;
            this.checkBoxLatinisch.Text = "Latinisch";
            this.checkBoxLatinisch.UseVisualStyleBackColor = true;
            this.checkBoxLatinisch.Visible = false;
            this.checkBoxLatinisch.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxKrittenimSpiel
            // 
            this.checkBoxKrittenimSpiel.Location = new System.Drawing.Point(6, 20);
            this.checkBoxKrittenimSpiel.Name = "checkBoxKrittenimSpiel";
            this.checkBoxKrittenimSpiel.Size = new System.Drawing.Size(159, 18);
            this.checkBoxKrittenimSpiel.TabIndex = 9;
            this.checkBoxKrittenimSpiel.Text = "Kritten im Spiel";
            this.checkBoxKrittenimSpiel.UseVisualStyleBackColor = true;
            this.checkBoxKrittenimSpiel.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxGuadeundBese
            // 
            this.checkBoxGuadeundBese.Location = new System.Drawing.Point(6, 40);
            this.checkBoxGuadeundBese.Name = "checkBoxGuadeundBese";
            this.checkBoxGuadeundBese.Size = new System.Drawing.Size(159, 18);
            this.checkBoxGuadeundBese.TabIndex = 10;
            this.checkBoxGuadeundBese.Text = "Guade und Bese";
            this.checkBoxGuadeundBese.UseVisualStyleBackColor = true;
            this.checkBoxGuadeundBese.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxWeliFarbe
            // 
            this.checkBoxWeliFarbe.Location = new System.Drawing.Point(6, 60);
            this.checkBoxWeliFarbe.Name = "checkBoxWeliFarbe";
            this.checkBoxWeliFarbe.Size = new System.Drawing.Size(159, 18);
            this.checkBoxWeliFarbe.TabIndex = 11;
            this.checkBoxWeliFarbe.Text = "Weli ist jede Farbe";
            this.checkBoxWeliFarbe.UseVisualStyleBackColor = true;
            this.checkBoxWeliFarbe.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxKrittenUber
            // 
            this.checkBoxKrittenUber.Location = new System.Drawing.Point(6, 80);
            this.checkBoxKrittenUber.Name = "checkBoxKrittenUber";
            this.checkBoxKrittenUber.Size = new System.Drawing.Size(159, 21);
            this.checkBoxKrittenUber.TabIndex = 12;
            this.checkBoxKrittenUber.Text = "Kritten Überspringen";
            this.checkBoxKrittenUber.UseVisualStyleBackColor = true;
            this.checkBoxKrittenUber.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // groupBoxSpielEinstellungen
            // 
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxKrittenUber);
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxKrittenimSpiel);
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxWeliFarbe);
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxGuadeundBese);
            this.groupBoxSpielEinstellungen.Location = new System.Drawing.Point(14, 12);
            this.groupBoxSpielEinstellungen.Name = "groupBoxSpielEinstellungen";
            this.groupBoxSpielEinstellungen.Size = new System.Drawing.Size(174, 109);
            this.groupBoxSpielEinstellungen.TabIndex = 13;
            this.groupBoxSpielEinstellungen.TabStop = false;
            this.groupBoxSpielEinstellungen.Text = "Spiel Einstellungen";
            // 
            // labelJoinStatus
            // 
            this.labelJoinStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJoinStatus.Location = new System.Drawing.Point(194, 42);
            this.labelJoinStatus.Name = "labelJoinStatus";
            this.labelJoinStatus.Size = new System.Drawing.Size(128, 28);
            this.labelJoinStatus.TabIndex = 14;
            this.labelJoinStatus.Text = "                      ";
            // 
            // textBoxJoin
            // 
            this.textBoxJoin.Enabled = false;
            this.textBoxJoin.Location = new System.Drawing.Point(15, 113);
            this.textBoxJoin.MaxLength = 15;
            this.textBoxJoin.Name = "textBoxJoin";
            this.textBoxJoin.Size = new System.Drawing.Size(176, 20);
            this.textBoxJoin.TabIndex = 2;
            this.textBoxJoin.Visible = false;
            // 
            // buttonStartNorm
            // 
            this.buttonStartNorm.Location = new System.Drawing.Point(196, 100);
            this.buttonStartNorm.Name = "buttonStartNorm";
            this.buttonStartNorm.Size = new System.Drawing.Size(126, 21);
            this.buttonStartNorm.TabIndex = 15;
            this.buttonStartNorm.Text = "Start";
            this.buttonStartNorm.UseVisualStyleBackColor = true;
            this.buttonStartNorm.Click += new System.EventHandler(this.buttonNetwork_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(196, 113);
            this.textBoxPassword.MaxLength = 15;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(126, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.Visible = false;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Enabled = false;
            this.labelPassword.Location = new System.Drawing.Point(193, 97);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 17;
            this.labelPassword.Text = "Password";
            this.labelPassword.Visible = false;
            // 
            // comboBoxSetting
            // 
            this.comboBoxSetting.AllowDrop = true;
            this.comboBoxSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSetting.FormattingEnabled = true;
            this.comboBoxSetting.Items.AddRange(new object[] {
            "2-Spieler Leicht",
            "4-Spieler Leicht",
            "2-Spieler Mittel",
            "4-Spieler Mittel",
            "2-Spieler Lokal",
            "4-Spieler Lokal"});
            this.comboBoxSetting.Location = new System.Drawing.Point(196, 73);
            this.comboBoxSetting.Name = "comboBoxSetting";
            this.comboBoxSetting.Size = new System.Drawing.Size(126, 21);
            this.comboBoxSetting.TabIndex = 18;
            // 
            // connectForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 126);
            this.Controls.Add(this.checkBoxLatinisch);
            this.Controls.Add(this.comboBoxSetting);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.buttonStartNorm);
            this.Controls.Add(this.textBoxJoin);
            this.Controls.Add(this.labelJoinStatus);
            this.Controls.Add(this.groupBoxSpielEinstellungen);
            this.Controls.Add(this.labelHost);
            this.Controls.Add(this.buttonHost);
            this.Controls.Add(this.comboBoxHost);
            this.Controls.Add(this.buttonJoin);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.labelJoin);
            this.Controls.Add(this.groupBoxHostJoin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "connectForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wattn PC - Multiplayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.connectForm_FormClosing);
            this.Load += new System.EventHandler(this.connectForm_Load);
            this.groupBoxHostJoin.ResumeLayout(false);
            this.groupBoxHostJoin.PerformLayout();
            this.groupBoxSpielEinstellungen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxHostJoin;
        private System.Windows.Forms.RadioButton radioButtonJoin;
        private System.Windows.Forms.RadioButton radioButtonHost;
        private System.Windows.Forms.Label labelJoin;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Button buttonJoin;
        private System.Windows.Forms.ComboBox comboBoxHost;
        private System.Windows.Forms.Button buttonHost;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.CheckBox checkBoxLatinisch;
        private System.Windows.Forms.CheckBox checkBoxKrittenimSpiel;
        private System.Windows.Forms.CheckBox checkBoxGuadeundBese;
        private System.Windows.Forms.CheckBox checkBoxWeliFarbe;
        private System.Windows.Forms.CheckBox checkBoxKrittenUber;
        private System.Windows.Forms.GroupBox groupBoxSpielEinstellungen;
        private System.Windows.Forms.Label labelJoinStatus;
        private System.Windows.Forms.TextBox textBoxJoin;
        private System.Windows.Forms.Button buttonStartNorm;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.ComboBox comboBoxSetting;
    }
}