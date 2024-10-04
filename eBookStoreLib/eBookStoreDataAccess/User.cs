using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eBookStoreLib.DataAccess
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Source { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int? RoleId { get; set; }
        public int? PubId { get; set; }
        public DateTime? HireDate { get; set; }        
        public virtual Publisher? Publisher { get; set; }
        public virtual Role? Role { get; set; }
    }
}
