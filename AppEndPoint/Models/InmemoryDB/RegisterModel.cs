using System.ComponentModel.DataAnnotations;

namespace MyTaskManagerEndPoint.Models.InmemoryDB
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string NationalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get; set;
        }
    }
}
