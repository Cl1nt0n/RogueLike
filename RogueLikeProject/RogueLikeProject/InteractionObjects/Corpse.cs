using RogueLikeProject.GameWorld;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeProject.InteractionObjects
{
    class Corpse : InteractionObject
    {
        public Corpse(Unit unit, Image sprite) : base(unit.X, unit.Y, sprite.Width, sprite.Height, sprite)
        {
            
        }
    }
}
