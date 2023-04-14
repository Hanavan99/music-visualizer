using MusicVisualizer.Scenes;
using NanoVGDotNet.NanoVG;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
//using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace MusicVisualizer
{
    internal class Program
    {
        //private static List<Point> points = new List<Point>();
        private static Graph g = new Graph();
        private static List<Point> _temp = new List<Point>();
        private static Scene scene = new SquareScene();
        private static Environment env;

        public const int BarWidth = 10;

        [STAThread]
        public static void Main(string[] args)
        {
            // using (Toolkit.Init())
            // {
            GameWindow window = new MyWindow();
            window.Run();
            //}
        }

        private static void Window_Load(object sender, EventArgs e)
        {

        }

        private static void Window_RenderFrame(object sender, FrameEventArgs e)
        {

        }

        private static bool LinesIntersect(Point a1, Point a2, Point b1, Point b2)
        {
            float A1 = a2.Y - a1.Y;
            float B1 = a1.X - a2.X;
            float C1 = A1 * a1.X + B1 * a1.Y;
            float A2 = b2.Y - b1.Y;
            float B2 = b1.X - b2.X;
            float C2 = B2 * b1.X + B2 * b1.Y;

            float det = A1 * B2 - A2 * B1;
            if (det == 0)
            {
                // Lines are parallel
                return false;
            }
            else
            {
                float x = (B2 * C1 - B1 * C2) / det;
                float y = (A1 * C2 - A2 * C1) / det;
                return x >= Math.Min(a1.X, a2.X) && x <= Math.Max(a1.X, a2.X) && y >= Math.Min(a1.Y, a2.Y) && y <= Math.Max(a1.Y, a2.Y)
                    && x >= Math.Min(b1.X, b2.X) && x <= Math.Max(b1.X, b2.X) && y >= Math.Min(b1.Y, b2.Y) && y <= Math.Max(b1.Y, b2.Y);
            }
        }

        private class MyWindow : GameWindow
        {
            public Scene Scene { get; set; }

            public MyWindow() : base(800, 600, new GraphicsMode(GraphicsMode.Default.ColorFormat, GraphicsMode.Default.Depth, 8), "Music Visualizer")
            {
                // need a custom graphics mode to add a stencil buffer
            }

            protected override void OnLoad(EventArgs e)
            {

                NvgContext ctx = GlNanoVg.CreateGl(NvgCreateFlags.AntiAlias | NvgCreateFlags.StencilStrokes);

                //ctx.CreateFont("default", Path.Combine(System.Environment.CurrentDirectory, "GothamMedium.ttf"));
                //ctx.CreateFont("default-bold", Path.Combine(System.Environment.CurrentDirectory, "GothamBold.ttf"));
                ctx.CreateFont("default", "C:\\Windows\\Fonts\\arial.ttf");
                ctx.CreateFont("default-bold", "C:\\Windows\\Fonts\\arialbd.ttf");

                //Random r = new Random();
                //for (int i = 0; i < 10; i++)
                //{
                //    g.AddNode(new Point { X = r.Next(1920), Y = r.Next(1080), VelX = (float)r.NextDouble() - 0.5f, VelY = (float)r.NextDouble() - 0.5f });
                //}

                env = new Environment(ctx);
                env.CaptureWindowSize(this);
                scene?.Init(env);

                GL.ClearColor(0, 0, 0, 1);
                base.OnLoad(e);
            }

            protected override void OnResize(EventArgs e)
            {
                env.CaptureWindowSize(this);
                GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
                base.OnResize(e);
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);



                //GL.Begin(PrimitiveType.Quads);
                //GL.Color3(1f, 1f, 1f);
                //GL.Vertex2(-1f, -1f);
                //GL.Color3(0f, 1f, 1f);
                //GL.Vertex2(-1f, 1f);
                //GL.Color3(1f, 0f, 1f);
                //GL.Vertex2(1f, 1f);
                //GL.Color3(1f, 1f, 0f);
                //GL.Vertex2(1f, -1f);
                //GL.End();


                env.Context.BeginFrame(env.Width, env.Height, ClientSize.Width / env.Width);

                //ctx.BeginPath();
                //ctx.RoundedRect(10, 10, 100, 100, 20);
                //ctx.FillColor(new NvgColor { R = 1, A = 1 });
                //ctx.Fill();

                env.Context.Save();
                scene?.Render(env);
                env.Context.Restore();

                string artist = "SAVOY";
                string title = "CONTEMPLATE";
                float[] bounds = new float[4];
                float line = env.Height * 0.6f;
                int barWidth = (env.Width / 50);
                int padding = (int)(env.Width * 0.05f);
                //env.Context.BeginPath();
                //env.Context.MoveTo(50, line);
                //env.Context.LineTo(500, line);
                //env.Context.StrokeColor(env.CreateColor(1, 1, 1));
                //env.Context.Stroke();

                /*
                env.Context.FillColor(env.CreateColor(1, 1, 1));
                for (int i = padding; i < env.Width - padding; i += barWidth + 2)
                {
                    env.Context.BeginPath();
                    env.Context.Rect(i, line, barWidth, -100);
                    env.Context.Fill();
                }

                int imageSize = (int)(env.Height * 0.25f);
                env.Context.BeginPath();
                env.Context.Rect(padding, line + 5, imageSize, imageSize);
                env.Context.Fill();

                float artistTextSize = env.Height * 0.15f;
                env.Context.FontSize(artistTextSize);
                env.Context.FontFace("default-bold");
                env.Context.BeginPath();
                env.Context.FillColor(env.CreateColor(1, 1, 1));
                env.Context.TextAlign(NvgAlign.Top | NvgAlign.Left);
                env.Context.Text(padding + imageSize + 10, line, artist);
                env.Context.TextBounds(padding + imageSize + 10, line, artist, bounds);
                env.Context.FontSize(artistTextSize * 0.5f);
                env.Context.FontFace("default");
                env.Context.Text(padding + imageSize + 10, bounds[3], title);
                */
                //env.Context.Fill();
                /*
                ctx.FontSize(24f);
                ctx.FillColor(new NvgColor { A = 1, G = 1 });
                ctx.Text(20, 20, "Test");

                //ctx.MoveTo(0, 0);
                //ctx.LineTo(10, 10);
                //ctx.Stroke();

                // compute triangles for points
                g.ClearEdges();
                foreach (Point p in g.Nodes)
                {
                    foreach (Point p2 in g.Nodes.Where(o => o != p))
                    {
                        if (!g.ContainsEdge(p, p2))
                        {
                            bool good = true;
                            foreach (Tuple<Point, Point> edge in g.Edges)
                            {
                                if (p != edge.Item1 && p2 != edge.Item2 && p != edge.Item2 && p2 != edge.Item1 && LinesIntersect(p, p2, edge.Item1, edge.Item2))
                                {
                                    good = false;
                                    break;
                                }
                            }

                            if (good)
                            {
                                g.AddEdge(p, p2);
                            }
                        }
                    }
                }

                ctx.StrokeColor(new NvgColor { A = 0.5f, R = 1, G = 0.7f, B = 0.2f });
                foreach (Tuple<Point, Point> edge in g.Edges)
                {
                    ctx.BeginPath();
                    ctx.MoveTo(edge.Item1.X, edge.Item1.Y);
                    ctx.LineTo(edge.Item2.X, edge.Item2.Y);
                    ctx.Stroke();
                }


                foreach (Point p in g.Nodes)
                {
                    ctx.BeginPath();
                    ctx.Circle(p.X, p.Y, 3);
                    ctx.FillColor(new NvgColor { A = 0.5f, R = 1, G = 1, B = 1 });
                    ctx.StrokeColor(new NvgColor { A = 1, R = 1, G = 1, B = 1 });
                    //ctx.Fill();
                    ctx.Stroke();


                    //float angle = 0;
                    //_temp.Clear();
                    //_temp.Add(p);
                    //while (_temp.Count < 10)
                    //{
                    //    if (g.Nodes.OrderBy(o => o.DistTo(p)).FirstOrDefault(o => !_temp.Contains(o) && p.AngleTo(o) >= angle) is Point p2)
                    //    {
                    //        angle = p.AngleTo(p2);
                    //        _temp.Add(p2);
                    //        ctx.LineTo(p2.X, p2.Y);
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}
                    ////foreach (Point p2 in points.Where(o => o != p).OrderBy(o => o.Dist(p)).Take(2))
                    ////{
                    ////    ctx.LineTo(p2.X, p2.Y);
                    ////}
                    //ctx.ClosePath();
                    //ctx.Stroke();

                    p.X += p.VelX;
                    p.Y += p.VelY;
                    if (p.X < 0 || p.X > window.ClientSize.Width)
                    {
                        p.X = Math.Min(window.ClientSize.Width, Math.Max(0, p.X));
                        p.VelX *= -1;
                    }
                    if (p.Y < 0 || p.Y > window.ClientSize.Height)
                    {
                        p.Y = Math.Min(window.ClientSize.Height, Math.Max(0, p.Y));
                        p.VelY *= -1;
                    }
                }
                */
                env.Context.EndFrame();
                SwapBuffers();
                env.UpdateTimes();
                base.OnRenderFrame(e);
            }
        }

    }

    public sealed class Graph
    {
        private readonly Dictionary<Point, List<Point>> adjList = new Dictionary<Point, List<Point>>();

        public IEnumerable<Point> Nodes => adjList.Keys;

        public IEnumerable<Tuple<Point, Point>> Edges => adjList.Aggregate(new Tuple<Point, Point>[0].AsEnumerable(), (o, p) => o.Union(p.Value.Select(q => Tuple.Create(p.Key, q))));

        public void AddNode(Point p)
        {
            adjList.Add(p, new List<Point>());
        }

        public void AddEdge(Point p, Point p2)
        {
            if (!adjList.ContainsKey(p)) AddNode(p);
            if (!adjList.ContainsKey(p2)) AddNode(p2);
            if (!adjList[p].Contains(p2)) adjList[p].Add(p2);
            if (!adjList[p2].Contains(p)) adjList[p2].Add(p);
        }

        public void RemoveEdge(Point p, Point p2)
        {
            if (adjList.ContainsKey(p)) adjList[p].Remove(p2);
            if (adjList.ContainsKey(p2)) adjList[p2].Remove(p);
        }

        public bool ContainsEdge(Point p, Point p2)
        {
            return adjList.ContainsKey(p) && adjList[p].Contains(p2);
        }

        public void ClearEdges()
        {
            foreach (List<Point> pointList in adjList.Values)
            {
                pointList.Clear();
            }
        }
    }

    public class Point
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float VelX { get; set; }

        public float VelY { get; set; }

        public Point()
        {

        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float DistTo(Point p)
        {
            return (float)Math.Sqrt((p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y));
        }

        public float AngleTo(Point p)
        {
            return (float)(Math.Atan2(p.Y - Y, p.X - X) + Math.PI) * 180f;
        }

        public void Tick(float dt)
        {
            X += VelX * dt;
            Y += VelY * dt;
        }
    }

}
