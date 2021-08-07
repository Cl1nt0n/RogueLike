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

    public class Projectile : InteractionObject
    {
        public Vector2 Direction { get; private set; }

        private Unit _source;
        private Map _map;

        public event Action WallHitting;
        public event Action KillingEnemy;

        public Projectile(int speed, Unit player, Image sprite, Map map) : base(player.X, player.Y, sprite.Width, sprite.Height, speed, sprite)
        {
            Direction = new Vector2();
            _source = player;
            _map = map;
            WallHitting?.Invoke();
            KillingEnemy?.Invoke();
            SetStartBulletPosition();
        }

        public void SetStartBulletPosition()
        {
            if (_source.Direction.DirectionX == 1 && _source.Direction.DirectionY == 0)
            {
                X += 25;
                Y += 7;
                Direction.DirectionX = 1;
                Direction.DirectionY = 0;
                Sprite = Resource.bulletRight;
            }
            if (_source.Direction.DirectionX == -1 && _source.Direction.DirectionY == 0)
            {
                Y += 3;
                Direction.DirectionX = -1;
                Direction.DirectionY = 0;
                Sprite = Resource.bulletLeft;
            }
            if (_source.Direction.DirectionX == 0 && _source.Direction.DirectionY == 1)
            {
                X += 7;
                Direction.DirectionX = 0;
                Direction.DirectionY = 1;
                Sprite = Resource.bulletUp;
            }
            if (_source.Direction.DirectionX == 0 && _source.Direction.DirectionY == -1)
            {
                Y += 25;
                X += 3;
                Direction.DirectionX = 0;
                Direction.DirectionY = -1;
                Sprite = Resource.bulletDown;
            }
        }

        public void Move(List<Unit> enemies)
        {
            if (Direction.DirectionX == 1 && Direction.DirectionY == 0)
            {
                if (_map.CheckIfWall(X + Speed, Y, X + Sprite.Width + Speed, Y + Sprite.Height) && IfProjectileHit(enemies))
                {
                    X += Speed;
                }
                else
                {
                    for (int i = Speed; i >= 0; i--)
                    {
                        if (_map.CheckIfWall(X + i, Y, X + Sprite.Width + i, Y + Sprite.Height))
                        {
                            X += i;
                        }
                    }

                    _source.Projectiles[_source.Projectiles.Count - 1] = null;
                }
            }
            if (Direction.DirectionX == -1 && Direction.DirectionY == 0)
            {
                if (_map.CheckIfWall(X - Speed, Y, X + Sprite.Width - Speed, Y + Sprite.Height) && IfProjectileHit(enemies))
                {
                    X -= Speed;
                }
                else
                {
                    for (int i = Speed; i >= 0; i--)
                    {
                        if (_map.CheckIfWall(X - i, Y, X + Sprite.Width - i, Y + Sprite.Height))
                        {
                            X -= i;
                        }
                    }

                    _source.Projectiles[_source.Projectiles.Count - 1] = null;
                }
            }
            if (Direction.DirectionX == 0 && Direction.DirectionY == 1)
            {
                if (_map.CheckIfWall(X, Y - Speed, X + Sprite.Width, Y + Sprite.Height - Speed) && IfProjectileHit(enemies))
                {
                    Y -= Speed;
                }
                else
                {
                    for (int i = Speed; i >= 0; i--)
                    {
                        if (_map.CheckIfWall(X, Y - i, X + Sprite.Width, Y + Sprite.Height - i))
                        {
                            Y -= i;
                        }
                    }

                    _source.Projectiles[_source.Projectiles.Count - 1] = null;
                }
            }
            if (Direction.DirectionX == 0 && Direction.DirectionY == -1)
            {
                if (_map.CheckIfWall(X, Y + Speed, X + Sprite.Width, Y + Sprite.Height + Speed) && IfProjectileHit(enemies))
                {
                    Y += Speed;
                }
                else
                {
                    if (_map.CheckIfWall(X, Y + Speed, X + Sprite.Width, Y + Sprite.Height + Speed) == false)
                    {
                        for (int i = Speed; i >= 0; i--)
                        {
                            if (_map.CheckIfWall(X, Y + i, X + Sprite.Width, Y + Sprite.Height + i))
                            {
                                Y += i;
                            }
                        }
                    }

                    _source.Projectiles[_source.Projectiles.Count - 1] = null;
                }
            }
        }

        public bool IfProjectileHit(List<Unit> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (X >= enemies[i].X &&
                    X + Sprite.Width <= enemies[i].X + enemies[i].Sprite.Width &&
                    Y >= enemies[i].Y && Y <= enemies[i].Y + enemies[i].Sprite.Height
                    && enemies[i].IsAlive == true)
                {
                    enemies[i].TakeDamage(_source.Damage, _map);
                    return false;
                }
            }

            return true;
        }
    }
}
