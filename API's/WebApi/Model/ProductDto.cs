namespace WebApi.API.Model
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string? ProductImage { get; set; }
    }
}
