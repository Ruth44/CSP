using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace CSP.ViewModels
{
    public class CreateUserAccount
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Email { get; set; }
        
        [Required]
         public string Phone { get; set; }
         [Required]
         public bool isAdmin { get; set; }
                 [Required]

         public string Gender { get; set; }
    }
}

    