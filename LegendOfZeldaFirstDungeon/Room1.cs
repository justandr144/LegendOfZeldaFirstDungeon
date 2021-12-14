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
            player.x = 400;
            player.y = 300;
        }

        private void Room1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Up):
                    player.Move("up");
                    break;
                case (Keys.Down):
                    player.Move("down");
                    break;
                case (Keys.Left):
                    player.Move("left");
                    break;
                case (Keys.Right):
                    player.Move("right");
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {

            Refresh();
        }

        private void Room1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.UpDoor, 397, 197);
            e.Graphics.DrawImage(Properties.Resources.LinkDown1, player.x, player.y);
        }
    }
}
