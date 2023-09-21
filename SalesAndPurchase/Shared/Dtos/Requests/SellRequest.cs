namespace SalesAndPurchase.Shared.Dtos.Requests
{
    public class SellRequest
    {
        public required string SupplierId { get; set; }
        public string SkuCode { get; set; }
        public DateTime? SalesDate { get; set; }
        public decimal SubTotal { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPriceRemaining { get; set; }
        public string? Status { get; set; }
        public List<SellDetailRequest>? SellDetails { get; set; }
        public SellRequest()
        {
            SkuCode = GenerateTransactionCode();
            SalesDate = DateTime.Now;
            SellDetails = new List<SellDetailRequest>();
        }
        string GenerateTransactionCode()
        {
            // Mendapatkan kode awal (misalnya, "TRX")
            string kodeAwal = "PNJ";

            // Mendapatkan tanggal saat ini dalam format "yyyy/MM/dd"
            string tanggal = DateTime.Now.ToString("yyyy/MM/dd");

            // Mendapatkan GUID 6 digit
            string guid6Digit = Guid.NewGuid().ToString("N").Substring(0, 6);

            // Menggabungkan semua komponen untuk membuat kode transaksi
            string kodeTransaksi = $"{kodeAwal}/{tanggal}/{guid6Digit}";

            return kodeTransaksi;
        }
    }
}
