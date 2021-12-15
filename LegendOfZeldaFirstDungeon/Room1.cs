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
        Link player = new Link(Form1.playerX, Form1.playerY, Form1.playerSize, Form1.playerSpeed, Form1.playerHealth, Form1.playerImage, Form1.playerDirect);

        bool upArrowDown, downArrowDown, leftArrowDown, rightArrowDown = false;
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
                    upArrowDown = true;
                    break;
                case (Keys.Down):
                    downArrowDown = true;
                    break;
                case (Keys.Left):
                    leftArrowDown = true;
                    break;
                case (Keys.Right):
                    rightArrowDown = true;
                    break;
            }
        }

        private void Room1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Up):
                    upArrowDown = false;
                    break;
                case (Keys.Down):
                    downArrowDown = false;
                    break;
                case (Keys.Left):
                    leftArrowDown = false;
                    break;
                case (Keys.Right):
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            if (upArrowDown)
            {
                player.Move("up");
            }
            if (downArrowDown)
            {
                player.Move("down");
            }
            if (leftArrowDown)
            {
                player.Move("left");
            }
            if (rightArrowDown)
            {
                player.Move("right");
            }

            Refresh();
        }

        private void Room1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.UpDoor, 397, 197);
            e.Graphics.DrawImage(Properties.Resources.LinkDown1, player.x, player.y);
        }
    }
}
