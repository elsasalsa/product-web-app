public class EditProductViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public IFormFile? ProductImage { get; set; }
    public string? ExistingImagePath { get; set; }
    public int CategoryId { get; set; }
}
