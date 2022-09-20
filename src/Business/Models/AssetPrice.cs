using Business.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    [ExcludeFromCodeCoverage]
    public class AssetPrice
    {
        [Key]
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public double LastPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastUpdateBySystem { get; set; }

    }
}
