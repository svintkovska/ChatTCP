using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
}
