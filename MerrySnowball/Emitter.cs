using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MerrySnowball
{
    public class Emitter
    {
        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 10; // начальная максимальная скорость движения частицы
        public int RadiusMin = 2; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 120; // максимальное время жизни частицы

        public int ParticlesPerTick = 5;

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();
        List<Particle> particles = new List<Particle>();
        public float GravitationX = 0;
        public float GravitationY = 1;
        public int ParticlesCount = 500;
        List<AntiGravityPoint> portals = new List<AntiGravityPoint>();

        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = Particle.rnd.Next(LifeMin, LifeMax);
            (particle as ParticleColorful).FromColor = ColorFrom;
            (particle as ParticleColorful).ToColor = ColorTo;
            particle.X = X;
            particle.Y = Y;

            particle.Radius = Particle.rnd.Next(RadiusMin, RadiusMax);
        }
        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.FromColor = ColorFrom;
            particle.ToColor = ColorTo;
            return particle;
        }
        public void UpdateState()
        {
            int particlesToCreate = ParticlesPerTick;
            foreach (var particle in particles)
            {
                if (particle.Life <= 0)
                {
                    if (particlesToCreate > 0)
                    {
                        particlesToCreate -= 1;
                        ResetParticle(particle);
                    }
                }
                else
                {
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;

                    particle.Life--;
                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                        if(point is GravityPoint)
                        {
                            (point as GravityPoint).TpIn(particle as ParticleColorful, portals);
                        }
                    }

                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;
                }
            }

            while (particlesToCreate > 0 && ParticlesCount > 0)
            {
                ParticlesCount--;
                particlesToCreate -= 1;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }
        }
        public void Render(Graphics g)
        {
            foreach (var part in particles)
            {
                part.Draw(g);
            }
            foreach(var point in impactPoints)
            {
                point.Render(g);
            }
        }

        public void CountPortals()
        {
            foreach (var point in impactPoints)
            {
                if(point is AntiGravityPoint)
                {
                    portals.Add(point as AntiGravityPoint);
                }
            }
        }
    }
}
