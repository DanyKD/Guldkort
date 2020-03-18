using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Guldkort
{
    class FileLoader
    {
        /// <summary>
        /// jag ska läsa kortlista och kontolista filer och spara dem på de två listar Cardlist,Accountlist.
        /// </summary>
        public List<Card> CardList = new List<Card>();
        public List<Account> AccountList = new List<Account>();

        /// <summary>
        /// klass struktoren användar de två metoder för att läsa filer och lägga de till två listar när ladda form upp
        /// </summary>
        public FileLoader()
        {
            // läsa kontolista fil och lägga den till AccountList lista
            ReadAccountList();
            // läsa kortlista fil och lägga den till CardList lista
            ReadCardList();
        }
        /// <summary>
        /// använda den metoden för att läsa filer 
        /// vi ska använda det en till kortlista filen och en för kontolista filen. 
        /// </summary>
        /// <param name="filename"> fil som ka skicka för att läsa och spara på listen</param>
        /// <returns></returns>
        public List<string[]> ReadFromFile(string filename) 
        {
            //Skapa lista där kort och konto ska sparas tillfälligt
            List<string[]> listfromfile = new List<string[]>();
            List<string> itemSaver = new List<string>();
            try
            {

                // kontrollera om filen finns
                if (File.Exists(filename))
                {
                    // läs alla rader i filen och lägg den i listan
                    StreamReader reader = new StreamReader(filename, Encoding.Default, true);
                    string item = "";
                    while ((item = reader.ReadLine()) != null)
                    {
                        itemSaver.Add(item);
                    }
                    //dela varje rad till vektor
                    foreach (string a in itemSaver)
                    {
                        string[] fileToArray = a.Split(new string[] { "###" }, StringSplitOptions.None);
                        listfromfile.Add(fileToArray);
                    }
                    //stäng filen
                    reader.Close();
                    return listfromfile;
                }
                else
                {
                    //om det finns inegn fil för att läsa
                    MessageBox.Show("Det finns ingen fil som heter: " + filename + ". ");
                    return null;
                }
            }
            catch(Exception)
            {
                //om det finns fel på filen när dela den efter läsa den
                MessageBox.Show("Det finns fel på filen som heter: " + filename + ". ");
                return null;
            }
        }
        /// <summary>
        /// spara CardList data att använda ReadFromFile metoden 
        /// </summary>
        public void ReadCardList()
        {
            List<string[]> list=ReadFromFile("kortlista.txt");
            try
            {
            // använda swtich för att fixa typen av Card klass på CardList lista och spara dem i CardList
            foreach (var  item in list)
            {
               switch (item[1])
                {
                    case "Dunderkatt":
                        CardList.Add(new Card.Dunderkatt(item[0]));
                        break;
                    case "Kristallhäst":
                        CardList.Add(new Card.Kristallhäst(item[0]));
                        break;
                    case "Överpanda":
                        CardList.Add(new Card.Överpanda(item[0]));
                        break;
                    case "Eldtomat":
                        CardList.Add(new Card.Eldtomat(item[0]));
                        break;
                    default:
                        break;
                }
            }
            }
            catch (Exception)
            {
                //om det finns fel på filen när dela den efter läsa den
                MessageBox.Show("Det finns fel på filen som heter: kortlista. ");
            }
        }
        /// <summary>
        /// spara AccountList data att använda ReadFromFile metoden 
        /// </summary>
        public void ReadAccountList()
        {
            List<string[]> list = ReadFromFile("kundlista.txt");
            try
            {
            // spara varja elementet som är Account klass i AccountList
            foreach (var kund in list)
            {
                AccountList.Add(new Account(kund[0],kund[1],kund[2]));
            }
            }
            catch (Exception)
            {
                //om det finns fel på filen när dela den efter läsa den
                MessageBox.Show("Det finns fel på filen som heter: kontolista. ");
            }
        }


    }
}
