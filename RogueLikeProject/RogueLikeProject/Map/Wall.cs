using System.Drawing;

namespace RogueLikeProject.GameWorld
{
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
