using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WattnPC
{
    class wattSpieler
    {
        int stichanzahl, score;
        byte difficulty; //0 difficulty means human
        string stichdoku; //behinhaltet die dokumentierung zb schell könig > /HERZ KÖNIG/ > herz sau > laub 7
        bool spielerturn = false; //ob der spieler dran ist
        bool spieleransagen = false; //ob der spieler ansagen muss, 1 mit 3, 3 mit 2, 2 mit 4, 4 mit 1
        List<wattKarte> spielerwattkarten = new List<wattKarte>();

        public wattSpieler(byte playernumber, string playername, byte difficulty)
        {
            PlayerNumber = playernumber;
            PlayerName = playername;
            Difficulty = difficulty;
        }

        public byte Difficulty { get { return difficulty; } set { if (value < 4) difficulty = value; else difficulty = 0; } }
        public bool Spieleransagen { get { return spieleransagen; } set { spieleransagen = value; } }
        public bool Spielerturn { get { return spielerturn; } set { spielerturn = value; } }
        public string Stichdoku { get { return stichdoku; } set { stichdoku = value; } }
        public string PlayerName { get; }
        public byte PlayerNumber { get; }
        public string PlayerIP { get; set; }
        public List<wattKarte> SpielerWattKarten { get { return spielerwattkarten; } set { if (spielerwattkarten.Count >= 5) Console.WriteLine("SpielerWattKarten FEHLER Spieler: " + PlayerNumber); else spielerwattkarten = value; } }
        public byte Kartenanzahl { get { return Convert.ToByte(spielerwattkarten.Count()); } }
        public int StichAnzahl { get { return stichanzahl; } set { if (value < 0 || value > 3) { Console.WriteLine("Stichanzahl FEHLER  Spieler: " + PlayerNumber); stichanzahl = 0; } else stichanzahl = value; } } //same
        public int Score { get { return score; } set { if (value > 11) score = 11; else if(value<0) { Console.WriteLine("Score FEHLER  Spieler: " + PlayerNumber); score = 0; } else score = value; } }
        }
}
