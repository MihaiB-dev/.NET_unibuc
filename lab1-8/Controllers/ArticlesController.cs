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

            //one message from delete
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Show([FromForm] Comment comment)
        {
            comment.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("/Articles/Show/" + comment.ArticleId);
            }
            else
            {
                Article art = db.Articles.Include("Category").Include("Comments")
                .Where(art => art.Id == comment.ArticleId)
                .First();
                //return Redirect("/Articles/Show/" + comm.ArticleId);
                return View(art);
            }
        }
        public ActionResult Show(int id)
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }

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
            s.Date = DateTime.Now;
            s.Listcateg = GetAllCategories();
            if (ModelState.IsValid)
            {
                db.Articles.Add(s);
                db.SaveChanges();
                TempData["message"] = "The article " + s.Title + " has been created";
                return RedirectToAction("Index");
            }
            else {
                return View(s);
            }
        }

        public IActionResult Edit(int id)
        {
            Article article = db.Articles.Include("Category")
                                        .Where(art => art.Id == id)
                                        .First();


            article.Listcateg = GetAllCategories();

            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(int id, Article requestArticle) {
            Article article = db.Articles.Find(id);
            requestArticle.Listcateg = GetAllCategories();

            if(ModelState.IsValid)
            {
                article.Title = requestArticle.Title;
                article.Content = requestArticle.Content;
                article.CategoryId = requestArticle.CategoryId;

                db.SaveChanges();
                TempData["message"] = "The articles" + article.Title + " has been modified";
                return RedirectToAction("Index");

            }
            else
            {

                return View(requestArticle);
            }

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();

            TempData["message"] = "Article with name " + article.Title + " has been deleted succesfully";
            return RedirectToAction("Index");
        }

    }
}
