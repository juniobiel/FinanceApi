using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserAsset
    {
        [Key]
        public Guid UserAssetId { get; set; }
        public Guid AssetId { get; set; }
        public string Ticker { get; set; }
        public int TotalQuantity { get; set; }
        public double MediumPrice { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
