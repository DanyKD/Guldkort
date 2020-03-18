using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guldkort
{
    public partial class Form1 : Form
    {
        /// <summary>
        ///  Identifiera FileLoader objekt för att använda det när vi vill jämföra med strängen som skicka från klient.
        /// </summary>
        FileLoader fileLoader = new FileLoader();
        /// <summary>
        /// TcpListener,TcpClient, port variabler är använda de för att ansluta mellan serevern och kllienten
        /// </summary>
        TcpListener listener;
        TcpClient client;
        int port = 12345;

        public Form1()
        {
            InitializeComponent();
            
        }

          /*
         *Asynkron metod för att starta ta emot informationen från klient
         */
        public async void StartRecieving()
        {
            try
            {
                //Accepterar en väntande anslutningsbegäran som en asynkron operation.
                client = await listener.AcceptTcpClientAsync();
            }
            catch (Exception error)
            {
                // om accepterar inte 
                MessageBox.Show(error.Message, Text);
                return;
            }

            // börja läsa strängen som ta mot från klienten
            StartReading(client);
        }

        /*
         * den metod för att läsa klient medelande och lägga till listbox
         */
        public async void StartReading(TcpClient k)
        {
            byte[] buffert = new byte[1024];
            int n = 0;
            try
            {
                n = await k.GetStream().ReadAsync(buffert, 0, buffert.Length);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }
            // ta emot data från klienten och spara den på strängen
            string incoming = Encoding.Unicode.GetString(buffert, 0, n);
            // skicka resultat till klienten efter jämföra på Getresult metoden
            StartSending(GetResult(incoming),k);
            // börja ta emot data från klienten om det skickar.
            StartReading(k);
        }

        /// <summary>
        /// metpden som skicka sträng till klienten
        /// </summary>
        /// <param name="message">medelande som skicka till klienten</param>
        /// <param name="client">klienten som ska skicka medelande till</param>
        public static async void StartSending(string message, TcpClient client)
        {
            byte[] utData = Encoding.Unicode.GetBytes(message);

            try
            {
                await client.GetStream().WriteAsync(utData, 0, utData.Length);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return;
            }
        }

        //knappen som ska starta serevern ach börja ta emot från klienter.
        private void btnServer_Click(object sender, EventArgs e)
        {
            try
            {
                 IPAddress adress = IPAddress.Parse("127.0.0.1");
                 listener = new TcpListener(adress, port);
                 listener.Start();
            }  
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }
             btnServer.Enabled = false;
            btnServer.Text = "Uppkopplad";
             btnServer.BackColor = Color.LightGreen; 
             StartRecieving();
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientdata">medelande som ta emot från klienten för att kolla om </param>
        /// <returns>medelande som skicka till klienten </returns>
        public string GetResult(string clientdata)
        {
            try
            {
            //dela data som ta emot från klienten till två stränger kortnummer och kontonummer
            string[] tempData = clientdata.Split(new string[] { "-" }, StringSplitOptions.None);

            string name = "nothing";
            string city = "nothing";
            bool findAccount = false;
                // loopen för att läsa alla elementer i konto listen
            foreach (var account in fileLoader.AccountList)
            {
                    //kolla om kontonummer är finns på konto lista
                if(tempData[0].Equals(account.UserId))
                {
                     findAccount = true;
                     name = account.Name;
                     city = account.City;
                }
            }

            string type = "nothing";
            bool findCard = false;
                // loopen för att läsa alla elementer i kort listen
                foreach (var card in fileLoader.CardList)
            {
                    //kolla om kortnummer är finns på kort lista
                    if (findAccount && tempData[1].Equals(card.Id))
                    {
                        findCard = true;
                        type = card.Type;
                        return  "\r\nGrattis: " +name+
                                           "\r\nDu har vunnit det exklusiva \r\n" +
                                           type +"-Kortet. "+
                                           "\r\nDu kan hämta det i din\r\n"
                                            + "lokala spelbutik i " + city; 
                    }
                    
            }
                //return findAccount.ToString()+"###"+findCard.ToString()+ "###"+name+ "###"+city+ "###"+type;
                return "Tyvärr, du är inte vunnit.";
            }
            catch(Exception)
            {
                MessageBox.Show("Det finns nånting fel.");
                return "Det finns nånting fel.";
            }
           
        }

        
    }
}
