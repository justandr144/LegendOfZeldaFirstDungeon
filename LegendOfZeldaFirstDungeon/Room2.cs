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
    public partial class Room2 : UserControl
    {
        #region Global Variables
        Link player = new Link(Form1.playerX, Form1.playerY, Form1.playerSpeed, 56, Form1.playerHealth, Form1.playerDirect);

        bool upArrowDown, downArrowDown, leftArrowDown, rightArrowDown, bDown = false;
        bool movement = false;
        bool attack = false;
        bool falseExit = false;
        bool clear = false;
        int spriteLoop = 0;
        int playImmune = 0;
        int attackCool = 0;
        int scoreLoop = 50;

        List<Enemy> enemies = new List<Enemy>();
        List<Death> deaths = new List<Death>();

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Font exitFont = new Font("Arial", 16);
        Font scoreFont = new Font("Arial", 20);

        Random randGen = new Random();

        System.Windows.Media.MediaPlayer attackSound;
        #endregion
        public Room2()
        {
            InitializeComponent();
            OnStart();
        }
        public void OnStart()       //Setting starting positions and adding enemies
        {
            Enemy keese1 = new Enemy(458, 290, 5, 56, 28, 40, 1, 0, 1, 2, "keese");
            Enemy keese2 = new Enemy(250, 400, 5, 56, 28, 20, 1, 0, 2, 2, "keese");
            Enemy keese3 = new Enemy(650, 400, 5, 56, 28, 0, 1, 0, 2, 1, "keese");
            enemies.Add(keese1);
            enemies.Add(keese2);
            enemies.Add(keese3);

            player.x = 458;
            player.y = 550;

            attackSound = new System.Windows.Media.MediaPlayer();
            attackSound.Open(new Uri(Application.StartupPath + "/Resources/AttackSound.mp3"));
        }

        private void Room2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) //Key press
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

        private void Room2_KeyUp(object sender, KeyEventArgs e)     //Key release
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
            scoreLoop--;

            Attack();

            Movement();

            foreach (Enemy i in enemies)     //Preparing enemies for movement
            {
                i.counter++;
                i.Move(i, randGen);
            }

            foreach (Enemy i in enemies)
            {
                if (player.Collision(i) && playImmune < 1)        //Enemies hurting player
                {
                    Form1.playerHealth--;
                    player.health = Form1.playerHealth;
                    playImmune = 50;
                    Form1.score -= 50;
                }
            }

            if (playImmune > 0)     //Imunnity cooldown
            {
                playImmune--;
            }

            for (int i = 0; i < enemies.Count; i++)     //Enemy immune cooldown and deaths
            {
                if (enemies[i].immune > 0)
                {
                    enemies[i].immune--;
                }

                if (enemies[i].health <= 0)
                {
                    Death death = new Death(enemies[i].x, enemies[i].y, 36);
                    deaths.Add(death);

                    enemies.RemoveAt(i);
                    Form1.score += 100;
                }
            }

            if (player.y < 229 && enemies.Count == 0)       //Clearing room
            {
                clear = true;
                falseExit = false;
            }

            if (clear)      //Load next room if current is cleared
            {
                clear = false;
                gameLoop.Enabled = false;

                Form f = this.FindForm();
                f.Controls.Remove(this);

                Room2 r2 = new Room2();
                r2.Location = new Point((f.Width - r2.Width) / 2, (f.Height - r2.Height) / 2);
                f.Controls.Add(r2);

                r2.Focus();
            }
            else if (player.y < 229 && enemies.Count > 0)   //Or tell player enemies remaining
            {
                falseExit = true;
            }

            foreach (Death d in deaths)      //death animation cooldown
            {
                if (d.timer > 0)
                {
                    d.timer--;
                }
            }

            if (scoreLoop <= 0)     //Lower score over time
            {
                Form1.score--;
                scoreLoop = 50;
            }

            Refresh();
        }

        private void Room2_Paint(object sender, PaintEventArgs e)
        {
           
            #region Death Animation
            foreach (Death d in deaths)
            {
                if (d.timer <= 36 && d.timer >= 34)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death1, d.x, d.y);
                }
                else if (d.timer <= 33 && d.timer >= 31)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death2, d.x, d.y);
                }
                else if (d.timer <= 30 && d.timer >= 28)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death8, d.x, d.y);
                }
                else if (d.timer <= 27 && d.timer >= 25)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death3, d.x, d.y);
                }
                else if (d.timer <= 24 && d.timer >= 22)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death8, d.x, d.y);
                }
                else if (d.timer <= 21 && d.timer >= 19)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death3, d.x, d.y);
                }
                else if (d.timer <= 18 && d.timer >= 16)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death4, d.x, d.y);
                }
                else if (d.timer <= 15 && d.timer >= 13)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death5, d.x, d.y);
                }
                else if (d.timer <= 12 && d.timer >= 10)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death6, d.x, d.y);
                }
                else if (d.timer <= 9 && d.timer >= 7)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death9, d.x, d.y);
                }
                else if (d.timer <= 6 && d.timer >= 4)
                {
                    e.Graphics.DrawImage(Properties.Resources.Death7, d.x, d.y);
                }
                else if (d.timer <= 3 && d.timer >= 1)
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

            #region Misc Display
            if (falseExit)
            {
                e.Graphics.DrawString("There are enemies remaining", exitFont, whiteBrush, 50, 100);
            }
            e.Graphics.DrawString($"{Form1.score}", scoreFont, whiteBrush, 402, 50);
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
        }   //Player attacking method

        private void Movement()     //player movement method
        {
            if (upArrowDown && attack == false && player.y > 260)
            {
                movement = player.Move("up");
                player.direction = "up";
            }
            else if (upArrowDown && attack == false && player.x >= 440 && player.x <= 480 && player.y > 228)
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
