using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeProject.InteractionObjects
{
    public class InteractionObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Speed { get; private set; }
        public Image Sprite { get; set; }

        public InteractionObject(int x, int y, int width, int height, int speed, Image sprite)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Sprite = sprite;
            Speed = speed;
        }

        public virtual void Print(Graphics graphics)
        {
            graphics.DrawImage(Sprite, X, Y);
        }
    }
}
