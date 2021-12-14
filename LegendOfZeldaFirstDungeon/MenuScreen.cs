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
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
        }

        private void MenuScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.B:
                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    Room1 r1 = new Room1();
                    r1.Location = new Point((f.Width - r1.Width) / 2, (f.Height - r1.Height) / 2);
                    f.Controls.Add(r1);

                    r1.Focus();
                    break;
                case Keys.M:
                    Application.Exit();
                    break;
            }
        }
    }
}
