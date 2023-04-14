using NanoVGDotNet.NanoVG;

namespace MusicVisualizer.Scenes
{
    public abstract class Scene
    {

        public string Name { get; private set; }

        public Scene(string name)
        {
            Name = name;
        }

        public abstract void Init(Environment env);

        public abstract void Render(Environment env);

    }
}
