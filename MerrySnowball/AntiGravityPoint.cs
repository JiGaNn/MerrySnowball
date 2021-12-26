using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrySnowball
{
    public class AntiGravityPoint : IImpactPoint
    {
        public int Power = 100;
        public override void ImpactParticle(Particle particle)
        {
            float gx = X - particle.X;
            float gy = Y - particle.Y;

            float r2 = (float)Math.Max(100, gx * gx + gy * gy);

            particle.SpeedX -= (gx) * Power / r2;
            particle.SpeedY -= (gy) * Power / r2;
        }
    }
}
