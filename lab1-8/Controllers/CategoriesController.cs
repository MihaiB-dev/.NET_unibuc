using lab1_8.Data;
using lab1_8.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab1_8.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoriesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var categories = db.Categories;
            ViewBag.Categories = categories;

            return View();
        }

        public IActionResult Show(int id) {

            var category = db.Categories.Find(id);
            ViewBag.Category = category;
           return View();
        }
        public IActionResult New() {
            return View();
        }

        [HttpPost]
        public IActionResult New(Category cat)
        {
            try
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ) {
                return View();
            }
        }
        
        public IActionResult Edit(int id) {
            var category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
            
        }

        [HttpPost]
        public IActionResult Edit(int id, Category functcategory)
        {
            Category category = db.Categories.Find(id);
            try
            {
                category.CategoryName = functcategory.CategoryName;
                db.SaveChanges();
                return RedirectToAction("Index");

            }catch(Exception)
            {
                ViewBag.category = functcategory;
                return View();
            }

        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
