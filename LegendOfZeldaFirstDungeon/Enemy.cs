using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LegendOfZeldaFirstDungeon
{
    class Enemy
    {
        public int x, y, speed, width, height, counter, health, immune, directX, directY;
        public string name;

        public Enemy(int _x, int _y, int _speed, int _width, int _height, int _counter, int _health, int _immune, int _directX, int _directY, string _name)
        {
            x = _x;
            y = _y;
            speed = _speed;
            width = _width;
            height = _height;
            counter = _counter;
            health = _health;
            immune = _immune;
            directX = _directX;
            directY = _directY;
            name = _name;
        }

        public void Move(Enemy i, Random randGen)
        {
            switch (i.name)
            {
                case "keese":
                    if (i.counter >= 30)
                    {
                        if (i.directX == 1)
                        {
                            if (i.x < 760)
                            {
                                i.x += i.speed;
                            }
                            else
                            {
                                i.directX = 2;
                            }
                        }
                        else if (i.directX == 2)
                        {
                            if (i.x > 157)
                            {
                                i.x -= i.speed;
                            }
                            else
                            {
                                i.directX = 1;
                            }
                        }

                        if (i.directY == 1)
                        {
                            if (i.y < 540)
                            {
                                i.y += i.speed;
                            }
                            else
                            {
                                i.directY = 2;
                            }
                        }
                        else if (i.directY == 2)
                        {
                            if (i.y > 260)
                            {
                                i.y -= i.speed;
                            }
                            else
                            {
                                i.directY = 1;
                            }
                        }

                        if (i.counter >= 50)
                        {
                            i.directX = randGen.Next(1, 3);
                            i.directY = randGen.Next(1, 3);
                            i.counter = 0;
                        }
                    }
                    break;
            }
        }
    }
}
