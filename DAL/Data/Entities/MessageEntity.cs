using SomeeMSSQLConsole.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblMessages")]
    public class MessageEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [StringLength(4000)]
        public string Text { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }

    }
}
