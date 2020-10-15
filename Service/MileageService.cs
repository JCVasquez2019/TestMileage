using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using Infraestructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Service
{
    public class MileageService: IMileageService
    {
        private readonly IMileageRepository m_mileageRepository;
        private readonly IConfiguration m_configuration;
        public MileageService(IMileageRepository repository, IConfiguration config)
        {
            m_mileageRepository = repository;
            m_configuration = config;
        }
        private decimal ConvertMeterToKM(int meter)
        {
            return meter / 1000;
        }
        private async Task<Response> FindDistanceInGoogleAPI(string postalCodeOrigin, string postalCodeDestination)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(GetRequestUrl(postalCodeOrigin, postalCodeDestination));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                    throw new Exception();
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Response>(content);
                }
            }
        }
        private async Task<decimal> GetDistanceFromGoogleMaps(string postalCodeOrigin, string postalCodeDestination)
        {
            var result = await FindDistanceInGoogleAPI(postalCodeOrigin, postalCodeDestination);
            Response response = result;
            Row firstRow = response.Rows.FirstOrDefault();
            Element firstElement = firstRow.Elements.FirstOrDefault();
            return ConvertMeterToKM(firstElement.Distance.Value);
        }
        private void AddNewMileage(string postalCodeOrigin, string postalCodeDestination, decimal distance)
        {
            m_mileageRepository.Add(new MILEAGE()
            {
                PostalCodeOrigin = postalCodeOrigin,
                PostalCodeDestination = postalCodeDestination,
                Mileage = distance
            });
            m_mileageRepository.Commit();
        }
        private string GetRequestUrl(string postalCodeOrigin, string postalCodeDestination)
        {
            List<string> originsAddresses = new List<string>() { postalCodeOrigin };
            var origins = string.Join("|", originsAddresses.Select(HttpUtility.UrlEncode).ToArray());

            List<string> destinationAddresses = new List<string>() { postalCodeDestination };
            var destinations = string.Join("|", destinationAddresses.Select(HttpUtility.UrlEncode).ToArray());

            string Url = m_configuration["GoogleAPI:Url"]; //"https://maps.googleapis.com/maps/api/distancematrix/json";
            string key = m_configuration["GoogleAPI:APIKey"];
            return $"{Url}?origins={origins}&destinations={destinations}&key={key}";
        }
     
        public async Task<decimal> GetDistance(string postalCodeOrigin, string postalCodeDestination)
        {
            try
            {
                var millage = m_mileageRepository.Find(postalCodeOrigin, postalCodeDestination);
                if (millage != null)
                    return millage.Mileage;

                decimal distance = await GetDistanceFromGoogleMaps(postalCodeOrigin, postalCodeDestination);
                AddNewMileage(postalCodeOrigin, postalCodeDestination, distance);
                return distance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
