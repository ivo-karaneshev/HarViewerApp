namespace HarViewer.Models
{
    public class Document : IFileSystemObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

        public string OldPath { get; set; }
    }
}