using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LegendOfZeldaFirstDungeon
{
    public partial class VictoryScreen : UserControl
    {
        List<int> scores = new List<int>();

        public VictoryScreen()
        {
            InitializeComponent();
            LoadFile();
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {

        }

        private void LoadFile()
        {
            XmlReader reader = XmlReader.Create("scores.xml");
        }
    }
}
