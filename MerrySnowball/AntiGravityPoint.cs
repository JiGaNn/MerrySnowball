using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MerrySnowball
{
    public class AntiGravityPoint : IImpactPoint
    {
        public int Power = 100;
        public Color color = Color.White;

        public override void ImpactParticle(Particle particle)
        {
            var isIn = (Math.Abs(X - particle.X) < 50 && Math.Abs(Y - particle.Y) < 50);
            if (isIn)
            {
                (particle as ParticleColorful).FromColor = color;
            }

        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(
                new Pen(color),
                X - Power / 2,
                Y - Power / 2,
                Power,
                Power
            );
        }
    }
}
