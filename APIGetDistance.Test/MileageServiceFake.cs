using Infraestructure;
using System.Threading.Tasks;

namespace APIGetdistance.Test
{
    internal class MileageServiceFake : IMileageService
    {
        public async Task<decimal> GetDistance(string postalCodeOrigin, string postalCodeDestination)
        {
            decimal distance = await Task.FromResult(130); ;
            return distance;
        }
    }
}