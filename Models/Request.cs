using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CSP.Models
{
    public class Request
    {
   public int Id { get; set; }

   
   public int UserId { get; set; }
   public User User{ get; set; }
   [Required]
      [MaxLength(20)]

   public string Status { get; set; }
   [Required]
   public DateTime CreatedAt { get; set; }
      [Required]
   public DateTime RequestedFor { get; set; }
   [Required]
  
   public int notification { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }

    }
}