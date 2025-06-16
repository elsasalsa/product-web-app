using System.ComponentModel.DataAnnotations;

namespace ProductManagementApp.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The category name field is required")]
        public string CategoryName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Product> Products { get; set; } = new List<Product>();

        public bool IsDeleted { get; set; } = false;  
    }

}
