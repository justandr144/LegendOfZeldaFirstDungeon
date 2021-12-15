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
        #region Global Variables
        Link player = new Link(Form1.playerX, Form1.playerY, Form1.playerSpeed, Form1.playerHealth, Form1.playerDirect);

        bool upArrowDown, downArrowDown, leftArrowDown, rightArrowDown, bDown = false;
        bool movement = false;
        bool attack = false;
        int spriteLoop = 0;

        public static int enemyX = 400;
        public static int enemyY = 400;
        public static int enemySpeed = 0;
        public static int enemyHealth = 3;

        Enemy keese1 = new Enemy(enemyX, enemyY, enemySpeed, enemyHealth);
        #endregion
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
                case (Keys.B):
                    bDown = true;
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
                case (Keys.B):
                    bDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            movement = false;
            attack = false;

            if (bDown)
            {
                attack = true;
            }

            if (upArrowDown && attack == false && player.y > 260)
            {
                movement = player.Move("up");
                player.direction = "up";
            }
            else if (upArrowDown && attack == false && player.x >= 440 && player.x <= 480)
            {
                movement = player.Move("up");
                player.direction = "up";
            }
            if (downArrowDown && attack == false && player.y < 540)
            {
                movement = player.Move("down");
                player.direction = "down";
            }
            if (leftArrowDown && attack == false && player.x > 157)
            {
                movement = player.Move("left");
                player.direction = "left";
            }
            if (rightArrowDown && attack == false && player.x < 760)
            {
                movement = player.Move("right");
                player.direction = "right";
            }

            if (movement)
            {
                spriteLoop++;
            }
            else
            {
                spriteLoop = 0;
            }
            if (spriteLoop > 19)
            {
                spriteLoop = 0;
            }

            Refresh();
        }

        private void Room1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.UpDoor, 397, 197);
            if (attack)
            {
                switch (player.direction)
                {
                    case "up":
                        e.Graphics.DrawImage(Properties.Resources.LinkAttackUp, player.x, player.y - 42);
                        break;
                    case "down":
                        e.Graphics.DrawImage(Properties.Resources.LinkAttackDown, player.x, player.y);
                        break;
                    case "left":
                        e.Graphics.DrawImage(Properties.Resources.LinkAttackLeft, player.x - 42, player.y);
                        break;
                    case "right":
                        e.Graphics.DrawImage(Properties.Resources.LinkAttackRight, player.x, player.y);
                        break;
                }
            }
            else if (movement)
            {
                switch (player.direction)
                {
                    case "up":
                        if (spriteLoop >= 0 && spriteLoop <= 9)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkUp2, player.x, player.y);
                        }
                        else if (spriteLoop >= 10 && spriteLoop <= 19)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkUp1, player.x, player.y);
                        }
                        break;
                    case "down":
                        if (spriteLoop >= 0 && spriteLoop <= 9)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkDown2, player.x, player.y);
                        }
                        else if (spriteLoop >= 10 && spriteLoop <= 19)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkDown1, player.x, player.y);
                        }
                        break;
                    case "left":
                        if (spriteLoop >= 0 && spriteLoop <= 9)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkLeft2, player.x, player.y);
                        }
                        else if (spriteLoop >= 10 && spriteLoop <= 19)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkLeft1, player.x, player.y);
                        }
                        break;
                    case "right":
                        if (spriteLoop >= 0 && spriteLoop <= 9)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkRight1, player.x, player.y);
                        }
                        else if (spriteLoop >= 10 && spriteLoop <= 19)
                        {
                            e.Graphics.DrawImage(Properties.Resources.LinkRight2, player.x, player.y);
                        }
                        break;
                }
            }
            else
            {
                switch (player.direction)
                {
                    case "up":
                        e.Graphics.DrawImage(Properties.Resources.LinkUp1, player.x, player.y);
                        break;
                    case "down":
                        e.Graphics.DrawImage(Properties.Resources.LinkDown1, player.x, player.y);
                        break;
                    case "left":
                        e.Graphics.DrawImage(Properties.Resources.LinkLeft1, player.x, player.y);
                        break;
                    case "right":
                        e.Graphics.DrawImage(Properties.Resources.LinkRight2, player.x, player.y);
                        break;
                }
            }

            e.Graphics.DrawImage(Properties.Resources.LinkAttackDown, enemyX, enemyY);
        }
    }
}
