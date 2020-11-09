using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSP.Models;
namespace CSP.ViewModels
{
    public class ReadOrganizations
    {
    [Required]
   public string Name { get; set; }
    [Required]
   public string Description { get; set; }

     }

}