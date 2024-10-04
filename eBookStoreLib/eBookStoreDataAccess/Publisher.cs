using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eBookStoreLib.DataAccess
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
            Users = new HashSet<User>();
        }
        [Key]
        public int PubId { get; set; }
        public string? PublisherName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        [JsonIgnore]public virtual ICollection<Book> Books { get; set; }
        [JsonIgnore]public virtual ICollection<User> Users { get; set; }
    }
}
