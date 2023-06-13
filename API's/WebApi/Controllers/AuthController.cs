using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApi.API.DataAccesLayer;
using WebApi.API.Entity;
using WebApi.API.Model;

namespace WebApi.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public AuthController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpPost("Login")]
		public string Login(LoginDto loginDto)
		{
			var user = _context.Users.SingleOrDefault(x=>x.Username == loginDto.Username) 
				?? throw new Exception("Böyle bir kullanıcı bulunmamaktadır");

			if(user.Password != loginDto.Password)
				throw new Exception("Kullanıcı adı veya şifre hatalı");

			return $"{user.Email} için giriş başarılı";

		}
		[HttpPost("Register")]
		public string Register(RegisterDto registerDto)
		{
			try
			{
				User user = new()
				{
					Email = registerDto.Email,
					Password = registerDto.Password,
					Username = registerDto.UserName
				};
				_context.Users.Add(user);
				_context.SaveChanges();

				return $"{user.Username} için kayıt başarılı";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}


    }
}
