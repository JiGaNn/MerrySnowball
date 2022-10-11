using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MerrySnowball
{
    public class GravityPoint : IImpactPoint
    {
        public int Power = 100;

        public override void ImpactParticle(Particle particle)
        {
            float gx = X - particle.X;
            float gy = Y - particle.Y;

            float r2 = (float)Math.Max(100, gx * gx + gy * gy);

            particle.SpeedX += (gx) * Power / r2;
            particle.SpeedY += (gy) * Power / r2;
        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(
                new Pen(Color.White),
                X - Power / 2,
                Y - Power / 2,
                Power,
                Power
            );
        }

        public void TpIn(ParticleColorful particle,  List<AntiGravityPoint> portals)
        {
            Random random = new Random();
            int index = random.Next(portals.Count);
            AntiGravityPoint tpOut = portals[index];

            var isIn = (Math.Abs(X - particle.X) < 50 && Math.Abs(Y - particle.Y) < 50);
            if (isIn)
            {
                particle.FromColor = tpOut.color;
                particle.X = tpOut.X;
                particle.Y = tpOut.Y;
            }
        }
    }
}
