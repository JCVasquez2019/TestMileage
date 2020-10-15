using APIGetDistance.Controllers;
using Infraestructure;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace APIGetdistance.Test
{
    public class Tests
    {
        private MileageController mController;
        private IMileageService mService;
        [SetUp]
        public void Setup()
        {
            mService = new MileageServiceFake();
            mController = new MileageController(mService);
        }

        [Test]
        public void OkResultTest()
        {
            var okResult =  mController.GetDistance("TX 77520", "TX 77303");
            Assert.IsTrue(okResult.Result.GetType() == typeof(OkObjectResult));
        }

        [Test]
        public void ValueTest()
        {
            IActionResult result = mController.GetDistance("TX 77520", "TX 77303").GetAwaiter().GetResult();
            OkObjectResult okresult = result as OkObjectResult;
            Assert.IsTrue(okresult.Value.ToString() == "130 km");
        }
    }
}