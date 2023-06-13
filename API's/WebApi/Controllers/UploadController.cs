using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using WebApi.API.Model;

namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly HttpContext _currentContext;
        public UploadController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _currentContext = httpContextAccessor.HttpContext;
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            ResponseResult rr = new ResponseResult();
            if (file == null || file.Length == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var filename = $@"{DateTime.Now.Ticks}_" + file.FileName;

            var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            rr.StatusCode = StatusCodes.Status200OK;
            rr.Message = GetImageUrl(_currentContext, filename);
            return Ok(rr);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ResponseResult rr = new ResponseResult();
            rr.StatusCode = StatusCodes.Status200OK;
            rr.Message = "https://localhost:7215/images/636494637368678780_controller.jpg";
            return Ok(rr);
        }
        public static string GetImageUrl(HttpContext context, string imageName)
        {
            return Path.Combine(GetBaseUrl(context), "images", imageName);
        }
        public static string GetBaseUrl(HttpContext context)
        {
            var request = context.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
