using lab1_8.Data;
using Microsoft.AspNetCore.Mvc;

namespace lab1_8.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext db;
        public CategoriesController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
