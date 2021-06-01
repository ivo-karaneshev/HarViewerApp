namespace HarViewer.Models
{
    public enum OperationType { Add, Move, Delete }

    public class FileSystemOperation
    {
        public OperationType OperationType { get; set; }

        public string Path { get; set; }

        public string OldPath { get; set; }

        public string ObjectType { get; set; }
    }
}
