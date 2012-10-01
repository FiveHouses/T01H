using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ADHDGame
{
    public abstract class Screen
    {
        protected ADHDGame game;

        protected Screen(ADHDGame g, SpriteBatch sb)
        {
            game = g;
        }

        public abstract void Update(GameTime time);
        public abstract void Draw(GameTime time);
        public abstract void Load();
        public abstract void Unload();
    }
}
