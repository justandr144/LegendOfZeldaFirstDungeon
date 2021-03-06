using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace LegendOfZeldaFirstDungeon
{
    public partial class Room1 : UserControl
    {
        #region Global Variables
        Link player = new Link(Form1.playerX, Form1.playerY, Form1.playerSpeed, 56, Form1.playerHealth, Form1.playerDirect);

        bool upArrowDown, downArrowDown, leftArrowDown, rightArrowDown, bDown = false;
        bool movement = false;
        bool attack = false;
        bool falseExit = false;
        bool clear = false;
        bool shoot = false;
        bool pickUp = false;
        bool healthUp = false;
        int spriteLoop = 0;
        int playImmune = 0;
        int attackCool = 0;
        int scoreLoop = 50;

        List<Enemy> enemies = new List<Enemy>();
        List<Projectile> projectiles = new List<Projectile>();
        List<Death> deaths = new List<Death>();

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Font exitFont = new Font("Arial", 16);
        Font scoreFont = new Font("Arial", 20);

        Projectile heart = new Projectile(222, 324, 46, 46, 0, "heart", "none");

        Random randGen = new Random();

        System.Windows.Media.MediaPlayer attackSound;
        #endregion
        public Room1()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()       //Setting starting positions and adding enemies
        {
            switch (Form1.room)
            {
                case 1:
                    Enemy keese1 = new Enemy(458, 290, 6, 56, 28, 0, 1, 0, 1, 2, "keese");
                    enemies.Add(keese1);
                    break;
                case 2:
                    Enemy keese2 = new Enemy(458, 290, 6, 56, 28, 40, 1, 0, 1, 2, "keese");
                    Enemy keese3 = new Enemy(250, 400, 6, 56, 28, 20, 1, 0, 2, 2, "keese");
                    Enemy keese4 = new Enemy(650, 400, 6, 56, 28, 0, 1, 0, 2, 1, "keese");
                    enemies.Add(keese2);
                    enemies.Add(keese3);
                    enemies.Add(keese4);

                    this.BackgroundImage = Properties.Resources.UpDownRoom;
                    break;
                case 3:
                    Enemy stalfos1 = new Enemy(458, 290, 4, 53, 56, 0, 3, 0, 1, 2, "stalfos");
                    Enemy keese5 = new Enemy(250, 400, 6, 56, 28, 20, 1, 0, 2, 2, "keese");
                    Enemy keese6 = new Enemy(650, 400, 6, 56, 28, 0, 1, 0, 2, 1, "keese");
                    enemies.Add(stalfos1);
                    enemies.Add(keese5);
                    enemies.Add(keese6);
                    break;
                case 4:
                    Enemy stalfos2 = new Enemy(650, 290, 4, 53, 56, 0, 3, 0, 1, 2, "stalfos");
                    Enemy stalfos3 = new Enemy(250, 290, 4, 53, 56, 0, 3, 0, 1, 2, "stalfos");
                    Enemy octorok1 = new Enemy(115, 262, 8, 49, 56, 0, 2, 0, 3, 0, "octorok");
                    Enemy octorok2 = new Enemy(708, 480, 8, 49, 56, 0, 2, 0, 1, 0, "octorok");
                    enemies.Add(stalfos2);
                    enemies.Add(stalfos3);
                    enemies.Add(octorok1);
                    enemies.Add(octorok2);
                    break;
                case 5:
                    Enemy octorok3 = new Enemy(409, 220, 8, 56, 49, 0, 2, 0, 2, 0, "octorok");
                    Enemy stalfos4 = new Enemy(250, 290, 4, 53, 56, 0, 3, 0, 1, 2, "stalfos");
                    Enemy stalfos5 = new Enemy(650, 290, 4, 53, 56, 0, 3, 0, 1, 2, "stalfos");
                    Enemy keese7 = new Enemy(250, 480, 6, 56, 28, 0, 1, 0, 2, 1, "keese");
                    Enemy keese8 = new Enemy(650, 480, 6, 56, 28, 30, 1, 0, 2, 2, "keese");
                    enemies.Add(octorok3);
                    enemies.Add(stalfos4);
                    enemies.Add(stalfos5);
                    enemies.Add(keese7);
                    enemies.Add(keese8);
                    break;
                case 6:
                    projectiles.Add(heart);

                    this.BackgroundImage = Properties.Resources.BeforeBossRoom;
                    break;
                case 7:

                    break;
            }

            deaths.Clear();

            player.x = 409;
            player.y = 530;

            attackSound = new System.Windows.Media.MediaPlayer();
            attackSound.Open(new Uri(Application.StartupPath + "/Resources/AttackSound.mp3"));
        }

        private void Room1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)     //Key press
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

        private void Room1_KeyUp(object sender, KeyEventArgs e)     //Key release
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

            foreach (Enemy i in enemies)     //Preparing enemies for movement and shooting
            {
                i.counter++;
                i.Move(i, randGen);
                shoot = i.Shoot(i);

                if (shoot)
                {
                    switch (i.directX)
                    {
                        case 1:
                            Projectile shotLeft = new Projectile(i.x - 28, i.y + 10, 28, 35, i.speed, "rock", "left");
                            projectiles.Add(shotLeft);
                            break;
                        case 2:
                            Projectile shotDown = new Projectile(i.x + 14, i.y + 49, 28, 35, i.speed, "rock", "down");
                            projectiles.Add(shotDown);
                            break;
                        case 3:
                            Projectile shotRight = new Projectile(i.x + 49, i.y + 10, 28, 35, i.speed, "rock", "right");
                            projectiles.Add(shotRight);
                            break;
                    }
                }
            }

            foreach (Projectile p in projectiles)
            {
                p.Move(p);
            }

            if (projectiles.Count > 0)
            {
                if (projectiles[0].x < 107 || projectiles[0].x > 725 || projectiles[0].y > 535)
                {
                    projectiles.RemoveAt(0);
                }
            }

            foreach (Enemy i in enemies)        //Enemies hurting player
            {
                if (player.Collision(i) && playImmune < 1)
                {
                    Form1.playerHealth--;
                    player.health = Form1.playerHealth;
                    playImmune = 50;
                    Form1.score -= 50;
                }
            }
            foreach (Projectile p in projectiles)
            {
                if (Form1.room == 6)
                {
                    if (player.CollisionProjectile(p))
                    {
                        pickUp = true;

                        switch (p.type)
                        {
                            case "heart":
                                healthUp = true;
                                Form1.playerHealth = 8;
                                player.health = Form1.playerHealth;
                                break;
                        }
                    }
                }
                else
                {
                    if (player.CollisionProjectile(p) && playImmune < 1)
                    {
                        Form1.playerHealth--;
                        player.health = Form1.playerHealth;
                        playImmune = 50;
                        Form1.score -= 50;
                    }
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

            if (player.y < 170 && enemies.Count == 0)     //Clearing room
            {
                clear = true;
            }

            if (clear)      //Load next room if current is cleared
            {
                clear = false;

                if (Form1.room < 7)
                {
                    Form1.room++;
                    OnStart();
                }
                else
                {
                    gameLoop.Enabled = false;
                    string points = Convert.ToString(Form1.score);

                    XmlWriter writer = XmlWriter.Create("scores.xml");

                    writer.WriteStartElement("Scores");

                    writer.WriteElementString("score", points);

                    writer.WriteEndElement();
                    writer.Close();
                }
            }
            else if (player.y < 165 && enemies.Count > 0)   //Or tell player enemies remaining
            {
                falseExit = true;
            }

            if (enemies.Count == 0)
            {
                falseExit = false;
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

            if (player.health == 0)
            {
                Refresh();
                Thread.Sleep(3000);
                Form1.playerHealth = 6;
                player.health = 6;
                Form1.gameStart = false;
                gameLoop.Enabled = false;

                Form f = this.FindForm();
                f.Controls.Remove(this);

                GameOverScreen gos = new GameOverScreen();
                gos.Location = new Point((f.Width - gos.Width) / 2, (f.Height - gos.Height) / 2);
                f.Controls.Add(gos);

                gos.Focus();
            }

            Refresh();
        }

        private void Room1_Paint(object sender, PaintEventArgs e) //death order 1, 2, 8, 3, 8, 3, 4, 5, 6, 9, 7, 1
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
                if (enemies[i].immune == 0 || enemies[i].immune > 0 && enemies[i].immune < 5 || enemies[i].immune > 10 && enemies[i].immune < 15 || enemies[i].immune > 20 && enemies[i].immune < 25 || enemies[i].immune > 30 && enemies[i].immune < 35 || enemies[i].immune > 40 && enemies[i].immune < 45)
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
                            case "stalfos":
                                if (enemies[i].counter >= 0 && enemies[i].counter <= 6 || enemies[i].counter >= 13 && enemies[i].counter <= 18)
                                {
                                    e.Graphics.DrawImage(Properties.Resources.Stalfos1, enemies[i].x, enemies[i].y);
                                }
                                else
                                {
                                    e.Graphics.DrawImage(Properties.Resources.Stalfos2, enemies[i].x, enemies[i].y);
                                }
                                break;
                            case "octorok":
                                switch (enemies[i].directX)
                                {
                                    case 1:
                                        if (enemies[i].counter >= 0 && enemies[i].counter <= 7 || enemies[i].counter >= 15 && enemies[i].counter <= 21 || enemies[i].counter >= 29 && enemies[i].counter <= 35 || enemies[i].counter >= 43 && enemies[i].counter <= 49 || enemies[i].counter >= 57 && enemies[i].counter <= 63)
                                        {
                                            e.Graphics.DrawImage(Properties.Resources.OctoLeft1, enemies[i].x, enemies[i].y);
                                        }
                                        else
                                        {
                                            e.Graphics.DrawImage(Properties.Resources.OctoLeft2, enemies[i].x, enemies[i].y);
                                        }
                                        break;
                                    case 2:
                                        if (enemies[i].counter >= 0 && enemies[i].counter <= 7 || enemies[i].counter >= 15 && enemies[i].counter <= 21 || enemies[i].counter >= 29 && enemies[i].counter <= 35 || enemies[i].counter >= 43 && enemies[i].counter <= 49 || enemies[i].counter >= 57 && enemies[i].counter <= 63)
                                        {
                                            e.Graphics.DrawImage(Properties.Resources.OctoDown1, enemies[i].x, enemies[i].y);
                                        }
                                        else
                                        {
                                            e.Graphics.DrawImage(Properties.Resources.OctoDown2, enemies[i].x, enemies[i].y);
                                        }
                                        break;
                                    case 3:
                                        if (enemies[i].counter >= 0 && enemies[i].counter <= 7 || enemies[i].counter >= 15 && enemies[i].counter <= 21 || enemies[i].counter >= 29 && enemies[i].counter <= 35 || enemies[i].counter >= 43 && enemies[i].counter <= 49 || enemies[i].counter >= 57 && enemies[i].counter <= 63)
                                        {
                                            e.Graphics.DrawImage(Properties.Resources.OctoRight1, enemies[i].x, enemies[i].y);
                                        }
                                        else
                                        {
                                            e.Graphics.DrawImage(Properties.Resources.OctoRight2, enemies[i].x, enemies[i].y);
                                        }
                                        break;
                                }
                                break;
                        }
                    }
            }

            foreach (Projectile p in projectiles)
            {
                switch (p.type)
                {
                    case "rock":
                        e.Graphics.DrawImage(Properties.Resources.Rock, p.x, p.y, p.width, p.height);
                        break;
                    case "fire":

                        break;
                    case "heart":
                        if (pickUp == false)
                        {
                            e.Graphics.DrawImage(Properties.Resources.HeartContainer, heart.x, heart.y, heart.width, heart.height);
                        }
                        break;
                }
            }

            #endregion

            #region Health Display
            if (healthUp)
            {
                switch (player.health)
                {
                    case (0):
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 755, 54);
                        break;
                    case (1):
                        e.Graphics.DrawImage(Properties.Resources.HalfHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 755, 54);
                        break;
                    case (2):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 755, 54);
                        break;
                    case (3):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.HalfHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 755, 54);
                        break;
                    case (4):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 755, 54);
                        break;
                    case (5):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.HalfHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 755, 54);
                        break;
                    case (6):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 755, 54);
                        break;
                    case (7):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.HalfHeart, 755, 54);
                        break;
                    case (8):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 720, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 755, 54);
                        break;
                }
            }
            else
            {
                switch (player.health)
                {
                    case (0):
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        break;
                    case (1):
                        e.Graphics.DrawImage(Properties.Resources.HalfHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        break;
                    case (2):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        break;
                    case (3):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.HalfHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        break;
                    case (4):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.EmptyHeart, 720, 54);
                        break;
                    case (5):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.HalfHeart, 720, 54);
                        break;
                    case (6):
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 650, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 685, 54);
                        e.Graphics.DrawImage(Properties.Resources.FullHeart, 720, 54);
                        break;
                }
            }
            #endregion

            #region Misc Display
            if (falseExit)
            {
                e.Graphics.DrawString("There are enemies remaining", exitFont, whiteBrush, 30, 60);
            }
            e.Graphics.DrawString($"{Form1.score}", scoreFont, whiteBrush, 400, 50);
            e.Graphics.DrawString($"Room: {Form1.room}", scoreFont, whiteBrush, 100, 20);

            if (Form1.room == 6)
            {
                e.Graphics.DrawImage(Properties.Resources.Sorry, 130, 240);
            }
            #endregion
        }

        private void Attack()   //Player attacking method
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

        private void Movement()     //player movement method
        {
            if (upArrowDown && attack == false && player.y > 180)
            {
                movement = player.Move("up");
                player.direction = "up";
            }
            else if (upArrowDown && attack == false && player.x >= 390 && player.x <= 430 && player.y > 160)
            {
                movement = player.Move("up");
                player.direction = "up";
            }
            if (downArrowDown && attack == false && player.y < 535)
            {
                movement = player.Move("down");
                player.direction = "down";
            }
            if (leftArrowDown && attack == false && player.x > 107)
            {
                movement = player.Move("left");
                player.direction = "left";
            }
            if (rightArrowDown && attack == false && player.x < 710)
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
