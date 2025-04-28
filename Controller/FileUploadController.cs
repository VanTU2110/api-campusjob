using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/upload")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly CloudinaryService _cloudinaryService;

    public FileUploadController(CloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost("file")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            var fileUrl = await _cloudinaryService.UploadFileAsync(file);
            return Ok(new { FileUrl = fileUrl });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteFile([FromQuery] string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
        {
            return BadRequest("Public ID is required.");
        }

        var result = await _cloudinaryService.DeleteFileAsync(publicId);
        return Ok(new { message = result });
    }
}
