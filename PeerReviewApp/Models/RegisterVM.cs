using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PeerReviewApp.Models;
public class RegisterVm
{
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    
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
    
    [Display(Name = "Instructor Code")]
    [MinLength(6, ErrorMessage = "Invalid Instructor Code.")]
    [StringLength(6)]
    public string? InstructorCode { get; set; } = string.Empty;

    public List<SelectListItem> AvailableRoles { get; set; } = new();
}