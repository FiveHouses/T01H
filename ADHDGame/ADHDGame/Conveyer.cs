using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ADHDGame
{
    class Conveyer : ADHDObject
    {
        public Robot.Direction cdir;
        public Rectangle rec;

        public Conveyer(ADHDGame g, SpriteBatch sb, Texture2D t, Vector2 position, Rectangle init, int nFrames, float rate, bool l, int type, Robot.Direction dir)
            : base(g, sb, t, position, init, nFrames, rate, l, type)
        {
            cdir = dir;
            rec = new Rectangle((int)position.X, (int)position.Y, Stage.TileSize, Stage.TileSize);
        }

        protected override float SortCoord()
        {
            return float.NegativeInfinity;
        }
    }
}
