namespace celsiaAssetsment.Interfaces
{
    public interface IFileUploadRepository
    {
        Task UploadFile(IFormFile file);
    }
}