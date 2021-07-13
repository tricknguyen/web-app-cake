using Microsoft.AspNetCore.Mvc;
using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _comRepo;
        public IEnumerable<Comment> CommentList { get; set; }
        public CommentController(ICommentRepository comRepo)
        {
            _comRepo = comRepo;
        }

        public IActionResult Index()
        {
            CommentList = _comRepo.GetAll(includeProperties: "CreatedBy,Product");
            return View(CommentList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var obj = _comRepo.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _comRepo.Remove(obj);
            _comRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
