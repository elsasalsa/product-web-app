using Microsoft.AspNetCore.Mvc;
using ProductManagementApp.Data;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

}
