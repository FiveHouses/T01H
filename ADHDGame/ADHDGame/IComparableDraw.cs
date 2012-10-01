using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ADHDGame
{
    public abstract class DrawComparable : IComparable<DrawComparable>
    {
        protected abstract float SortCoord();
        public abstract void Draw(Vector2 camera);

        public int CompareTo(DrawComparable other)
        {
            return SortCoord().CompareTo(other.SortCoord());
        }
    }
}
