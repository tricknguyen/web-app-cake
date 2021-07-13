using Microsoft.AspNetCore.Mvc;
using Rocky_DataAcess;
using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky_Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Rocky_Utility;
using Rocky_DataAcess.Repository.IRepository;

namespace Rocky.Controllers
{   
    [Authorize(Roles = WC.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _proRepo;
        private readonly IOrderDetailRepository _orderDRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository proRepo, IWebHostEnvironment webHostEnvironment, IOrderDetailRepository orderDRepo)
        {
            _proRepo = proRepo;
            _webHostEnvironment = webHostEnvironment;
            _orderDRepo = orderDRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _proRepo.GetAll(includeProperties:"Category,ApplicationType");
            return View(objList);
        }

        //GET-UPSERT
      
        public IActionResult Upsert(int? id)
        {
          
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList  = _proRepo.GetAllDropDownList(WC.CategoryName),
                
                ApplicationTypeSelectList  = _proRepo.GetAllDropDownList(WC.ApplicationTypeName)
            };
            
            if(id==null)
            {
                //this is for created
                return View(productVM);
            }
            else
            {
                productVM.Product = _proRepo.Find(id.GetValueOrDefault());
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productVM);
                }
            }
        }

        //POST-UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            //kiểm tra điều kiện các rule trong model có giá trị true hay k?
            if (ModelState.IsValid)
            {
                //lấy image đã upload
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {
                    //creating
                    //lấy hình ảnh từ input image
                    string upload = webRootPath + WC.ImagePath;
                    string filename = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload,filename+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.Image = filename + extension;
                    _proRepo.Add(productVM.Product);

                }
                else
                {
                    //updating
                    //lấy đối tượng product từ database để update
                    /*.AsNoTracking(): nên sử dụng cho các hành động đọc dl từ database
                    EF (entity framework) theo dõi các thực thể bất cứ khi nào thông qua việc truy xuất của chúng thông qua khóa chính
                    Nếu không thêm AsNoTracking ở đây thì lệnh update ở dưới cũng tracked cùng id 
                    Khi muốn update 1 thứ thì nên dùng asnotracking()
                    */
                    var objFromDb = _proRepo.FirstOrDefault(u => u.Id == productVM.Product.Id, isTracking:false);

                    if(files.Count > 0)
                    {
                        //1 file mới đã được update cho 1 file hiện có
                        //cách lấy hình ảnh từ input image
                        string upload = webRootPath + WC.ImagePath;
                        string filename = Guid.NewGuid().ToString(); //new file name
                        string extension = Path.GetExtension(files[0].FileName);

                        //xóa file image cũ
                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if(System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productVM.Product.Image = filename + extension;
                    }
                    else
                    {   
                        //không sửa đổi hình nếu k muốn update
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _proRepo.Update(productVM.Product);
                }
                _proRepo.Save();
                return RedirectToAction("Index");
            }
            //nếu modelstate k có giá trị
            //vẫn reload các giá trị của category
            productVM.CategorySelectList = _proRepo.GetAllDropDownList(WC.CategoryName);

            productVM.ApplicationTypeSelectList = _proRepo.GetAllDropDownList(WC.ApplicationTypeName);
            return View(productVM);
        }


        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product product = _proRepo.FirstOrDefault(u=>u.Id==id,includeProperties:"Category,ApplicationType");
            

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _proRepo.Find(id.GetValueOrDefault());
            //kiểm tra điều kiện các rule trong model có giá trị true hay k?
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
           
            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _proRepo.Remove(obj);
            _proRepo.Save();
            return RedirectToAction("Index");           
        }

        public IActionResult Interaction()
        {
            IEnumerable<Product> objList = _proRepo.GetAll(includeProperties: "Category,ApplicationType");
            List<string> nameProduct = new List<string>();
            foreach(var item in objList)
            {
                nameProduct.Add(item.Name);
            }
            ViewBag.NameProduct = nameProduct.ToList();
            List<int> numberOfInteraction = new List<int>();
            foreach(var item in objList)
            {
                numberOfInteraction.Add(item.Love);
            }
            ViewBag.Interaction = numberOfInteraction.ToList();
            return View();
        }

        public IActionResult RankOfProduct()
        {
            IEnumerable<Product> productList = _proRepo.GetAll(includeProperties: "Category,ApplicationType");
            List<string> nameProduct = new List<string>();
            foreach(var item in productList)
            {
                nameProduct.Add(item.Name);
            }
            ViewBag.nameProduct = nameProduct.ToList();


            List<int> numberofproduct = new List<int>();
            IEnumerable<OrderDetail> orderDetailsList = _orderDRepo.GetAll();
            foreach(var item in productList)
            {
                int a = orderDetailsList.Count(n => n.ProductId == item.Id);
                if (a == 0) numberofproduct.Add(0);
                else
                {
                    numberofproduct.Add(a);
                }
            }
            ViewBag.NumberofProduct = numberofproduct.ToList();
            return View();
        }
    }
}
