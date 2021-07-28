using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeProject.GameWorld
{
    public class MapElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Image Sprite { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public MapElement(Image sprite)
        {
            Sprite = sprite;
            Width = sprite.Width;
            Height = sprite.Height;

            int tempX = X;
            int tempY = Y;
        }

        public virtual void Print(Graphics graphics)
        {
            graphics.DrawImage(Sprite, X, Y);
        }
    }

    public class Floor : MapElement
    {
        public Floor(int x, int y) : base(Resource.floor)
        {
            X = x/* * Resource.floor.Width */;
            Y = y/* * Resource.floor.Height*/;
        }
    }

    public class Wall : MapElement
    {
        public Point[,] WallBoundaries;
        public Wall(int x, int y) : base(Resource.wall)
        {
            X = x/* * Resource.wall.Width*/;
            Y = y/* * Resource.wall.Height*/;
            WallBoundaries = new Point[25, 25];

            int tempY = Y;
            int tempX = X;
            for (int i = 0; i < WallBoundaries.GetLength(0); i++)
            {
                for (int j = 0; j < WallBoundaries.GetLength(1); j++)
                {
                    WallBoundaries[i, j] = new Point(tempX, tempY);
                }
                tempX = X;
                tempY++;
            }
        }
    }
}
