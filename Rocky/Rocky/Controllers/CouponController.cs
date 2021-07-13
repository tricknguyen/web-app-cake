using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class CouponController : Controller
    {
        private readonly ICouponRepository _couRepo;

        public CouponController(ICouponRepository couRepo)
        {
            _couRepo = couRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Coupon> objList = _couRepo.GetAll();
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Coupon obj)
        {
            //kiểm tra điều kiện các rule trong model có giá trị true hay k?
            if (ModelState.IsValid)
            {
                _couRepo.Add(obj);
                _couRepo.Save();
                TempData[WC.Sucess] = "Category Added";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error while creating category!";
            return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //find method chỉ làm việc với primary key
            var obj = _couRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Coupon obj)
        {
            //kiểm tra điều kiện các rule trong model có giá trị true hay k?
            if (ModelState.IsValid)
            {
                _couRepo.Update(obj);
                _couRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //find method chỉ làm việc với primary key
            var obj = _couRepo.Find(id.GetValueOrDefault());
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
            var obj = _couRepo.Find(id.GetValueOrDefault());
            //kiểm tra điều kiện các rule trong model có giá trị true hay k?
            if (obj == null)
            {
                return NotFound();
            }
            _couRepo.Remove(obj);
            _couRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
