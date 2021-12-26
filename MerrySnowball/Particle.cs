using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MerrySnowball
{
    public class Particle
    {
        public float X;
        public float Y;
        public int Radius;
        public float SpeedX;
        public float SpeedY;
        public float Life;

        public static Random rnd = new Random();

        public Particle()
        {
            var direction = (double)rnd.Next(360);
            var speed = 1 + rnd.Next(10);
            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            Radius = 2 + rnd.Next(10);
            Life = 20 + rnd.Next(100);
        }
        public virtual void Draw(Graphics g)
        {
            float k = Math.Min(1f, Life / 100);

            int alpha = (int)(k * 255);

            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);

            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose();
        }
    }
    class ParticleColorful : Particle
    {
        public Color FromColor;
        public Color ToColor;
        public static Color MixColor(Color color1, Color color2, float k)
        {
            return Color.FromArgb(
                (int)(color2.A*k + color1.A*(1-k)),
                (int)(color2.R * k + color1.R * (1 - k)),
                (int)(color2.G * k + color1.G * (1 - k)),
                (int)(color2.B * k + color1.B * (1 - k))
                );
        }
        public override void Draw(Graphics g)
        {
            float k = Math.Min(1f, Life / 100);
            var color = MixColor(ToColor, FromColor, k);
            var b = new SolidBrush(color);

            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
        }
    }
}
