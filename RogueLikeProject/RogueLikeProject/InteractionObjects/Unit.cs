using RogueLikeProject.InteractionObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogueLikeProject.GameWorld
{

    //убрать лишние методы
    public delegate void DelShowStats(string str);
    public class Unit : InteractionObject
    {
        public int Hp { get; private set; }
        public int Damage { get; private set; }
        public bool IsAlive { get; private set; }
        public Vector2 Direction { get; private set; }
        public List<Projectile> Projectiles { get; set; }

        public event Action Dying;
        public event Action HpChanged;

        public Unit(Image sprite, int x, int y, int hp, int damage, Map map) : base(x, y, sprite.Width, 8, sprite.Height, sprite)
        {
            Damage = damage;
            Hp = hp;
            Direction = new Vector2();
            Projectiles = new List<Projectile>();
            Direction.DirectionX = 1;
            Direction.DirectionY = 0;
            IsAlive = true;
        }

        public void Shot(Map map)
        {
            Projectiles.Add(new Projectile(15, this, Resource.bulletRight, map));
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\ЧинякOFF\Downloads\z_uk-vystrel-s-pistoleta(1) (2) (1).wav");
            simpleSound.Play();
            //Projectile.EnemyHitting += OnEnemyHitting;
        }

        public void TakeDamage(int damage, Map map)
        {
            if (Hp > 0)
            {
                IsAlive = true;
                Hp -= damage;
                if (this is Player)
                {
                    HpChanged?.Invoke();
                }
                if (Hp <= 0)
                {
                    IsAlive = false;
                    Dying?.Invoke();
                }
                //Projectile.EnemyHitting += OnEnemyHitting;
            }
        }

        public void Update(List<Unit> enemies, Graphics graphics)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i]?.Move(enemies);
                Projectiles[i]?.Print(graphics);
            }
        }
    }
}
