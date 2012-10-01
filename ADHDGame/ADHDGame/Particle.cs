using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ADHDGame
{
    public class Particle
    {
        private Vector3 fadeData;
        private Vector2 pos;
        private Vector2 vel;
        private Texture2D tex;
        private float angle;
        private float angleVel;
        private Color color;
        private Color toColor; 
        private float size;
        private float sizeChange;
        private float sizeMax;
        private float sizeMin;
        private int initialLife;
        private int life;
        private float alpha;
        private float alphaChange;
        private int r, g, b;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float particleAngle, float turnRate, Color col, Color toCol, float particleSize, float particleSizeChange,
            float pSizeMax, float pSizeMin, int timeToLive, float transparency, float transparencyChangeRate)
        {
            tex = texture;
            pos = position;
            vel = velocity;
            angle = particleAngle;
            angleVel = turnRate;
            color = col;
            size = particleSize;
            sizeMax = pSizeMax;
            sizeMin = pSizeMin;
            sizeChange = particleSizeChange;
            initialLife = timeToLive;
            life = timeToLive;
            alpha = transparency;
            alphaChange = transparencyChangeRate;
            toColor = toCol;
            fadeData = fadeToColor(color, toColor);
            r = color.R;
            g = color.G;
            b = color.B;
        }

        public void Update()
        {
            life--;
            pos += vel;
            angle += angleVel;
            alpha = (1-((float)life / (float)initialLife));
            float nSize = size + sizeChange;
            if (nSize <= sizeMax && nSize >= sizeMin)
            {
                size += sizeChange;
            }
            
            if((r > toColor.R && r+(int)fadeData.X < toColor.R) || (r < toColor.R && r + (int)fadeData.X > toColor.R))
            {
                r = toColor.R;
            }
            if ((g > toColor.G && g + (int)fadeData.Y < toColor.G) || (g < toColor.G && g + (int)fadeData.Y > toColor.G))
            {
                g = toColor.G;
            }
            if ((b > toColor.B && b + (int)fadeData.Z < toColor.B) || (b < toColor.B && b + (int)fadeData.Z > toColor.B))
            {
                b = toColor.B;
            }

            if (r != toColor.R)
            {
                r -= (int)fadeData.X;
            }

            if (g != toColor.G)
            {
                g -= (int)fadeData.Y;
            }

            if (b != toColor.B)
            {
                b -= (int)fadeData.Z;
            }
            color = new Color(r, g, b);
        }

        //take difference between rgb values, divide by time interval (lifetime of particle)
        public Vector3 fadeToColor(Color from, Color to)
        {
            double r = from.R;
            double g = from.G;
            double b = from.B;
            r -= to.R;
            g -= to.G;
            b -= to.B;

            int Rchange = (int)Math.Ceiling(r / life);
            int Gchange = (int)Math.Ceiling(g / life);
            int Bchange = (int)Math.Ceiling(b / life);

            return new Vector3(Rchange,Gchange,Bchange);
        }

        public void Draw(SpriteBatch sb)
        {
            Rectangle rect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 origin = new Vector2(tex.Width / 2, tex.Height / 2);
            sb.Draw(tex,pos,rect,Color.Lerp(color, Color.Transparent, alpha),angle,origin,size,SpriteEffects.None,0f);
        }

        //Getters and setters
        #region
        //getters
        public float getTransparency()
        {
            return alpha;
        }
        public float getTransparencyChangeRate()
        {
            return alphaChange;
        }
        public float getSize()
        {
            return size;
        }
        public int getTimeToLive()
        {
            return life;
        }
        public float getAngle()
        {
            return angle;
        }
        public float getTurnRate()
        {
            return angleVel;
        }
        public Vector2 getPos()
        {
            return pos;
        }
        public Vector2 getVel()
        {
            return vel;
        }
        public Color getColor()
        {
            return color;
        }
        public Texture2D getTexture()
        {
            return tex;
        }

        //setters
        public void setTransparency(float transparency)
        {
            alpha = transparency;
        }
        public void setTransparencyChangeRate(float tcr)
        {
            alphaChange = tcr;
        }
        public void setSize(float particleSize)
        {
            size = particleSize;
        }
        public void setTimeToLive(int ttl)
        {
            life = ttl;
        }
        public void setAngle(float partAngle)
        {
            angle = partAngle;
        }
        public void setTurnRate(float turnRate)
        {
            angleVel = turnRate;
        }
        public void setPos(Vector2 position)
        {
            pos = position;
        }
        public void setVel(Vector2 velocity)
        {
            vel = velocity;
        }
        public void setColor(Color col)
        {
            color = col;
        }
        public void setTexture(Texture2D texture)
        {
            tex = texture;
        }
        #endregion
    }
}
