using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knuckles
{
    public partial class ResultOfTheGame : UserControl
    {
        public ResultOfTheGame(bool win, int point1, int point2)
        {
            InitializeComponent();

            if (win)
            {
                label1.Visible = true;
                label2.Visible = false;

                label3.Text = point1.ToString();
                label4.Text = point2.ToString();
            }
            else
            {
                label1.Visible = false;
                label2.Visible = true;

                label3.Text = point1.ToString();
                label4.Text = point2.ToString();
            }
        }
    }
}
