using HarViewer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HarViewer.Services
{
    public class FileManager : IFileManager
    {
        private readonly IConfiguration _configuration;
        private DirectoryInfo _rootDirectory;

        public FileManager(IConfiguration configuration)
        {
            _configuration = configuration;
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["UploadsFolderName"]);

            if (!Directory.Exists(uploadsPath))
            {
                _rootDirectory = Directory.CreateDirectory(uploadsPath);
            }
            else
            {
                _rootDirectory = new DirectoryInfo(uploadsPath);
            }
        }

        public Folder GetTreeView()
        {
            var result = CreateFolder(_rootDirectory);

            var test = Newtonsoft.Json.JsonConvert.SerializeObject(result);

            return result;
        }

        public void UpdateTreeView(IEnumerable<FileSystemOperation> operations)
        {
            foreach (var operation in operations)
            {
                if (operation.ObjectType.Equals(typeof(Folder).Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    HandleFolder(operation);
                }

                if (operation.ObjectType.Equals(typeof(File).Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    HandleFile(operation);
                }
            }
        }

        public async Task UploadFiles(IEnumerable<IFormFile> files, string destinationPath)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = Path.Combine(_rootDirectory.FullName, destinationPath, file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }

        public async Task<string> ReadTextFile(string path)
        {
            var fullPath = Path.Combine(_rootDirectory.FullName, path);
            var result = string.Empty;

            if (File.Exists(fullPath))
            {
                using (var file = File.OpenText(fullPath))
                {
                    result = await file.ReadToEndAsync();
                }
            }

            return result;
        }

        private void HandleFolder(FileSystemOperation operation)
        {
            var path = Path.Combine(_rootDirectory.FullName, operation.Path);
            if (operation.OperationType == OperationType.Add)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            if (operation.OperationType == OperationType.Delete)
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }

            if (operation.OperationType == OperationType.Move)
            {
                var oldPath = Path.Combine(_rootDirectory.FullName, operation.OldPath);
                if (Directory.Exists(oldPath) && !Directory.Exists(path))
                {
                    Directory.Move(oldPath, path);
                }
            }
        }

        private void HandleFile(FileSystemOperation operation)
        {
            var path = Path.Combine(_rootDirectory.FullName, operation.Path);
            if (operation.OperationType == OperationType.Delete)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            if (operation.OperationType == OperationType.Move)
            {
                var oldPath = Path.Combine(_rootDirectory.FullName, operation.OldPath);
                if (File.Exists(oldPath) && !File.Exists(path))
                {
                    File.Move(oldPath, path);
                }
            }
        }

        private Folder CreateFolder(DirectoryInfo directoryInfo)
        {
            var folder = new Folder()
            {
                Name = directoryInfo.Name,
                Path = Path.GetRelativePath(_rootDirectory.Name, directoryInfo.FullName)
            };

            foreach (var directory in directoryInfo.GetDirectories())
            {
                folder.Folders.Add(CreateFolder(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                folder.Files.Add(new Document()
                {
                    Name = file.Name,
                    Path = Path.GetRelativePath(_rootDirectory.Name, file.FullName)
                });
            }

            return folder;
        }
    }
}