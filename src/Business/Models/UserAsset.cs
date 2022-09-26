using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    [ExcludeFromCodeCoverage]
    public class UserAsset
    {
        [Key]
        public Guid UserAssetId { get; set; }
        public string Ticker { get; set; }
        public int TotalQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastSell { get; set; }
        public double? MediumPrice { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
