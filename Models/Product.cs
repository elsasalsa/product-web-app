using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProductManagementApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The product name field is required")]
        public string ProductName { get; set; } = string.Empty;

        public string ProductImage { get; set; } = string.Empty;

        public int UserId { get; set; }

        [ValidateNever]
        public User User { get; set; } = null!;

        [Required(ErrorMessage = "The product category field is required")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public ProductCategory ProductCategory { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
