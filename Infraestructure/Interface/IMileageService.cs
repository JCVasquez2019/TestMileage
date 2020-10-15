using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public interface IMileageService
    {
        Task<decimal> GetDistance(string postalCodeOrigin, string postalCodeDestination);
    }
}
