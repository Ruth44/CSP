using System.ComponentModel.DataAnnotations;

namespace CSP.ViewModels
{
    public class ReadServices
    {
  

   [Required]
   public string Name { get; set; }
   public string Description { get; set; }
 

    }
}