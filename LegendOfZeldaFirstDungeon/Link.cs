using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendOfZeldaFirstDungeon
{
    class Link
    {
        public int x, y, size, speed, health, image;
        public string direction;

        public Link(int _x, int _y, int _size, int _speed, int _health, int _image, string _direction)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            health = _health;
            image = _image;
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
