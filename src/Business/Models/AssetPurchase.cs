using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class AssetPurchase
    {
        [Key]
        public Guid PurchaseId { get; set; }
        public Guid UserAssetId { get; set; }
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double BrokerTax { get; set; }
        public double IncomeTax { get; set; }
        public double OtherTaxes { get; set; }
        public double TotalPaid { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
