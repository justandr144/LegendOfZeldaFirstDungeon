using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendOfZeldaFirstDungeon
{
    class Death
    {
        public int x, y, timer;

        public Death (int _x, int _y, int _timer)
        {
            x = _x;
            y = _y;
            timer = _timer;
        }
    }
}
