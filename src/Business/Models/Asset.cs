using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    [ExcludeFromCodeCoverage]
    public class Asset
    {
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPaid { get => UnitPrice * Quantity; }
    }
}
