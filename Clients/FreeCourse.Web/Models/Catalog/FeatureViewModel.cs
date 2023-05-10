using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Models.Catalog;

public class FeatureViewModel
{
    [Display(Name = "Kurs Süre")]
    [Required]
    public int Duration { get; set; }
}