using lab1_8.Data;
using lab1_8.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab1_8.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;
        public CommentsController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);
            var ArticleId = comm.ArticleId;
            db.Comments.Remove(comm);
            db.SaveChanges();
            return Redirect("/Articles/Show/" + ArticleId);
        }
        public IActionResult Edit(int id)
        {
            Comment comm = db.Comments.Find(id);
            ViewBag.Comment = comm;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id,  Comment requestComment)
        {
            Comment comm = db.Comments.Find(id);
            try
            {
                comm.Content = requestComment.Content;
                db.SaveChanges();
                return Redirect("/Articles/Show/" + comm.ArticleId);
            }catch(Exception )
            {
                return Redirect("/Articles/Edit/" + comm.ArticleId);
            }
        }
    }
}
