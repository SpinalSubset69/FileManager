using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DownloadController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IWebHostEnvironment _host;

    public DownloadController(IWebHostEnvironment host, UserService userService)
    {
        _host = host;
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetFile(int id)
    {
        var file = await _userService.GetFileStreamBasedOnId(id, _host.ContentRootPath);
        return File(file.Content, "application/octet-stream", file.FileName);
    }
}
