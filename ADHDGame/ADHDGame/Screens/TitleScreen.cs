using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace ADHDGame
{
    public class TitleScreen : Screen
    {
        SpriteBatch sb;
        GameplayScreen gs;
        SoundEffectInstance snoring;
        SoundEffectInstance alarm;
        List<Texture2D> titleSequence;
        Animation titleAnim;
        Texture2D introNews;
        bool paper = false;
        bool mouse = false;
        bool first = true;
        double sec, sec2;
        float animTimer = 0.0f;
        float animRate = 0.07f;
        Texture2D space;

        int frame = 0;
        public TitleScreen(ADHDGame g, SpriteBatch spb)
            : base(g,spb)
        {
            RobotActions.Instance.energy = 20;
            paper = false;
            mouse = false;
            game = g;
            sb = spb;
            titleSequence = new List<Texture2D>(40);
            gs = new GameplayScreen(g, spb);
            titleAnim = new Animation(new Rectangle(0, 0, 1024, 768), 40, 30, true);
            introNews = g.Content.Load<Texture2D>("newspaper_intro");
            space = g.Content.Load<Texture2D>("spaceBG");
            int i;
            for (i = 0; i < 40; i++)
            {
                titleSequence.Add(g.Content.Load<Texture2D>("titlescreen\\titlescreen" + (i+1).ToString()));
            }
            snoring = g.Content.Load<SoundEffect>("Snoring").CreateInstance();
            alarm = g.Content.Load<SoundEffect>("alarm_02").CreateInstance();

            alarm.IsLooped = true;
            alarm.Volume = 0.2f;
            snoring.IsLooped = true;
            snoring.Play();
        }

        public override void Update(GameTime time)
        {
            if (!paper)
            {
                if(first)
                {
                    sec2 = time.TotalGameTime.TotalMilliseconds;
                    first = false;
                }
                animTimer += (float)time.ElapsedGameTime.TotalSeconds;
                if (animTimer > animRate)
                {
                    animTimer -= animRate;
                    if (frame < 39)
                    {
                        frame++;
                    }
                    else
                    {
                        frame = 0;
                    }
                }

                if ((Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed || Mouse.GetState().MiddleButton == ButtonState.Pressed
                    || Keyboard.GetState().GetPressedKeys().Length > 0) &&  time.TotalGameTime.TotalMilliseconds > sec2 + 250)
                {
                    mouse = true;
                    paper = true;
                    snoring.Stop();
                    sec = time.TotalGameTime.TotalMilliseconds;
                }
            }
            else
            {
                alarm.Play();
                if ((Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed || Mouse.GetState().MiddleButton == ButtonState.Pressed
                    || Keyboard.GetState().GetPressedKeys().Length > 0 && !mouse) && time.TotalGameTime.TotalMilliseconds > sec + 250)
                {
                    alarm.Stop();
                    game.LoadScreen(gs);
                }

                if (Mouse.GetState().LeftButton == ButtonState.Released || Mouse.GetState().RightButton == ButtonState.Released || Mouse.GetState().MiddleButton == ButtonState.Released
                    || Keyboard.GetState().GetPressedKeys().Length == 0)
                {
                    mouse = false;
                }
            }
            //titleSequence.ForEach((b) => { b.draw(sb); });
            //play.Update();
            /*if(play.isClicked())
            {
                game.LoadScreen(gs);
                play.resetClicked();
            }*/
        }

        public override void Draw(GameTime time)
        {
            sb.Begin();
            if (paper)
            {
                sb.Draw(space, space.Bounds, Color.White);
                sb.Draw(introNews, introNews.Bounds, Color.White);
            }
            else
            {
                sb.Draw(titleSequence[frame], new Rectangle(0, 0, 1024, 768), Color.White);
            }
            sb.End();
        }

        public override void Load()
        {
        }

        public override void Unload()
        {
            alarm.Stop();
        }
    }
}
