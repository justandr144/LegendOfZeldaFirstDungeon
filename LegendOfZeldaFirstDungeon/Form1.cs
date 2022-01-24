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
    public partial class Form1 : Form
    {
        public static int playerX = 0;  //Variables
        public static int playerY = 0;
        public static int playerSize = 56;
        public static int playerSpeed = 7;
        public static int playerHealth = 6;
        public static int playerImage = 0;
        public static int score = 0;
        public static string playerDirect = "up";
        public static bool gameStart = false;

        System.Windows.Media.MediaPlayer music;
        int musicCounter = 590;

        public Form1()
        {
            InitializeComponent();

            music = new System.Windows.Media.MediaPlayer();
            music.Open(new Uri(Application.StartupPath + "/Resources/DungeonTheme.mp3"));
        }

        private void Form1_Load(object sender, EventArgs e) //Open main menu
        {
            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);

            ms.Focus();
            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            if (gameStart) //For dungeon music to play consistently throughout rooms
            {
                musicCounter++;

                if (musicCounter >= 600)
                {
                    music.Stop();
                    music.Play();
                    musicCounter = 0;
                }
            }
        }
    }
}
