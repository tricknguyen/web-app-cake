using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAcess;
using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _appRepo;
        public ApplicationTypeController(IApplicationTypeRepository appRepo)
        {
            _appRepo = appRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _appRepo.GetAll();
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {

            if (ModelState.IsValid)
            {
                _appRepo.Add(obj);
                _appRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id==0)
            {
                return NotFound();
            }
            //find method chỉ làm việc với primary key
            var obj = _appRepo.Find(Id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            //kiểm tra điều kiện các rule trong model có giá trị true hay k?
            if (ModelState.IsValid)
            {
                _appRepo.Update(obj);
                _appRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //find method chỉ làm việc với primary key
            var obj = _appRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _appRepo.Find(id.GetValueOrDefault());
            //kiểm tra điều kiện các rule trong model có giá trị true hay k?
            if (obj == null)
            {
                return NotFound();
            }
            _appRepo.Remove(obj);
            _appRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
