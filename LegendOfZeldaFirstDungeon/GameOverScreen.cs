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
    public partial class GameOverScreen : UserControl
    {
        public GameOverScreen()
        {
            InitializeComponent();
        }

        private void GameOverScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.B:
                    Form1.room = 1;
                    Form1.score = 0;

                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    MenuScreen ms = new MenuScreen();
                    ms.Location = new Point((f.Width - ms.Width) / 2, (f.Height - ms.Height) / 2);
                    f.Controls.Add(ms);

                    ms.Focus();
                    break;
                case Keys.M:
                    Application.Exit();
                    break;
            }
        }
    }
}
