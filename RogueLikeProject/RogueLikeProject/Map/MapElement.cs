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
}
