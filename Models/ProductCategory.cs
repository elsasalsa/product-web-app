using System.Collections.Generic;

namespace ProductManagementApp.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Product> Products { get; set; } = new List<Product>();

        public bool IsDeleted { get; set; } = false;  
    }

}
