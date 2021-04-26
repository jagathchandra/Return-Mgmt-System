using System;
using System.Collections.Generic;

#nullable disable

namespace PackagingAndDeliveringService.Models
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
