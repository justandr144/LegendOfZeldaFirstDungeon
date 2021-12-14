using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendOfZeldaFirstDungeon
{
    public partial class Room1 : UserControl
    {
        Link player = new Link(Form1.playerX, Form1.playerY, Form1.playerSize, Form1.playerSpeed, Form1.playerHealth, Form1.playerDirect);
        public Room1()
        {
            InitializeComponent();
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Room1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.UpDoor, 397, 197);
        }
    }
}
