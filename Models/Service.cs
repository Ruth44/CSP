using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CSP.Models
{
    public class Service
    {
   public int Id { get; set; }
   [Required]
   public string Name { get; set; }
   public string Description { get; set; }
   public int OrganizationId {get;set; }
        public Organization Organization { get; set; }

    }
}