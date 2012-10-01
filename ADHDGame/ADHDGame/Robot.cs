using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ADHDGame
{
    public class Robot : DrawComparable
    {
        public Vector2 position;
        public bool moving = false;
        Stage stage;
        SpriteEffects flip;

        SpriteBatch batch;
        Texture2D tex;
        readonly Animation right;
        readonly Animation up;
        readonly Animation down;
        readonly Animation banjo;
        readonly Animation banjoPlay;
        readonly Animation die;
        readonly Animation pushRight;
        readonly Animation pushUp;
        readonly Animation pushDown;
        Animation currAnim;
        const float rate = 1.0f / 30.0f;
        public bool scaling = false;
        public static readonly Point Size = new Point(103, 103);

        float dist = 0.0f;
        float travelled = 0.0f;
        public enum Direction { Left, Right, Up, Down, None }
        public Direction Dir { get; set; }
        const float speed = 160.0f;
        Vector2 velocity;
        ADHDGame game;

        public Robot(ADHDGame g, Stage st)
        {
            //init anims
            right = new Animation(new Rectangle(0, 206, Size.X, Size.Y), 9, rate, true);
            up = new Animation(new Rectangle(0, 103, Size.X, Size.Y), 9, rate, true);
            down = new Animation(new Rectangle(0, 0, Size.X, Size.Y), 9, rate, true);
            banjo = new Animation(new Rectangle(0, 618, Size.X, Size.Y), 9, 1.0f / 10.0f, false);
            banjoPlay = new Animation(new Rectangle(0, 721, Size.X, Size.Y), 9, 1.0f / 10.0f, true);
            die = new Animation(new Rectangle(0, 824, Size.X, Size.Y), 9, 1.0f / 10.0f, false);
            pushDown = new Animation(new Rectangle(0, 309, Size.X, Size.Y), 9, 1.0f / 20.0f, false);
            pushUp = new Animation(new Rectangle(0, 412, Size.X, Size.Y), 9, 1.0f / 20.0f, false);
            pushRight = new Animation(new Rectangle(0, 515, Size.X, Size.Y), 9, 1.0f / 20.0f, false);

            tex = g.Content.Load<Texture2D>("robot");
            batch = st.batch;
            stage = st;
            position = Stage.TileToPos(new Point(11, 17));
            currAnim = down;
            currAnim.Stop();
            Dir = Direction.Down;
            velocity = Vector2.Zero;
            flip = SpriteEffects.None;
            game = g;
            SnapToTile();
        }

        public Vector2 Center
        {
            get { return position + new Vector2(Size.X, Size.Y) / 2.0f; }
        }

        public void SnapToTile()
        {
            //HACK the fact that he's not tile-sized
            Point tileCenter = Stage.PosToTile(Center);
            float diffX = Size.X - Stage.TileSize;
            float diffY = Size.Y - Stage.TileSize;
            position = new Vector2(tileCenter.X * Stage.TileSize - (diffX / 2.0f), tileCenter.Y * Stage.TileSize - (diffY));
        }

        public void Update(GameTime time)
        {
            currAnim.Update(time);
            float t = (float)time.ElapsedGameTime.TotalSeconds;

            //scale down
            if (scaling)
            {
                scale -= 0.2f * t;
            }

            if (velocity != Vector2.Zero)
            {
                position += velocity * t;
                travelled += Math.Abs(velocity.X * t) + Math.Abs(velocity.Y * t);

                if (travelled >= dist)
                {
                    StopMove();
                    moving = false;
                }
            }
            if (!moving)
            {
                Direction onConveyor = OnConveyor();
                if (onConveyor != Direction.None)
                {
                    BeginMove(onConveyor, 1);
                }
            }
            if (RobotActions.Instance.energy <= 0 && game.GameScreen.endingTime <= 0.0f)
            {
                currAnim = die;
                currAnim.Play();
                game.GameScreen.End(EndScreen.Ending.Inaction, 4);
            }

        }

        private Direction OnConveyor()
        {
            Direction on = Direction.None;
            foreach (ADHDObject o in game.GameScreen.stage.objects)
            {
                Conveyer c = o as Conveyer;
                if (c != null && c.rec.Contains(new Point((int)Center.X, (int)Center.Y)))
                {
                    on = c.cdir;
                    break;
                }
            }

            return on;
        }

        float scale = 1.0f;
        public override void Draw(Vector2 camera)
        {
            batch.Draw(tex, position - camera, currAnim.currSource, Color.White, 0.0f, Vector2.Zero, scale, flip, 0.0f);
            
        }

        protected override float SortCoord()
        {
            return position.Y + 15.0f;
        }

        public void StopMove()
        {
            currAnim.Stop();
            SnapToTile();
            travelled = 0.0f;
            dist = 0.0f;
            velocity = Vector2.Zero;
        }

        public bool BeginMove(Direction dir, int spaces)
        {
            Dir = dir;
            Rotate();
            var p = Adjacent(Center, dir);
            if (!stage.PassableLayer[p.X, p.Y])
            {
                return false;
            }

            moving = true;
            travelled = 0.0f;
            dist = spaces * Stage.TileSize;

            switch (dir)
            {
                case Direction.Left:
                    velocity.X = -speed;
                    velocity.Y = 0;
                    break;
                case Direction.Right:
                    velocity.X = speed;
                    velocity.Y = 0;
                    break;
                case Direction.Down:
                    velocity.X = 0;
                    velocity.Y = speed;
                    break;
                case Direction.Up:
                    velocity.X = 0;
                    velocity.Y = -speed;
                    break;
            }

            currAnim.Play();
            return true;
        }

        public Point AdjacentTile()
        {
            return Adjacent(Center, Dir);
        }

        public static Point Adjacent(Vector2 centerPos, Direction dir)
        {
            var p = Stage.PosToTile(centerPos);

            switch (dir)
            {
                case Direction.Left:
                    p.X--;
                    break;
                case Direction.Right:
                    p.X++;
                    break;
                case Direction.Up:
                    p.Y--;
                    break;
                case Direction.Down:
                    p.Y++;
                    break;
            }

            return p;
        }

        public void PlayPush()
        {
            switch (Dir)
            {
                case Direction.Left:
                    flip = SpriteEffects.FlipHorizontally;
                    currAnim = pushRight;
                    break;
                case Direction.Right:
                    currAnim = pushRight;
                    break;
                case Direction.Up:
                    currAnim = pushUp;
                    break;
                case Direction.Down:
                    currAnim = pushDown;
                    break;
            }

            currAnim.Play();
        }

        public void Rotate()
        {
            flip = SpriteEffects.None;
            switch (Dir)
            {
                case Direction.Left:
                    flip = SpriteEffects.FlipHorizontally;
                    currAnim = right;
                    break;
                case Direction.Right:
                    currAnim = right;
                    break;
                case Direction.Up:
                    currAnim = up;
                    break;
                case Direction.Down:
                    currAnim = down;
                    break;
            }
        }

        public void Banjo()
        {
            currAnim = banjo;
            currAnim.Play();
        }

        public void BanjoPlay()
        {
            currAnim = banjoPlay;
            currAnim.Play();
        }

        public bool IsBanjoOut()
        {
            return !currAnim.animating;
        }
    }
}