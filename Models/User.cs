using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSP.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
           [StringLength(30)]

        public string Username{ get; set; }
                [Required]

        public string Password { get; set; }
                [Required]

        public string Fullname { get; set; }
                [Required]
   [StringLength(40)]
         public string Email { get; set; }
                 [Required]
                    [StringLength(15)]
                             public string Phone { get; set; }

                                     [Required]

        public bool isAdmin {get;set;}

                 [Required]
[MaxLength(6) , MinLength(1)]
         public string Gender { get; set; }
 
    }
}
