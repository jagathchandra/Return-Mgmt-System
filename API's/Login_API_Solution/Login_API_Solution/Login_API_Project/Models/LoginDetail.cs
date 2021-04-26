using System;
using System.Collections.Generic;

#nullable disable

namespace Login_API_Project.Models
{
    public partial class LoginDetail
    {
        public LoginDetail()
        {
            ProcessDetails = new HashSet<ProcessDetail>();
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ProcessDetail> ProcessDetails { get; set; }
    }
}
