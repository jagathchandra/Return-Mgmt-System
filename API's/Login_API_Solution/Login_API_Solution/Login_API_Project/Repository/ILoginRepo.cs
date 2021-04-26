using Login_API_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Login_API_Project.Repository
{
    public interface ILoginRepo
    {
        public string AuthenticateUser(string username, string password);

        //public LoginDetail login(string username, string password);

        
    }
}
