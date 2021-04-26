using Login_API_Project.Models;
using Login_API_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        private readonly ILoginRepo loginRepo;
        public LoginController(ILoginRepo Ilogin)
        {
            this.loginRepo = Ilogin;
        }
        [HttpGet]
        public string Get()
        {
            return "Hello";
        }
       
        [AllowAnonymous]
        [HttpPost("AuthenicateUser")]
        public IActionResult AuthenticateUser([FromBody] LoginDetail loginDetail)
        {
            
            _log4net.Info(" Http Authentication request Initiated");
            var token = loginRepo.AuthenticateUser(loginDetail.Username, loginDetail.Password);
            if (token == null)
            {
                
                return Unauthorized();

            }              
            return Ok(token);
        }

        
    }
}
