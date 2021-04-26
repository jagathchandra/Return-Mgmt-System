using ComponentProcessingAPIProject.Models;
using ComponentProcessingAPIProject.Repository;
using ComponentProcessingAPIProject.Test.view_model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessingAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProcessController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProcessController));

        private readonly IRepo _repo;


        public ProcessController(IRepo repo)
        {
            _repo = repo;
        }


        [HttpGet]
        [Route("GetAllDetails")]
        public IEnumerable<ProcessDetail> GetAllDetails()
        {
            _log4net.Info("");
            return _repo.GetAllDetails();
        }


        [HttpPost]
        [Route("ProcessRequest")]
        public IActionResult ProcessRequest(ProcessRequest p)
        {
            try
            {
                return Ok(_repo.ProcessResponse(p.Name, p.ContactNumber, p.ComponentType, p.ComponentName, p.Quantity, p.IsPriorityRequest));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("CompleteProcessing/{RequestId}")]
        public IActionResult CompleteProcessing(int RequestId)
        {
            try
            {
                _log4net.Info("complete  processing method invoked");

                return Ok(_repo.CompleteProcess(RequestId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
