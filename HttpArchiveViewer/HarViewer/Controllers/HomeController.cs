using HarFileParser.Services;
using HarViewer.Models;
using HarViewer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HarViewer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileManager _fileManager;
        private readonly IHarParser _harParser;

        public HomeController(ILogger<HomeController> logger, IFileManager fileManager, IHarParser harParser)
        {
            _logger = logger;
            _fileManager = fileManager;
            _harParser = harParser;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var treeView = _fileManager.GetTreeView();
            return View(treeView);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            var treeView = _fileManager.GetTreeView();
            return View(treeView);
        }

        [HttpGet]
        public async Task<IActionResult> OpenFile(string path)
        {
            try
            {
                var fileContent = await _fileManager.ReadTextFile(path);
                var harFile = _harParser.Parse(fileContent);

                return PartialView("_HarFile", harFile);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Error();
            }
        }

        [HttpPost]
        public JsonResult UpdateTreeView(List<FileSystemOperation> operations)
        {
            try
            {
                // Update files and folders on the server
                _fileManager.UpdateTreeView(operations);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new JsonResult(false);
            }

            return new JsonResult(true);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string operations, string destinationPath)
        {
            try
            {
                var folderOperations = JsonConvert.DeserializeObject<List<FileSystemOperation>>(operations);
                // First create new folders if any
                _fileManager.UpdateTreeView(folderOperations);
                // Then upload the new files
                await _fileManager.UploadFiles(files, destinationPath);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Error();
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
