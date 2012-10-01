using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ADHDGame
{
    public class EndScreen : Screen
    {
        public enum Ending { Bathroom, Reverse, Thrusters, Inaction, Help, Airlock, Banjo, Dubstep };
        private Ending end;
        private SpriteBatch sb;
        Texture2D banjoEnd;
        Texture2D airlockEnd;
        Texture2D space;
        Texture2D blackhole;
        Texture2D breaks;
        Texture2D thrusters;
        Texture2D tutorial;
        Texture2D inactive;
        Texture2D dub;
        Texture2D help;
        SoundEffectInstance explosion;
        double sec, sec2;
        bool first = true;
        bool first2 = true;
        bool tut = false;
        bool here = false;

        public EndScreen(ADHDGame g, SpriteBatch spb, Ending e)
            : base(g, spb)
        {
            end = e;
            sb = spb;
            game = g;
            banjoEnd = game.Content.Load<Texture2D>("newspaper_death01");
            space = game.Content.Load<Texture2D>("spaceBG");
            explosion = game.Content.Load<SoundEffect>("Explosion").CreateInstance();
            airlockEnd = game.Content.Load<Texture2D>("newspaper_death02");
            blackhole = game.Content.Load<Texture2D>("newspaper_death03");
            breaks = game.Content.Load<Texture2D>("newspaper_death04");
            thrusters = game.Content.Load<Texture2D>("newspaper_death05");
            tutorial = game.Content.Load<Texture2D>("tutorial");
            inactive = game.Content.Load<Texture2D>("newspaper_death06");
            dub = game.Content.Load<Texture2D>("newspaper_death07");
            help = game.Content.Load<Texture2D>("newspaper_death08");

            explosion.Play();

            if (e != Ending.Banjo)
            {
                MediaPlayer.Play(game.Content.Load<Song>("SpaceRock_V1"));
            }
        }

        public override void Update(GameTime time)
        {
            if (!tut)
            {
                if (first)
                {
                    sec = time.TotalGameTime.TotalMilliseconds;
                    first = false;
                }
                if ((Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed || Mouse.GetState().MiddleButton == ButtonState.Pressed
                        || Keyboard.GetState().GetPressedKeys().Length > 0) && time.TotalGameTime.TotalMilliseconds > sec + 250)
                {
                    game.LoadScreen(new TitleScreen(game, sb));
                }
            }
        }

        public override void Draw(GameTime time)
        {
            sb.Begin();
            switch(end)
            {
                case Ending.Airlock:
                    sb.Draw(space, space.Bounds, Color.White);
                    sb.Draw(airlockEnd, airlockEnd.Bounds, Color.White);
                    break;
                case Ending.Banjo:
                    sb.Draw(space, space.Bounds, Color.White);
                    sb.Draw(banjoEnd, banjoEnd.Bounds, Color.White);
                    break;
                case Ending.Bathroom:
                    sb.Draw(space, space.Bounds, Color.White);
                    sb.Draw(blackhole, blackhole.Bounds, Color.White);
                    break;
                case Ending.Thrusters:
                    sb.Draw(space, space.Bounds, Color.White);
                    sb.Draw(thrusters, thrusters.Bounds, Color.White);
                    break;
                case Ending.Help:
                    tut = true;
                    if (first2)
                    {
                        sec2 = time.TotalGameTime.TotalMilliseconds;
                        first2 = false;
                    }
                    sb.Draw(space, space.Bounds, Color.White);
                    if ((Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed || Mouse.GetState().MiddleButton == ButtonState.Pressed
                    || Keyboard.GetState().GetPressedKeys().Length > 0) && (time.TotalGameTime.TotalMilliseconds > sec2 + 250 || here))
                    {
                        sb.Draw(help, tutorial.Bounds, Color.White);
                        here = true;
                        tut = false;
                    }
                    else
                    {
                        if (here)
                        {
                            sb.Draw(help, tutorial.Bounds, Color.White);
                        }
                        else
                        {
                            sb.Draw(tutorial, help.Bounds, Color.White);
                        }
                    }
                    break;
                case Ending.Inaction:
                    sb.Draw(space, space.Bounds, Color.White);
                    sb.Draw(inactive, inactive.Bounds, Color.White);
                    break;
                case Ending.Reverse:
                    sb.Draw(space, space.Bounds, Color.White);
                    sb.Draw(breaks, breaks.Bounds, Color.White);
                    break;
                case Ending.Dubstep:
                    sb.Draw(space, space.Bounds, Color.White);
                    sb.Draw(dub, dub.Bounds, Color.White);
                    break;
            }
            sb.End();
        }

        public override void Load()
        {
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
        }
    }
}
