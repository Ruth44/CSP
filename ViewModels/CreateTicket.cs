using System;
using System.ComponentModel.DataAnnotations;

namespace CSP.ViewModels
{
    public class CreateTicket
    {
   

   [Required]
   
   
   public string Status { get; set; }

     [Required]
   public DateTime CreatedFor { get; set; }
    public string ServiceName { get; set; }
        
    public int RequestId { get; set; }

    }
}