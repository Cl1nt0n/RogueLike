using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeProject.GameWorld
{
    public class Map
    {
        private Random _random;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Wall> Walls { get; private set; }
        public List<Floor> Floors { get; private set; }

        private int _oneCellWidth;

        public Map()
        {
            Walls = new List<Wall>();
            Floors = new List<Floor>();
            _random = new Random();

            _oneCellWidth = 40;
            Width = 800;
            Height = 800;
        }

        public void Load()
        {
            for (int i = 0; i < Width; i += _oneCellWidth)
            {
                for (int j = 0; j < Height; j += _oneCellWidth)
                {
                    switch (_random.Next(0, 10))
                    {
                        case 0:
                            Walls.Add(new Wall(j, i));
                            break;
                        default:
                            Floors.Add(new Floor(j, i));
                            break;
                    }
                }
            }
        }

        public bool CheckIfWall(int xTopLeft, int yTopLeft, int xRightBottom, int yRightBottom)
        {
            foreach (var wall in Walls)
            {
                if (xRightBottom >= wall.X && xTopLeft <= wall.X + wall.Width && yRightBottom >= wall.Y && yTopLeft <= wall.Y + wall.Height)
                {
                    return false;
                }
            }

            return true;
        }

        public void Print(Graphics graphics)
        {
            foreach (var item in Walls)
            {
                item.Print(graphics);
            }
            foreach (var item in Floors)
            {
                item.Print(graphics);
            }
        }
    }
}
