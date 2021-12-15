using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendOfZeldaFirstDungeon
{
    class Link
    {
        public int x, y, speed, health;
        public string direction;

        public Link(int _x, int _y, int _speed, int _health, string _direction)
        {
            x = _x;
            y = _y;
            speed = _speed;
            health = _health;
            direction = _direction;
        }

        public bool Move(string direct)
        {
            if (direct == "left")
            {
                x -= speed;
            }
            if (direct == "right")
            {
                x += speed;
            }
            if (direct == "up")
            {
                y -= speed;
            }
            if (direct == "down")
            {
                y += speed;
            }

            return true;
        }

        public void Attack()
        {
            
        }
    }
}
