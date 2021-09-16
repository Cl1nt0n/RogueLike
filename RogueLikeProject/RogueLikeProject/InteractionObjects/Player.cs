using RogueLikeProject.GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogueLikeProject.InteractionObjects
{
    public class Player : Unit
    {
        public int Score { get; private set; }
        public event Action ScoreChanged;

        public Player(Map map) : base(Resource.playerRight, 1, 1, 900, 100, map)
        {
            Score = 0;
            Dying += () => Sprite = Resource.deadPlayer;
            Dying += () => MessageBox.Show("Вы были попущены :/");
        }

        public void Walk(Map map, KeyEventArgs keyDown)
        {
            if (IsAlive == true)
            {
                switch (keyDown.KeyData)
                {
                    case Keys.A:
                        this.Sprite = Resource.playerLeft;
                        if (X - Speed >= 0)
                        {
                            Direction.DirectionX = -1;
                            Direction.DirectionY = 0;

                            if (map.CheckIfCanWalk(X - Speed, Y, X + Sprite.Width - Speed, Y + Sprite.Height))
                            {
                                X -= Speed;
                            }
                            else
                            {
                                for (int i = Speed; i >= 0; i--)
                                {
                                    if (map.CheckIfCanWalk(X - i, Y, X + Sprite.Width - i, Y + Sprite.Height))
                                    {
                                        X -= i;
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.D:
                        this.Sprite = Resource.playerRight;
                        if (X + Speed <= map.Width - 12)
                        {
                            Direction.DirectionX = 1;
                            Direction.DirectionY = 0;
                            if (map.CheckIfCanWalk(X + Speed, Y, X + Sprite.Width + Speed, Y + Sprite.Height))
                            {
                                X += Speed;
                            }
                            else
                            {
                                for (int i = Speed; i >= 0; i--)
                                {
                                    if (map.CheckIfCanWalk(X + i, Y, X + Sprite.Width + i, Y + Sprite.Height))
                                    {
                                        X += i;
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.S:
                        this.Sprite = Resource.playerDown;
                        if (Y + Speed <= map.Height - 12)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = -1;
                            if (map.CheckIfCanWalk(X, Y + Speed, X + Sprite.Width, Y + Sprite.Height + Speed))
                            {
                                Y += Speed;
                            }
                            else
                            {
                                for (int i = Speed; i >= 0; i--)
                                {
                                    if (map.CheckIfCanWalk(X, Y + i, X + Sprite.Width, Y + Sprite.Height + i))
                                    {
                                        Y += i;
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.W:
                        this.Sprite = Resource.playerUp;
                        if (Y - Speed >= 0)
                        {
                            Direction.DirectionX = 0;
                            Direction.DirectionY = 1;
                            if (map.CheckIfCanWalk(X, Y - Speed, X + Sprite.Width, Y + Sprite.Height - Speed))
                            {
                                Y -= Speed;
                            }
                            else
                            {
                                for (int i = Speed; i >= 0; i--)
                                {
                                    if (map.CheckIfCanWalk(X, Y - i, X + Sprite.Width, Y + Sprite.Height - i))
                                    {
                                        Y -= i;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }

        public void TakeBounty(int bounty)
        {
            Score += bounty;
            ScoreChanged?.Invoke();
        }

        public void ShowStatistic(DelShowStats Statistics)
        {
            Statistics(Score.ToString());
        }

        public void ShowHp(DelShowStats Statistics)
        {
            Statistics(Hp.ToString());
        }

        public void Spawn(Map map)
        {
            Random random = new Random();
            X = random.Next(0, map.Width);
            Y = random.Next(0, map.Height);
            while (map.CheckIfCanWalk(X, Y, X + Sprite.Width, Y + Sprite.Height) == false)
            {
                X = random.Next(0, map.Width);
                Y = random.Next(0, map.Height);
            }
        }
    }
}
