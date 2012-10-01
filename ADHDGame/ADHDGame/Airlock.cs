using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace ADHDGame
{
    class Airlock : ADHDObject
    {
        bool opening = false;
        public Airlock(ADHDGame g, SpriteBatch sb, Texture2D t, Vector2 position)
            : base(g, sb, t, position, new Rectangle(0, 0, 320, 192), 3, 1.0f / 8.0f, false, 100)
        {
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            Robot r = game.GameScreen.robot;
            if (!opening && !r.moving && IsOver(Stage.PosToTile(r.Center)))
            {
                //trigger airlock
                opening = true;
                SoundEffect sound = game.Content.Load<SoundEffect>("airlock");
                sound.Play();
                PlayAnimation();
                game.GameScreen.End(EndScreen.Ending.Airlock, 5.0f);
                game.GameScreen.robot.scaling = true;
                if(game.GameScreen.robot.position.Y > position.Y - 96)
                {
                    //game.GameScreen.robot.
                }
            }
        }

        protected override float SortCoord()
        {
            return float.NegativeInfinity;// base.SortCoord();
        }
    }
}

