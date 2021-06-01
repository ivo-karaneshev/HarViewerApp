using HarViewer.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarViewer.Services
{
    public interface IFileManager
    {
        Folder GetTreeView();

        void UpdateTreeView(IEnumerable<FileSystemOperation> operations);

        Task UploadFiles(IEnumerable<IFormFile> files, string destinationPath);

        Task<string> ReadTextFile(string path);
    }
}