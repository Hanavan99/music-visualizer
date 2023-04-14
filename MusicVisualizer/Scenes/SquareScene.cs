using NanoVGDotNet.NanoVG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicVisualizer.Scenes
{
    public class SquareScene : Scene
    {
        public const int SquareSize = 30;

        public SquareScene() : base("Squares")
        {
        }

        public override void Init(Environment env)
        {

        }

        public override void Render(Environment env)
        {
            int size = (int)(Math.Max(env.Width, env.Height) * Math.Sqrt(2));
            float scale = (float)Math.Sin(env.Time / 20f) / 4f + 1.2f;

            env.Context.Translate(env.Width / 2, env.Height / 2);
            env.Context.Rotate(env.Time / 50f);
            env.Context.Translate(-size / 2, -size / 2);
            env.Context.Scale(scale, scale);
            env.Context.Translate((float)Math.Cos(env.Time / 5f) * 50f, (float)Math.Sin(env.Time / 5f) * 50f);

            for (int x = 0; x < size; x += SquareSize)
            {
                for (int y = 0; y < size; y += SquareSize)
                {
                    float padding = 0;//(float)Math.Sin(x + env.Time);
                    float opacity = Math.Max(Math.Min(1f / (float)Math.Pow((x + y) / (float)(SquareSize) - (env.Time * 5 % (size / SquareSize)), 2), 1), 0.8f);
                    env.Context.BeginPath();
                    env.Context.FillColor(env.CreateColor((float)x / size, (float)y / size, 1));
                    env.Context.RoundedRect(x + padding, y + padding, SquareSize - padding * 2, SquareSize - padding * 2, 3);
                    env.Context.Fill();


                    //env.Context.BeginPath();
                    //env.Context.StrokeColor(env.CreateColor(1, 1, 1, opacity));
                    //env.Context.MoveTo(x, y + SquareSize);
                    //env.Context.LineTo(x, y);
                    //env.Context.LineTo(x + SquareSize, y);
                    //env.Context.Stroke();
                }
            }
        }
    }
}
