using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalog;

public class CourseCreateInput
{
    [Display(Name = "Kurs İsmi")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "Kurs Açıklama")]
    [Required]
    public string Description { get; set; }

    [Display(Name = "Kurs Fiyat")]
    [Required]
    public decimal Price { get; set; }
    
    public string UserId { get; set; }

    public string Picture { get; set; }

    public FeatureViewModel Feature { get; set; }

    public string CategoryId { get; set; }
}