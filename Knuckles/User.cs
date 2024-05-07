using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuckles
{
    public class User
    {
        public string name { get; private set; }
        public int money { get; set; }

        public User(string name, int money)
        {
            this.name = name;
            this.money = money;
        }
    }
}
