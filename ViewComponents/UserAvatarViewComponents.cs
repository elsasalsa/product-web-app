using Microsoft.AspNetCore.Mvc;
using ProductManagementApp.Data; // sesuaikan namespace
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductManagementApp.ViewComponents
{
    public class UserAvatarViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public UserAvatarViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return View("Default", "/image/avatar_png.png");
            }

            int userId = int.Parse(userIdClaim.Value);

            var user = await _context.Users.FindAsync(userId);

            string profilePicturePath = string.IsNullOrEmpty(user?.ProfilePicture)
                ? "/image/avatar.png"
                : user.ProfilePicture;

            return View("Default", profilePicturePath);
        }
    }
}
