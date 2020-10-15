using System.Linq;
using System.Threading.Tasks;
using Infraestructure;

namespace Data
{
    public class MileageRepository : IMileageRepository
    {
        private readonly MileageDBContext m_dbContext;

        public MileageRepository(MileageDBContext dbContext)
        {
            m_dbContext = dbContext;
        }

        public bool Add(MILEAGE mileage)
        {
            try
            {
                m_dbContext.Mileage.Add(mileage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MILEAGE Find(string postalCodeOrigin, string postalCodeDestination)
        {
            MILEAGE millage = m_dbContext.Mileage.Where(x => x.PostalCodeOrigin == postalCodeOrigin && x.PostalCodeDestination == postalCodeDestination).FirstOrDefault();
            if (millage != null)
                return millage;

            millage = m_dbContext.Mileage.Where(x => x.PostalCodeOrigin == postalCodeDestination && x.PostalCodeDestination == postalCodeOrigin).FirstOrDefault();
            return millage;
        }
        public async Task<int> Commit()
        {
            return await m_dbContext.SaveChangesAsync();
        }
    }
}
