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
    public partial class Form1 : Form
    {
        public static int playerX = 0;
        public static int playerY = 0;
        public static int playerSize = 20;
        public static int playerSpeed = 7;
        public static int playerHealth = 6;
        public static string playerDirect = "up";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);

            ms.Focus();
            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);
        }
    }
}
