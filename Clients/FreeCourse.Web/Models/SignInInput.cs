using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models;

public class SignInInput
{
    [Required(ErrorMessage = "Email Adresiniz hatalı !")]
    [Display(Name = "Email Adresiniz")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifreniz hatalı !")]
    [Display(Name = "Şifreniz")]
    public string Password { get; set; }
    
    [Required]
    [Display(Name = "Beni Hatırla")]
    public bool Remember { get; set; }
}