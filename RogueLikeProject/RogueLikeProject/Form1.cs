using RogueLikeProject.GameWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogueLikeProject
{
    public partial class Form1 : Form
    {
        Map map;
        Unit player;
        List<Unit> enemies;
        List<Timer> enemeisTimers;
        Graphics graphics;
        Random random;

        public Form1()
        {
            InitializeComponent();
            map = new Map();
            player = new Player();
            enemies = new List<Unit>();
            enemeisTimers = new List<Timer>();
            random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\ЧинякOFF\Downloads\hotline_miami_04. M.O.O.N - Crystals (online-audio-converter.com).wav");
            //simpleSound.Play();

            LoadGameSpace();

        }

        private void LoadGameSpace()
        {
            map.Load();
            AddEnemies();
            for (int i = 0; i < enemeisTimers.Count; i++)
            {
                enemeisTimers[i].Interval = random.Next(1000, 10000);
                enemeisTimers[i].Enabled = true;
                enemeisTimers[i].Tick += (object sender, EventArgs e) => (enemies[i] as Enemy).Walk(map);
            }

            pictureBoxMap.Width = map.Width;
            pictureBoxMap.Height = map.Height;

            playerHealthBar.Maximum = player.Hp;
            playerHealthBar.Value = player.Hp;

            this.Width = pictureBoxMap.Width + 50;
            this.Height = pictureBoxMap.Height + 85;

            pictureBoxMap.Image = new Bitmap(pictureBoxMap.Width, pictureBoxMap.Height);
            graphics = Graphics.FromImage(pictureBoxMap.Image);

            map.Print(graphics);
            player.Print(graphics);

            (player as Player).StatsChanged += () => (player as Player).ShowStatistic(CheckCount);
        }

        private void CheckCount(string str)
        {
            labelBountyCount.Text = str;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < enemies.Count; i++)
            //{
            //    (enemies[i] as Enemy).Walk(map);
            //}
            //enemy.Print(graphics);
            //player.Print(graphics);
            pictureBoxMap.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            (player as Player).Walk(map, e);
            pictureBoxMap.Invalidate();
        }

        private void pictureBoxMap_Paint(object sender, PaintEventArgs e)
        {
            map.Print(graphics);
            for (int i = 0; i < enemies.Count; i++)
            {
                (enemies[i] as Enemy).Print(graphics);
            }
            player.Print(graphics);
            player.Update(graphics, enemies);
        }

        private void pictureBoxMap_MouseClick(object sender, MouseEventArgs e)
        {
            (player as Player).Shot(map);
            PlayShotSound();
            timerShot.Enabled = true;
            if (CheckIsEnemiesAlive() == false)
            {
                map = new Map();
                player = new Player();
                enemies = new List<Unit>();
                LoadGameSpace();
            }
        }

        private void PlayShotSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\ЧинякOFF\Downloads\z_uk-vystrel-s-pistoleta(1) (2) (1).wav");
            simpleSound.Play();
        } 

        private bool CheckIsEnemiesAlive()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsAlive == true)
                {
                    return true;
                }
            }

            return false;
        }

        private void timerShot_Tick(object sender, EventArgs e)
        {
            pictureBoxMap.Invalidate();
        }

        private void AddEnemies()
        {
            int enemyCount = random.Next(2, 8);
            for (int i = 0; i < enemyCount; i++)
            {
                int tempX = random.Next(0, map.Width);
                int tempY = random.Next(0, map.Height);
                while (map.CheckCanWalk(tempX, tempY, tempX + Resource.enemyRight.Width, tempY + Resource.enemyRight.Height) == false)
                {
                    tempX = random.Next(0, map.Width);
                    tempY = random.Next(0, map.Height);
                }
                enemies.Add(new Enemy(tempX, tempY));
                enemeisTimers.Add(new Timer());
            }
        }

        private void timerEnemyWalk_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < enemies.Count; i++)
            //{
            //    (enemies[i] as Enemy).Walk(map);
            //}
        }
    }
}
