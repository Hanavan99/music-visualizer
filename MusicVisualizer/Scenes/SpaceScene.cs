using NanoVGDotNet.NanoVG;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicVisualizer.Scenes
{
    public class SpaceScene : Scene
    {

        private readonly List<Star> stars = new List<Star>();

        public SpaceScene() : base("Space")
        {

        }

        public override void Init(Environment env)
        {
            Random r = new Random();

            // generate stars;
            for (int i = 0; i < 400; i++)
            {
                stars.Add(new Star
                {
                    X = r.Next(env.Width),
                    Y = r.Next(env.Height),
                    VelX = (float)r.NextDouble() * 20f + 2f,
                    VelY = (float)r.NextDouble() * 10f - 5f,
                    Opacity = 0.7f,
                    Size = (float)r.NextDouble() * 1.5f + 0.5f,
                    Color = env.CreateColor(1 - (float)r.NextDouble() / 10f, 1 - (float)r.NextDouble() / 10f, 1 - (float)r.NextDouble() / 10f, 0.7f)
                });
            }

            // generate cloud shapes
            for (int i = 0; i < 500; i++)
            {
                stars.Add(new Star
                {
                    X = r.Next(env.Width),
                    Y = r.Next(env.Height),
                    VelX = (float)r.NextDouble() * 5f + 2f,
                    VelY = (float)r.NextDouble() * 2f - 1f,
                    Opacity = 1f / 255f,
                    Size = (float)r.NextDouble() * 25 + 75f,
                    Color = env.CreateColor(1, 1, 1, 1f / 255f)
                });
            }
        }

        public override void Render(Environment env)
        {

            foreach (Star p in stars)
            {
                env.Context.BeginPath();
                env.Context.Circle(p.X, p.Y, p.Size);
                env.Context.FillColor(p.Color);
                env.Context.Fill();
                p.Tick(env.DeltaTime);

                if (p.X >= env.Width + p.Size)
                {
                    p.X = -p.Size;
                }

                if (p.Y < -p.Size)
                {
                    p.Y = env.Height + p.Size;
                }
                else if (p.Y > env.Height + p.Size)
                {
                    p.Y = -p.Size;
                }
            }
        }

        private class Star : Point
        {
            public float Size { get; set; }
            public float Opacity { get; set; }
            public NvgColor Color { get; set; }
        }
    }
}
