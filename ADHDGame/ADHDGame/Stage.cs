using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ADHDGame
{
    public class Stage
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int[,] MainLayer { get; private set; }
        public int[,] TopLayer { get; set; }
        public bool[,] PassableLayer { get; set; }
        public List<ADHDObject> objects;
        Texture2D mainTex;
        Texture2D objTex;
        public SpriteBatch batch;
        private ADHDGame game;

        //consts
        public const int TileSize = 64;
        const int TileSheetWidth = 16;
        const int TileSheetHeight = 16;
        const string mainLayerName = "Tile Layer 1";
        const string topLayerName = "Tile Layer 2";
        const string texName = "ADHD_Tileset";
        const string objTexName = "ADHD_ObjectAtlas";
        public readonly List<int> passables = new List<int> {
            TileToID(0, 5),
            TileToID(1, 5),
            TileToID(2, 5),
            TileToID(3, 5),
            TileToID(11, 5),
            TileToID(13, 5),
            TileToID(10, 6),
            TileToID(11, 6),
            TileToID(12, 6),
            TileToID(13, 6),
            TileToID(14, 6),
            TileToID(0, 12),
            TileToID(0, 13),
            TileToID(0, 14),
            TileToID(0, 15),
            TileToID(0, 6),
            TileToID(3, 4),
            TileToID(1, 6)
        };

        public Stage(ADHDGame g, string file)
        {
            game = g;
            mainTex = g.Content.Load<Texture2D>(texName);
            objTex = g.Content.Load<Texture2D>(objTexName);
            batch = new SpriteBatch(g.GraphicsDevice);

            //get xml data in a document
            XmlDocument xml = new XmlDocument();
            string xmlString = "";
            using (StreamReader fr = new StreamReader(TitleContainer.OpenStream("Content\\" + file)))
            {
                xmlString = fr.ReadToEnd();
            }
            xml.LoadXml(xmlString);

            //read the width and height
            Width = Convert.ToInt32(xml.SelectSingleNode("map/layer/@width").Value);
            Height = Convert.ToInt32(xml.SelectSingleNode("map/layer/@height").Value);

            InitObjects();

            MainLayer = new int[Width, Height];
            TopLayer = new int[Width, Height];

            //get main layer
            int x = 0, y = 0;
            XmlNodeList list = xml.SelectNodes("map/layer[@name='" + mainLayerName + "']/data/tile/@gid");
            foreach (XmlNode node in list)
            {
                //minus 1 because Tiled uses 0 as blank, we won't!
                MainLayer[x, y] = Convert.ToInt32(node.Value) - 1;

                //hack special objects
                if (MainLayer[x, y] == TileToID(0, 12))
                {
                    AddConveyor(new Point(x, y), Robot.Direction.Right);
                }
                else if (MainLayer[x, y] == TileToID(0, 13))
                {
                    AddConveyor(new Point(x, y), Robot.Direction.Left);
                }
                else if (MainLayer[x, y] == TileToID(0, 14))
                {
                    AddConveyor(new Point(x, y), Robot.Direction.Up);
                }
                else if (MainLayer[x, y] == TileToID(0, 15))
                {
                    AddConveyor(new Point(x, y), Robot.Direction.Down);
                }

                ++x;

                //test for next row
                if (x >= Width)
                {
                    ++y;
                    x = 0;
                }
            }

            //get main layer
            x = 0;
            y = 0;
            list = xml.SelectNodes("map/layer[@name='" + topLayerName + "']/data/tile/@gid");
            foreach (XmlNode node in list)
            {
                //minus 1 because Tiled uses 0 as blank, we won't!
                TopLayer[x, y] = Convert.ToInt32(node.Value) - 1;

                ++x;

                //test for next row
                if (x >= Width)
                {
                    ++y;
                    x = 0;
                }
            }

            InitPassables();
        }

        private void AddConveyor(Point tile, Robot.Direction dir)
        {
            Rectangle initSource = new Rectangle(832, 192, TileSize, TileSize);
            switch (dir)
            {
                case Robot.Direction.Left:
                    initSource.Y += 64;
                    break;
                case Robot.Direction.Up:
                    initSource.Y += 128;
                    break;
                case Robot.Direction.Down:
                    initSource.Y += 192;
                    break;
            }
            var con = new Conveyer(game, batch,
                game.Content.Load<Texture2D>(objTexName),
                TileToPos(tile), initSource, 3, 1.0f / 10.0f, true, 2, dir);
            con.PlayAnimation();
            objects.Add(con);
        }

        private static int TileToID(int x, int y)
        {
            return y * TileSheetWidth + x;
        }

        public static Point PosToTile(Vector2 pos)
        {
            return new Point((int)pos.X / TileSize, (int)pos.Y / TileSize);
        }

        public static Vector2 TileToPos(Point tile)
        {
            return new Vector2(tile.X * TileSize, tile.Y * TileSize);
        }

        private void InitObjects()
        {
            var objs = new ADHDObject[] {
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(9, 9))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(14, 10))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(15, 15))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(8, 22))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(14, 21))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(17, 21))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(17, 27))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(7, 21))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(7, 15))),
                new HoverDolly(game, batch, objTex, Stage.TileToPos(new Point(17, 27))),
                new Button(game, batch, objTex, Stage.TileToPos(new Point(10, 6)), EndScreen.Ending.Thrusters, 0.4f),
                new Button(game, batch, objTex, Stage.TileToPos(new Point(12, 6)), EndScreen.Ending.Reverse, 0.4f),
                new Button(game, batch, objTex, Stage.TileToPos(new Point(9, 27)), EndScreen.Ending.Dubstep, 2.0f),
                new Button(game, batch, objTex, Stage.TileToPos(new Point(18, 26)), EndScreen.Ending.Bathroom, 1.0f),
                new Airlock(game, batch, objTex, Stage.TileToPos(new Point(9, 13)))
            };

            objects = new List<ADHDObject>(objs);
        }

        private void InitPassables()
        {
            PassableLayer = new bool[Width, Height];

            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    PassableLayer[x, y] = IsPassable(MainLayer[x, y]);
                }
            }

            foreach (ADHDObject o in objects)
            {
                if (o is HoverDolly)
                {
                    var p = PosToTile(o.Center);
                    PassableLayer[p.X, p.Y] = false;
                }
            }
        }

        private bool IsPassable(int gid)
        {
            return passables.Contains(gid);
        }

        public void Update(GameTime time)
        {
            objects.ForEach((o) => { o.Update(time); });
        }

        public void Draw(Vector2 camera)
        {
            //draw all main layer tiles first
            batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    batch.Draw(mainTex, new Vector2(x * TileSize, y * TileSize) - camera, GetSourceRect(MainLayer[x, y]), Color.White);
                }
            }
            batch.End();
        }

        public void DrawTopLayer(Vector2 camera)
        {
            //draw all top layer tiles
            batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    batch.Draw(mainTex, new Vector2(x * TileSize, y * TileSize) - camera, GetSourceRect(TopLayer[x, y]), Color.White);
                }
            }
            batch.End();
        }

        //grabs source rect from gid
        private Rectangle GetSourceRect(int gid)
        {
            int tileX = gid % TileSheetWidth;
            int tileY = gid / TileSheetWidth;
            return new Rectangle(tileX * TileSize, tileY * TileSize, TileSize, TileSize);
        }
    }
}
