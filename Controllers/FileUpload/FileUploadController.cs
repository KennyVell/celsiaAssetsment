
using Microsoft.AspNetCore.Mvc;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.DTOs;


namespace celsiaAssetsment.Controllers.FileUpload
{
    public class FileUploadController : Controller
    {
        private readonly IFileUploadRepository _fileUploadRepository;

        public FileUploadController(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        public IActionResult Index()
        {
            return View(new FileUploadDTO());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(FileUploadDTO model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                await _fileUploadRepository.UploadFile(model.File);
            }
            return RedirectToAction("Index");
        }
    }
}