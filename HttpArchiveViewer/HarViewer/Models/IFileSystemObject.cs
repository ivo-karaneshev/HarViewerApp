namespace HarViewer.Models
{
    interface IFileSystemObject
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string OldPath { get; set; }
    }
}