using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Data;
using ProductManagementApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductManagementApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.ProductCategory)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            var deletedProducts = await _context.Products
                .Include(p => p.ProductCategory)
                .Where(p => p.IsDeleted)
                .ToListAsync();

            ViewData["DeletedProduct"] = deletedProducts;

            return View(products);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            ViewBag.Categories = await _context.ProductCategories
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile ProductImage)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.ProductCategories
                    .Where(c => !c.IsDeleted)
                    .ToListAsync();

                return View(product);
            }

            if (ProductImage != null && ProductImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProductImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProductImage.CopyToAsync(stream);
                }

                product.ProductImage = "/images/" + fileName;
            }

            product.UserId = userId;
            product.IsDeleted = false;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            var viewModel = new EditProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                ExistingImagePath = product.ProductImage
            };

            ViewData["CategoryList"] = new SelectList(
                await _context.ProductCategories
                    .Where(c => !c.IsDeleted)
                    .ToListAsync(),
                "Id", "CategoryName",
                viewModel.CategoryId
            );

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!ModelState.IsValid)
            {
                ViewData["CategoryList"] = new SelectList(
                    await _context.ProductCategories
                        .Where(c => !c.IsDeleted)
                        .ToListAsync(),
                    "Id", "CategoryName",
                    model.CategoryId
                );
                return View(model);
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            product.ProductName = model.ProductName;
            product.CategoryId = model.CategoryId;
            product.UserId = userId;

            if (model.ProductImage != null && model.ProductImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProductImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProductImage.CopyToAsync(stream);
                }

                product.ProductImage = "/images/" + fileName;
            }
            else if (!string.IsNullOrEmpty(model.ExistingImagePath))
            {
                product.ProductImage = model.ExistingImagePath;
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null) return NotFound();

            product.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted);

            if (product == null) return NotFound();

            product.IsDeleted = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PermanentDelete(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted);

            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
