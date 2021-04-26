using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackagingAndDeliveringService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagingAndDeliveringService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageDeliveryController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PackageDeliveryController));

        private readonly IPackageDelivery _repo;

        
        public PackageDeliveryController(IPackageDelivery repo)
        {
            _repo = repo;
        }

        [HttpGet("GetPackagingDeliveryCharge/{ComponentType},{Count}")]
        //[Route("GetPackagingDeliveryCharge")]
       
        public IActionResult GetPackagingDeliveryCharge(string ComponentType, int Count)
        {
            try
            {
                _log4net.Info("get package and delivery process method invoked");

                return Ok(_repo.PackageDeliver(ComponentType,Count));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }


    }
}
