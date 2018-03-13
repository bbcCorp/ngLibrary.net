using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngLibrary.Model
{
    public class Book
    {

      public int ID { get; set; }

      [Required]
      public string ISBN { get; set; }
      public string ISBN13 { get; set; }

      [Required]
      public string Title { get; set; }

      public string Description { get; set; }

      [Required]
      public string[] Authors { get; set; }

      [Required]
      public string Publisher { get; set; }

      public int PublicationYear { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [Column("tstamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    }
}
