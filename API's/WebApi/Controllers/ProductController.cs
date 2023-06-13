using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.API.DataAccesLayer;
using WebApi.API.Entity;
using WebApi.API.Model;

namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsContext _context;
        private readonly HttpContext _currentContext;
        public ProductController(ProductsContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _currentContext = httpContextAccessor.HttpContext;

            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product { Title = "Royal Canin", Price = "391.40", ProductImage = UploadController.GetImageUrl(_currentContext, "royalcanin.jpg") });
                _context.Products.Add(new Product { Title = "N&D", Price = "1646.50", ProductImage = UploadController.GetImageUrl(_currentContext, "wii.jpg") });
                _context.Products.Add(new Product { Title = "Hills", Price = "295.26", ProductImage = UploadController.GetImageUrl(_currentContext, "controller.jpg") });
                _context.SaveChanges();
            }
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            //try
            //{
            //    Product product = new()
            //    {
            //        Description = productDto.Description,
            //        Price = productDto.Price,
            //        Title = productDto.Title,
            //    };
            //    _context.Products.Add(product);
            //    _context.SaveChanges();

            //    return Ok();
            //}
            //catch (Exception ex)
            //{

            //    return BadRequest(ex.Message);
            //}
            if (product == null || product.Id != 0 || String.IsNullOrEmpty(product.Title))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
             _context.Products.AddAsync(product);
            _context.SaveChanges();

            return Ok(product);

        }
        
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct([FromBody] ProductDto productDto)
        {
            try
            {
                Product product = new()
                {
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Title = productDto.Title,
                };
                _context.Products.Update(product); 
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
               
                return BadRequest(ex.Message);
            }

        }
    }
}
