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
   [Required]
   
   public string UserName { get; set; }
     [Required]
   
   public int TicketNumber { get; set; }
   [Required]
   
   public string Status { get; set; }
   [Required]
   public DateTime CreatedAt { get; set; }
     [Required]
   public DateTime CreatedFor { get; set; }
    public string ServiceDescription { get; set; }
      [Required]
        public Service Service { get; set; }
    }
}