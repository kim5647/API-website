using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Xabe.FFmpeg;


[ApiController]
[Route("api/")]
public class VideoController : ControllerBase
{
    private string? fileVideo;
    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo(IFormFile video, [FromForm] string startTime, [FromForm] string endTime)
    {
        try
        {
            if (video != null && video.Length > 0)
            {
                fileVideo = Path.GetTempFileName();

                using (var stream = new FileStream(fileVideo, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }

                var trimmedFileVideo = Path.GetTempFileName();

                await FFmpeg.Conversions.New()
                    .AddParameter($"-i \"{fileVideo}\"")
                    .AddParameter($"-ss \"{startTime}\"")
                    .AddParameter($"-to \"{endTime}\"")
                    .AddParameter($"-c copy \"{trimmedFileVideo}\"")
                    .Start();

                System.IO.File.Delete(fileVideo);

                var fileResult = await System.IO.File.ReadAllBytesAsync(trimmedFileVideo);
                var fileStream = new MemoryStream(fileResult);
                var contentType = "video/mp4";
                var fileName = $"trimmed_{Path.GetFileName(video.FileName)}";

                System.IO.File.Delete(trimmedFileVideo);

                return File(fileStream, contentType, fileName);
            }

            return BadRequest("No video file uploaded.");
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
    [HttpPost("video")]
    public async Task<IActionResult> Vodeo(IFormFile video)
    {
        try
        {
            if (video != null && video.Length > 0)
            {
                fileVideo = Path.Combine("G:/video", video.FileName);

                using (var stream = new FileStream(fileVideo, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }

                //var fileResult = await System.IO.File.ReadAllBytesAsync(fileVideo);
                //var fileStream = new MemoryStream(fileResult);
                //var contentType = "video/mp4";
                //var fileName = $"{Path.GetFileName(video.FileName)}";

                //System.IO.File.Delete(fileVideo);

                //return File(fileStream, contentType, fileName);

                return Ok("OK");
            }

            return BadRequest("No video file uploaded.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
    [HttpGet("video/{filename}")]
    public IActionResult GetVideo([FromForm] string filename)
    {
        var filePath = Path.Combine("G:/video", filename);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return File(fileStream, "video/mp4");
    }
    private readonly DBSave _dbContext;

    // Получаем контекст через DI
    public VideoController(DBSave dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromForm] string username, [FromForm] int password)
    {
        try
        {
            // Создаем нового пользователя
            var newUser = new User(username, password);

            // Добавляем его в базу данных через контекст DBSave
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}