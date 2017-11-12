using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WattnPC
{
    class wattKarte
    {
        public wattKarte(string farbe, string zahl, Image kartenimage)
        {
            Farbe = farbe;
            Zahl = zahl;
            Kartenimage = kartenimage;
        }

        public wattKarte() //für die angesagte Karte
        {

        }

        public string Farbe { get; set; }
        public string Zahl { get; set; }
        public Image Kartenimage { get; set; }

        public int GetCardNumber(List<wattKarte> wattKarten)
        {
            return wattKarten.IndexOf(this);
        }
    }
}
