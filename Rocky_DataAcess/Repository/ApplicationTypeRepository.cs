using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rocky_DataAcess.Repository
{
    public class ApplicationTypeRepository : Repository<ApplicationType> , IApplicationTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationTypeRepository(ApplicationDbContext db) : base(db) //muốn gọi lại dbcontext thì thêm base(db)
        {
            _db = db;
        }
        public void Update(ApplicationType obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id); //ta cos thể sử dụng base từ repository vì hàm firstordefault ta đã triển khai trên repository
            if (objFromDb != null)
            {
                objFromDb.NameApp = obj.NameApp;
            }
        }
    }
}
