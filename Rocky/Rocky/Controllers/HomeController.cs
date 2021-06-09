using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rocky_DataAcess;
using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModel;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _prodRepo;
        private readonly ICategoryRepository _catRepo;
        public HomeController(ILogger<HomeController> logger, IProductRepository prodRepo, ICategoryRepository catRepo)
        {
            _logger = logger;
            _prodRepo = prodRepo;
            _catRepo = catRepo;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _prodRepo.GetAll(includeProperties:"Category,ApplicationType"),
                Categories = _catRepo.GetAll()
            };
            return View(homeVM);//chỉ dịnh 2 thuộc tính cho View Model
        }

        public IActionResult Details(int ?id)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                 && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)

            {
                //lấy dữ liệu từ session
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);

            }
            
            

                DetailsVM DetailsVM = new DetailsVM()
                {
                    Product = _prodRepo.FirstOrDefault(u => u.Id == id, includeProperties: "Category,ApplicationType"),

                    //có có thể trả về nhiều đối tưowngj vì vậy phải thêm .First
                    ExistsInCart = false
                };


                foreach (var item in shoppingCartsList)
                {
                    if (item.ProductId == id)
                    {
                        DetailsVM.ExistsInCart = true;
                    }
                }
            
                return View(DetailsVM);
            
        }

        [HttpPost]
        public IActionResult DetailsPost(int id, DetailsVM detailsVM)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!=null
                 && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
               
            {
                //lấy dữ liệu từ session
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);

            }
            //thêm new item vào shopping cart
            shoppingCartsList.Add(new ShoppingCart { ProductId = id , SqFt = detailsVM.Product.TempSqFt} );
            HttpContext.Session.Set(WC.SessionCart, shoppingCartsList);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                 && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)

            {
                //lấy dữ liệu từ session
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);

            }

            var itemToRemove = shoppingCartsList.SingleOrDefault(r => r.ProductId == id); //SingleOrDefault trả về p.tu duy nhất của chuỗi hoặc giá trị mặc định nếu chuỗi trống
            if (itemToRemove != null)
            {
                shoppingCartsList.Remove(itemToRemove);
            }

            //thêm new item vào shopping cart
            //shoppingCartsList.Add(new ShoppingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, shoppingCartsList);
            return RedirectToAction(nameof(Index));
            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
