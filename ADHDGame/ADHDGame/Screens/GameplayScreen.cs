using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ADHDGame
{
    public class GameplayScreen : Screen
    {
        public Stage stage;
        GuiHUD hud;
        public SpriteBatch sb;
        public Robot robot;
        Vector2 camera;
        List<DrawComparable> drawComps;
        Texture2D space;
        int spacePos = 0;
        bool hudEnabled;
        public float endingTime = 0.0f;
        float endingTimer = 0.0f;
        EndScreen.Ending ending;
        Texture2D blackHole;
        float blackHoleRotate = 0.0f;
        float blackHoleScale = 0.2f;
        Vector2 blackHolePos = new Vector2(640.0f, 384.0f);
        Vector2 blackHoleOrigin;
        Vector2 bhsize;
        float whiteTime;

        public GameplayScreen(ADHDGame g, SpriteBatch spb)
            : base(g,spb)
        {
            sb = spb;
            space = g.Content.Load<Texture2D>("spaceBG");
            hudEnabled = true;
        }

        public void dubstepOff()
        {
            MediaPlayer.Stop();
        }

        public override void Load()
        {
            stage = new Stage(game, "level.tmx");
            hud = new GuiHUD(game, sb);
            robot = new Robot(game, stage);
            CenterCamera();
            drawComps = new List<DrawComparable>(stage.objects);
            drawComps.Add(robot);
            MediaPlayer.Play(game.Content.Load<Song>("SpaceDub_V1"));
            MediaPlayer.IsRepeating = true;
            blackHole = game.Content.Load<Texture2D>("blackhole");
            bhsize = new Vector2(blackHole.Width, blackHole.Height);
        }

        /// <summary>
        /// FAKE CENTER HACK!!!!!!!!!!!!!
        /// </summary>
        public void CenterCamera()
        {
            camera = robot.position - new Vector2(589, 333);
        }

        public override void Unload()
        {
        }

        public void End(EndScreen.Ending end, float time)
        {
            ending = end;
            endingTime = time;
            hudEnabled = false;
        }

        readonly Vector2 bhCenter = new Vector2(500.0f, 500.0f);

        public override void Update(GameTime time)
        {
            float t = (float)time.ElapsedGameTime.TotalSeconds;

            stage.Update(time);
            if (hudEnabled)
            {
                hud.Update();
            }
            robot.Update(time);
            if (spacePos == 766)
            {
                spacePos = 0;
            }
            spacePos++;

            CenterCamera();

             //ENDING SEQUENCE
            if (endingTime > 0.0f)
            {
                endingTimer += (float)time.ElapsedGameTime.TotalSeconds;
                if (endingTimer > endingTime)
                {
                    game.LoadScreen(new EndScreen(game, sb, ending));
                }

                switch (ending)
                {
                    case EndScreen.Ending.Banjo:
                        if (robot.IsBanjoOut())
                        {
                            robot.BanjoPlay();
                        }
                        break;
                    case EndScreen.Ending.Bathroom:
                        blackHoleRotate += 8.0f * t;
                        blackHoleScale += 2.0f * t;
                        blackHoleOrigin = bhsize / 2.0f;
                        break;
                    case EndScreen.Ending.Dubstep:
                        MediaPlayer.Volume = 2.0f;
                        whiteTime += t;
                        break;
                }
            }
        }

        public override void Draw(GameTime time)
        {
            sb.Begin();
            sb.Draw(space, new Rectangle(0, spacePos, space.Width, space.Height), Color.White);
            sb.Draw(space, new Rectangle(0, spacePos - 768, space.Width, space.Height), Color.White);
            sb.End();
            CenterCamera();
            stage.Draw(camera);
            drawComps.Sort();
            stage.batch.Begin();
            drawComps.ForEach((d) => { d.Draw(camera); });
            stage.batch.End();
            stage.DrawTopLayer(camera);
            hud.Draw(sb);

            //ending
            if (endingTime > 0.0f)
            {
                switch (ending)
                {
                    case EndScreen.Ending.Bathroom:
                        sb.Begin();
                        sb.Draw(blackHole, blackHolePos, null, Color.White, blackHoleRotate, blackHoleOrigin, blackHoleScale, SpriteEffects.None, 0.0f);
                        sb.End();
                        break;
                    case EndScreen.Ending.Dubstep:
                        if (whiteTime % 0.2f > 0.1f)
                        {
                            game.GraphicsDevice.Clear(Color.White);
                        }
                        break;
                }
            }
        }
    }
}
