using System.ComponentModel.DataAnnotations;

namespace Music.ViewModels
{
    public class ReadArtists
    {
  

   [Required]
   public string Name { get; set; }
    [Required]
   public string Nationality { get; set; }


    }
}