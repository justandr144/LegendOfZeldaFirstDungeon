using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendOfZeldaFirstDungeon
{
    class Projectile
    {
        public int x, y, width, height, speed;
        public string type, direct;
        
        public Projectile(int _x, int _y, int _width, int _height, int _speed, string _type, string _direct)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
            type = _type;
            direct = _direct;
        }

        public void Move(Projectile i)
        {
            switch (i.type)
            {
                case "rock":
                    switch (i.direct)
                    {
                        case "left":
                            i.x -= i.speed;
                            break;
                        case "down":
                            i.y += i.speed;
                            break;
                        case "right":
                            i.x += i.speed;
                            break;
                    }
                    break;
                case "fire":

                    break;
            }
        }
    }
}
