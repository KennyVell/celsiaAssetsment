using celsiaAssetsment.Models;

namespace celsiaAssetsment.Interfaces
{
    public interface IDataProcessingRepository
    {
        public Task ProcessFileAsync(string filePath);
    }
}