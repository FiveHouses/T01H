using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ADHDGame
{
    class RobotActions
    {
        ADHDGame g;
        private static RobotActions instance;
        private Vector2 push = new Vector2(0,0);
        private Vector2 adj = new Vector2(0, 0);
        private RobotActions(){}
        public int energy = 20;
        SoundEffectInstance banjo;

        public static RobotActions Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RobotActions();
                }
                return instance;
            }
        }

        public void setADHDGame(ADHDGame game)
        {
            g = game;
        }

        public void MoveForward(int spaces)
        {
            if (!g.GameScreen.robot.moving && energy > 0)
            {
                if (g.GameScreen.robot.BeginMove(Robot.Direction.Up, spaces))
                {
                    energy--;
                }
            }
        }

        public void MoveBack(int spaces)
        {
            if (!g.GameScreen.robot.moving && energy > 0)
            {
                if (g.GameScreen.robot.BeginMove(Robot.Direction.Down, spaces))
                {
                    energy--;
                }
            }
        }

        public void MoveLeft(int spaces)
        {
            if (!g.GameScreen.robot.moving && energy > 0)
            {
                if (g.GameScreen.robot.BeginMove(Robot.Direction.Left, spaces))
                {
                    energy--;
                }
            }
        }

        public void MoveRight(int spaces)
        {
            if (!g.GameScreen.robot.moving && energy > 0)
            {
                if (g.GameScreen.robot.BeginMove(Robot.Direction.Right, spaces))
                {
                    energy--;
                }
            }
        }

        public void PieceOfMind()
        {
            if(energy > 10)
            {
                g.GameScreen.dubstepOff();
                energy -= 10;
            }
        }

        public void Banjo()
        {
            if (energy > 0)
            {
                energy--;
                banjo = g.Content.Load<SoundEffect>("BanjoRift_01").CreateInstance();
                g.GameScreen.dubstepOff();
                banjo.Play();
                g.GameScreen.robot.Banjo();
                g.GameScreen.End(EndScreen.Ending.Banjo, 7f);
            }
        }

        public void PushObject()
        {
            if(energy > 0)
            {
                energy--;
                push = g.GameScreen.robot.position;
                adj.X = g.GameScreen.robot.AdjacentTile().X;
                adj.Y = g.GameScreen.robot.AdjacentTile().Y;
                if(!g.GameScreen.stage.PassableLayer[Stage.PosToTile(adj).X, Stage.PosToTile(adj).Y])
                {
                    g.GameScreen.stage.objects.ForEach((o) => {
                        if(o.type == 1)
                        {
                            if (Stage.PosToTile(o.Center) == g.GameScreen.robot.AdjacentTile())
                            {
                                g.GameScreen.robot.PlayPush();
                                ((HoverDolly)o).push(g.GameScreen.robot.Dir);
                            }
                        };
                    });
                }
            }
        }

        public void ActivateObject()
        {
            if (energy > 0)
            {
                energy--;
            Point tile = g.GameScreen.robot.AdjacentTile();

            g.GameScreen.stage.objects.ForEach((o) =>
            {
                if (Stage.PosToTile(o.Center) == tile)
                {
                    if (o is Button)
                    {
                        g.GameScreen.robot.PlayPush();
                        ((Button)o).push();
                    }
                }
            });
            }
        }

        public void Help()
        {
            g.GameScreen.End(EndScreen.Ending.Help,0.0001f);
        }
    }
}
