using BookingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ICsvDataUploadService _csvDataUploadService;

        public UploadController(ICsvDataUploadService csvDataUploadService)
        {
            _csvDataUploadService = csvDataUploadService;
        }

        [HttpPost("members")]
        public async Task<IActionResult> UploadMembers(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var extension = Path.GetExtension(file.FileName);
            if(extension.ToLower() != ".csv")
            {
                return BadRequest("Upload csv only.");
            }

            var result = await _csvDataUploadService.UploadMembersFromCsv(file);
            if(result)
            {
                return Ok("Members Uploaded");
            }
            else
            {
                return BadRequest("Error importing members");
            }
        }

        [HttpPost("inventory")]
        public async Task<IActionResult> Uploadinventory(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var extension = Path.GetExtension(file.FileName);
            if (extension.ToLower() != ".csv")
            {
                return BadRequest("Upload csv only.");
            }

            var result = await _csvDataUploadService.UploadInventoryFromCsv(file);
            if (result)
            {
                return Ok("inventory Uploaded");
            }
            else
            {
                return BadRequest("Error importing inventory");
            }
        }
    }
}
