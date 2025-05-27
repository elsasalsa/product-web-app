using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementApp.ViewModels
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama wajib diisi")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; }

        public string? ProfilePicture { get; set; }

        public IFormFile? ProfilePictureFile { get; set; }
    }
}
