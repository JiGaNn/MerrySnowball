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
    }
}
