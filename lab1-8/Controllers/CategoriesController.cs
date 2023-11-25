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

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }
            return View();
        }

        public IActionResult Show(int id) {
           Category category = db.Categories.Find(id);
           return View(category);
        }
        public IActionResult New() {
            return View();
        }

        [HttpPost]
        public IActionResult New(Category cat)
        {
            if(ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                TempData["message"] = "The category " + cat.CategoryName + " has been created";
                return RedirectToAction("Index");
            }
            else{
                return View(cat);
            }
        }
        
        public IActionResult Edit(int id) {
            Category category = db.Categories.Find(id);
            return View(category);   
        }

        [HttpPost]
        public IActionResult Edit(int id, Category functcategory)
        {
            Category category = db.Categories.Find(id);
            if(ModelState.IsValid)
            {
                category.CategoryName = functcategory.CategoryName;
                db.SaveChanges();
                TempData["message"] = "The category " + functcategory.CategoryName + " has been edited";
                return RedirectToAction("Index");

            }else
            {
                // ViewBag.category = functcategory;
                return View(functcategory);
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
