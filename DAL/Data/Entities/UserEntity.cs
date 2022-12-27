using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SomeeMSSQLConsole.Data.Entities
{   
    [Table("tblUsers")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
