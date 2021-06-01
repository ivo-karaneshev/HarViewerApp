using System.Collections.Generic;

namespace HarViewer.Models
{
    public class Folder : IFileSystemObject
    {
        public Folder()
        {
            Folders = new List<Folder>();
            Files = new List<Document>();
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public string OldPath { get; set; }

        public IList<Folder> Folders { get; set; }

        public IList<Document> Files { get; set; }
    }
}