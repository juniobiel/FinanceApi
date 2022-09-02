using Business.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    [ExcludeFromCodeCoverage]
    public class Asset
    {
        [Key]
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public decimal CurrentPrice { get; set; }
        public AssetType AssetType { get; set; }
    }
}
