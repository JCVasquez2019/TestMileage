using System.Threading.Tasks;

namespace Infraestructure
{
    public interface IMileageRepository
    {
        MILEAGE Find(string postalCodeOrigin, string postalCodeDestination);
        bool Add(MILEAGE millage);
        Task<int> Commit();
    }
}