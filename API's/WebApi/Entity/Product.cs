﻿namespace WebApi.API.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string? ProductImage { get; set; }
    }
}
