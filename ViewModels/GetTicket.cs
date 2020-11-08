using System;
using System.ComponentModel.DataAnnotations;

namespace CSP.ViewModels
{
    public class GetTicket
    {
                 
      [Required]
   public string Username { get; set; }
      [Required]
    public string FullName{get;set;}  
   
   public string Status { get; set; }
   [Required]
   public DateTime CreatedAt { get; set; }
      [Required]
   public DateTime RequestedFor { get; set; }
   [Required]
  
    public string ServiceName { get; set; }
   [Required]

   public int TicketNumber { get; set; }
   [Required]
   
    public int RequestId { get; set; }
      
    
    }
}