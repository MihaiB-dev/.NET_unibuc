using lab1_8.Data;
using lab1_8.Models;
using Microsoft.AspNetCore.Mvc;

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
            var articles = from article in db.Articles
                           orderby article.Date
                           select article;

            ViewBag.Articles = articles;

            return View();
        }

        public ActionResult Show(int id)
        {
            Article article = db.Articles.Find(id);
            ViewBag.Article = article;
            return View();
        }

        public IActionResult New() {
            return View();
        }

        [HttpPost]
        public IActionResult New(Article s)
        {
            try
            {
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
            Article article = db.Articles.Find(id);
            ViewBag.Article = article;
            return View();
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
