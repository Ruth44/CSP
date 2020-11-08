using System.ComponentModel.DataAnnotations;

namespace CSP.ViewModels
{
    public class GetServices
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string OrganizationName { get; set; }

    }
}