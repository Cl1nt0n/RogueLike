using RogueLikeProject.GameWorld;
using RogueLikeProject.InteractionObjects;
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
        private Map _map;
        private Unit _player;
        private List<Unit> _enemies;
        private List<Timer> _enemiesTimers;
        private Graphics _graphics;
        private Random _random;

        private event Action GameSpaceSpawning;

        public Form1()
        {
            InitializeComponent();
            _map = new Map();
            _enemies = new List<Unit>();
            _player = new Player(_map);
            _enemiesTimers = new List<Timer>();
            _random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\ЧинякOFF\Downloads\hotline_miami_04. M.O.O.N - Crystals (online-audio-converter.com).wav");
            //simpleSound.Play();

            playerHealthBar.Maximum = _player.Hp;

            pictureBoxMap.Width = _map.Width;
            pictureBoxMap.Height = _map.Height;

            this.Width = pictureBoxMap.Width + 50;
            this.Height = pictureBoxMap.Height + 85;

            LoadGameSpace();

            GameSpaceSpawning += () =>
            {
                _map = new Map();
                _enemies = new List<Unit>();
                _enemiesTimers = new List<Timer>();
                LoadGameSpace();
            };
        }

        private void LoadGameSpace()
        {
            _map.Load();

            AddEnemies();
            (_player as Player).Spawn(_map);

            playerHealthBar.Value = _player.Hp;

            pictureBoxMap.Image = new Bitmap(pictureBoxMap.Width, pictureBoxMap.Height);
            _graphics = Graphics.FromImage(pictureBoxMap.Image);

            _map.Print(_graphics);
            _player.Print(_graphics);

            for (int i = 0; i < _enemies.Count; i++)
            {
                int index = i;
                _enemiesTimers[index].Enabled = true;
                _enemiesTimers[index].Tick += (object sender, EventArgs e) => _enemiesTimers[index].Interval = _random.Next(100, 800);
                _enemiesTimers[index].Tick += (object sender, EventArgs e) => (_enemies[index] as Enemy).Walk(_map, (_player as Player));
                _enemies[index].Dying += () =>
                {
                    for (int j = 0; j < _enemies.Count; j++)
                    {
                        if (_enemies[j].IsAlive == true)
                        {
                            return;
                        }
                    }

                    GameSpaceSpawning?.Invoke();
                };
            }

            (_player as Player).ScoreChanged += () => (_player as Player).ShowStatistic(CheckCount);
            (_player as Player).HpChanged += () => { (_player as Player).ShowHp(CheckHp); playerHealthBar.MarqueeAnimationSpeed = 0; };
        }

        private void CheckCount(string str)
        {
            labelBountyCount.Text = str;
        }

        private void CheckHp(string str)
        {
            try
            {
                playerHealthBar.Value = int.Parse(str);
            }
            catch (Exception)
            {
                playerHealthBar.Value = 0;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pictureBoxMap.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            (_player as Player).Walk(_map, e);
            pictureBoxMap.Invalidate();
        }

        private void pictureBoxMap_Paint(object sender, PaintEventArgs e)
        {
            _map.Print(_graphics);
            for (int i = 0; i < _enemies.Count; i++)
            {
                int index = i;
                (_enemies[i] as Enemy).Update(new List<Unit> { _player }, _graphics);
                (_enemies[i] as Enemy).Print(_graphics);
            }
            _player.Print(_graphics);
            _player.Update(_enemies, _graphics);
        }

        private void pictureBoxMap_MouseClick(object sender, MouseEventArgs e)
        {
            (_player as Player).Shot(_map);
            //(_player as Player).Shooting += () => (_player as Enemy).Update(_graphics, _enemies, timer);
            //timerShot.Enabled = true;
        }

        private void timerShot_Tick(object sender, EventArgs e)
        {
            pictureBoxMap.Invalidate();
            //timerShot.Enabled = false;
        }

        private void AddEnemies()
        {
            int enemyCount = 12;
            for (int i = 0; i < enemyCount; i++)
            {
                int tempX = _random.Next(0, _map.Width);
                int tempY = _random.Next(0, _map.Height);
                while (_map.CheckIfCanWalk(tempX, tempY, tempX + Resource.enemyRight.Width, tempY + Resource.enemyRight.Height) == false)
                {
                    tempX = _random.Next(0, _map.Width);
                    tempY = _random.Next(0, _map.Height);
                }
                _enemies.Add(new Enemy(tempX, tempY, (_player as Player), _map));
                _enemiesTimers.Add(new Timer());
            }
        }
    }
}
