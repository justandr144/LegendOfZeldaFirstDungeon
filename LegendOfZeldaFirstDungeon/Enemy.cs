using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendOfZeldaFirstDungeon
{
    class Enemy
    {
        public int x, y, speed, health;

        public Enemy(int _x, int _y, int _speed, int _health)
        {
            x = _x;
            y = _y;
            speed = _speed;
            health = _health;
        }

        public bool Collision()
        {

            return false;
        }
    }
}
