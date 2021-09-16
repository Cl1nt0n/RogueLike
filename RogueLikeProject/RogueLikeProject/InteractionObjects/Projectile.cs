using RogueLikeProject.InteractionObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogueLikeProject.GameWorld
{
    //разобраться с событиями
    public delegate void EnemyHitting(int damage, Map map);

    public class Projectile : InteractableObject
    {
        public Vector2 Direction { get; private set; }

        private Unit _source;
        private Map _map;

        public event Action Hitting;
        public event Action KillingEnemy;

        public Projectile(int speed, Unit player, Image sprite, Map map) : base(player.X, player.Y, sprite.Width, sprite.Height, speed, sprite)
        {
            Direction = new Vector2();
            _source = player;
            _map = map;
            KillingEnemy?.Invoke();
            SetStartPosition();
        }

        public void SetStartPosition()
        {
            if (_source.Direction.DirectionX == 1 && _source.Direction.DirectionY == 0)
            {
                X += 25;
                Y += 7;
                NewMethod(1, 0, Resource.bulletRight);
            }
            if (_source.Direction.DirectionX == -1 && _source.Direction.DirectionY == 0)
            {
                Y += 3;
                NewMethod(-1, 0, Resource.bulletLeft);
            }
            if (_source.Direction.DirectionX == 0 && _source.Direction.DirectionY == 1)
            {
                X += 7;
                NewMethod(0, 1, Resource.bulletUp);
            }
            if (_source.Direction.DirectionX == 0 && _source.Direction.DirectionY == -1)
            {
                Y += 25;
                X += 3;
                NewMethod(0, -1, Resource.bulletDown);
            }
        }

        private void NewMethod(int x, int y, Image sprite)
        {
            Direction.DirectionX = x;
            Direction.DirectionY = y;
            Sprite = sprite;
        }

        public void Move(List<Unit> enemies)
        {
            if (Direction.DirectionX == 1 && Direction.DirectionY == 0)
            {
                NewMethod1(enemies, Speed, 0);
            }
            if (Direction.DirectionX == -1 && Direction.DirectionY == 0)
            {
                if (_map.CheckIfCanWalk(X - Speed, Y, X + Sprite.Width - Speed, Y + Sprite.Height) && CheckEnemyHit(enemies))
                {
                    X -= Speed;
                }
                else
                {
                    if (_map.CheckIfCanWalk(X - Speed, Y, X + Sprite.Width - Speed, Y + Sprite.Height) == false)
                    {
                        for (int i = Speed; i >= 0; i--)
                        {
                            if (_map.CheckIfCanWalk(X - i, Y, X + Sprite.Width - i, Y + Sprite.Height))
                            {
                                X -= i;
                            }
                        }
                    }
                }
            }
            if (Direction.DirectionX == 0 && Direction.DirectionY == 1)
            {
                if (_map.CheckIfCanWalk(X, Y - Speed, X + Sprite.Width, Y + Sprite.Height - Speed) && CheckEnemyHit(enemies))
                {
                    Y -= Speed;
                }
                else
                {
                    if (_map.CheckIfCanWalk(X, Y - Speed, X + Sprite.Width, Y + Sprite.Height - Speed) == false)
                    {
                        for (int i = Speed; i >= 0; i--)
                        {
                            if (_map.CheckIfCanWalk(X, Y - i, X + Sprite.Width, Y + Sprite.Height - i))
                            {
                                Y -= i;
                            }
                        }
                    }
                }
            }
            if (Direction.DirectionX == 0 && Direction.DirectionY == -1)
            {
                if (_map.CheckIfCanWalk(X, Y + Speed, X + Sprite.Width, Y + Sprite.Height + Speed) && CheckEnemyHit(enemies))
                {
                    Y += Speed;
                }
                else
                {
                    if (_map.CheckIfCanWalk(X, Y + Speed, X + Sprite.Width, Y + Sprite.Height + Speed) == false)
                    {
                        for (int i = Speed; i >= 0; i--)
                        {
                            if (_map.CheckIfCanWalk(X, Y + i, X + Sprite.Width, Y + Sprite.Height + i))
                            {
                                Y += i;
                            }
                        }
                    }
                }
            }
        }

        private void NewMethod1(List<Unit> enemies, int xspeed, int yspeed)
        {
            int XSpeed = xspeed;
            int YSpeed = yspeed;
            if (_map.CheckIfCanWalk(X + XSpeed, Y + YSpeed, X + Sprite.Width + XSpeed, Y + Sprite.Height + YSpeed) && CheckEnemyHit(enemies))
            {
                X += XSpeed;
                Y += YSpeed;
            }
            else
            {
                if (_map.CheckIfCanWalk(X + XSpeed, Y + YSpeed, X + Sprite.Width + XSpeed, Y + Sprite.Height + YSpeed) == false)
                {
                    for (int i = Math.Abs(XSpeed); i >= 0; i--)
                    {
                        for (int j = Math.Abs(YSpeed); j >= 0; j--)
                        {
                            if (_map.CheckIfCanWalk(X + i, Y + j, X + Sprite.Width + i, Y + Sprite.Height + j))
                            {
                                X += Math.Sign(XSpeed) * i;
                                Y += Math.Sign(YSpeed) * j;
                            }
                        }
                    }
                }
            }
        }

        public bool CheckEnemyHit(List<Unit> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (X >= enemies[i].X &&
                    X + Sprite.Width <= enemies[i].X + enemies[i].Sprite.Width &&
                    Y >= enemies[i].Y && Y <= enemies[i].Y + enemies[i].Sprite.Height
                    && enemies[i].IsAlive == true)
                {
                    enemies[i].TakeDamage(_source.Damage, _map);
                    Hitting?.Invoke();
                    return false;
                }
            }

            return true;
        }

        public bool CheckEnemyHit(Unit unit)
        {
            if (X >= unit.X &&
                X + Sprite.Width <= unit.X + unit.Sprite.Width &&
                Y >= unit.Y && Y <= unit.Y + unit.Sprite.Height
                && unit.IsAlive == true)
            {
                unit.TakeDamage(_source.Damage, _map);
                Hitting?.Invoke();
                return false;
            }

            return true;
        }
    }
}
