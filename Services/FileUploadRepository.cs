using celsiaAssetsment.Interfaces;

namespace celsiaAssetsment.Services
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly IDataProcessingRepository _dataProcessingRepository;

        public FileUploadRepository(IDataProcessingRepository dataProcessingRepository)
        {
            _dataProcessingRepository = dataProcessingRepository;
        }

        public async Task UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                await _dataProcessingRepository.ProcessFileAsync(filePath);
            }
        }
    }
}