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
        private int MousePositionX = 0;
        private int MousePositionY = 0;

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

        private void picDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MousePositionX = e.X;
                MousePositionY = e.Y;
                emitter.impactPoints.Add(new AntiGravityPoint { X = MousePositionX, Y = MousePositionY, color = Color.BurlyWood });
                emitter.portals.Add(new AntiGravityPoint { X = MousePositionX, Y = MousePositionY, color = Color.BurlyWood });
            }
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    emitter.impactPoints.RemoveAt(5);
                    emitter.portals.RemoveAt(3);
                } catch(Exception ex)
                {

                }
            }
            if (e.Button == MouseButtons.Middle)
            {
                emitter.GravitationY = -(emitter.GravitationY);
            }
        }
    }
}
