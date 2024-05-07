using System;
using Newtonsoft;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Knuckles
{
    public partial class Authorization : Form
    {
        public string name {  get; set; }
        public User user { get; private set; }
        public Authorization()
        {
            InitializeComponent();
        }

        private User CheckNameInJson()
        {
            var json = File.ReadAllText("../../Resources/Users.json");

            var data = JsonConvert.DeserializeObject<List<User>>(json);

            if(data != null)
            {
                foreach (var item in data)
                {
                    if (item.name == name)
                    {
                        MessageBox.Show("Вход разрешен");
                        DialogResult = DialogResult.OK;
                        user = item;
                        return user;
                    }
                }

                user = new User(name, 0);
                data.Add(user);
                json = JsonConvert.SerializeObject(data);

                File.WriteAllText("../../Resources/Users.json", json);
                MessageBox.Show("Пользователь зарегистрирован");

                return user;
            }
            else
            {
                user = new User(name, 0);
                List<User> users = new List<User>();
                users.Add(user);
                json = JsonConvert.SerializeObject(users);

                File.WriteAllText("../../Resources/Users.json", json);
                MessageBox.Show("Пользователь зарегистрирован");
                return user;
            }
        }

        private void bt_accept_Click(object sender, EventArgs e)
        {
            name = tb_nick.Text;
            CheckNameInJson();
        }
    }
}
