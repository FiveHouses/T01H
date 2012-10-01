using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ADHDGame
{
    public class ADHDObject : DrawComparable
    {
        Texture2D tex;
        SpriteBatch batch;
        public Vector2 position;
        Point upLeft;
        Point lowerRight;
        Animation anim;
        public int type = -1; //1 = hoverDolly, //2 = conveyer, //0 = button
        protected Vector2 size;
        protected ADHDGame game;

        public ADHDObject(ADHDGame g, SpriteBatch sb, Texture2D t, Vector2 pos, Rectangle init, int nFrames, float rate, bool l, int TYPE)
        {
            game = g;
            upLeft = Stage.PosToTile(pos);
            lowerRight = new Point(
                upLeft.X + init.Width / Stage.TileSize - 1,
                upLeft.Y + init.Height / Stage.TileSize - 1);
            position = pos;

            tex = t;
            batch = sb;
            size = new Vector2(init.Width, init.Height);

            anim = new Animation(init, nFrames, rate, l);
            if (l)
            {
                anim.Play();
            }
            type = TYPE;
        }

        protected override float SortCoord()
        {
            return position.Y;
        }

        public Vector2 Center
        {
            get { return position + new Vector2(size.X, size.Y) / 2.0f; }
        }

        /// <summary>
        /// are we OVER THE OBJECT?
        /// </summary>
        /// <param name="tile">position of "we"</param>
        /// <returns></returns>
        public bool IsOver(Point tile)
        {
            return
                tile.X >= upLeft.X &&
                tile.X <= lowerRight.X &&
                tile.Y >= upLeft.Y &&
                tile.Y <= lowerRight.Y;
        }

        public virtual void Update(GameTime time)
        {
            anim.Update(time);
        }

        public override void Draw(Vector2 camera)
        {
            batch.Draw(tex, position - camera, anim.currSource, Color.White);
        }

        public void PlayAnimation()
        {
            anim.Play();
        }
    }
}
