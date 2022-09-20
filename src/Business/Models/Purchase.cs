using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    [ExcludeFromCodeCoverage]
    public class Purchase
    {
        [Key]
        public Guid PurchaseId { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public double TotalTaxes { get; set; }
        public double TotalPaid { get => Assets.Sum(x => x.TotalPaid) + TotalTaxes; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Asset
    {
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPaid { get => UnitPrice * Quantity; }
    }
}
