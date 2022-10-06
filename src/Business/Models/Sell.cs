using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    [ExcludeFromCodeCoverage]
    public class Sell
    {
        [Key]
        public Guid Id { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public double TotalTaxes { get; set; }
        public double TotalReceived => Assets.Sum(x => x.TotalPaid) - TotalTaxes;
        public DateTime SellDate { get; set; }
        public double MediumPrice { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
