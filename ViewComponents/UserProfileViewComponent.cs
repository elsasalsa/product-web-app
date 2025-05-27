using Microsoft.AspNetCore.Mvc;
using ProductManagementApp.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementApp.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public UserProfileViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Ambil user ID dari session/claims (misalnya pakai Identity)
            var userIdClaim = UserClaimsPrincipal.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return View(null); // fallback view

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return View(user); // modelnya bisa null
        }
    }
}
