using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerrySnowball
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;
        public Form1()
        {
            InitializeComponent();

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new TopEmitter { GravitationY = 0.25f, Width = picDisplay.Width };

            emitters.Add(emitter);

            emitter.impactPoints.Add(new GravityPoint { X = (int)(picDisplay.Width / 3), Y = picDisplay.Height / 3 });
            emitter.impactPoints.Add(new GravityPoint { X = (int)(picDisplay.Width * 2 / 3), Y = picDisplay.Height / 3 });
            
            emitter.impactPoints.Add(new AntiGravityPoint { X = (int)(picDisplay.Width / 6), Y = picDisplay.Height * 3 / 5, color = Color.Red });
            emitter.impactPoints.Add(new AntiGravityPoint { X = (int)(picDisplay.Width / 2), Y = picDisplay.Height * 3 / 5, color = Color.YellowGreen });
            emitter.impactPoints.Add(new AntiGravityPoint { X = (int)(picDisplay.Width * 5 / 6), Y = picDisplay.Height * 3 / 5, color = Color.Purple });

            emitter.CountPortals();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
            }
            picDisplay.Invalidate();
        }
    }
}
