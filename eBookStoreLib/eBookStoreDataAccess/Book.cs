using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eBookStoreLib.DataAccess
{
    public partial class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public int? PubId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public int? Royalty { get; set; }
        public int? YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime? PublishedDate { get; set; }        

        public virtual Publisher? PublisherPub { get; set; }
        [JsonIgnore] public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
