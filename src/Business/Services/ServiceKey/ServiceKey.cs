using System.Diagnostics.CodeAnalysis;

namespace Business.Services.ServiceKey
{
    [ExcludeFromCodeCoverage]
    public class ServiceKey : IServiceKey
    {
        public string AlphaVantageKey { get; set; }
    }
}
