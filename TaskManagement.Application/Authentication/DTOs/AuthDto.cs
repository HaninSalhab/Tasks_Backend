using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Authentication.DTOs
{
    public class AuthDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
