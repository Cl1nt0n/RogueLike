using RogueLikeProject.InteractionObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeProject.GameWorld
{
    //разобраться с событиями
    public delegate void EnemyHitting(int damage, Map map);

    public class Projectile : InteractionObject
    {
        public Vector2 Direction { get; private set; }

        private int _speed;
        private Unit _source;
        private Map _map;

        public event Action WallHitting;
        public event Action KillingEnemy;
        public event Action EnemyLost;

        public Projectile(Unit player, Image sprite, Map map) : base(player.X, player.Y, sprite.Width, sprite.Height, sprite)
        {
            Direction = new Vector2();
            _source = player;
            _speed = 10;
            _map = map;
            SetStartBulletPosition();
            WallHitting?.Invoke();
            EnemyLost?.Invoke();
            KillingEnemy?.Invoke();
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
                if (_map.CheckCanWalk(this.X + _speed, this.Y, this.X + this.Width + _speed, this.Y + this.Height) && IfProjectileHit(enemies))
                {
                    X += _speed;
                }
                else
                {
                    _source.Projectile = null;
                }
            }
            if (Direction.DirectionX == -1 && Direction.DirectionY == 0)
            {
                if (_map.CheckCanWalk(this.X - _speed, this.Y, this.X + this.Width - _speed, this.Y + this.Height) && IfProjectileHit(enemies))
                {
                    X -= _speed;
                }
                else
                {
                    _source.Projectile = null;
                }
            }
            if (Direction.DirectionX == 0 && Direction.DirectionY == 1)
            {
                if (_map.CheckCanWalk(this.X, this.Y - _speed, this.X + this.Width, this.Y + this.Height - _speed) && IfProjectileHit(enemies))
                {
                    Y -= _speed;
                }
                else
                {
                    _source.Projectile = null;
                }
            }
            if (Direction.DirectionX == 0 && Direction.DirectionY == -1)
            {
                if (_map.CheckCanWalk(this.X, this.Y + _speed, this.X + this.Width, this.Y + this.Height + _speed) && IfProjectileHit(enemies))
                {
                    Y += _speed;
                }
                else
                {
                    _source.Projectile = null;
                }
            }

            WallHitting += OnWallHitting;
        }


        public bool IfProjectileHit(List<Unit> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (this.X >= enemies[i].X &&
                    this.X + this.Width <= enemies[i].X + enemies[i].Width &&
                    this.Y >= enemies[i].Y && this.Y <= enemies[i].Y + enemies[i].Height)
                {
                    enemies[i].GetDamage(_source.Damage, _map);
                    enemies[i].Dying += () => enemies[i].Sprite = Resource.deadEnemy;
                    if (_source is Player)
                    {
                        enemies[i].Dying += () => (_source as Player).GetBounty((enemies[i] as Enemy).Bounty);
                    }
                    return false;
                }
            }

            return true;
        }

        public void OnWallHitting()
        {
            _map.CheckCanWalk(this.X, this.Y, this.X + this.Width, this.Y + this.Height);
            WallHitting -= OnWallHitting;
            //this = null;
        }
    }
}
