namespace WattnPC
{
    partial class settForm
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
            this.comboBoxDiff = new System.Windows.Forms.ComboBox();
            this.labelDiff = new System.Windows.Forms.Label();
            this.checkBoxLatinisch = new System.Windows.Forms.CheckBox();
            this.checkBoxKrittenimSpiel = new System.Windows.Forms.CheckBox();
            this.checkBoxGuadeundBese = new System.Windows.Forms.CheckBox();
            this.checkBoxWeliFarbe = new System.Windows.Forms.CheckBox();
            this.checkBoxKrittenUber = new System.Windows.Forms.CheckBox();
            this.groupBoxSpielEinstellungen = new System.Windows.Forms.GroupBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBoxSpielEinstellungen.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxDiff
            // 
            this.comboBoxDiff.AllowDrop = true;
            this.comboBoxDiff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiff.FormattingEnabled = true;
            this.comboBoxDiff.Items.AddRange(new object[] {
            "Sandbox",
            "Single Player Leicht",
            "Single Player Hart",
            "2-Spieler Lokal",
            "4-Spieler Lokal"});
            this.comboBoxDiff.Location = new System.Drawing.Point(12, 26);
            this.comboBoxDiff.Name = "comboBoxDiff";
            this.comboBoxDiff.Size = new System.Drawing.Size(171, 21);
            this.comboBoxDiff.TabIndex = 0;
            this.comboBoxDiff.SelectedValueChanged += new System.EventHandler(this.comboBoxHost_SelectedValueChanged);
            // 
            // labelDiff
            // 
            this.labelDiff.AutoSize = true;
            this.labelDiff.BackColor = System.Drawing.SystemColors.Control;
            this.labelDiff.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDiff.Location = new System.Drawing.Point(9, 10);
            this.labelDiff.Name = "labelDiff";
            this.labelDiff.Size = new System.Drawing.Size(58, 13);
            this.labelDiff.TabIndex = 7;
            this.labelDiff.Text = "Einstellung";
            // 
            // checkBoxLatinisch
            // 
            this.checkBoxLatinisch.Location = new System.Drawing.Point(6, 19);
            this.checkBoxLatinisch.Name = "checkBoxLatinisch";
            this.checkBoxLatinisch.Size = new System.Drawing.Size(159, 18);
            this.checkBoxLatinisch.TabIndex = 8;
            this.checkBoxLatinisch.Text = "Latinisch";
            this.checkBoxLatinisch.UseVisualStyleBackColor = true;
            this.checkBoxLatinisch.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxKrittenimSpiel
            // 
            this.checkBoxKrittenimSpiel.Location = new System.Drawing.Point(6, 38);
            this.checkBoxKrittenimSpiel.Name = "checkBoxKrittenimSpiel";
            this.checkBoxKrittenimSpiel.Size = new System.Drawing.Size(159, 18);
            this.checkBoxKrittenimSpiel.TabIndex = 9;
            this.checkBoxKrittenimSpiel.Text = "Kritten im Spiel";
            this.checkBoxKrittenimSpiel.UseVisualStyleBackColor = true;
            this.checkBoxKrittenimSpiel.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxGuadeundBese
            // 
            this.checkBoxGuadeundBese.Location = new System.Drawing.Point(6, 57);
            this.checkBoxGuadeundBese.Name = "checkBoxGuadeundBese";
            this.checkBoxGuadeundBese.Size = new System.Drawing.Size(159, 18);
            this.checkBoxGuadeundBese.TabIndex = 10;
            this.checkBoxGuadeundBese.Text = "Guade und Bese";
            this.checkBoxGuadeundBese.UseVisualStyleBackColor = true;
            this.checkBoxGuadeundBese.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxWeliFarbe
            // 
            this.checkBoxWeliFarbe.Location = new System.Drawing.Point(6, 76);
            this.checkBoxWeliFarbe.Name = "checkBoxWeliFarbe";
            this.checkBoxWeliFarbe.Size = new System.Drawing.Size(159, 18);
            this.checkBoxWeliFarbe.TabIndex = 11;
            this.checkBoxWeliFarbe.Text = "Weli ist jede Farbe";
            this.checkBoxWeliFarbe.UseVisualStyleBackColor = true;
            this.checkBoxWeliFarbe.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // checkBoxKrittenUber
            // 
            this.checkBoxKrittenUber.Location = new System.Drawing.Point(6, 93);
            this.checkBoxKrittenUber.Name = "checkBoxKrittenUber";
            this.checkBoxKrittenUber.Size = new System.Drawing.Size(159, 21);
            this.checkBoxKrittenUber.TabIndex = 12;
            this.checkBoxKrittenUber.Text = "Kritten Überspringen";
            this.checkBoxKrittenUber.UseVisualStyleBackColor = true;
            this.checkBoxKrittenUber.CheckedChanged += new System.EventHandler(this.checkBoxEinstellungenChanged);
            // 
            // groupBoxSpielEinstellungen
            // 
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxLatinisch);
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxKrittenUber);
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxKrittenimSpiel);
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxWeliFarbe);
            this.groupBoxSpielEinstellungen.Controls.Add(this.checkBoxGuadeundBese);
            this.groupBoxSpielEinstellungen.Location = new System.Drawing.Point(12, 65);
            this.groupBoxSpielEinstellungen.Name = "groupBoxSpielEinstellungen";
            this.groupBoxSpielEinstellungen.Size = new System.Drawing.Size(171, 117);
            this.groupBoxSpielEinstellungen.TabIndex = 13;
            this.groupBoxSpielEinstellungen.TabStop = false;
            this.groupBoxSpielEinstellungen.Text = "Spiel Einstellungen";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 198);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(165, 37);
            this.buttonStart.TabIndex = 14;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // settForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 245);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxSpielEinstellungen);
            this.Controls.Add(this.labelDiff);
            this.Controls.Add(this.comboBoxDiff);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "settForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wattn PC - Multiplayer";
            this.Load += new System.EventHandler(this.connectForm_Load);
            this.groupBoxSpielEinstellungen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxDiff;
        private System.Windows.Forms.Label labelDiff;
        private System.Windows.Forms.CheckBox checkBoxLatinisch;
        private System.Windows.Forms.CheckBox checkBoxKrittenimSpiel;
        private System.Windows.Forms.CheckBox checkBoxGuadeundBese;
        private System.Windows.Forms.CheckBox checkBoxWeliFarbe;
        private System.Windows.Forms.CheckBox checkBoxKrittenUber;
        private System.Windows.Forms.GroupBox groupBoxSpielEinstellungen;
        private System.Windows.Forms.Button buttonStart;
    }
}