namespace ProductManagementApp.Models

{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public int UserId { get; set; }

        [ValidateNever]
        public User User { get; set; } = null!;
        public int CategoryId { get; set; }

        [ValidateNever]
        public ProductCategory ProductCategory { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
