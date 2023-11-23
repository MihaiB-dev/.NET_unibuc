using lab1_8.Data;
using Microsoft.AspNetCore.Mvc;

namespace lab1_8.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly AppDbContext db;
        public ArticlesController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
