using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace WattnPC
{
    public partial class hostForm : Form
    {
        public hostForm()
        {
            InitializeComponent();
        }

        public byte Spieleranzahl { get; set; }
        public string MyIP { get; set; }
        public string Spieler2IP, Spieler3IP, Spieler4IP;
        public string Password;

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (Spieleranzahl == 4)//check for duplicate ips and if the players are reachable
            {
                if (textBoxSpieler2IP.Text == textBoxSpieler3IP.Text || textBoxSpieler2IP.Text == textBoxSpieler4IP.Text || textBoxSpieler3IP.Text == textBoxSpieler4IP.Text)
                 {
                     labelError.Visible = true;
                     labelError.Text = "Kann nicht zweimal die gleiche IP verwenden.";
                     return;
                 }
             }

            if(textBoxSpieler2IP.Text==MyIP|| textBoxSpieler2IP.Text == MyIP || textBoxSpieler2IP.Text == MyIP)
            {
                labelError.Visible = true;
                labelError.Text = "Kann nicht zu meiner eigenen IP verbinden!";
                return;
            }

            try
            {
                IPAddress.Parse(textBoxSpieler2IP.Text);
                if (Spieleranzahl == 4)
                {
                    IPAddress.Parse(textBoxSpieler3IP.Text);
                    IPAddress.Parse(textBoxSpieler4IP.Text);
                }
            }
            catch
            {
                labelError.Visible = true;
                labelError.Text = "IP Addresse ungültig!";
                return;
            }

                //if sending data to all players is possible
                if ((Send(textBoxSpieler2IP.Text, "Ping") && Spieleranzahl == 2) || (Spieleranzahl == 4 && Send(textBoxSpieler2IP.Text, "Ping") && Send(textBoxSpieler3IP.Text, "Ping") && Send(textBoxSpieler4IP.Text, "Ping")))
                    {
                        Password = textBoxPassword.Text;
                        Spieler2IP = textBoxSpieler2IP.Text;
                        Send(Spieler2IP, "GO"+Spieleranzahl+"+"+Password);

                    if (Spieleranzahl == 4)
                    {
                        Spieler3IP = textBoxSpieler3IP.Text;
                        Spieler4IP = textBoxSpieler4IP.Text;
                        Send(Spieler3IP, "GO" + Spieleranzahl + "+" + Password);
                        Send(Spieler4IP, "GO" + Spieleranzahl + "+" + Password);
                    }

                        Thread.Sleep(2000);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                }

                else
                {
                    labelError.Visible = true;
                    labelError.Text = "Konnte verbindung nicht herstellen!";
                }
        }

        #region Controlls enable und disable

        private void hostForm_Load(object sender, EventArgs e)
        {
            if(Spieleranzahl == 2) { textBoxSpieler3IP.Enabled = false; textBoxSpieler4IP.Enabled = false; } //only two ip textboxes enabled
            textBoxSpielerIP.Text = MyIP;
        }

        #endregion

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
        } //for normal sending
    }
}
