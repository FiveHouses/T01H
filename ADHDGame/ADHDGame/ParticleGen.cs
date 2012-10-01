using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ADHDGame
{
    public class ParticleGen
    {
        private List<Particle> particles;
        private Texture2D tex;
        private Color pColor;
        private Color toCol;
        private Random rand;
        private Vector2 pos;
        private Vector2 pVel;
        private float pAngle;
        private float pAngleVel;
        private float pSize;
        private float pAlpha;
        private float pAlphaChange;
        private float sizeChangeRate;
        private float sizeMax;
        private float sizeMin;
        private int pLife;
        private int pNum;
        private int pVelMin;
        private int pVelMax;
        private bool randomizeAngle;

        public ParticleGen(Texture2D texture, Vector2 position, Vector2 particleVel, int particleVelMinVar, int particleVelMaxVar, float initParticleAngle,
            bool randomAngle, float particleAngleChange, Color particleColor, Color fadeToColor, float particleSize, float particleSizeChangeRate, 
            float particleMaxSize, float particleMinSize, int particleTimeToLive, float particleInitAlpha, float particleAlphaChange, int numParticles)
        {
            toCol = fadeToColor;
            tex = texture;
            pos = position;
            pNum = numParticles;
            pVel = particleVel;
            pAngle = initParticleAngle;
            pAngleVel = particleAngleChange;
            pColor = particleColor;
            pSize = particleSize;
            pLife = particleTimeToLive;
            pAlpha = particleInitAlpha;
            pAlphaChange = particleAlphaChange;
            pVelMax = particleVelMaxVar;
            pVelMin = particleVelMinVar;
            sizeChangeRate = particleSizeChangeRate;
            sizeMax = particleMaxSize;
            sizeMin = particleMinSize;
            randomizeAngle = randomAngle;

            particles = new List<Particle>();
            rand = new Random();
        }

        public int getNumParticles()
        {
            return getParticleList().Count();
        }

        public void Update()
        {
            for(int i = 0; i < pNum; i++)
            {
                particles.Add(generateParticle());
            }

            for(int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].getTimeToLive() <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }

        }

        public void draw(SpriteBatch sb)
        {
            sb.Begin();
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(sb);
            }
            sb.End();
        }

        private Particle generateParticle()
        {
            /*Vector2 npVel = pVel + pVelChange;
            if (npVel.X <= pVelMax.X && npVel.Y <= pVelMax.Y && npVel.X >= pVelMin.X && npVel.Y >= pVelMin.Y)
            {
                pVel += pVelChange;
            }*/
            
            if (randomizeAngle)
            {
                pAngle += (float)rand.NextDouble();
            }

            Vector2 fVel;
            if (pVelMin < pVelMax && (pVelMin != 0 || pVelMax != 0))
            {
                fVel = new Vector2(pVel.X + (float)(rand.Next(pVelMin, pVelMax)) / 10f, pVel.Y + (float)(rand.Next(pVelMin, pVelMax)) / 10f);
            }
            else
            {
                fVel = pVel;
            }
            return new Particle(tex, pos, fVel, pAngle, pAngleVel, pColor, toCol, pSize, sizeChangeRate, sizeMax, sizeMin, pLife, pAlpha, pAlphaChange);
        }

        public Vector2 getPos()
        {
            return pos;
        }

        public void setPos(Vector2 position)
        {
            pos = position;
        }

        public List<Particle> getParticleList()
        {
            return particles;
        }
    }
}
