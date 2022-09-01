namespace EasyUIFrame.Frame
{
    public class UIType
    {
        private string path;
        
        public string Path
        {
            get => path;
        }

        private string name;
        
        public string Name
        {
            get => name;
        }

        public UIType(string path, string name)
        {
            this.path = path;
            this.name = name;
        }
    }
}