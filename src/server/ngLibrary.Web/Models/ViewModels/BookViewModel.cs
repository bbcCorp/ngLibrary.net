using System;
using System.ComponentModel.DataAnnotations;

namespace ngLibrary.Web.Models.ViewModels
{
    public class BookViewModel
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

    }
}
