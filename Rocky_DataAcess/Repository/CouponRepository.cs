using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocky_DataAcess.Repository
{
    public class CouponRepository : Repository<Coupon> , ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        public CouponRepository(ApplicationDbContext db) : base(db) //muốn gọi lại dbcontext thì thêm base(db)
        {
            _db = db;
        }
        public void Update(Coupon obj)
        {
            
        }

        
    }
}
