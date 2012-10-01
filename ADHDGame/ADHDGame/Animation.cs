using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ADHDGame
{
    class Animation
    {
        Rectangle initSource;
        public Rectangle currSource;
        int totalFrames;
        int currFrame;
        float animTimer;
        float animRate;
        public bool animating;
        bool loop;

        public Animation(Rectangle init, int frames, float rate, bool l)
        {
            totalFrames = frames;
            currFrame = 0;
            initSource = init;
            currSource = init;
            animTimer = 0.0f;
            animRate = rate;
            loop = l;

            //start off stopped, play explicitly
            animating = false;
        }

        public void Update(GameTime time)
        {
            if (animating)
            {
                animTimer += (float)time.ElapsedGameTime.TotalSeconds;

                if (animTimer > animRate)
                {
                    animTimer -= animRate;

                    //next source frame
                    if (!loop && currFrame + 1 >= totalFrames)
                    {
                        animating = false;
                        return;
                    }

                    currFrame = (currFrame + 1) % totalFrames;
                    currSource = new Rectangle(
                        initSource.X + currFrame * initSource.Width,
                        initSource.Y, initSource.Width, initSource.Height);
                }
            }
        }

        public void Play()
        {
            animating = true;
        }

        public void Stop()
        {
            animating = false;
            currFrame = 0;
            animTimer = 0.0f;
        }
    }
}
