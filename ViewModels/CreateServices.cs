using System.ComponentModel.DataAnnotations;

namespace CSP.ViewModels
{
    public class CreateServices
    {
  

 [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
       


    }
}