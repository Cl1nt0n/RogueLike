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

    //убрать лишние методы
    public class Unit : InteractionObject
    {
        public int Hp { get; private set; }
        public int Damage { get; private set; }
        public int Speed { get; private set; }
        public bool IsAlive { get; private set; }
        public Vector2 Direction { get; private set; }
        public Projectile Projectile { get; set; }

        public event Action Dying;

        public Unit(Image sprite, int x, int y, int hp) : base(x, y, sprite.Width, sprite.Height, sprite)
        {
            Speed = 5;
            Damage = 100;
            Hp = hp;
            Direction = new Vector2();
            Direction.DirectionX = 1;
            Direction.DirectionY = 0;
            IsAlive = true;
        }

        public void Shot(Map map)
        {
            Projectile = new Projectile(this, Resource.bulletRight, map);
            //Projectile.EnemyHitting += OnEnemyHitting;
        }

        public void GetDamage(int damage, Map map)
        {
            if (Hp > 0)
            {
                IsAlive = true;
                Hp -= damage;
                if (Hp <= 0)
                {
                    Dying?.Invoke();
                    IsAlive = false;
                }
                //Projectile.EnemyHitting += OnEnemyHitting;
            }
        }

        public void Update(Graphics graphics, List<Unit> enemies)
        {
            Projectile?.Move(enemies);
            Projectile?.Print(graphics);
        }
    }

    public delegate void DelShow(string str);

    public class Player : Unit
    {
        public int Count { get; set; }
        public event Action StatsChanged;

        public Player() : base(Resource.playerRight, 1, 1, 100) { }

        public void Walk(Map map, KeyEventArgs keyDown)
        {
            if (Hp > 0)
            {
                switch (keyDown.KeyData)
                {
                    case Keys.A:
                        this.Sprite = Resource.playerLeft;
                        if (X - Speed >= 0)
                        {
                            Direction.DirectionX = -1;
                            Direction.DirectionY = 0;

                            if (map.CheckCanWalk(X - Speed, Y, X + Width - Speed, Y + Height))
                            {
                                X -= Speed;
                            }
                        }
                        break;
                    case Keys.D:
                        this.Sprite = Resource.playerRight;
                        if (X + Speed <= map.Width - 12)
                        {
                            Direction.DirectionX = 1;
                            Direction.DirectionY = 0;
                            if (map.CheckCanWalk(X + Speed, Y, X + Width + Speed, Y + Height))
                            {
                                X += Speed;
                            }
                        }
                        break;
                    case Keys.S:
                        this.Sprite = Resource.playerDown;
                        if (Y + Speed <= map.Height - 12)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = -1;
                            if (map.CheckCanWalk(X, Y + Speed, X + Width, Y + Height + Speed))
                            {
                                Y += Speed;
                            }
                        }
                        break;
                    case Keys.W:
                        this.Sprite = Resource.playerUp;
                        if (Y - Speed >= 0)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = 1;
                            if (map.CheckCanWalk(X, Y - Speed, X + Width, Y + Height - Speed))
                            {
                                Y -= Speed;
                            }
                        }
                        break;
                }
            }
        }

        public void GetBounty(int bounty)
        {
            Count += bounty;
            StatsChanged?.Invoke();
        }

        public void ShowStatistic(DelShow Statistics)
        {
            Statistics(Count.ToString());
        }
    }

    public class Enemy : Unit
    {
        public int Bounty { private set; get; }

        private Random _random;
        public Enemy(int x, int y) : base(Resource.enemyRight, x, y, 250)
        {
            _random = new Random();
            Bounty = 20;
        }

        public void Walk(Map map)
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
                            if (map.CheckCanWalk(X - Speed, Y, X + Width - Speed, Y + Height))
                            {
                                X -= Speed;
                            }
                        }
                        break;
                    case 1:
                        this.Sprite = Resource.enemyRight;
                        if (X + Speed <= map.Width - 25)
                        {
                            Direction.DirectionX = 1;
                            Direction.DirectionY = 0;
                            if (map.CheckCanWalk(X + Speed, Y, X + Width + Speed, Y + Height))
                            {
                                X += Speed;
                            }
                        }
                        break;
                    case 2:
                        this.Sprite = Resource.enemyDown;
                        if (Y + Speed <= map.Height - 25)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = -1;
                            if (map.CheckCanWalk(X, Y + Speed, X + Width, Y + Height + Speed))
                            {
                                Y += Speed;
                            }
                        }
                        break;
                    case 3:
                        this.Sprite = Resource.enemyUp;
                        if (Y - Speed >= 0)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = 1;
                            if (map.CheckCanWalk(X, Y - Speed, X + Width, Y + Height - Speed))
                            {
                                Y -= Speed;
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
