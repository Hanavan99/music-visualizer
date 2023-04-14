using NanoVGDotNet.NanoVG;
using OpenTK;

namespace MusicVisualizer
{
    public sealed class Environment
    {

        public int Width { get; private set; }
        public int Height { get; private set; }
        public NvgContext Context { get; private set; }
        public float LastTime { get; private set; }
        public float Time { get; private set; }
        public float DeltaTime => Time - LastTime;
        public Environment(NvgContext context)
        {
            Context = context;
            LastTime = Time = GetTime();
        }
        public NvgColor CreateColor(float r, float g, float b, float a = 1)
        {
            return new NvgColor { R = r, G = g, B = b, A = a };
        }
        public void CaptureWindowSize(NativeWindow window)
        {
            if (window.Width > 0 && window.Height > 0)
            {
                Width = window.Width;
                Height = window.Height;
            }
        }
        public void UpdateTimes()
        {
            LastTime = Time;
            Time = GetTime();
        }

        private float GetTime()
        {
            return System.Environment.TickCount / 1000f;
        }
    }
}
