using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace WattnPC
{
    public partial class connectForm : Form
    {
        public connectForm()
        {
            InitializeComponent();
        }

        public bool Kritten, Guadebese, Krittenuberspringen, Latinisch, WeliFarbe; //settings are made by the host and send to the player
        public byte Spieleranzahl;
        public string Spielmodi;
        /*
        public string MyIP, DataRecieved;
        public string Spieler2IP, Spieler3IP, Spieler4IP;
        public string HostIP;
        public string Password;
        bool searching = true; NET
        */

        #region Form Load

        private void connectForm_Load(object sender, EventArgs e)
        {
            /*
            radioButtonJoin.Checked = true;

            try
            {
                IPAddress.Parse(MyIP);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                labelIP.ForeColor = Color.Red;
                groupBoxHostJoin.Enabled = false;
                buttonJoin.Enabled = false;             
            }
            
            labelIP.Text = "IP: " + MyIP; NET
            */
        }

        #endregion

        #region Controlls enablen und disablen 

        private void comboBoxHost_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        //Elements are enabled/disabled depending on the option that is selected
        private void radioButtonCheckedChanged(object sender, EventArgs e)
        { 
            /*
            if (radioButtonHost.Checked)
            {
                labelHost.Font = new Font(labelHost.Font, FontStyle.Bold); 
                labelJoin.Font = new Font(labelJoin.Font, FontStyle.Regular);

                //join elemente disabled
                buttonJoin.Enabled = false;
                textBoxJoin.Enabled = false;
                textBoxPassword.Enabled = false;

                //host elemente enabled
                comboBoxHost.Enabled = true;
                groupBoxSpielEinstellungen.Enabled = true;

                checkBoxKrittenUber.Enabled = false;
            }
            else if(radioButtonJoin.Checked)
            {
                labelJoin.Font = new Font(labelJoin.Font, FontStyle.Bold);
                labelHost.Font = new Font(labelHost.Font, FontStyle.Regular);

                //join elemente enabled
                textBoxJoin.Enabled = true;
                buttonJoin.Enabled = true;
                textBoxPassword.Enabled = true;

                //host elemente edisabled
                comboBoxHost.Enabled = false;
                groupBoxSpielEinstellungen.Enabled = false;
                buttonHost.Enabled = false;
            }
            */
        }

        private void checkBoxEinstellungenChanged(object sender, EventArgs e) //some modifications only work when others are off
        {
            if (checkBoxKrittenimSpiel.Checked)
            { 
                checkBoxWeliFarbe.Enabled = false;
                checkBoxWeliFarbe.Checked = false;
                checkBoxKrittenUber.Enabled = true;
            }
            else if (checkBoxWeliFarbe.Checked)
            { 
                checkBoxKrittenimSpiel.Enabled = false;
                checkBoxKrittenUber.Enabled = false;
                checkBoxKrittenimSpiel.Checked = false;
                checkBoxKrittenUber.Checked = false;
            }

            if (checkBoxKrittenimSpiel.Checked == false && checkBoxWeliFarbe.Checked == false)
            {
                checkBoxKrittenimSpiel.Enabled = true;
                checkBoxWeliFarbe.Enabled = true;
                checkBoxKrittenUber.Enabled = false;
                checkBoxKrittenUber.Checked = false;
            }

        }

        #endregion

        #region Join und Host (NET UNAKTIV)

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            /*
            if(buttonJoin.Text == "Abbrechen")
            {                
                buttonJoin.Text = "Join";
                labelJoinStatus.Text = "";
                searching = false;
            }
            else
            {
                searching = true;             
                buttonJoin.Text = "Abbrechen";
                Password = textBoxPassword.Text;

                try
                { 
                    IPAddress.Parse(textBoxJoin.Text); //checks if ip is uasable
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    buttonJoin.Text = "Join";
                    labelJoinStatus.Text = "IP falsch";
                    labelJoinStatus.ForeColor = Color.Red;
                    await Task.Delay(2000);
                    labelJoinStatus.ForeColor = Color.Black;
                    labelJoinStatus.Text = "";
                    return;
                }

                HostIP = textBoxJoin.Text;

                //go2 if 2 player, 4 if 4 player
                while (searching && (DataRecieved != "GO2+" + Password && DataRecieved != "GO4+"+Password))
                {
                    await Task.Delay(10);
                    Console.Write(DataRecieved);
                }
                if(DataRecieved == "GO2+"+Password)
                {
                    Spieleranzahl = 2;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if(DataRecieved == "GO4+" + Password)
                {
                    Spieleranzahl = 4;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }NET
            */
        }

        private void buttonHost_Click(object sender, EventArgs e)
        {
            /*
            hostForm hf = new hostForm();
            hf.Spieleranzahl = Convert.ToByte(comboBoxHost.SelectedItem.ToString().Substring(0, 1)); //da 2-Spieler und 4-Spieler nehme ich den ersten character 2 oder 4 und mache ein byte draus          
            hf.MyIP = this.MyIP;
            hf.ShowDialog();
            if (hf.DialogResult == DialogResult.OK)
            {
                this.Spieler2IP = hf.Spieler2IP;
                this.Spieler3IP = hf.Spieler3IP;
                this.Spieler4IP = hf.Spieler4IP;
                this.Password = hf.Password;
                Kritten = checkBoxKrittenimSpiel.Checked;
                Guadebese = checkBoxGuadeundBese.Checked;
                Krittenuberspringen = checkBoxKrittenUber.Checked;
                Latinisch = checkBoxLatinisch.Checked;
                WeliFarbe = checkBoxWeliFarbe.Checked;
                HostIP = MyIP;
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            hf.Dispose(); NET
            */
        }


        #endregion

        private async void buttonNetwork_Click(object sender, EventArgs e) //play without network, standard atm
        {
            if (comboBoxSetting.SelectedItem == null)
            {
                labelJoinStatus.Visible = true;
                labelJoinStatus.Text = "Bitte Spielmodus auswählen!";
                await Task.Delay(2000);
                labelJoinStatus.Visible = false;
                return;
            }

            Latinisch = checkBoxLatinisch.Checked;
            Kritten = checkBoxKrittenimSpiel.Checked;
            Guadebese = checkBoxGuadeundBese.Checked;
            WeliFarbe = checkBoxWeliFarbe.Checked;
            Krittenuberspringen = checkBoxKrittenUber.Checked;

            Spielmodi = comboBoxSetting.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void connectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel) { Environment.Exit(0); } //if form is closed with the red X, just close the program
        }

    /*
    private bool Send(string hostip, string text)
    {

        TcpClient tcpclnt = new TcpClient();
        Stream stm;
        byte[] ba = Encoding.UTF8.GetBytes(text);

        if (hostip == MyIP) //can't connect to own ip lmo
            return false;

        try
        {
            tcpclnt.Connect(hostip, 7777);
        }
        catch
        {
            Console.WriteLine("NTW: Connect Failure");
            return false;
        }

        try
        {
            stm = tcpclnt.GetStream();
        }
        catch
        {
            Console.WriteLine("NTW: GetStream Failure");
            return false;
        }

        try
        {
            stm.Write(ba, 0, ba.Length);
        }
        catch
        {
            Console.WriteLine("NTW: Write Failure");
            return false;
        }

        return true; //if everything worked return true 
} //for normal sending NET
*/
}
}
