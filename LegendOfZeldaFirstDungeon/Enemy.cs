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
        public int x, y, speed, width, height, counter, health, immune;
        public string name;
        Random randGen = new Random();

        public Enemy(int _x, int _y, int _speed, int _width, int _height, int _counter, int _health, int _immune, string _name)
        {
            x = _x;
            y = _y;
            speed = _speed;
            width = _width;
            height = _height;
            counter = _counter;
            health = _health;
            immune = _immune;
            name = _name;
        }

        public void Move(Enemy i)
        {
            switch (i.name)
            {
                case "keese":
                    if (i.counter >= 50)
                    {
                        int directX = randGen.Next(1, 3);
                        int directY = randGen.Next(1, 3);

                        if (directX == 1)
                        {
                            i.x += i.speed;
                        }
                        else
                        {
                            i.x -= i.speed;
                        }

                        if (directY == 1)
                        {
                            i.y += i.speed;
                        }
                        else
                        {
                            i.y -= i.speed;
                        }

                        i.counter = 0;
                    }
                    break;
            }
        }
    }
}
