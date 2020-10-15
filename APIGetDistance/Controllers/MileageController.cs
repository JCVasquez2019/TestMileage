using System;
using System.Threading.Tasks;

using Infraestructure;
using Microsoft.AspNetCore.Mvc;

namespace APIGetDistance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MileageController : Controller
    {
        private readonly IMileageService m_service;

        public MileageController(IMileageService service)
        {
            this.m_service = service;
        }

        [HttpGet("GetDistance")]
        public async Task<IActionResult> GetDistance(string postalCodeOrigin, string postalCodeDestination)
        {
            try
            {
                decimal distance = await m_service.GetDistance(postalCodeOrigin, postalCodeDestination);
                return Ok(string.Format("{0} km",Convert.ToInt32(distance)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
