using System;
using System.ComponentModel.DataAnnotations;

namespace CSP.ViewModels
{
    public class GetRequest
    {
                   [Required]

           public int Id { get; set; }
           [Required]
   
   public string Username { get; set; }
      [Required]
      
   
   public string Status { get; set; }
   [Required]
   public DateTime CreatedAt { get; set; }
      [Required]
   public DateTime RequestedFor { get; set; }
   [Required]
  
   public int notification { get; set; }
    public string ServiceName { get; set; }

    }
}