using System.ComponentModel.DataAnnotations;

namespace rottenpotatoes.Models
{
  public class MovieEditModel
  {
    [Required]
    public int MovieId { get; set; }

    [Required]
    public int MovieDescId { get; set; }

    [Required(ErrorMessage = "Enter title!")]
    [StringLength(40, ErrorMessage = "Title can contain maximum of 40 characters!")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Enter director!")]
    [StringLength(40, ErrorMessage = "Director can contain maximum of 40 characters!")]
    public string Director { get; set; }

    [Required(ErrorMessage = "Enter producer!")]
    [StringLength(80, ErrorMessage = "Producer can contain maximum of 80 characters!")]
    public string Producer { get; set; }

    [Url]
    [Required(ErrorMessage = "Enter image url!")]
    [StringLength(200, ErrorMessage = "Image source can contain maximum of 200 characters!")]
    public string ImageSrc { get; set; }

    [Required(ErrorMessage = "Enter genre!")]
    [StringLength(40, ErrorMessage = "Genre can contain maximum of 40 characters!")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "Enter runtime!")]
    [StringLength(20, ErrorMessage = "Runtime can contain maximum of 20 characters!")]
    public string Runtime { get; set; }

    [Required(ErrorMessage = "Enter description!")]
    [StringLength(2000, ErrorMessage = "Description can contain maximum of 2000 characters!")]
    public string Desc { get; set; }
  }
}
