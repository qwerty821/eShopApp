using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Images.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly string _storagePath;
        private readonly string _baseUrl;

        public ImagesController(IConfiguration config)
        {
            _storagePath = config["IMAGES_PATH"];  
            _baseUrl = config["IMAGES_BASE_URL"];  
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var publicUrl = $"{_baseUrl}/{fileName}";
            return Ok(new { url = publicUrl });
        }
    }
}
