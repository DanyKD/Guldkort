using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldkort
{
        //Abstract Card klass för att spara kortlista fil elementen.
        public abstract class Card
        {
            internal string Id;
            internal string Type;
            // klss konstruktoren
            public Card(string id)
            {
                Id = id;
                
            }

            //arvklass Dunderkatt
            internal class Dunderkatt : Card
            {
            /// <summary>
            /// klass konstruktorn baserat om överklassen men typen blir "Dunderkatt"
            /// </summary>
            /// <param name="id"></param>
            internal Dunderkatt(string id) :
                    base(id)
                {
                    Type = "Dunderkatt";
                }
            // använda den metoden när vi vill skriva ut klassen
            public override string ToString()
                {
                    return Id + "###" + Type;
                }
            }

        //arvklass Kristallhäst
        internal class Kristallhäst : Card
            {
            /// <summary>
            /// klass konstruktorn baserat om överklassen men typen blir "Kristallhäst"
            /// </summary>
            /// <param name="id"></param>
            internal Kristallhäst(string id) :
                    base(id)
                {
                    Type = "Kristallhäst";
                }
            // använda den metoden när vi vill skriva ut klassen
            public override string ToString()
                {
                    return Id + "###" + Type;
                }
            }

        //arvklass Överpanda
        internal class Överpanda : Card
            {
            /// <summary>
            /// klass konstruktorn baserat om överklassen men typen blir "Överpanda"
            /// </summary>
            /// <param name="id"></param>
            internal Överpanda(string id) :
                    base(id)
                {
                    Type = "Överpanda";
                }

            // använda den metoden när vi vill skriva ut klassen
            public override string ToString()
                {
                    return Id + "###" + Type;
                }
            }

        //arvklass Överpanda
        internal class Eldtomat : Card
            {
            /// <summary>
            /// klass konstruktorn baserat om överklassen men typen blir "Eldtomat"
            /// </summary>
            /// <param name="id"></param>
            internal Eldtomat(string id) :
                    base(id)
                {
                    Type = "Eldtomat";
                }

                // använda den metoden när vi vill skriva ut klassen
                public override string ToString()
                {
                    return Id + "###" + Type;
                }
            }
        }
}
