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
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

namespace WattnPC
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        #region wattListen, Images, main Bitmap, tooltip, myplayernumber und mousedown

        List<wattSpieler> wattMitspieler = new List<wattSpieler>(); //Contains all the Players Infos - needed everywhere
        List<Image> kartenAnzahlimages = new List<Image>(); //Contains the Images for the enemy player cards
        List<PictureBox> wattMeineKartenBoxen = new List<PictureBox>(); //Contains the PictureBoxes that display my cards
        List<wattKarte> wattKarten = new List<wattKarte>(); //Contains all cards that are in the game 
        List<PictureBox> wattMainKartenBoxen = new List<PictureBox>(); //Contains the Pictureboxes in the Middle

        Bitmap kartenBitmap = new Bitmap(Properties.Resources.ALLIN1WATTN); //Converts the Image that contains all cards to a bitmap
        Image midKarte = Properties.Resources.MidKarte, midSideKarte = Properties.Resources.MidKarteFat; //When there are no cards in the middle these are used, 2 Different ones because the smaller PicBoxes have display errors if same image
        ToolTip tt = new ToolTip(); //Used to display the Stiche other players

        bool mousedown, kartenreset, ansagen, sandbox, loadingcomplete = false; //Mousedown is used in MouseDown and MouseUp, kartenreset to reset cards, loadingcomplete for waiting in form load
        byte myplayernumber, dieserundescore; //get myplayernumber in the beginning, used to check what to display 
                                              //string myip, hostip; //the clients IP and host IP
                                              //string conPW; //password for connections

        #endregion

        #region Mainform Load and Exit

        private async void mainForm_Load(object sender, EventArgs e)
        {

            #region Connection Form Settings, playernumber set here (INACTIVE)
            /*         

            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())//if network is available then list IP on the bottom of the form and make joining possible
            {
                if (GetLocalIPv4(NetworkInterfaceType.Ethernet) != "") //prefer ethernet to using wireless 
                { myip = GetLocalIPv4(NetworkInterfaceType.Ethernet); var t = Task.Factory.StartNew(() => Listen(myip)); } //starts the listen task if is able to get own IP
                else if (GetLocalIPv4(NetworkInterfaceType.Wireless80211) != "")
                { myip = GetLocalIPv4(NetworkInterfaceType.Wireless80211); var t = Task.Factory.StartNew(() => Listen(myip)); }
                else
                {
                    myip = "IP nicht aufrufbar!";
                }
            }
            else
            {
                myip = "Kein Netzwerk verfügbar!";
            }

            cf.MyIP = myip;
			NET           
			*/
            #endregion

            connectForm cf = new connectForm();
            cf.ShowDialog();
            cf.Dispose();

            #region Player NET (INACTIVE)
            /*
            if (cf.DialogResult == DialogResult.OK) //OK is you are a player
            {
                hostip = cf.HostIP;
                conPW = cf.Password;

                while((!dataRecieved.Contains("1") && !dataRecieved.Contains("0")) || !dataRecieved.Contains(conPW)) //wait till a message with host ip is sent
                {
                    await Task.Delay(10);
                    Console.Write(dataRecieved);
                }
                string tempdata = dataRecieved; //get the mods, if 1 then true 0 then false
                latinisch = tempdata[0].ToString()=="1";
                kritten = tempdata[1].ToString() == "1";
                guadebese = tempdata[2].ToString() == "1";
                welifarbe = tempdata[3].ToString() == "1";
                krittenuber = tempdata[4].ToString() == "1";

                //get my playernumber
                while ((!dataRecieved.Contains("2") && !dataRecieved.Contains("3") && !dataRecieved.Contains("4")) || !dataRecieved.Contains(conPW)) //wait till a message with host ip is sent
                {
                    await Task.Delay(10);
                    Console.Write(dataRecieved);
                }
                myplayernumber = Convert.ToByte(dataRecieved.Substring(0, 1)); //only first char 

                //a player doesn't need to know other players IP
                wattMitspieler.Add(new wattSpieler(1, "Spieler 1", hostip));
                wattMitspieler.Add(new wattSpieler(2, "Spieler 2", "IP2"));

                if(cf.Spieleranzahl == 4)
                { 
                wattMitspieler.Add(new wattSpieler(3, "Spieler 3", "IP3"));
                wattMitspieler.Add(new wattSpieler(4, "Spieler 4", "IP4"));
                }

                wattMitspieler[myplayernumber - 1].PlayerIP = myip; //sets my own ip

            }
			*/
            #endregion

            #region Host NET (INACTIVE)
            /*
            else if (cf.DialogResult == DialogResult.Yes)//YES is host
            {
                hostip = myip;
                conPW = cf.Password;
                myplayernumber = 1; //host is always 1

                Send(cf.Spieler2IP, "Pong"); //this needs to be done else the next message won't be sent
                Send(cf.Spieler2IP, Convert.ToInt32(cf.Latinisch)+""+ Convert.ToInt32(cf.Kritten) + "" + 
                    Convert.ToInt32(cf.Guadebese) + "" + Convert.ToInt32(cf.WeliFarbe) + "" + Convert.ToInt32(cf.Krittenuberspringen) + "+" +
                    conPW);
                //send 0 and 1s (true and false) and the password
                wattMitspieler.Add(new wattSpieler(1, "Spieler 1", myip));
                wattMitspieler.Add(new wattSpieler(2, "Spieler 2", cf.Spieler2IP));

                if (cf.Spieleranzahl == 4)
                {
                    Send(cf.Spieler3IP, Convert.ToInt32(cf.Latinisch) + "" + Convert.ToInt32(cf.Kritten) + "" +
                    Convert.ToInt32(cf.Guadebese) + "" + Convert.ToInt32(cf.WeliFarbe) + "" + Convert.ToInt32(cf.Krittenuberspringen) + "+" +
                    conPW);
                    Send(cf.Spieler4IP, Convert.ToInt32(cf.Latinisch) + "" + Convert.ToInt32(cf.Kritten) + "" +
                    Convert.ToInt32(cf.Guadebese) + "" + Convert.ToInt32(cf.WeliFarbe) + "" + Convert.ToInt32(cf.Krittenuberspringen) + "+" +
                    conPW);

                    wattMitspieler.Add(new wattSpieler(3, "Spieler 3", cf.Spieler3IP));
                    wattMitspieler.Add(new wattSpieler(4, "Spieler 4", cf.Spieler4IP));

                    await Task.Delay(1500); //waits till sends out next message
                    Send(wattMitspieler[1].PlayerIP, "2+" + conPW);
                    Send(wattMitspieler[2].PlayerIP, "3+" + conPW);
                    Send(wattMitspieler[3].PlayerIP, "4+" + conPW);
                }
                else
                { 
                await Task.Delay(1500); //waits till sends out next message
                Send(wattMitspieler[1].PlayerIP, "2+"+ conPW);
                }
            }

            

            else if(cf.DialogResult == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }
			*/
            #endregion

            #region Game mode settings (2-4, cpu, human)

            sandbox = false; //for dev trying out
            myplayernumber = 1;
            labelInfo.Visible = true;
            labelInfo.Text = cf.Spielmodi;

            if (cf.Spielmodi.Contains("Leicht") || cf.Spielmodi.Contains("Mittel"))
            {
                byte diff = 0;
                if (cf.Spielmodi.Contains("Leicht"))
                    diff = 1;
                else if (cf.Spielmodi.Contains("Mittel"))
                    diff = 2;

                wattMitspieler.Add(new wattSpieler(1, "Spieler 1", 0));
                wattMitspieler.Add(new wattSpieler(2, "CPU 2", diff));

                if (cf.Spielmodi.Contains("4-Spieler"))
                {
                    wattMitspieler.Add(new wattSpieler(3, "CPU 3", diff));
                    wattMitspieler.Add(new wattSpieler(4, "CPU 4", diff));
                }
            }
            else
            {
                wattMitspieler.Add(new wattSpieler(1, "Spieler 1", 0));
                wattMitspieler.Add(new wattSpieler(2, "Spieler 2", 0));
              
                if (cf.Spielmodi.Contains("4-Spieler")) //if there is 4 player add 2 more              
                {
                    wattMitspieler.Add(new wattSpieler(3, "Spieler 3", 0));
                    wattMitspieler.Add(new wattSpieler(4, "Spieler 4", 0));
                }
            }
             
            //2 player only 1 spieleransagen is true
            if (wattMitspieler.Count == 2)
            {
                wattMitspieler[0].Spieleransagen = true;
            }

            else if (wattMitspieler.Count == 4) //4 player, 2 spieleransagen is true
            {
                wattMitspieler[0].Spieleransagen = wattMitspieler[2].Spieleransagen = true;
            }

            #endregion

            #region Karten und PicBoxes meinekartenboxes Laden

            string[] farben = new string[4] { "Herz", "Laub", "Eichel", "Schell" };
            string[] zahlen = new string[8] { "Neun", "Acht", "Sieben", "Sau", "König", "Ober", "Unter", "Zehn" };

            //Meassured in Gimp a card is about: height = 550px, width = 330 , starting at 15px to reduce white zone
            int kartex = 15, kartey = 590;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    wattKarten.Add(new wattKarte(farben[x], zahlen[y], LoadKartenImage(kartex, kartey, 360, 580))); //Add a new card with X Farbe and Y Zahl and image with +30px white zone

                    pictureBoxMeineKarte1.Image = wattKarten[wattKarten.Count() - 1].Kartenimage;

                    kartex += 380; //Adds another 380 px for the next card
                    if (kartex == 380 * 4 + 15) //IF the end of one row has been reached, another row will be initiated
                    {
                        kartey += 590;
                        kartex = 15;
                    }
                }
            }

            wattMeineKartenBoxen.Add(pictureBoxMeineKarte1); //Fill wattMeineKartenBoxen with the Pictureboxes that contain my CardImages
            wattMeineKartenBoxen.Add(pictureBoxMeineKarte2);
            wattMeineKartenBoxen.Add(pictureBoxMeineKarte3);
            wattMeineKartenBoxen.Add(pictureBoxMeineKarte4);
            wattMeineKartenBoxen.Add(pictureBoxMeineKarte5);

            #endregion

            #region Size and Player Placement 

            PosSizeAnpassung();
            ExecuteSecure(() => ResetMitteKarten());

            if (wattMitspieler.Count() == 2) //When there are only 2 Players several PictureBoxes are disabled because they are not needed
            {
                pictureBoxSpielerLinks.Enabled = false;
                pictureBoxSpielerRechts.Enabled = false;

                pictureBoxMainKarte2.Enabled = false;
                pictureBoxMainKarte1.Enabled = false;

                pictureBoxSpielerLinks.Visible = false;
                pictureBoxSpielerRechts.Visible = false;

                pictureBoxMainKarte2.Visible = false;
                pictureBoxMainKarte1.Visible = false;

                wattMainKartenBoxen.Add(pictureBoxMainKarte);
                wattMainKartenBoxen.Add(pictureBoxMainKarte3);
            }
            else //When there are 4 Players all Elements are activated
            {
                wattMainKartenBoxen.Add(pictureBoxMainKarte);
                wattMainKartenBoxen.Add(pictureBoxMainKarte3);
                wattMainKartenBoxen.Add(pictureBoxMainKarte2);
                wattMainKartenBoxen.Add(pictureBoxMainKarte1);
            }

            switch (myplayernumber) //Sets the names of the other players pictureboxes, later used to define which stichanzahl to get
            {
                case 1: //when I am player 1, player 3 is left, player 4 is right, player 2 is above
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 3";
                        pictureBoxSpielerRechts.Name = "Spieler 4";
                        pictureBoxSpielerOben.Name = "Spieler 2";
                        break;
                    }
                case 2:
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 4";
                        pictureBoxSpielerRechts.Name = "Spieler 3";
                        pictureBoxSpielerOben.Name = "Spieler 1";
                        break;
                    }
                case 3:
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 2";
                        pictureBoxSpielerRechts.Name = "Spieler 1";
                        pictureBoxSpielerOben.Name = "Spieler 4";
                        break;
                    }
                case 4:
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 1";
                        pictureBoxSpielerRechts.Name = "Spieler 2";
                        pictureBoxSpielerOben.Name = "Spieler 3";
                        break;
                    }
            }


            //kartenAnzahlImages is being filled with the cardimages
            kartenAnzahlimages.Add(Properties.Resources._0Karte); //no cards 1px image is being used
            kartenAnzahlimages.Add(Properties.Resources._1Karte);
            kartenAnzahlimages.Add(Properties.Resources._2Karten);
            kartenAnzahlimages.Add(Properties.Resources._3Karten);
            kartenAnzahlimages.Add(Properties.Resources._4Karten);
            kartenAnzahlimages.Add(Properties.Resources._5Karten);

            for (int i = 0; i < 6; i++)
            {
                kartenAnzahlimages[i].Tag = i.ToString(); //Tag is to check if the image has been used already
            }

            pictureBoxSpielerOben.Image = (Image)kartenAnzahlimages[5].Clone();
            pictureBoxSpielerLinks.Image = (Image)kartenAnzahlimages[5].Clone();
            pictureBoxSpielerRechts.Image = (Image)kartenAnzahlimages[5].Clone();
            pictureBoxSpielerOben.Image.Tag = kartenAnzahlimages[5].Tag;
            pictureBoxSpielerRechts.Image.Tag = kartenAnzahlimages[5].Tag;
            pictureBoxSpielerLinks.Image.Tag = kartenAnzahlimages[5].Tag;

            pictureBoxSpielerOben.Image.RotateFlip(RotateFlipType.Rotate180FlipNone); //Images have to be flipped or they are wrong
            pictureBoxSpielerLinks.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBoxSpielerRechts.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);

            #endregion

            #region Mod Karteneinstellungen

            if (cf.Kritten||cf.WeliFarbe)//wenn Kritten oder Welifarbe an ist dann wird der weli hinzugefügt
            {
                wattKarten.Add(new wattKarte("Schell", "Weli", LoadKartenImage(1150, 12, 360, 580))); 
            }

            #endregion

            System.GC.Collect();
            kartenBitmap.Dispose(); //Free up some resources because the bitmap is not needed anymore

            loadingcomplete = true;
            while (!loadingcomplete) //keeps it from starting maingamethread and causing errors
            {
                await Task.Delay(500);
            }

            Thread mainGameThread = new Thread(t => MainGame(cf.Krittenuberspringen,cf.Latinisch,cf.Kritten,cf.Guadebese,cf.WeliFarbe)); //create a new thread to run maingame
            mainGameThread.Start();

        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion

        #region Main Game (UPDATED)

        private void MainGame(bool krittenuber, bool latinisch, bool kritten, bool guadebese, bool welifarbe)
        {
            bool gamerunning = true; //is set to false when someone has won or the game is canceled  
            wattKarte wattAngesagt; //is the card that is angesagt
            List<wattKarte> wattRangliste = new List<wattKarte>(); //which cards stech other cards like if kritten then herz könig > everything        

            while (gamerunning)
            {
                //visual adjustments
                RandomKartenAusgeben(wattKarten);
                ExecuteSecure(() => PosSizeAnpassung()); 
                SetKartenDisplay(); 
                wattAngesagt = KarteAnsagen(latinisch, kritten, welifarbe, wattKarten);

                if (wattMitspieler[myplayernumber - 1].Spieleransagen || wattMitspieler.Count == 2 || !latinisch) //only people who ansagen know what is wattAngesagt or when it's not latinisch then everybody knows
                {
                    ExecuteSecure(()=>LabelInfoWrite(wattAngesagt.Farbe + " " + wattAngesagt.Zahl + " ist angesagt!", 6000));
                }

                dieserundescore = 2; //2 Points is the standart, only changed if a player says "drei"

                #region SpielerDran, who lays the first card

                byte[] spielerDran;  //wer anfängt und danach dran kommt etc
                byte spielerStecher; //wer sticht ist der der erster legt also wird auch am anfang verwendet ohne dass ein stich vorhanden ist weil es text spart

                if (wattMitspieler.Count == 2) 
                {
                    if (wattMitspieler[0].Spieleransagen)
                        spielerStecher = 1;
                    else
                        spielerStecher = 0;
                }
                else
                {
                    if (wattMitspieler[0].Spieleransagen && wattMitspieler[2].Spieleransagen)
                        spielerStecher = 2;
                    else if (wattMitspieler[2].Spieleransagen && wattMitspieler[1].Spieleransagen)
                        spielerStecher = 1;
                    else if (wattMitspieler[1].Spieleransagen && wattMitspieler[3].Spieleransagen)
                        spielerStecher = 3;
                    else if (wattMitspieler[3].Spieleransagen && wattMitspieler[0].Spieleransagen)
                        spielerStecher = 0;
                    else
                    {
                        Console.WriteLine("FEHLER bei spielerdran");
                        spielerStecher = 255;
                    }
                }

                #endregion

                for (int o = 0; o < 5; o++)//Runs 5 times for 5 cards
                {
                    #region spielerDran select

                    if (wattMitspieler.Count == 2)
                    {
                        if (spielerStecher == 0)
                        {
                            spielerDran = new byte[] { 0, 1 }; //2 Players is easy it's either 0 or 1
                        }
                        else
                        {
                            spielerDran = new byte[] { 1, 0 };
                        }
                    }
                    else
                    {
                        if (spielerStecher == 0)
                            spielerDran = new byte[] { 0, 2, 1, 3 }; //It's always a rotation of 0,2,1,3 just different positions
                        else if (spielerStecher == 1)
                            spielerDran = new byte[] { 1, 3, 0, 2 };
                        else if (spielerStecher == 2)
                            spielerDran = new byte[] { 2, 1, 3, 0 };
                        else if (spielerStecher == 3)
                            spielerDran = new byte[] { 3, 0, 2, 1 };
                        else
                        {
                            Console.WriteLine("FEHLER bei IF-spielerStecher");
                            spielerDran = new byte[] { 255 };
                        }
                    }

                    #endregion

                    for (int i = 0; i < wattMitspieler.Count; i++) //2 Players 2 Cards placed in a round, 4 Players 4 CARDS
                    {
                        #region Card Lay (NEW)

                        wattMitspieler[spielerDran[i]].Spielerturn = true;  

                        if (wattMitspieler[spielerDran[i]].Difficulty == 0) //if the player is human, diff 0
                        {
                            foreach(PictureBox wm in wattMeineKartenBoxen) //boxes only enabled when it's humans turn
                            {
                                ExecuteSecure(() => wm.Enabled = true);
                            }

                            if (spielerDran[i] != myplayernumber - 1)//if same players turn don't show the label, don't switch
                            {
                                ExecuteSecure(() => ChangePlayer(spielerDran[i])); //switch to the human player position

                                if (wattMitspieler[1].Difficulty == 0) //only show when there are other players, not AI
                                {
                                    ExecuteSecure(() => labelPlayerWait.Visible = true);
                                    ExecuteSecure(() => labelPlayerWait.Text = "Spieler " + myplayernumber + " ist dran. Drücke ENTER um zu beginnen.");
                                    ExecuteSecure(() => labelPlayerWait.BringToFront());
                                }

                            bool waitplayer = true;
                            while (waitplayer) //wait until player is there (enter has been pressed)
                            {
                                    ExecuteSecure(() => waitplayer = labelPlayerWait.Visible); //waitplayer == the status of the labelplayerwait visible
                                    Thread.Sleep(50);
                            }

                            //other player should know what is angesagt, only in first round
                            if ((wattMitspieler[myplayernumber - 1].Spieleransagen || wattMitspieler.Count == 2 || !latinisch)&&o<1) 
                            {
                                ExecuteSecure(() => LabelInfoWrite(wattAngesagt.Farbe + " " + wattAngesagt.Zahl + " ist angesagt!", 6000));
                            }

                            }
                        }
                        else
                        {
                            Thread.Sleep(500); //so bot doesn't lay card instantly, realistic thinking time
                            if(i==0)
                                AIPlace(wattMitspieler[spielerDran[i]].Difficulty, spielerDran[i],null);
                            else
                                AIPlace(wattMitspieler[spielerDran[i]].Difficulty, spielerDran[i], wattRangliste);
                        }
                       
                        while (wattMitspieler[spielerDran[i]].Spielerturn == true) //wait till card is played
                        {
                            Thread.Sleep(10);
                        }

                        foreach (PictureBox wm in wattMeineKartenBoxen) //boxes only enabled when it's humans turn, else errors could occur
                        {
                            ExecuteSecure(() => wm.Enabled = false);
                        }

                        if (i == 0) //wattRangListe is set when the first card is laid down
                            WattRanglisteSet(wattKarten, wattRangliste, wattAngesagt, wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Kartenimage == pictureBoxMainKarte.Image).FirstOrDefault())], guadebese, kritten, welifarbe, krittenuber);

                        SetKartenDisplay(); //Update the Cardnumber of other Players
                        Thread.Sleep(1000);

                        #endregion
                    }

                    #region Stichzuweisung

                    int[] index = new int[] { 33, 33, 33, 33 }; //Set to 33 changed if card is on the wattRangliste
                    for (int i = 0; i < wattMitspieler.Count; i++) 
                    {
                        if (wattRangliste.Any(w => w.Kartenimage == wattMainKartenBoxen[i].Image)) //is the card in the rangliste
                        {
                            //yes it is, get it's index to compare it later
                            index[i] = wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattRangliste.Where(w => w.Kartenimage == wattMainKartenBoxen[i].Image).FirstOrDefault())]);
                        }

                    }

                    if (wattMitspieler.Count == 4)
                        Array.Reverse(index); //reverse the array for it to be correct
                    else
                    { int tempzahl; tempzahl = index[0]; index[0] = index[1]; index[1] = tempzahl; } //reverse doesn't work on 2 player 

                    #region First angesagter schlag 
                    //wenn eine zahl angesagt ist und sie öfters gespielt wird bekommt der erste die sie gespielt hat, den stich
                    for(int e = 0; e<index.Count(); e++)
                    {
                        if (index[e] != 33 && wattAngesagt.Zahl == wattRangliste[index[e]].Zahl)
                        {
                            for (int i = 1; i < index.Count(); i++)
                            {
                                //kritten und angesagte karte werden nicht getauscht 
                                if (index[i] != 33 && kritten && (wattRangliste[index[i]].Zahl == "König" && wattRangliste[index[i]].Farbe == "Herz" || wattRangliste[index[i]].Zahl == "Sieben" && wattRangliste[index[i]].Farbe == "Eichel") || index[i]!=33 && wattRangliste[index[i]].Farbe == wattAngesagt.Farbe) { }
                                else if (index[i] != 33 && wattAngesagt.Zahl == wattRangliste[index[i]].Zahl && index[i] < index[e]) //wenn der index einer karte die später gelegt wurde kleiner ist wird er getauscht
                                {
                                    wattKarte tempcard = wattRangliste[index[i]]; //karte wird auch getauscht 
                                    wattRangliste[index[i]] = wattRangliste[index[e]];
                                    wattRangliste[index[e]] = tempcard;

                                    int tempdex = index[i];
                                    index[i] = index[e];
                                    index[e] = tempdex;


                                    break;
                                }
                             }
                            break;
                        }
                    }
                    #endregion

                    if (index[0] < index[1] && wattMitspieler.Count == 2) //2 Player is easy again it's either p1 or p2 which has the smaller index
                    {
                        StichGeben(spielerDran[0]+1, index[0], wattRangliste); //+1 then it's the player number
                        spielerStecher = spielerDran[0];
                    }
                    else if (index[1] < index[0] && wattMitspieler.Count == 2)
                    {
                        StichGeben(spielerDran[1]+1, index[1], wattRangliste);
                        spielerStecher = spielerDran[1];
                    }

                    else if (wattMitspieler.Count == 4 && index[0] < index[1] && index[0] < index[2] && index[0] < index[3])//4player has to check if it is the smallest index of them all
                    {
                        StichGeben(spielerDran[0]+1, index[0], wattRangliste);
                        spielerStecher = spielerDran[0];
                    }
                    else if (wattMitspieler.Count == 4 && index[1] < index[0] && index[1] < index[2] && index[1] < index[3])
                    {
                        StichGeben(spielerDran[1]+1, index[1], wattRangliste);
                        spielerStecher = spielerDran[1];
                    }
                    else if (wattMitspieler.Count == 4 && index[2] < index[1] && index[2] < index[0] && index[2] < index[3])
                    {
                        StichGeben(spielerDran[2]+1, index[2], wattRangliste);
                        spielerStecher = spielerDran[2];
                    }
                    else if (wattMitspieler.Count == 4 && index[3] < index[1] && index[3] < index[2] && index[3] < index[0])
                    {
                        StichGeben(spielerDran[3]+1, index[3], wattRangliste);
                        spielerStecher = spielerDran[3];
                    }
                    else
                    {
                        Console.WriteLine("FEHLER bei STICHZUWEISUNG");
                    }

                    #endregion

                    #region Score Addieren und Displayen and Winner Display
                    
                    if (wattMitspieler.Count == 2)
                    {
                        if (wattMitspieler[0].StichAnzahl == 3)//add the score of this round which normally is 2 but can be more as soon as the "drei" system is built in
                        {
                            wattMitspieler[0].Score += dieserundescore;
                            if (wattMitspieler[0].Score == 11)
                            {
                                ExecuteSecure(()=>LabelInfoWrite(wattMitspieler[0].PlayerName + " Gewinnt! Vielen Dank fürs Spielen!", 15000));
                                
                            }
                            else
                            {  
                                ExecuteSecure(()=>LabelInfoWrite(wattMitspieler[0].PlayerName + " hat die Runde gewonnen", 5000));
                            } 
                        }
                        else if (wattMitspieler[1].StichAnzahl == 3)
                        {
                            wattMitspieler[1].Score += dieserundescore;
                            if (wattMitspieler[1].Score == 11)
                            {
                                ExecuteSecure(() => LabelInfoWrite(wattMitspieler[1].PlayerName + " Gewinnt! Vielen Dank fürs Spielen!", 15000));
                            }
                            else
                            {
                                ExecuteSecure(() => LabelInfoWrite(wattMitspieler[1].PlayerName + " hat die Runde gewonnen", 5000));
                            }
                        }
                    }
                    
                    else
                    {
                        if(wattMitspieler[0].StichAnzahl + wattMitspieler[1].StichAnzahl == 3) //in 4 player modus stiche count for the whole team so 1&2 count together and 3&4
                        {
                            wattMitspieler[0].Score += dieserundescore; wattMitspieler[1].Score += dieserundescore;
                            if (wattMitspieler[0].Score == 11)
                            {
                                ExecuteSecure(()=>LabelInfoWrite(wattMitspieler[0].PlayerName + " und " + wattMitspieler[1].PlayerName + " Gewinnen! Vielen Dank fürs Spielen!", 15000));
                            }
                            else
                            {
                                ExecuteSecure(()=>LabelInfoWrite(wattMitspieler[0].PlayerName + " und " + wattMitspieler[1].PlayerName + " haben die Runde gewonnen", 5000));
                            }
                        }
                        else if (wattMitspieler[2].StichAnzahl + wattMitspieler[3].StichAnzahl == 3)
                        {
                            wattMitspieler[2].Score += dieserundescore; wattMitspieler[3].Score += dieserundescore;
                            if (wattMitspieler[2].Score == 11)
                            {
                                ExecuteSecure(() => LabelInfoWrite(wattMitspieler[2].PlayerName + " und " + wattMitspieler[3].PlayerName + " Gewinnen! Vielen Dank fürs Spielen!", 15000));
                            }
                            else
                            {
                                ExecuteSecure(() => LabelInfoWrite(wattMitspieler[2].PlayerName + " und " + wattMitspieler[3].PlayerName + " haben die Runde gewonnen", 5000));
                            }
                        }
                    }

                    if (wattMitspieler.Count == 2 && (wattMitspieler[0].Score >= 11 || wattMitspieler[1].Score >= 11) || wattMitspieler.Count == 4 && (wattMitspieler[0].Score >= 11 || wattMitspieler[2].Score >= 11))
                    {
                        ExecuteSecure(() => LabelScoreWrite());
                        Thread.Sleep(3000);
                        gamerunning = false;
                        break;
                    }

                    #endregion

                    #region Visual Adjustments and StichReset if 3 Stiche

                    if (wattMitspieler.Count==4 &&(wattMitspieler[0].StichAnzahl + wattMitspieler[1].StichAnzahl == 3 || wattMitspieler[2].StichAnzahl + wattMitspieler[3].StichAnzahl == 3)||
                         wattMitspieler.Count == 2 && (wattMitspieler[0].StichAnzahl == 3 || wattMitspieler[1].StichAnzahl == 3)) //if anybody has reached 3 stiche
                        {
                        foreach (wattSpieler ws in wattMitspieler)
                        {
                            ws.StichAnzahl = 0;
                            ws.Stichdoku = null; //clear the stiches for next round
                        }

                        Invoke(new Action(() =>
                        {
                            Refresh(); //so there will be no stiches displayed anymore
                        }));
                        ExecuteSecure(() => ResetMitteKarten());
                        ExecuteSecure(() => LabelScoreWrite()); //updates the score
                        kartenreset = true; //so 5 cards of the other players are shown again
                        Thread.Sleep(3000); //wait a few secs, mostly because of the label but looks good in design too so w/e
                        break;
                    }
                    ResetMitteKarten(); //mid cards image is reset

                    #endregion
                }  
                
                RotateAnsager(); 
            }

            Environment.Exit(0);
        }

        #endregion

        #region Change Player (NEW)

        public void ChangePlayer(byte arraypos)
        {
            foreach (PictureBox pb in wattMeineKartenBoxen)
            {
                pb.Image = null;
            }

            //be the player, that has to lay a card
            myplayernumber = arraypos;//playernumber has to be +1
            myplayernumber++;

            for (int i = 0; i < wattMitspieler[arraypos].SpielerWattKarten.Count; i++)
            {
                wattMeineKartenBoxen[i].Image = wattMitspieler[arraypos].SpielerWattKarten[i].Kartenimage;
            }

            switch (myplayernumber) //Sets the names of the other players pictureboxes, later used to define which stichanzahl to get
            {
                case 1: //when I am player 1, player 3 is left, player 4 is right, player 2 is above
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 3";
                        pictureBoxSpielerRechts.Name = "Spieler 4";
                        pictureBoxSpielerOben.Name = "Spieler 2";
                        break;
                    }
                case 2:
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 4";
                        pictureBoxSpielerRechts.Name = "Spieler 3";
                        pictureBoxSpielerOben.Name = "Spieler 1";
                        break;
                    }
                case 3:
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 2";
                        pictureBoxSpielerRechts.Name = "Spieler 1";
                        pictureBoxSpielerOben.Name = "Spieler 4";
                        break;
                    }
                case 4:
                    {
                        pictureBoxSpielerLinks.Name = "Spieler 1";
                        pictureBoxSpielerRechts.Name = "Spieler 2";
                        pictureBoxSpielerOben.Name = "Spieler 3";
                        break;
                    }
            }

            LabelScoreWrite();
            SetKartenDisplay();
            Refresh();
            PosSizeAnpassung();
            //to reset displays for the player
        }

        #endregion 

        #region Card Ansagen

        private wattKarte KarteAnsagen(bool latinisch, bool kritten, bool welifarbe, List<wattKarte> wattKarten)
        {
            #region Ansager Settings, zahlansager and farbansager

            byte farbansager = 0, zahlansager = 0;
            if (wattMitspieler.Count == 2) //2 player the one wwith spieleransagen true is the farbansager
            {
                if (wattMitspieler[0].Spieleransagen)                
                {farbansager = 0; zahlansager = 1;}               
                else                
                {zahlansager = 0; farbansager = 1;}
                
            }
            else
            {
                if (wattMitspieler[0].Spieleransagen && wattMitspieler[2].Spieleransagen) //rotation player 0 with 2, 2 with 1 etc
                { farbansager = 0; zahlansager = 2; }
                else if (wattMitspieler[2].Spieleransagen && wattMitspieler[1].Spieleransagen)
                { farbansager = 2; zahlansager = 1; }
                else if (wattMitspieler[1].Spieleransagen && wattMitspieler[3].Spieleransagen)
                { farbansager = 1; zahlansager = 3; }
                else if (wattMitspieler[3].Spieleransagen && wattMitspieler[0].Spieleransagen)
                { farbansager = 3; zahlansager = 0; }
            }

            #endregion

            #region Latinisch 

            if (latinisch)
            {
                byte farbansagerkarte=0, zahlansagerkarte = 0; //only >0 when there is a krit at the 1st spot

                if (kritten)//if kritten is enabled you have to put the card back if it is a krit
                {
                    for(int i = 0; i<5; i++)
                    {
                        if (wattMitspieler[farbansager].SpielerWattKarten[i].Farbe == "Herz" && wattMitspieler[farbansager].SpielerWattKarten[i].Zahl == "König" ||
                            wattMitspieler[farbansager].SpielerWattKarten[i].Farbe == "Eichel" && wattMitspieler[farbansager].SpielerWattKarten[i].Zahl == "Sieben" ||
                            wattMitspieler[farbansager].SpielerWattKarten[i].Farbe == "Schell" && wattMitspieler[farbansager].SpielerWattKarten[i].Zahl == "Weli")
                        { farbansagerkarte++; }
                        else { break; }                            
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        if (wattMitspieler[zahlansager].SpielerWattKarten[i].Farbe == "Herz" && wattMitspieler[zahlansager].SpielerWattKarten[i].Zahl == "König" ||
                            wattMitspieler[zahlansager].SpielerWattKarten[i].Farbe == "Eichel" && wattMitspieler[zahlansager].SpielerWattKarten[i].Zahl == "Sieben" ||
                            wattMitspieler[zahlansager].SpielerWattKarten[i].Farbe == "Schell" && wattMitspieler[zahlansager].SpielerWattKarten[i].Zahl == "Weli")
                        { zahlansagerkarte++;}
                        else { break; }
                    }

                    if (farbansagerkarte > 0 && zahlansagerkarte > 0)
                        ExecuteSecure(()=>LabelInfoWrite("Beide haben zurückgesteckt", 2500));
                    else if (farbansagerkarte > 0)
                        ExecuteSecure(() => LabelInfoWrite(wattMitspieler[farbansager].PlayerName + " (Farbe) hat zurückgesteckt", 2500));
                    else if (zahlansagerkarte > 0)
                        ExecuteSecure(() => LabelInfoWrite(wattMitspieler[zahlansager].PlayerName + " (Zahl) hat zurückgesteckt", 2500));
                }

                if (welifarbe)//same with welifarbe
                {
                    if (wattMitspieler[farbansager].SpielerWattKarten[0].Farbe == "Schell" && wattMitspieler[farbansager].SpielerWattKarten[0].Zahl == "Weli") { farbansagerkarte++; }
                    if (wattMitspieler[zahlansager].SpielerWattKarten[0].Farbe == "Schell" && wattMitspieler[zahlansager].SpielerWattKarten[0].Zahl == "Weli") { farbansagerkarte++; }
                }

                //searching after the index of the card that contains the farbe and zahl and returns the card that has these values
                int picindex = wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == wattMitspieler[farbansager].SpielerWattKarten[farbansagerkarte].Farbe && w.Zahl == wattMitspieler[zahlansager].SpielerWattKarten[zahlansagerkarte].Zahl).FirstOrDefault());
                return new wattKarte(wattMitspieler[farbansager].SpielerWattKarten[farbansagerkarte].Farbe, wattMitspieler[zahlansager].SpielerWattKarten[zahlansagerkarte].Zahl, wattKarten[picindex].Kartenimage);

            }

            #endregion

            #region Normal Ansagen 

            else if(!latinisch&&(myplayernumber - 1 == farbansager || myplayernumber - 1 == zahlansager))
            {
                string farbkarte = "", zahlkarte = "";

                if (myplayernumber - 1 == farbansager)//wenn ich der farbansager bin
                {
                    ansagen = true;
                    Invoke(new Action(() =>
                    {
                    comboBoxAnsagen.Visible = true;
                    buttonAnsagen.Visible = true;
                    comboBoxAnsagen.Items.Clear();
                    string[] farben = new string[] { "Herz", "Schell", "Eichel", "Laub" };
                    comboBoxAnsagen.Items.AddRange(farben);
                    comboBoxAnsagen.SelectedItem = "Herz"; //if an item is already selected that makes it easier
                    }));

                    while(ansagen) //waiting till the button has been pressed NETHERE
                        {
                            Thread.Sleep(100);
                        }

                    Invoke(new Action(() =>
                    {
                    farbkarte = comboBoxAnsagen.SelectedItem.ToString();
                    buttonAnsagen.Visible = false;
                    comboBoxAnsagen.Visible = false;
                    }));

                }
                else if (myplayernumber - 1 == zahlansager)//wenn ich der zahlansager bin
                {
                    ansagen = true;
                    Invoke(new Action(() =>
                    {
                        comboBoxAnsagen.Visible = true;
                        buttonAnsagen.Visible = true;
                        comboBoxAnsagen.Items.Clear();
                        string[] zahlen = new string[] { "Sieben", "Acht", "Neun", "Zehn", "Unter", "Ober", "König", "Sau" };
                        comboBoxAnsagen.Items.AddRange(zahlen);
                        comboBoxAnsagen.SelectedItem = "Sieben";
                    }));

                    while (ansagen)
                        {
                            Thread.Sleep(100);
                        }

                     Invoke(new Action(() =>
                     {
                       farbkarte = comboBoxAnsagen.SelectedItem.ToString();
                       buttonAnsagen.Visible = false;
                       comboBoxAnsagen.Visible = false;
                     }));
                    }
                
                return wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == farbkarte && w.Zahl == zahlkarte).FirstOrDefault())];
            }

            #endregion

            Console.WriteLine("Nichts angesagt FEHLER");
            return new wattKarte();
        }

        private void buttonAnsagen_Click(object sender, EventArgs e)
        {
            ansagen = false; //so the zahl or farbe is angesagt
        }

        #endregion

        #region RotateAnsager

        private void RotateAnsager() 
        {
            if (wattMitspieler.Count == 2) //in 2 player it's a simple switcharoo
            {
                wattMitspieler[1].Spieleransagen = !wattMitspieler[1].Spieleransagen;
                wattMitspieler[0].Spieleransagen = !wattMitspieler[0].Spieleransagen;
            }
            else if(wattMitspieler.Count == 4)
            {
                if (wattMitspieler[0].Spieleransagen && wattMitspieler[2].Spieleransagen) //4player could maybe be solved better todo
                {
                    wattMitspieler[0].Spieleransagen = false; wattMitspieler[2].Spieleransagen = false;
                    wattMitspieler[2].Spieleransagen = true; wattMitspieler[1].Spieleransagen = true;
                }
                else if (wattMitspieler[2].Spieleransagen && wattMitspieler[1].Spieleransagen)
                {
                    wattMitspieler[2].Spieleransagen = false; wattMitspieler[1].Spieleransagen = false;
                    wattMitspieler[1].Spieleransagen = true; wattMitspieler[3].Spieleransagen = true;
                }
                else if (wattMitspieler[1].Spieleransagen && wattMitspieler[3].Spieleransagen)
                {
                    wattMitspieler[1].Spieleransagen = false; wattMitspieler[3].Spieleransagen = false;
                    wattMitspieler[3].Spieleransagen = true; wattMitspieler[0].Spieleransagen = true;
                }
                else if (wattMitspieler[3].Spieleransagen && wattMitspieler[0].Spieleransagen)
                {
                    wattMitspieler[3].Spieleransagen = false; wattMitspieler[0].Spieleransagen = false;
                    wattMitspieler[0].Spieleransagen = true; wattMitspieler[2].Spieleransagen = true;
                }
            }
        }

        #endregion

        #region RandomKartenAusgeben

        private void RandomKartenAusgeben(List<wattKarte> wattKarten)
        {
            List<wattKarte> randomkartenlist = wattKarten.ToList(); //makes a new list so the original wattkarten isn't affected
            Random rnd = new Random();
            foreach (wattSpieler ws in wattMitspieler)
            {
                ws.SpielerWattKarten.Clear(); //removes all wattKarten from the player, this is needed when somebody gave up on "drei"
                
                for (int i = 0; i < 5; i++)
                {
                    ws.SpielerWattKarten.Add(randomkartenlist[rnd.Next(0, randomkartenlist.Count)]); //adds a random card from the randomkartenlist
                    randomkartenlist.Remove(ws.SpielerWattKarten[i]);  //removes the card from the list so it can't occur again               
                }
            }

            for(int i = 0; i<5; i++)
            {
                wattMeineKartenBoxen[i].Image = wattMitspieler[myplayernumber - 1].SpielerWattKarten[i].Kartenimage; //sets my cardboxes to my cardimages
            }

        }

        #endregion

        #region wattRangListeSet 

        private void WattRanglisteSet(List<wattKarte> wattKarten, List<wattKarte> wattRangliste, wattKarte wattAngesagt, wattKarte ersteKarte, bool guadebese, bool kritten, bool welifarbe, bool krittenuber)
        {
            wattRangliste.Clear(); //clear list from before 

            string[] normFarben = new string[] { "Herz", "Schell", "Laub", "Eichel" };
            string[] normZahlen = new string[] { "Sieben", "Acht", "Neun", "Zehn", "Unter", "Ober", "König", "Sau" };

            if (kritten) 
            {
                wattRangliste.Add(wattKarten[4]);//herz könig best card with kritten
                wattRangliste.Add(wattKarten[32]);//weli    
                wattRangliste.Add(wattKarten[18]);//spitz
            }

            if (guadebese) //guade and bese guad>bes>recht
            {
                //GUAD
                if (krittenuber && (wattAngesagt == wattKarten[5] || wattAngesagt == wattKarten[19])) 
                {
                    if (wattAngesagt == wattKarten[5]) //if card is herz ober then it would be herz könig but krittenuber so it's herz sau
                    {
                        wattRangliste.Add(wattKarten[3]);
                    }
                    else if (wattAngesagt == wattKarten[19]) //if card is eichel sau then it would be spitz but krittenuber so it's eichel acht
                    {
                        wattRangliste.Add(wattKarten[17]);
                    }
                }
                else if (wattAngesagt.Zahl == "Sau") //if wattangesagt is a sau(the last card) the guade is the first card (sieben)
                {
                    wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == wattAngesagt.Farbe && w.Zahl == "Sieben").FirstOrDefault())]);
                }
                else
                {
                    //normally it's just the card one above
                    wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == wattAngesagt.Farbe && w.Zahl == normZahlen[Array.IndexOf(normZahlen, wattAngesagt.Zahl) + 1]).FirstOrDefault())]);
                }

                //BESE
                if (krittenuber && (wattAngesagt == wattKarten[3] || wattAngesagt == wattKarten[17])) //again same as above just in the other direction
                {
                    if (wattAngesagt == wattKarten[3]) 
                    {
                        wattRangliste.Add(wattKarten[5]);
                    }
                    else if (wattAngesagt == wattKarten[17]) 
                    {
                        wattRangliste.Add(wattKarten[19]);
                    }
                }
                else if (wattAngesagt.Zahl == "Sieben")//again if wattangesagt is sieben then it has to be sau
                {
                    wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == wattAngesagt.Farbe && w.Zahl == "Sau").FirstOrDefault())]);
                }
                else
                {
                    //normal -1 card from the one that is angesagt
                    wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == wattAngesagt.Farbe && w.Zahl == normZahlen[Array.IndexOf(normZahlen, wattAngesagt.Zahl) - 1]).FirstOrDefault())]);
                }
            }

            //the rechte
            wattRangliste.Add(wattAngesagt);

            //and the linken (card that have the same zahl)
            wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Zahl == wattAngesagt.Zahl && w.Farbe != wattRangliste[wattRangliste.Count-1].Farbe).FirstOrDefault())]);
            wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Zahl == wattAngesagt.Zahl && w.Farbe != wattRangliste[wattRangliste.Count - 2].Farbe && w.Farbe != wattRangliste[wattRangliste.Count - 1].Farbe).FirstOrDefault())]);
            wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Zahl == wattAngesagt.Zahl && w.Farbe != wattRangliste[wattRangliste.Count - 3].Farbe && w.Farbe != wattRangliste[wattRangliste.Count - 2].Farbe && w.Farbe != wattRangliste[wattRangliste.Count - 1].Farbe).FirstOrDefault())]);
            //this is fixed in maingame stichgeben, the first linke laid down is the most worth

            //then it's the farbe from wattangesagt
            for (int i = 7; i>-1; i--)
            {
                if (!wattRangliste.Any(w => w.Farbe == wattAngesagt.Farbe && w.Zahl == normZahlen[i])) //only add if the card doesn't exist already
                {
                    wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == wattAngesagt.Farbe && w.Zahl == normZahlen[i]).FirstOrDefault())]);
                }
            }

            if (welifarbe) //if welifarbe then the weli is the lowest of the angesagte farbe
            {
                wattRangliste.Add(wattKarten[32]);
            }

            //first farbe 
            for (int i = 7; i > -1; i--)
            {
                if (!wattRangliste.Any(w => w.Farbe == ersteKarte.Farbe && w.Zahl == normZahlen[i])) //again if the card doesn't exist already
                {
                    wattRangliste.Add(wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Farbe == ersteKarte.Farbe && w.Zahl == normZahlen[i]).FirstOrDefault())]);
                }
            }
        }

        #endregion

        #region StichGeben

        private void StichGeben(int playernumber, int stichindex, List<wattKarte> wattRangliste)
        {
            wattMitspieler[playernumber - 1].StichAnzahl++; //adds a stich to the player that got one
            ExecuteSecure(() => Refresh());
             //refresh so it can be displayed

            if (wattMitspieler.Count == 2)
            {
                wattKarte karte2 = new wattKarte();
                if(wattRangliste[stichindex].Kartenimage == wattMainKartenBoxen[0].Image)
                    karte2 = wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[1].Image).FirstOrDefault())]; //gets the card from the image int he middle
                else
                    karte2 = wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[0].Image).FirstOrDefault())];

                //puts the cards in the stichdoku of the player so other players can see why he got the stich
                wattMitspieler[playernumber - 1].Stichdoku += String.Format("\n /{0} {1}/\n{2} {3}\n ", wattRangliste[stichindex].Farbe, wattRangliste[stichindex].Zahl, karte2.Farbe, karte2.Zahl);
            }
            else if(wattMitspieler.Count == 4)
            {
                wattKarte karte2 = new wattKarte();
                wattKarte karte3 = new wattKarte();
                wattKarte karte4 = new wattKarte();
                for(int i = 0; i<4; i++)
                {
                    if (wattRangliste[stichindex].Kartenimage != wattMainKartenBoxen[i].Image) //gets the other cards that are not the wattRangliste[stichindex] card
                    {
                        if (karte2.Farbe == null) //fills karte2 then karte3 then karte4
                            karte2 = wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[i].Image).FirstOrDefault())]; 
                        else if (karte3.Farbe == null)
                            karte3 = wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[i].Image).FirstOrDefault())];
                        else if (karte4.Farbe == null)
                            karte4 = wattKarten[wattKarten.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[i].Image).FirstOrDefault())];
                    }
                }

                wattMitspieler[playernumber - 1].Stichdoku += String.Format("\n /{0} {1}/\n{2} {3}\n{4} {5}\n{6} {7}\n ", wattRangliste[stichindex].Farbe, wattRangliste[stichindex].Zahl, karte2.Farbe, karte2.Zahl, karte3.Farbe, karte3.Zahl, karte4.Farbe, karte4.Zahl);
            }

            ExecuteSecure(() => Refresh());
		//refresh again just to be sure
        }

        #endregion

        #region LabelInfo Ausgabe

        private async void LabelInfoWrite(string text, int duration)
        {
            try
            { 
                labelInfo.Text = text;
                labelInfo.Visible = true;
                await Task.Delay(duration);
                labelInfo.Visible = false;
            }
            catch
            {
                Console.WriteLine("Info Write not possible");
            }
        }

        #endregion

        #region LabelScore Ausgabe

        private void LabelScoreWrite()
        {          
                //the number of my team/my number is displayed first 
                if (wattMitspieler[myplayernumber-1] == wattMitspieler[0] && wattMitspieler.Count == 2)
                    labelScore.Text = wattMitspieler[0].Score + "-" + wattMitspieler[1].Score;
                else if (wattMitspieler[myplayernumber - 1] == wattMitspieler[1] && wattMitspieler.Count == 2)
                    labelScore.Text = wattMitspieler[1].Score + "-" + wattMitspieler[0].Score;

                else if ((wattMitspieler[myplayernumber - 1] == wattMitspieler[0] || wattMitspieler[myplayernumber - 1] == wattMitspieler[1]) && wattMitspieler.Count == 4)
                    labelScore.Text = wattMitspieler[0].Score + "-" + wattMitspieler[2].Score;
                else if ((wattMitspieler[myplayernumber - 1] == wattMitspieler[2] || wattMitspieler[myplayernumber - 1] == wattMitspieler[3]) && wattMitspieler.Count == 4)
                    labelScore.Text = wattMitspieler[2].Score + "-" + wattMitspieler[0].Score;
        }

        #endregion

        #region Size and adjustments 

        private void PosSizeAnpassung() //is called everytime the form is resized or something needs to be placed to the original spot
        {

            if (this.WindowState == FormWindowState.Minimized) //if the form is minimized it won't resize or there will be an error
                return;

            if(this.Width*2 < this.Height || this.Width > this.Height*2) //MAX SIZE is twice the height or width - that is because it it was bigger it looks horrible
            {
                this.MaximumSize = new Size(this.Width, this.Height);
            }
            else
            {
                this.MaximumSize = new Size(0, 0); //resets max size
            }


            #region KartenGrößen

            Size KartenSize = new Size(this.ClientSize.Width / 8, this.ClientSize.Height / 3); //size of a normal card
            
            pictureBoxMeineKarte1.Size = KartenSize;
            pictureBoxMeineKarte2.Size = KartenSize;
            pictureBoxMeineKarte3.Size = KartenSize;
            pictureBoxMeineKarte4.Size = KartenSize;
            pictureBoxMeineKarte5.Size = KartenSize;
            pictureBoxMainKarte.Size = KartenSize;
            pictureBoxMainKarte3.Size = new Size(KartenSize.Width / 3, KartenSize.Height/ 3); //a third of a normal card
            pictureBoxMainKarte2.Size = new Size(KartenSize.Width / 3, KartenSize.Height / 3);
            pictureBoxMainKarte1.Size = new Size(KartenSize.Width / 3, KartenSize.Height / 3);

            #endregion

            #region Mycardpositions and mystichposition & size

            Point midKarteUngerade = new Point(this.ClientSize.Width / 2 - KartenSize.Width / 2, this.ClientSize.Height - KartenSize.Height); //mid card LOCATION when 1,3,or 5 cards
            Point midKarteGeradeLinks = new Point(this.ClientSize.Width / 2 - KartenSize.Width, this.ClientSize.Height - KartenSize.Height); 
            Point midKarteGeradeRechts = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - KartenSize.Height); //midkartegeradelinks and rechts is for 2 and 4 cards

            List<PictureBox> meineBoxen = wattMeineKartenBoxen.ToList(); 
            foreach(PictureBox pb in wattMeineKartenBoxen) //empty boxes removed
            {
                if (pb.Image == null)
                {
                    pb.Location = new Point(this.Width, this.Height); //gtfo picbox
                    meineBoxen.Remove(pb);
                }

            }

            if(meineBoxen.Count != 0) 
            {

            if(meineBoxen.Count % 2  == 0) //even count of picboxes
            {
                if (meineBoxen.Count == 4)
                {
                    meineBoxen[0].Location = new Point(midKarteGeradeLinks.X - KartenSize.Width, midKarteGeradeLinks.Y);
                    meineBoxen[1].Location = midKarteGeradeLinks;
                    meineBoxen[2].Location = midKarteGeradeRechts;
                    meineBoxen[3].Location = new Point(midKarteGeradeRechts.X + KartenSize.Width, midKarteGeradeRechts.Y);
                }
                else
                {
                    meineBoxen[0].Location = midKarteGeradeLinks;
                    meineBoxen[1].Location = midKarteGeradeRechts;
                }
            }

            else  //uneven count of picboxes
            {
                if (meineBoxen.Count == 5)
                {
                    meineBoxen[0].Location = new Point(midKarteUngerade.X - KartenSize.Width*2, midKarteUngerade.Y);
                    meineBoxen[1].Location = new Point(midKarteUngerade.X - KartenSize.Width, midKarteUngerade.Y);
                    meineBoxen[2].Location = midKarteUngerade;
                    meineBoxen[3].Location = new Point(midKarteUngerade.X + KartenSize.Width, midKarteUngerade.Y);
                    meineBoxen[4].Location = new Point(midKarteUngerade.X + KartenSize.Width*2, midKarteUngerade.Y);
                }
                else if(meineBoxen.Count == 3)
                {
                    meineBoxen[0].Location = new Point(midKarteUngerade.X - KartenSize.Width, midKarteUngerade.Y);
                    meineBoxen[1].Location = midKarteUngerade;
                    meineBoxen[2].Location = new Point(midKarteUngerade.X + KartenSize.Width, midKarteUngerade.Y);
                }
                else
                {
                    meineBoxen[0].Location = midKarteUngerade;
                }
            }

                int meinesticheX = 0;
            foreach(PictureBox picb in meineBoxen) //the label is placed after the last card picturebox
                {
                    if (picb.Location.X > meinesticheX)
                        meinesticheX = picb.Location.X;
                }

                labelMeineStiche.Font = new Font("Arial", this.ClientSize.Height / 20, FontStyle.Bold);
                labelMeineStiche.Size = new Size(KartenSize.Width / 3 * 2, KartenSize.Height / 2);
                labelMeineStiche.Location = new Point(meinesticheX + KartenSize.Width, this.ClientSize.Height - labelMeineStiche.Height);
            }

            pictureBoxMainKarte.Location = new Point(midKarteUngerade.X, this.ClientSize.Height - KartenSize.Height*2 - KartenSize.Height/3); //in mid
            pictureBoxMainKarte3.Location = new Point(pictureBoxMainKarte.Location.X - KartenSize.Width / 3, pictureBoxMainKarte.Location.Y); //side cards
            pictureBoxMainKarte2.Location = new Point(pictureBoxMainKarte.Location.X - KartenSize.Width / 3, pictureBoxMainKarte3.Location.Y + KartenSize.Height / 3);
            pictureBoxMainKarte1.Location = new Point(pictureBoxMainKarte.Location.X - KartenSize.Width / 3, pictureBoxMainKarte2.Location.Y + KartenSize.Height / 3);

            #endregion

            #region Cards of other players and control elements

            Size KartenPackSize = new Size(this.ClientSize.Width / 10, this.ClientSize.Height / 2);

            pictureBoxSpielerOben.Size = new Size(KartenPackSize.Height,KartenPackSize.Width); //height and width are switched
            pictureBoxSpielerLinks.Size = KartenPackSize;
            pictureBoxSpielerRechts.Size = KartenPackSize;

            //these locations are guessed but they work so whateva
            pictureBoxSpielerOben.Location = new Point(this.ClientSize.Width / 2 - KartenPackSize.Height / 2, -this.ClientSize.Height/20);
            pictureBoxSpielerLinks.Location = new Point(-this.ClientSize.Width / 30, this.ClientSize.Height / 2 - KartenPackSize.Height / 2);
            pictureBoxSpielerRechts.Location = new Point(this.ClientSize.Width - this.ClientSize.Width /  15, this.ClientSize.Height / 2 - KartenPackSize.Height / 2); 

            //control elements like "drei" or "ansagen"

            comboBoxAnsagen.Size = new Size(pictureBoxMainKarte.Size.Width+pictureBoxMainKarte3.Size.Width, comboBoxAnsagen.Size.Height);
            comboBoxAnsagen.Location = new Point(pictureBoxMainKarte3.Location.X, pictureBoxMainKarte.Location.Y + pictureBoxMainKarte.Size.Height);

            buttonSchlagwechsel.Size = new Size(comboBoxAnsagen.Size.Width/3*2, buttonSchlagwechsel.Size.Height);
            buttonSchlagwechsel.Location = new Point(comboBoxAnsagen.Location.X, comboBoxAnsagen.Location.Y + comboBoxAnsagen.Size.Height + 5);

            buttonAnsagen.Size = new Size(comboBoxAnsagen.Size.Width / 3 , buttonSchlagwechsel.Size.Height);
            buttonAnsagen.Location = new Point(comboBoxAnsagen.Location.X + buttonSchlagwechsel.Size.Width, comboBoxAnsagen.Location.Y + comboBoxAnsagen.Size.Height + 5);

            buttonSchenasDrei.Size = new Size(comboBoxAnsagen.Size.Width / 4 * 3, buttonSchenasDrei.Size.Height);
            buttonSchenasDrei.Location = new Point(pictureBoxMeineKarte1.Location.X, comboBoxAnsagen.Location.Y);

            buttonBodenschauerGemma.Size = new Size(comboBoxAnsagen.Size.Width / 4 * 3, buttonSchenasDrei.Size.Height);
            buttonBodenschauerGemma.Location = new Point(pictureBoxMeineKarte1.Location.X, buttonSchlagwechsel.Location.Y);

            #endregion

            #region Infobox und Score

            //infobox and score
            labelInfo.Size = new Size(this.ClientSize.Width / 3, buttonAnsagen.Location.Y + buttonAnsagen.Size.Height - pictureBoxMainKarte.Location.Y);
            labelInfo.Location = new Point(pictureBoxMainKarte.Location.X + pictureBoxMainKarte.Size.Width, pictureBoxMainKarte.Location.Y - this.ClientSize.Height/20);

            labelScore.Size = new Size(this.ClientSize.Width / 4, this.ClientSize.Height / 10);
            labelScore.Font = new Font("Arial", this.ClientSize.Height / 20, FontStyle.Bold);
            labelScore.Location = new Point(0, 0);

            labelPlayerWait.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            labelPlayerWait.Font = new Font("Arial", this.ClientSize.Height / 15, FontStyle.Bold);
            labelPlayerWait.Location = new Point(0, 0);

            #endregion

        }

        private void ExecuteSecure(Action a) //if invoke is needed it is used else it is not 
        {
            if (InvokeRequired)
            {
                BeginInvoke(a);
            }
            else
            {
                a();
            }
        }

        private void mainForm_SizeChanged(object sender, EventArgs e)
        {
            PosSizeAnpassung();
        }

        #endregion

        #region Help and Game Tutorial (NEW)

        private void mainForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            helpForm hf = new helpForm();
            hf.ShowDialog();
            hf.Dispose();
        }

        #endregion

        #region Kartenimage Load 

        private Image LoadKartenImage(int x, int y, int width, int height)
        {
            Rectangle cloneRect = new Rectangle(x, y, width, height);
            System.Drawing.Imaging.PixelFormat pixelFormat = kartenBitmap.PixelFormat;
            if (x > 1534 - width || y > 5400 - height) { throw new Exception("Außerhalb des Images - Imageload Fehler, check die Image File auf Fehler/Falsche größe ab"); }
            Bitmap karteBitmap = kartenBitmap.Clone(cloneRect, pixelFormat);
            return karteBitmap;
        }

        #endregion

        #region Reset images from the cards in the middle

        private void ResetMitteKarten()
        {
                pictureBoxMainKarte.Image = midKarte;

                pictureBoxMainKarte1.Image = midSideKarte;
                pictureBoxMainKarte2.Image = midSideKarte;
                pictureBoxMainKarte3.Image = midSideKarte;
        }

        #endregion

        #region AI (NEW)

        private void AIPlace(byte difficulty, byte playernumber, List<wattKarte> wattRangliste)
        {
            Random rnd = new Random();
            int cardtolay = 0; //0-4 array pos, fallback to 0 if nothing is true

            if (difficulty == 1)
            {
                cardtolay = rnd.Next(0, wattMitspieler[playernumber].SpielerWattKarten.Count); //random card from the bots cards
            }
            else if (difficulty == 2)
            {
                if (wattRangliste == null)
                {
                    cardtolay = rnd.Next(0, wattMitspieler[playernumber].SpielerWattKarten.Count); //first card, random card
                }
                else
                {
                    List<PictureBox> tempboxes = new List<PictureBox>();
                    foreach (PictureBox pb in wattMainKartenBoxen)
                    {
                        tempboxes.Add(pb);

                        if (pb.Image == midSideKarte) //if there is no card, count is to determine which tactic to use
                        {
                            tempboxes.Remove(pb);
                        }
                    }

                    foreach (wattKarte wk in wattMitspieler[playernumber].SpielerWattKarten)
                    {
                        #region 2 Player
                        //2 player if the card of the bot is lower in index, than the card in the middle, lay it & also eleminate the possibility of the position being -1 ( not found )
                        if (wattMitspieler.Count == 2 && wattRangliste.IndexOf(wk)>-1 && wattRangliste.IndexOf(wk)< wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattRangliste.Where(w => w.Kartenimage == wattMainKartenBoxen[0].Image).FirstOrDefault())]))
                        {                           
                            cardtolay = wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wk);
                            break;
                        }
                        #endregion

                        #region 4 Player

                        else if(wattMitspieler.Count == 4)
                        {

                            //4 players is more difficult, only beat the cards that are not laid by the teammember
                            if (tempboxes.Count == 1)
                            {
                                if (wattRangliste.IndexOf(wk) > -1 && wattRangliste.IndexOf(wk) < wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattRangliste.Where(w => w.Kartenimage == wattMainKartenBoxen[0].Image).FirstOrDefault())]))
                                {
                                    cardtolay = wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wk);
                                    break;
                                }
                            }

                            #region 2 Cards on Table 
                            else if (tempboxes.Count == 2)
                            {
                                try
                                {
                                if (wattRangliste.Any(w => w.Kartenimage == wattMainKartenBoxen[1].Image)&&wattRangliste.IndexOf(wk) > -1 && wattRangliste.IndexOf(wk) < wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[1].Image).FirstOrDefault())]))
                                {
                                    //only lay if the teammate didn't already beat the card
                                    if (wattRangliste.Any(w => w.Kartenimage == wattMainKartenBoxen[0].Image)) //always have to check if it exists in the rangliste
                                    {
                                        try
                                        {
                                            if (wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattRangliste.Where(w => w.Kartenimage == wattMainKartenBoxen[0].Image).FirstOrDefault())]) > -1 && wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattRangliste.Where(w => w.Kartenimage == wattMainKartenBoxen[0].Image).FirstOrDefault())]) < wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattRangliste.Where(w => w.Kartenimage == wattMainKartenBoxen[1].Image).FirstOrDefault())]))
                                            {
                                                //if teammate laid better card, lay bad card
                                                foreach (wattKarte wak in wattMitspieler[playernumber].SpielerWattKarten)
                                                {
                                                    if (wattRangliste.IndexOf(wak) == -1)
                                                        cardtolay = wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wak);
                                                }
                                            }

                                            else
                                            {
                                                cardtolay = wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wk);
                                                break;
                                            }
                                        }
                                        catch (System.ArgumentOutOfRangeException) { } 
                                    }
                                }
                                else
                                {
                                    //if lay bad card
                                    foreach (wattKarte wak in wattMitspieler[playernumber].SpielerWattKarten)
                                    {
                                        if (wattRangliste.IndexOf(wak) == -1)
                                            cardtolay = wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wak);
                                    }
                                }

                                }
                                catch (System.ArgumentOutOfRangeException) { } //do not even give a single shit about this exception, it has absolutely no value
                            }
                            #endregion

                            #region 3 Cards on Table

                            else if (tempboxes.Count == 3)  //probably most difficult
                            {
                                int[] laidcards = new int[3] { 34, 34, 34 };
                                //ENEMY   
                                if(wattRangliste.Any(w => w.Kartenimage == wattMainKartenBoxen[0].Image)) //always have to check if it exists in the rangliste
                                {
                                    try //find better solution?
                                    {
                                        laidcards[0] = wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[0].Image).FirstOrDefault())]);
                                    }
                                    catch(System.ArgumentOutOfRangeException) { }
                                }
                                //TEAMMATE
                                if (wattRangliste.Any(w => w.Kartenimage == wattMainKartenBoxen[1].Image)) 
                                {
                                    try //find better solution?
                                    {
                                        laidcards[1] = wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[1].Image).FirstOrDefault())]);
                                    }
                                    catch (System.ArgumentOutOfRangeException) { }
                                }
                                //ENEMY
                                if (wattRangliste.Any(w => w.Kartenimage == wattMainKartenBoxen[2].Image)) 
                                {
                                    try //find better solution?
                                    {
                                        laidcards[2] = wattRangliste.IndexOf(wattRangliste[wattRangliste.IndexOf(wattKarten.Where(w => w.Kartenimage == wattMainKartenBoxen[2].Image).FirstOrDefault())]);
                                    }
                                    catch (System.ArgumentOutOfRangeException) { }
                                }

                                //if my teammate has the best card
                                if (laidcards[1] < laidcards[0] && laidcards[1] < laidcards[2])
                                {
                                    //lay bad card
                                    foreach (wattKarte wak in wattMitspieler[playernumber].SpielerWattKarten)
                                    {
                                        if (wattRangliste.IndexOf(wak) == -1)
                                            cardtolay = wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wak);
                                    }
                                }
                                else if (wattRangliste.IndexOf(wk) > -1 && wattRangliste.IndexOf(wk) < laidcards[0] && wattRangliste.IndexOf(wk) < laidcards[2])
                                {
                                    cardtolay= wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wk);
                                    break;
                                }
                                else
                                {
                                    //can't beat, lay bad card
                                    foreach (wattKarte wak in wattMitspieler[playernumber].SpielerWattKarten)
                                    {
                                        if (wattRangliste.IndexOf(wak) == -1)
                                            cardtolay = wattMitspieler[playernumber].SpielerWattKarten.IndexOf(wak);
                                    }
                                }

                                #endregion

                            }
                        }
                    }
                    #endregion

                }

                //this AI places cards that are higher in rank in wattrangliste, checks for cards, but does not always lay the perfect card
            }

            if (pictureBoxMainKarte.Image != midKarte) //for design purposes, looks weird if this is not done
            {
                pictureBoxMainKarte1.Image = pictureBoxMainKarte2.Image;
                pictureBoxMainKarte2.Image = pictureBoxMainKarte3.Image;
                pictureBoxMainKarte3.Image = pictureBoxMainKarte.Image;
            }
            
            //sets the card in the middle to the bots card
            pictureBoxMainKarte.Image = wattMitspieler[playernumber].SpielerWattKarten[cardtolay].Kartenimage;
            wattMitspieler[playernumber].SpielerWattKarten.Remove(wattMitspieler[playernumber].SpielerWattKarten[cardtolay]); //deletes the placed card from spielerwattkarten
            wattMitspieler[playernumber].Spielerturn = false;
            PosSizeAnpassung();

        }

        #endregion
       
        #region Move, switch and place cards

        private async void pictureBoxMeineKarte_MouseDown(object sender, MouseEventArgs e)
        {         
            PictureBox pb = (PictureBox)sender;
            if (pb.Image == null || e.Button == MouseButtons.Right)
                return; //picturebox with no image cannot be moves as well as if rightclick was clicked

            mousedown = true;
            pb.BringToFront();

            while (mousedown)
            { 
                pb.Location = new Point(PointToClient(Cursor.Position).X - pb.Size.Width / 2, PointToClient(Cursor.Position).Y - pb.Size.Height / 2); //cursor is in the middle of the picbox
                await Task.Delay(2);
            }
        }

        private void pictureBoxMeineKarte_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            //if it's my turn and the card is dragged in the middle then the card is placed
            if (pb.Location.X + pb.Size.Width / 2 > pictureBoxMainKarte.Location.X &&
                pb.Location.X + pb.Size.Width / 2 < pictureBoxMainKarte.Location.X + pictureBoxMainKarte.Size.Width &&
                pb.Location.Y + pb.Size.Height / 2 > pictureBoxMainKarte.Location.Y &&
                pb.Location.Y + pb.Size.Height / 2 < pictureBoxMainKarte.Location.Y + pictureBoxMainKarte.Size.Height)
            { 
                if (wattMitspieler[myplayernumber-1].Spielerturn)
                {
                    if(pictureBoxMainKarte.Image != midKarte) //for design purposes, looks weird if this is not doen
                    {
                    pictureBoxMainKarte1.Image = pictureBoxMainKarte2.Image;
                    pictureBoxMainKarte2.Image = pictureBoxMainKarte3.Image;
                    pictureBoxMainKarte3.Image = pictureBoxMainKarte.Image;
                    }

                    pictureBoxMainKarte.Image = pb.Image;
                    wattMitspieler[myplayernumber - 1].SpielerWattKarten.RemoveAll(w => w.Kartenimage == pb.Image); //deletes the placed card from spielerwattkarten
                    pb.Image = null;
                    wattMitspieler[myplayernumber - 1].Spielerturn = false;
                    PosSizeAnpassung(); 
                }
            }
            else 
            {
                for(int i = 0; i<wattMeineKartenBoxen.Count; i++) //switch cards
                {
                    if(pb.Location.X + pb.Size.Width / 2 > wattMeineKartenBoxen[i].Location.X &&
                    pb.Location.X + pb.Size.Width / 2 < wattMeineKartenBoxen[i].Location.X + wattMeineKartenBoxen[i].Size.Width &&
                    pb.Location.Y + pb.Size.Height / 2 > wattMeineKartenBoxen[i].Location.Y &&
                    pb.Location.Y + pb.Size.Height / 2 < wattMeineKartenBoxen[i].Location.Y + wattMeineKartenBoxen[i].Size.Height)
                    {
                        Image tempimage = pb.Image;
                        pb.Image = wattMeineKartenBoxen[i].Image;
                        wattMeineKartenBoxen[i].Image = tempimage;
                    }
                }
            }
                PosSizeAnpassung(); //put the picturebox back
                mousedown = false;
        }

        private void pictureBoxKarte_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (e.Button == MouseButtons.Right && wattMitspieler[myplayernumber-1].Spielerturn) //same as above just here the trigger is rightclick
            {
                if (pictureBoxMainKarte.Image != midKarte) 
                {
                    pictureBoxMainKarte1.Image = pictureBoxMainKarte2.Image;
                    pictureBoxMainKarte2.Image = pictureBoxMainKarte3.Image;
                    pictureBoxMainKarte3.Image = pictureBoxMainKarte.Image;
                }

                pictureBoxMainKarte.Image = pb.Image;
                wattMitspieler[myplayernumber - 1].SpielerWattKarten.RemoveAll(w => w.Kartenimage == pb.Image); 
                pb.Image = null;
                wattMitspieler[myplayernumber - 1].Spielerturn = false;
                PosSizeAnpassung(); 
            }
        }

        #endregion

        #region Display Stiche and Cards from other players 

        #region SetKartenDisplay

        private void SetKartenDisplay()
        {
            //everytime an image is set it's tag is saved so it won't be set to the same again

            //reset the cards to image 5
            if(kartenreset||(pictureBoxSpielerOben.Image.Tag == pictureBoxSpielerRechts.Image.Tag && pictureBoxSpielerOben.Image.Tag == pictureBoxSpielerLinks.Image.Tag && pictureBoxSpielerOben.Image.Tag == kartenAnzahlimages[0].Tag))
            {
                pictureBoxSpielerOben.Image = (Image)kartenAnzahlimages[5].Clone();
                pictureBoxSpielerLinks.Image = (Image)kartenAnzahlimages[5].Clone();
                pictureBoxSpielerRechts.Image = (Image)kartenAnzahlimages[5].Clone();
                pictureBoxSpielerOben.Image.Tag = kartenAnzahlimages[5].Tag;
                pictureBoxSpielerRechts.Image.Tag = kartenAnzahlimages[5].Tag;
                pictureBoxSpielerLinks.Image.Tag = kartenAnzahlimages[5].Tag;

                Invoke(new Action(() =>
                {
                          pictureBoxSpielerOben.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                          pictureBoxSpielerLinks.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                          pictureBoxSpielerRechts.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);                  
                }));

                kartenreset = false;
            }

            for (int i = 0; i < 6; i++)
            {
                for(int o = 1; o<wattMitspieler.Count+1; o++)
                {
                        if (pictureBoxSpielerOben.Name == "Spieler " + o && wattMitspieler[o-1].SpielerWattKarten.Count == i && pictureBoxSpielerOben.Image.Tag != kartenAnzahlimages[i].Tag)
                        {
                        Invoke(new Action(() =>
                            {
                                pictureBoxSpielerOben.Image = (Image)kartenAnzahlimages[i].Clone();
                                pictureBoxSpielerOben.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                pictureBoxSpielerOben.Image.Tag = kartenAnzahlimages[i].Tag;
                            }));
                            return;
                        }

                        if (pictureBoxSpielerLinks.Name == "Spieler " + o && wattMitspieler[o - 1].SpielerWattKarten.Count == i && pictureBoxSpielerLinks.Image.Tag != kartenAnzahlimages[i].Tag)
                        {
                        Invoke(new Action(() =>
                            {
                                pictureBoxSpielerLinks.Image = (Image)kartenAnzahlimages[i].Clone();
                                pictureBoxSpielerLinks.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                pictureBoxSpielerLinks.Image.Tag = kartenAnzahlimages[i].Tag;
                            }));
                        return;
                        }

                        if (pictureBoxSpielerRechts.Name == "Spieler " + o && wattMitspieler[o - 1].SpielerWattKarten.Count == i && pictureBoxSpielerRechts.Image.Tag != kartenAnzahlimages[i].Tag)
                        {
                        Invoke(new Action(() =>
                            {
                                pictureBoxSpielerRechts.Image = (Image)kartenAnzahlimages[i].Clone();
                                pictureBoxSpielerRechts.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                pictureBoxSpielerRechts.Image.Tag = kartenAnzahlimages[i].Tag;
                            }));
                        return;
                     }
                }
            }
        }

        #endregion

        private void labelMeineStiche_MouseHover(object sender, EventArgs e) //hover stichdoku for me
        {
            Label lb = (Label)sender;
            tt.SetToolTip(lb, wattMitspieler[myplayernumber - 1].Stichdoku);
        }

        private void pictureBoxSpieler_MouseHover(object sender, EventArgs e) //hover stichdoku for other players
        {
            PictureBox pb = (PictureBox)sender;

            if (pb.Name == "Spieler 1" && GetSpielerStiche(pb.Name) != 0)
                tt.SetToolTip(pb, wattMitspieler[0].Stichdoku);
            else if (pb.Name == "Spieler 2" && GetSpielerStiche(pb.Name) != 0)
                tt.SetToolTip(pb, wattMitspieler[1].Stichdoku); 
            else if(wattMitspieler.Count == 4 && GetSpielerStiche(pb.Name)!=0)
            { 
            if (pb.Name == "Spieler 3")
                tt.SetToolTip(pb, wattMitspieler[2].Stichdoku);
            else if (pb.Name == "Spieler 4")
                tt.SetToolTip(pb, wattMitspieler[3].Stichdoku);
            }
            else
                tt.SetToolTip(pb,"Keine Stiche vorhanden.");

            //everybody gets shown other data because spieler 1,2,3,4 is not at the same position

        }

        private void labelMeineStiche_Paint(object sender, PaintEventArgs e)
        {
            //nothing is actually graphically drawn the paint event is just used to set the number un the label so it can be called with Refresh();
            if (GetSpielerStiche("Spieler " + myplayernumber)!=0)
            {
                labelMeineStiche.Text = GetSpielerStiche("Spieler " + myplayernumber).ToString();
            }
            else
            {
                labelMeineStiche.Text = "";
            }
            
        }

        private void pictureBoxSpielerLinks_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (!pb.Enabled || wattMitspieler.Count != 4 || GetSpielerStiche(pb.Name) == 0)
                return;
            //bei links und rechts muss gecheckt werden ob 4 spieler aktiv sind, und wenn sie keine stichee haben wird nix angezeigt

            using (Font myFont = new Font("Arial", labelScore.Font.Size / 3 * 2, FontStyle.Bold)) //2/3 von der schriftgröße im label score
            {
                e.Graphics.DrawString(GetSpielerStiche(pb.Name).ToString(), myFont, Brushes.Black, new Point(pictureBoxSpielerLinks.Width/3*2,0));
            }
        }

        private void pictureBoxSpielerRechts_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (!pb.Enabled || wattMitspieler.Count != 4 || GetSpielerStiche(pb.Name) == 0)
                return;

            using (Font myFont = new Font("Arial", labelScore.Font.Size / 3 * 2, FontStyle.Bold))
            {
                e.Graphics.DrawString(GetSpielerStiche(pb.Name).ToString(), myFont, Brushes.Black, new Point(0, 0));
            }
        }

        private void pictureBoxSpielerOben_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (GetSpielerStiche(pb.Name) == 0)
                return;
            //bei links und rechts muss gecheckt werden ob 4 spieler modus aktiv ist, und wenn sie keine stichee haben wird nix angezeigt

            using (Font myFont = new Font("Arial", labelScore.Font.Size / 3 * 2, FontStyle.Bold))
            {
                e.Graphics.DrawString(GetSpielerStiche(pb.Name).ToString(), myFont, Brushes.Black, new Point(0, pictureBoxSpielerOben.Height/2));
            }
        }

        //left and right are only displayed if active and if the player has 0 stiche it's not displayed either

        private int GetSpielerStiche(string pbname)
        {

            if (pbname == "Spieler 1") 
                return wattMitspieler[0].StichAnzahl;
            else if (pbname == "Spieler 2")
                return wattMitspieler[1].StichAnzahl;

            if (wattMitspieler.Count == 4)
            { 
            if (pbname == "Spieler 3")
                return wattMitspieler[2].StichAnzahl;
            else if (pbname == "Spieler 4")
                return wattMitspieler[3].StichAnzahl;
            }

            return 0;
        }

        #endregion

        #region Display Card name on Hover

        private void pictureBoxKarten_Hover(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            
            if(pb.Image != midKarte && pb.Image != midSideKarte && pb.Image != null)
            {
                int index = wattKarten.IndexOf(wattKarten.Where(wk => wk.Kartenimage == pb.Image).FirstOrDefault());

                if (index == -1) 
                { Console.WriteLine("Index fehler bei pbkhover bitte melden");  return; }

                tt.SetToolTip(pb, wattKarten[index].Farbe + " " + wattKarten[index].Zahl);          
            }
            else //so emtpy image does not contain a toolip
            {
                tt.Hide(pb);
            }
        }

        #endregion

        #region Keyboard Input MainForm

        private void mainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                if(this.WindowState == FormWindowState.Maximized && this.FormBorderStyle ==  FormBorderStyle.None)
                {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                }

                else
                {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                }
            }

            if(e.KeyCode == Keys.Enter)
            {
                labelPlayerWait.Visible = false;
            }

            //when testing out in sandbox mode, can switch to all players and modify cards
            #region Sandbox mode

            if(e.KeyCode == Keys.NumPad0&&sandbox)
            {
                foreach(wattSpieler ws in wattMitspieler)
                {
                    Console.WriteLine(wattMitspieler.IndexOf(ws));
                    foreach(wattKarte wk in ws.SpielerWattKarten)
                    {
                        Console.Write("| "+wk.Farbe+" "+wk.Zahl);
                    }
                    Console.WriteLine();
                }
            }

            if ((e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.NumPad4)&&sandbox)
            {
                foreach(PictureBox pb in wattMeineKartenBoxen)
                {
                    pb.Image = null;
                }

                if (e.KeyCode == Keys.NumPad1 && (wattMitspieler.Count == 2 || wattMitspieler.Count == 4))
                {
                    myplayernumber = 1;
                    for (int i = 0; i < wattMitspieler[0].SpielerWattKarten.Count; i++)
                    {
                        wattMeineKartenBoxen[i].Image = wattMitspieler[0].SpielerWattKarten[i].Kartenimage;
                    }

                }
                if (e.KeyCode == Keys.NumPad2 && (wattMitspieler.Count == 4))
                {
                    myplayernumber = 3;
                    for (int i = 0; i < wattMitspieler[2].SpielerWattKarten.Count; i++)
                    {
                        wattMeineKartenBoxen[i].Image = wattMitspieler[2].SpielerWattKarten[i].Kartenimage;
                    }
                }
                if (e.KeyCode == Keys.NumPad3 && (wattMitspieler.Count == 4 || wattMitspieler.Count == 2))
                {
                    myplayernumber = 2;
                    for (int i = 0; i < wattMitspieler[1].SpielerWattKarten.Count; i++)
                    {
                        wattMeineKartenBoxen[i].Image = wattMitspieler[1].SpielerWattKarten[i].Kartenimage;
                    }
                }
                if (e.KeyCode == Keys.NumPad4 && wattMitspieler.Count == 4)
                {
                    myplayernumber = 4;
                    for (int i = 0; i < wattMitspieler[3].SpielerWattKarten.Count; i++)
                    {
                        wattMeineKartenBoxen[i].Image = wattMitspieler[3].SpielerWattKarten[i].Kartenimage;
                    }
                }

                PosSizeAnpassung();
            }
            #endregion
        }

        #endregion

        #region NET (INACTIVE)
         /*
        private string GetLocalIPv4(NetworkInterfaceType _type) //thanks to https://stackoverflow.com/questions/6803073/get-local-ip-address wouldn't have gotten it that well on my own
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.GetIPProperties().GatewayAddresses.FirstOrDefault() != null)
                {
                    if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                output = ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            return output;
        }

        private bool Send(string hostip, string text)
        {
            TcpClient tcpclnt = new TcpClient();
            Stream stm;
            byte[] ba = Encoding.UTF8.GetBytes(text);

            if (hostip == GetLocalIPv4(NetworkInterfaceType.Ethernet) || hostip == GetLocalIPv4(NetworkInterfaceType.Wireless80211)) //can't connect to own ip lmo
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

        private void Listen(string myip)
        {
            IPAddress iplisten = IPAddress.Parse(myip); 
            TcpListener listener = new TcpListener(iplisten, 7777);
            try
            { 
            listener.Start();
            }
            catch
            {
                Console.WriteLine("Couldn't start listener");
                return;
            }

            while (true)
            {
                Listening(listener);
            }


        } //for getting data

        private void Listening(TcpListener listener)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient(); //connection accepting

                NetworkStream nwStream = client.GetStream();

                byte[] buffer = new byte[client.ReceiveBufferSize]; //buffer byte 

                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                var dataRecieved = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                ExecuteSecure(() => cf.DataRecieved = dataRecieved);
                ExecuteSecure(() => this.dataRecieved = dataRecieved);
            }
            catch
            {
                Console.WriteLine("Couldn't recieve data, trying again");
            }
        }

        private void ExecuteSecure(Action a) //if invoke is needed it is used else it is not 
        {
            if (InvokeRequired)
            {
                BeginInvoke(a);
            }
            else
            {
                a();
            }
        }
        */
        #endregion
    }
}
