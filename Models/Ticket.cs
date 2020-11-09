using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CSP.Models
{
    public class Ticket
    {
         public int Id { get; set; }
   public int UserId { get; set; }
   public User User{ get; set; }
     [Required]
   
   public int TicketNumber { get; set; }
   [Required]
   [MaxLength(20)]
   public string Status { get; set; }
   [Required]
   public DateTime CreatedAt { get; set; }
     [Required]
   public DateTime CreatedFor { get; set; }
    public int ServiceId { get; set; }
        public Service Service { get; set; }
        
    public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}