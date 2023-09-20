﻿namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class CategoryResponse
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
    }
}
