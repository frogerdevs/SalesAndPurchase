﻿namespace SalesAndPurchase.Shared.Dtos.Requests
{
    public class TransactionProductItem
    {
        public string TempId { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get { return (Quantity*Price) - ((Quantity*Price) * (Discount/100)); } }
    }

}
