using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAcess;
using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModel;
using Rocky_Utility;
using Rocky_Utility.BrainTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rocky.Controllers
{   
    //cần quyền truy cập khi vào page cartshoping
    [Authorize]
    public class CartController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApplicationUserRepository _userRepo;
        private readonly IProductRepository _prodRepo;
        private readonly IInquiryDetailRepository _inqDRepo;
        private readonly IInquiryHeaderRepository _inqHRepo;
        private readonly IOrderDetailRepository _orderDRepo;
        private readonly IOrderHeaderRepository _orderHRepo;
        private readonly ICouponRepository _couponRepo;
        private readonly IBrainTreeGate _brain;
        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }
        public CartController(ApplicationDbContext db, 
            IProductRepository prodRepo, IApplicationUserRepository userRepo, IInquiryDetailRepository inqDRepo, 
            IInquiryHeaderRepository inqHRepo, IOrderDetailRepository orderDRepo,
            IOrderHeaderRepository orderHRepo, IBrainTreeGate brain, ICouponRepository couponRepo)
        {
            _userRepo = userRepo;
            _prodRepo = prodRepo;
            _inqDRepo = inqDRepo;
            _inqHRepo = inqHRepo;
            _orderDRepo = orderDRepo;
            _orderHRepo = orderHRepo;
            _couponRepo = couponRepo;
            _brain = brain;
        }


        public IActionResult Index(double? total)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            //GET DATA FROM SESSION
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!=null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0 ) 
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i=>i.ProductId).ToList();


            IEnumerable<Product> prodListTemp = _prodRepo.GetAll(u => prodInCart.Contains(u.Id)); //lấy tất cả các id phù hợp với prodincart
            //IList<Product> prodList = new List<Product>();
            
            CartVM cartVM = new CartVM();

           
           
            foreach(var cartObj in shoppingCartList)
            {
                Product prodTemp = prodListTemp.FirstOrDefault(u => u.Id == cartObj.ProductId); //truy xuất từng product trong session
                prodTemp.TempSqFt = cartObj.SqFt;
              // prodList.Add(prodTemp);
                 cartVM.ProdList.Add(prodTemp);
               
            }

            List<string> couponCode = new List<string>();
            List<int> disCount = new List<int>();
            List<Coupon> couponTemp = _couponRepo.GetAll().ToList();
            foreach(var item in couponTemp)
            {
                couponCode.Add(item.CouponCode);
                disCount.Add(item.Discount);
            }

            ViewBag.CouponCode = couponCode;
            ViewBag.Discount = disCount;
                        
            return View(cartVM);           
        }
        
       

        //GET-POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        //public IActionResult IndexPost(List<Product> ProdList)
        //{
        public IActionResult IndexPost(CartVM CartVM)
        {
            List<ShoppingCart> shoppingCartLists = new List<ShoppingCart>();
            foreach (Product prod in CartVM.ProdList)
            {
                shoppingCartLists.Add(new ShoppingCart { ProductId = prod.Id, SqFt = prod.TempSqFt });
            }
            HttpContext.Session.Set(WC.SessionCart, shoppingCartLists);

            return RedirectToAction("Summary", new { total = CartVM.Total });
        }

        public IActionResult Summary(double total)
        {
            ApplicationUser applicationUser = new ApplicationUser();        
          
            //CREATE TOKEN
            var gateway = _brain.GetGateway();
            var clientToken = gateway.ClientToken.Generate(); 
            //token dữ liệu tạm thời nên dùng viewbag
            ViewBag.ClientToken = clientToken;//lưu trữ client token trong ViewBag               
          
            //tìm id của user để lấy dl
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            applicationUser = _userRepo.FirstOrDefault(u => u.Id == claim.Value);
           
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            //lấy dữ liệu từ session
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            //IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));
            IEnumerable<Product> prodList = _prodRepo.GetAll(u => prodInCart.Contains(u.Id)); //chưa lấy đc sqft
            //lấy tất cả các dữ liệu truy xuất của iser đấy
            ProductUserVM = new ProductUserVM()
            {
                //ApplicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value),
                ApplicationUser = applicationUser,
                orderTotal = total
            };
            foreach(var cartObj in shoppingCartList)
            {
                Product prodTemp = _prodRepo.FirstOrDefault(u => u.Id == cartObj.ProductId);
                prodTemp.TempSqFt = cartObj.SqFt;
                ProductUserVM.ProductList.Add(prodTemp);
            }
                 
            return View(ProductUserVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(IFormCollection collection, ProductUserVM ProductUserVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                OrderHeader orderHeader = new OrderHeader()
                {
                    CreatedByUserId = claim.Value,
                    //FinalOrderTotal = ProductUserVM.ProductList.Sum(x=>x.Price*x.TempSqFt),
                    FinalOrderTotal = ProductUserVM.orderTotal,
                    City = ProductUserVM.ApplicationUser.City,
                    Address = ProductUserVM.ApplicationUser.Address,
                    OrderDate = DateTime.Now,
                    PhoneNumber = ProductUserVM.ApplicationUser.PhoneNumber,
                    Cmnd = ProductUserVM.ApplicationUser.Cmnd,
                    FullName = ProductUserVM.ApplicationUser.FullName,
                    Email = ProductUserVM.ApplicationUser.Email,
                    OrderStatus = WC.StatusPending
                };
                ////add order header vao database
                _orderHRepo.Add(orderHeader);
                _orderHRepo.Save();

                foreach (var prod in ProductUserVM.ProductList)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderHeaderId = orderHeader.Id,
                        PricePerSqFt = prod.Price,
                        Sqft = prod.TempSqFt,
                        ProductId = prod.Id
                    };
                    _orderDRepo.Add(orderDetail);
                }
                _orderDRepo.Save();

                string nonceFromTheClient = collection["payment_method_nonce"];
                var request = new TransactionRequest
                {
                    Amount = Convert.ToDecimal(orderHeader.FinalOrderTotal),
                    PaymentMethodNonce = nonceFromTheClient,
                    OrderId = orderHeader.Id.ToString(),
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = true //submit sau khi đã xác thực xong
                    }
                };

                var gateway = _brain.GetGateway();
                Result<Transaction> result = gateway.Transaction.Sale(request);
                if(result.Target.ProcessorResponseText=="Approved")
                {
                    orderHeader.TransactionId = result.Target.Id;
                    orderHeader.OrderStatus = WC.StatusApproved;
                }
                else
                {
                    orderHeader.OrderStatus = WC.StatusCancelled;
                }
                _orderHRepo.Save();

            //  return RedirectToAction(nameof(InquiryConfirmation), new { id = orderHeader.Id });
            return RedirectToAction("MyOrder", "Order");
        }
        
        
        public IActionResult InquiryConfirmation(int id=0)
        {
            OrderHeader orderHeader = _orderHRepo.FirstOrDefault(u => u.Id == id); //orderheader sẽ lấy dữ liệu từ orderheader_repo

            HttpContext.Session.Clear();
            return View(orderHeader);
        }
        
        
        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            //lấy dữ liệu từ session
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            //sau khi remove thì set lại cái session 
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear(int id)
        {


            HttpContext.Session.Clear();

            return RedirectToAction("Index","Home"); // quay lại index của HomeController
        }

        public IActionResult UpdateCart (IEnumerable<Product> ProdList)
        {
            List<ShoppingCart> shoppingCartLists = new List<ShoppingCart>();
            foreach(Product prod in ProdList)
            {
                shoppingCartLists.Add(new ShoppingCart {ProductId = prod.Id, SqFt = prod.TempSqFt });
            }
            HttpContext.Session.Set(WC.SessionCart, shoppingCartLists);
            return RedirectToAction(nameof(Index));
        }
    }
}
