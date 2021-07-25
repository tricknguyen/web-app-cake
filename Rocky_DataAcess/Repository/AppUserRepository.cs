using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rocky_DataAcess.Repository
{
   
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly ApplicationDbContext _db;
        public AppUserRepository(ApplicationDbContext db) : base(db) //muốn gọi lại dbcontext thì thêm base(db)
        {
            _db = db;
        }

    }
}
