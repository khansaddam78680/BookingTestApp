namespace BookingApp.Services
{
    public interface ICsvDataUploadService
    {
        Task<bool> UploadMembersFromCsv(IFormFile file);
        Task<bool> UploadInventoryFromCsv(IFormFile file);
    }
}
