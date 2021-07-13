using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Rocky.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _prodRepo;
        private readonly ICategoryRepository _catRepo;
        private readonly IMessRepository _messRepo;
        private readonly ICommentRepository _comRepo;
        private readonly IApplicationUserRepository _userRepo;
        //private readonly UserManager<AppUser> _userManager;
        //public readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IProductRepository prodRepo, ICategoryRepository catRepo, IMessRepository messRepo,
            ICommentRepository comRepo,   IApplicationUserRepository userRepo)
        {
            _logger = logger;
            _prodRepo = prodRepo;
            _catRepo = catRepo;
            _messRepo = messRepo;
            _comRepo = comRepo;
            _userRepo = userRepo;
          //  _userManager = userManager;
        }
       
        public async Task<IActionResult> Index()
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            //ViewBag.CurrentUserName = currentUser.UserName;
            //var messages = await _context.Messages.ToListAsync();

            HomeVM homeVM = new HomeVM()
            {
                Products = _prodRepo.GetAll(includeProperties:"Category,ApplicationType"),
                Categories = _catRepo.GetAll()
            };
            return View(homeVM);//chỉ dịnh 2 thuộc tính cho View Model
        }

        //public async Task<IActionResult> Create(Message message)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        message.UserName = User.Identity.Name;
        //        var sender = await _userManager.GetUserAsync(User);
        //        message.UserID = sender.Id;
        //        await _context.Messages.AddAsync(message);
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }
        //    return Error();
        //}



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
                    ExistsInCart = false,

                    CommentList = _comRepo.GetAll(includeProperties: "CreatedBy,Product")
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

      
        [HttpPost]
        [Authorize]
        public IActionResult CommentPost(DetailsVM detailsVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //claim.value
            Comment Comment = new Comment();

            Comment.ProductID = detailsVM.Product.Id;
            Comment.Content = detailsVM.Comment.Content;
            Comment.CreatedByUserId = claim.Value;
            _comRepo.Add(Comment);
            _comRepo.Save();

            return RedirectToAction("Details", new { id = detailsVM.Product.Id });
        }

        [Authorize]
        [HttpPost]
        public JsonResult IncreaseLove(int id)
        {
            Product product = new Product();
            product = _prodRepo.FirstOrDefault(u => u.Id == id);
            product.Love = product.Love + 1;
            _prodRepo.Update(product);
            _prodRepo.Save();
            return Json(new
            {
                data = product
            });
        }
            
        public IActionResult Covid()
        {
            return View();
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

        public IActionResult CreateMessage(Message message)
        {
            _messRepo.Add(message);
            _catRepo.Save();
            return Ok();
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

        public IActionResult tellmemore()
        {
            return View();
        }
    }
}
