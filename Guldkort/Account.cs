using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldkort
{
    class Account
    {
        /// <summary>
        /// de tre egenskaper sparar kontolista fil elementen
        /// </summary>
        internal string UserId;
        internal string Name;
        internal string City;

        // klass konstruktor 
        public Account(string userId, string name, string city)
        {
            UserId = userId;
            Name = name;
            City = city;
        }
        // använda den metoden när vi vill skriva ut klassen
        public override string ToString()
        {
            return UserId + "###" + Name + "###" + City;
        }
    }
}
