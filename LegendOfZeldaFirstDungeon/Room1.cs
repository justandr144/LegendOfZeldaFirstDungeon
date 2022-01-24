using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LegendOfZeldaFirstDungeon
{
    public partial class Room1 : UserControl
    {
        #region Global Variables
        Link player = new Link(Form1.playerX, Form1.playerY, Form1.playerSpeed, 56, Form1.playerHealth, Form1.playerDirect);

        bool upArrowDown, downArrowDown, leftArrowDown, rightArrowDown, bDown = false;
        bool movement = false;
        bool attack = false;
        int spriteLoop = 0;
        int playImmune = 0;
        int attackCool = 0;
        int deathLoop = 0;

        List<Enemy> enemies = new List<Enemy>();
        List<Death> deaths = new List<Death>();

        SolidBrush testBrush = new SolidBrush(Color.Red);
        Font testFont = new Font("Arial", 16);

        System.Windows.Media.MediaPlayer attackSound;
        #endregion
        public Room1()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            Enemy keese1 = new Enemy(400, 500, 5, 56, 14, 0, 1, 0, "keese");
            enemies.Add(keese1);

            player.x = 400;
            player.y = 300;

            attackSound = new System.Windows.Media.MediaPlayer();
            attackSound.Open(new Uri(Application.StartupPath + "/Resources/AttackSound.mp3"));
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

            Attack();

            Movement();

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].counter++;
                enemies[i].Move(enemies[i]);
            }

            foreach (Enemy i in enemies)
            {
                if (player.Collision(i) && playImmune < 1)
                {
                    player.health--;
                    playImmune = 50;
                }
            }

            if (playImmune > 0)
            {
                playImmune--;
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].immune > 0)
                {
                    enemies[i].immune--;
                }

                if (enemies[i].health <= 0)
                {
                    Death death = new Death(enemies[i].x, enemies[i].y);
                    deaths.Add(death);

                    deathLoop = 36;
                    enemies.RemoveAt(i);
                }
            }

            if (deathLoop > 0)
            {
                deathLoop--;
            }

            Refresh();
        }

        private void Room1_Paint(object sender, PaintEventArgs e) //death order 1, 2, 8, 3, 8, 3, 4, 5, 6, 9, 7, 1
        {
            e.Graphics.DrawImage(Properties.Resources.UpDoor, 397, 197);
            //e.Graphics.FillRectangle(testBrush, enemies[0].x, enemies[0].y, enemies[0].width, enemies[0].height);
            //e.Graphics.FillRectangle(testBrush, player.x + 47, player.y + 21, 48, 24);

            #region Death Animation
            foreach (Death d in deaths)
            {
                if (deathLoop <= 36 && deathLoop >= 34)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death1, d.x, d.y);
                }
                else if (deathLoop <= 33 && deathLoop >= 31)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death2, d.x, d.y);
                }
                else if (deathLoop <= 30 && deathLoop >= 28)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death8, d.x, d.y);
                }
                else if (deathLoop <= 27 && deathLoop >= 25)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death3, d.x, d.y);
                }
                else if (deathLoop <= 24 && deathLoop >= 22)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death8, d.x, d.y);
                }
                else if (deathLoop <= 21 && deathLoop >= 19)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death3, d.x, d.y);
                }
                else if (deathLoop <= 18 && deathLoop >= 16)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death4, d.x, d.y);
                }
                else if (deathLoop <= 15 && deathLoop >= 13)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death5, d.x, d.y);
                }
                else if (deathLoop <= 12 && deathLoop >= 10)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death6, d.x, d.y);
                }
                else if (deathLoop <= 9 && deathLoop >= 7)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death9, d.x, d.y);
                }
                else if (deathLoop <= 6 && deathLoop >= 4)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death7, d.x, d.y);
                }
                else if (deathLoop <= 3 && deathLoop >= 1)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death1, d.x, d.y);
                }
            }

            #endregion

            #region Player Movement
            if (playImmune == 0 || playImmune > 0 && playImmune < 5 || playImmune > 10 && playImmune < 15 || playImmune > 20 && playImmune < 25 || playImmune > 30 && playImmune < 35 || playImmune > 40 && playImmune < 45)
            {
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
            }
            #endregion

            #region Enemy Movement
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].health > 0)
                {
                    switch (enemies[i].name)
                    {
                        case "keese":
                            if (enemies[i].counter >= 0 && enemies[i].counter <= 4 || enemies[i].counter >= 10 && enemies[i].counter <= 14 || enemies[i].counter >= 20 && enemies[i].counter <= 24 || enemies[i].counter >= 30 && enemies[i].counter <= 34 || enemies[i].counter >= 40 && enemies[i].counter <= 44)
                            {
                                e.Graphics.DrawImage(Properties.Resources.Keese1, enemies[i].x, enemies[i].y);
                            }
                            else
                            {
                                e.Graphics.DrawImage(Properties.Resources.Keese2, enemies[i].x + 12, enemies[i].y);
                            }
                            break;
                    }
                }
            }

            #endregion

            #region Health Display
            switch (player.health)
            {
                case (0):
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 650, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 100);
                    break;
                case (1):
                    e.Graphics.DrawImage(Properties.Resources.HalfHeart, 650, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 100);
                    break;
                case (2):
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 100);
                    break;
                case (3):
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 100);
                    e.Graphics.DrawImage(Properties.Resources.HalfHeart, 685, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 100);
                    break;
                case (4):
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 100);
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 100);
                    e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 100);
                    break;
                case (5):
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 100);
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 100);
                    e.Graphics.DrawImage(Properties.Resources.HalfHeart, 720, 100);
                    break;
                case (6):
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 100);
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 100);
                    e.Graphics.DrawImage(Properties.Resources.FullHeart, 720, 100);
                    break;
            }
            #endregion
        }

        private void Attack()
        {
            if (bDown && attackCool < 1)
            {
                attackCool = 8;
                attack = true;
                attackSound.Play();
            }

            if (attack)
            {
                foreach (Enemy i in enemies)
                {
                    player.Attack(i);
                }
            }

            if (attackCool > 0)
            {
                attackCool--;
            }
            else
            {
                attack = false;
                attackSound.Stop();
            }
        }

        private void Movement()
        {
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
        }
    }
}
