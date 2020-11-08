using System;
using System.ComponentModel.DataAnnotations;

namespace CSP.ViewModels
{
    public class CreateRequest
    {
   

      [Required]
   public DateTime RequestedFor { get; set; }
   [Required]
  
   public int notification { get; set; }
    public string ServiceName { get; set; }

    }
}