using System.ComponentModel.DataAnnotations;

namespace WoundClinic.ViewModels.Account
{
    public sealed class RegisterUserViewModel
    {
        [Required]
        [Display(Name = "کد ملی")]
        public long PersonNationalCode { get; set; }

        [Required]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";

    }
}
