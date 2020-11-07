using System.ComponentModel.DataAnnotations;

namespace Music.ViewModels
{
    public class ReadAlbums
    {
  

   [Required]
   public string Name { get; set; }
    [Required]
   public int ReleaseYear { get; set; }


    }
}