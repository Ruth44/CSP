using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CSP.Models
{
    public class Organization
    {
   public int Id { get; set; }
   [Required]
   [StringLength(30)]
   public string Name { get; set; }
    [Required]
   public string Description { get; set; }
   public ICollection<Service> Services { get; set; }     
      
    }
}