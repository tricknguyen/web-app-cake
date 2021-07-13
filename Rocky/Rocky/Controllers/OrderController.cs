using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAcess;
using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModel;
using Rocky_Utility;
using Rocky_Utility.BrainTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rocky.Controllers
{   
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IOrderDetailRepository _orderDRepo;
        private readonly IOrderHeaderRepository _orderHRepo;
        private readonly IBrainTreeGate _brain;
        private readonly IApplicationUserRepository _userRepo;
        private readonly ICouponRepository _couponRepo;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IOrderDetailRepository orderDRepo,
            IOrderHeaderRepository orderHRepo, IBrainTreeGate brain,
            IApplicationUserRepository userRepo, ICouponRepository couponRepo)
        {
     
            _orderDRepo = orderDRepo;
            _orderHRepo = orderHRepo;
            _brain = brain;
            _userRepo = userRepo;
            _couponRepo = couponRepo;
        }
        public IActionResult Index(string searchName=null, string searchEmail=null, string searchPhone=null, string Status=null)
        {
            OrderListVM orderListVM = new OrderListVM()
            {
                OrderHList = _orderHRepo.GetAll(),
                StatusList = WC.listStatus.ToList().Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem {
                    Text = i,
                    Value = i
                })
            };

            if(!string.IsNullOrEmpty(searchName))
            {
                orderListVM.OrderHList = orderListVM.OrderHList.Where(u => u.FullName.ToLower().Contains(searchName.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchEmail))
            {
                orderListVM.OrderHList = orderListVM.OrderHList.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                orderListVM.OrderHList = orderListVM.OrderHList.Where(u => u.PhoneNumber.ToLower().Contains(searchPhone.ToLower()));
            }
            if (!string.IsNullOrEmpty(Status) && Status!= "--Order Status--")
            {
                orderListVM.OrderHList = orderListVM.OrderHList.Where(u => u.OrderStatus.ToLower().Contains(Status.ToLower()));
            }

            return View(orderListVM);
        }

        public IActionResult Detail(int id)
        {
            OrderVM = new OrderVM()
            {
                OrderHeader = _orderHRepo.FirstOrDefault(u => u.Id == id),
                OrderDetail = _orderDRepo.GetAll(o => o.OrderHeaderId == id, includeProperties:"Product")
            };
            return View(OrderVM);
        }
        
        [HttpPost]
        public IActionResult StartProcessing()
        {
            OrderHeader orderHeader = _orderHRepo.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.OrderStatus = WC.StatusInProcess;
            _orderHRepo.Save();
            TempData[WC.Sucess] = "Order Is In Process";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ShipOrder()
        {
            OrderHeader orderHeader = _orderHRepo.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.OrderStatus = WC.StatusShiped;
            orderHeader.ShippingDate = DateTime.Now;
            _orderHRepo.Save();
            TempData[WC.Sucess] = "Order Shipped Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CancelOrder()
        {
            OrderHeader orderHeader = _orderHRepo.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);

            var gateway = _brain.GetGateway();
            Transaction transaction = gateway.Transaction.Find(orderHeader.TransactionId);
            if(transaction.Status==TransactionStatus.AUTHORIZED|| transaction.Status == TransactionStatus.SUBMITTED_FOR_SETTLEMENT)
            {   
                //tiep tuc giao dich
                Result<Transaction> resultvoid = gateway.Transaction.Void(orderHeader.TransactionId);

            }
            else
            {
                //khong giao dich
                Result<Transaction> resultRefund = gateway.Transaction.Refund(orderHeader.TransactionId);
            }
            orderHeader.OrderStatus = WC.StatusRefunded;
            _orderHRepo.Save();
            TempData[WC.Sucess] = "Order Cancelled Successfully";
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult UpdateOrderDetails()
        {
            OrderHeader orderHeaderFromDb = _orderHRepo.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.FullName = OrderVM.OrderHeader.FullName;
            orderHeaderFromDb.Email = OrderVM.OrderHeader.Email;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.Cmnd = OrderVM.OrderHeader.Cmnd;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;

            _orderHRepo.Save();
            TempData[WC.Sucess] = "Order Details Updated Successfully";
            return RedirectToAction("Detail", "Order", new { id = orderHeaderFromDb.Id });
            //return lại action Detail Controller Order
        }

        public IActionResult MyOrder()
        {
            MyorderVM myorder = new MyorderVM();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).ToString();

            IEnumerable<OrderHeader> orderHList = _orderHRepo.GetAll(u => claim.Contains(u.CreatedByUserId));
            // IEnumerable<OrderDetail> orderDList = _orderDRepo.GetAll(u => orderHList.)
            // OrderDetail = _orderDRepo.GetAll(o => o.OrderHeaderId == id, includeProperties: "Product")
            myorder.OrderHList = orderHList.ToList();

            List<OrderDetail> temp = new List<OrderDetail>();
            foreach(var item in myorder.OrderHList)
            {
                temp = _orderDRepo.GetAll(o => o.OrderHeaderId == item.Id, includeProperties: "Product").ToList();
                myorder.OrderDList.AddRange(temp);

            }

            double money = 0;
            myorder.ApplicationUser.Id = claim;
            foreach(var item in myorder.OrderHList)
            {
                money += item.FinalOrderTotal;
            }
            if(money>=10000)
            {
                myorder.ApplicationUser.Rank = 1;
            }
            else
            {
                if (money >= 5000)
                    myorder.ApplicationUser.Rank = 2;
                else
                {
                    myorder.ApplicationUser.Rank = 3;
                }
            }
            Coupon coupon = _couponRepo.FirstOrDefault(u => u.Id == 1);
            ViewBag.Rank = myorder.ApplicationUser.Rank;
            ViewBag.Code = coupon.CouponCode;
            return View(myorder);
        }
    }
}
