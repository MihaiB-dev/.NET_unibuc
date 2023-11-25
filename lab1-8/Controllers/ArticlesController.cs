using lab1_8.Data;
using lab1_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lab1_8.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext db;
        public ArticlesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var articles = db.Articles.Include("Category");

            ViewBag.Articles = articles;

            return View();
        }

        public ActionResult Show(int id)
        {
            //de schimbat
            Article article = db.Articles.Include("Category").Include("Comments")
                               .Where(art => art.Id == id)
                               .First();
            
            ViewBag.Comments = article.Comments.OrderByDescending(el => el.Date);
            ViewBag.Category = article.Category;
            return View(article);
        }

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generate a list of type SelectListItem without elements
            var selectList = new List<SelectListItem>();
            // extragem toate categoriile din baza de date
            var categories = from cat in db.Categories
                             select cat;
            // iteram prin categorii

            // Sau se poate implementa astfel:
            foreach (var category in categories)
            {
            var listItem = new SelectListItem();
            listItem.Value = category.Id.ToString();
            listItem.Text = category.CategoryName.ToString();
            selectList.Add(listItem);
            }
            // returnam lista de categorii
            return selectList;
        }
        public IActionResult New() {
            Article article = new Article();
            article.Listcateg = GetAllCategories();

            return View(article);
        }

        [HttpPost]
        public IActionResult New(Article s)
        {
            try
            {
                s.Date = DateTime.Now;
                db.Articles.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            Article article = db.Articles.Include("Category")
                                        .Where(art => art.Id == id)
                                        .First();
            

            ViewBag.Category = article.Category;
            var categories = from categ in db.Categories
                             select categ;
            ViewBag.Categories = categories;

            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(int id, Article requestArticle) {
            Article article = db.Articles.Find(id);

            try
            {
                article.Title = requestArticle.Title;
                article.Content = requestArticle.Content;
                article.Date = requestArticle.Date;

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("Edit", article.Id);
            }

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
