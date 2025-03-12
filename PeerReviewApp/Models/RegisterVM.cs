using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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

    [Required(ErrorMessage = "Please select a role.")]
    [Display(Name = "Role")]
    public string SelectedRole { get; set; } = string.Empty;

    // This property will provide the dropdown options
    public List<SelectListItem> AvailableRoles { get; set; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "Student", Text = "Student" },
        new SelectListItem { Value = "Instructor", Text = "Instructor" }
        // You can add Admin here too if you want it selectable during registration
        // new SelectListItem { Value = "Admin", Text = "Administrator" }
    };
}