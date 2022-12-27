using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClientWPF.dto
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
