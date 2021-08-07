using RogueLikeProject.GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeProject.InteractionObjects
{
    public class Enemy : Unit
    {
        public int Bounty { private set; get; }

        private Random _random;

        public Enemy(int x, int y, Player player, Map map) : base(Resource.enemyRight, x, y, 250, 70, map)
        {
            _random = new Random();
            Bounty = 20;

            Dying += () => Sprite = Resource.deadEnemy;
            Dying += () => player.TakeBounty(Bounty);
        }

        public void Walk(Map map, Player player)
        {
            if (Hp > 0)
            {
                switch (_random.Next(0, 6))
                {
                    case 0:
                        this.Sprite = Resource.enemyLeft;
                        if (X - Speed >= 0)
                        {
                            Direction.DirectionX = -1;
                            Direction.DirectionY = 0;
                            if (map.CheckIfWall(X - Speed, Y, X + Sprite.Width - Speed, Y + Sprite.Height))
                            {
                                X -= Speed;
                            }

                            if (X >= player.X && Y + Sprite.Height / 2 <= player.Y + player.Sprite.Height && Y + Sprite.Height / 2 >= player.Y)
                            {
                                Shot(map);
                            }
                        }
                        break;
                    case 1:
                        this.Sprite = Resource.enemyRight;
                        if (X + Speed <= map.Width - 25)
                        {
                            Direction.DirectionX = 1;
                            Direction.DirectionY = 0;
                            if (map.CheckIfWall(X + Speed, Y, X + Sprite.Width + Speed, Y + Sprite.Height))
                            {
                                X += Speed;
                            }

                            if (X <= player.X && Y + Sprite.Height / 2 <= player.Y + player.Sprite.Height && Y + Sprite.Height / 2 >= player.Y)
                            {
                                Shot(map);
                            }
                        }
                        break;
                    case 2:
                        this.Sprite = Resource.enemyDown;
                        if (Y + Speed <= map.Height - 25)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = -1;
                            if (map.CheckIfWall(X, Y + Speed, X + Sprite.Width, Y + Sprite.Height + Speed))
                            {
                                Y += Speed;
                            }

                            if (Y <= player.Y && X + Sprite.Width / 2 <= player.X + player.Sprite.Width && X + Sprite.Width / 2 >= player.X)
                            {
                                Shot(map);
                            }
                        }
                        break;
                    case 3:
                        this.Sprite = Resource.enemyUp;
                        if (Y - Speed >= 0)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = 1;
                            if (map.CheckIfWall(X, Y - Speed, X + Sprite.Width, Y + Sprite.Height - Speed))
                            {
                                Y -= Speed;
                            }

                            if (Y >= player.Y && X + Sprite.Width / 2 <= player.X + player.Sprite.Width && X + Sprite.Width / 2 >= player.X)
                            {
                                Shot(map);
                            }
                        }
                        break;
                    default:

                        break;
                }
            }
        }
    }
}
