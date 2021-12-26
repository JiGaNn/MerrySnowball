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
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();
        List<Particle> particles = new List<Particle>();
        public int MousePositionX = 0;
        public int MousePositionY = 0;
        public float GravitationX = 0;
        public float GravitationY = 0;
        public int ParticlesCount = 500;
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 20 + Particle.rnd.Next(100);
            particle.X = MousePositionX;
            particle.Y = MousePositionY;

            var direction = (double)Particle.rnd.Next(360);
            var speed = 1 + Particle.rnd.Next(10);
            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            particle.Radius = 2 + Particle.rnd.Next(10);
        }
        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.Life -= 1;
                if (particle.Life < 0)
                {
                    ResetParticle(particle);
                }
                else
                {
                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }

                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;

                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                }
            }

            for (int i = 0; i < 10; ++i)
            {
                if (particles.Count < ParticlesCount)
                {
                    var particle = new ParticleColorful();

                    particle.FromColor = Color.White;
                    particle.ToColor = Color.FromArgb(0, Color.Black);

                    ResetParticle(particle);

                    particles.Add(particle);
                }
                else
                {
                    break;
                }
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
    }
}
