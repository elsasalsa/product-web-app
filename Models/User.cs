using System.Collections.Generic;

namespace ProductManagementApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public List<Product> Products { get; set; } = new List<Product>();

        public User()
        {
        }
    }
}
