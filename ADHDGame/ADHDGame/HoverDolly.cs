using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ADHDGame
{
    class HoverDolly:ADHDObject
    {
        Vector2 vel, pos;
        Robot.Direction Dir;
        public bool moving = false;
        public bool stopping = false;
        float stopDist = 0.0f;
        Texture2D tex;
        const float speed = 4.0f;

        public HoverDolly(ADHDGame g, SpriteBatch sb, Texture2D t, Vector2 position)
            : base(g, sb, t, position, new Rectangle(0, 384, 64, 64), 3, 1.0f / 3.0f, true, 1)
        {
            pos = position;
            vel = new Vector2(0, 0);
            tex = t;
        }

        public override void Update(GameTime t)
        {
            if (moving)
            {
                var p = Robot.Adjacent(Center, Dir);

                if (game.GameScreen.stage.PassableLayer[p.X, p.Y])
                {
                    pos += vel;
                }
                else
                {
                    moving = false;
                    stopping = true;
                }
            }
            else if (stopping)
            {
                stopDist += Math.Abs(vel.X) + Math.Abs(vel.Y);
                if (stopDist > Stage.TileSize / 2)
                {
                    vel = Vector2.Zero;
                    pos = Stage.TileToPos(Stage.PosToTile(Center));
                    stopping = false;
                    moving = false;
                    game.GameScreen.stage.PassableLayer[Stage.PosToTile(Center).X, Stage.PosToTile(Center).Y] = false;
                }
                else
                {
                    pos += vel;
                }
            }
        }

        public override void Draw(Vector2 camera)
        {
            position = pos;
            base.Draw(camera);
        }

        public void push(Robot.Direction dir)
        {
            Dir = dir;

            switch(dir)
            {
                case Robot.Direction.Down:
                    vel.Y = speed;
                    vel.X = 0;
                    break;
                case Robot.Direction.Up:
                    vel.Y = -speed;
                    vel.X = 0;
                    break;
                case Robot.Direction.Right:
                    vel.X = speed;
                    vel.Y = 0;
                    break;
                case Robot.Direction.Left:
                    vel.X = -speed;
                    vel.Y = 0;
                    break;
            }

            Point tileToCheck = Robot.Adjacent(Center, dir);

            if (game.GameScreen.stage.PassableLayer[tileToCheck.X, tileToCheck.Y])
            {
                moving = true;
                game.GameScreen.stage.PassableLayer[Stage.PosToTile(Center).X, Stage.PosToTile(Center).Y] = true;
            }
        }
    }
}

