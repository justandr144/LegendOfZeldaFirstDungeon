using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LegendOfZeldaFirstDungeon
{
    class Link
    {
        public int x, y, speed, size, health;
        public string direction;

        public Link(int _x, int _y, int _speed, int _size, int _health, string _direction)
        {
            x = _x;
            y = _y;
            speed = _speed;
            size = _size;
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

        public void Attack(Enemy i)
        {
            switch (direction)
            {
                case "up":
                    Rectangle swordUpBox = new Rectangle(x + 11, y - 44, 21, 43);
                    switch (i.name)
                    {
                        case ("keese"):
                            Rectangle enemyBox = new Rectangle(i.x, i.y, i.width, i.height);

                            if (enemyBox.IntersectsWith(swordUpBox) && i.immune < 1)
                            {
                                i.health--;
                                i.immune = 50;
                            }
                            break;
                    }
                    break;
                case "down":
                    Rectangle swordDownBox = new Rectangle(x + 18, y + 50, 24, 48);
                    switch (i.name)
                    {
                        case ("keese"):
                            Rectangle enemyBox = new Rectangle(i.x, i.y, i.width, i.height);

                            if (enemyBox.IntersectsWith(swordDownBox) && i.immune < 1)
                            {
                                i.health--;
                                i.immune = 50;
                            }
                            break;
                    }
                    break;
                case "left":
                    Rectangle swordLeftBox = new Rectangle(x - 42, y + 21, 48, 24);
                    switch (i.name)
                    {
                        case ("keese"):
                            Rectangle enemyBox = new Rectangle(i.x, i.y, i.width, i.height);

                            if (enemyBox.IntersectsWith(swordLeftBox) && i.immune < 1)
                            {
                                i.health--;
                                i.immune = 50;
                            }
                            break;
                    }
                    break;
                case "right":
                    Rectangle swordRightBox = new Rectangle(x + 47, y + 21, 48, 24);
                    switch (i.name)
                    {
                        case ("keese"):
                            Rectangle enemyBox = new Rectangle(i.x, i.y, i.width, i.height);

                            if (enemyBox.IntersectsWith(swordRightBox) && i.immune < 1)
                            {
                                i.health--;
                                i.immune = 50;
                            }
                            break;
                    }
                    break;
            }
        }

        public bool Collision(Enemy i)
        {
            Rectangle playerBox = new Rectangle(x, y, size, size);

            switch (i.name)
            {
                case ("keese"):
                    Rectangle enemyBox = new Rectangle(i.x, i.y, i.width, i.height);

                    if (enemyBox.IntersectsWith(playerBox))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
