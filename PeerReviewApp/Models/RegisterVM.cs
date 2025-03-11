using System.ComponentModel.DataAnnotations;

namespace PeerReviewApp.Models;

public class RegisterVm
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(255)]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please confirm your password.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}