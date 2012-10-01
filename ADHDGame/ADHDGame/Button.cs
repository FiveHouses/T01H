using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace ADHDGame
{
    class Button : ADHDObject
    {
        EndScreen.Ending ending;
        float endTime;
        SoundEffect b1, b2;

        public Button(ADHDGame g, SpriteBatch sb, Texture2D t, Vector2 pos, EndScreen.Ending end, float time)
            : base(g, sb, t, pos, Rectangle.Empty, 0, 0.0f, false, 0)
        {
            ending = end;
            endTime = time;

            b1 = g.Content.Load<SoundEffect>("Bloop_01");
            b2 = g.Content.Load<SoundEffect>("bloop_02");
        }

        public void push()
        {
            if (ending == EndScreen.Ending.Reverse)
            {
                b2.Play();
            }
            else if (ending == EndScreen.Ending.Thrusters)
            {
                b1.Play();
            }

            game.GameScreen.End(ending, endTime);
        }
    }
}
