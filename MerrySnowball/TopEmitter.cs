using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrySnowball
{
    public class TopEmitter : Emitter
    {
        public int Width;
        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle);
            particle.X = Particle.rnd.Next(Width);
            particle.Y = 0;

            particle.SpeedY = 1;
            particle.SpeedX = Particle.rnd.Next(-2, 2);
        }
    }
}
