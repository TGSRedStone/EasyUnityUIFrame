using System;

namespace EasyUIFrame.Frame.UI
{
    public sealed class UIType
    {
        public string Path { get; }
        public string Name { get; }

        public UIType(string path, string name)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("UI path cannot be null or empty.", nameof(path));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("UI name cannot be null or empty.", nameof(name));
            }

            Path = path;
            Name = name;
        }
    }
}
