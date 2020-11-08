using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSP.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        
        public string Username{ get; set; }
                [Required]

        public string Password { get; set; }
                [Required]

        public string Fullname { get; set; }
                [Required]
[MaxLength(25) , MinLength(7)]
         public string Email { get; set; }
                 [Required]
         public string Phone { get; set; }
                 [Required]
[MaxLength(6) , MinLength(1)]
         public string Gender { get; set; }
 
    }
}
