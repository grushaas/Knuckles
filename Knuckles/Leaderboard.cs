using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Knuckles
{
    public partial class Leaderboard : Form
    {
        public Leaderboard()
        {
            InitializeComponent();

            var json = File.ReadAllText("../../Resources/Users.json");
            var data = JsonConvert.DeserializeObject<List<User>>(json);
            
            if (data != null)
            {
                data.Sort((x1, x2) => x2.money.CompareTo(x1.money));

                int i = 1;
                foreach (var item in data)
                {
                    if (item != null)
                    {
                        lbox_users.Items.Add($"{i}: Имя: {item.name}, монеты: {item.money}");
                        i++;
                    }
                }
            }

        }
    }
}
