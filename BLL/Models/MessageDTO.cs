using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
