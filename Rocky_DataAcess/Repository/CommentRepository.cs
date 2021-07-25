using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocky_DataAcess.Repository
{
    public class CommentRepository : Repository<Comment> , ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db) //muốn gọi lại dbcontext thì thêm base(db)
        {
            _db = db;
        }

       
        public void Update(Comment obj)
        {
            _db.Comment.Update(obj); //cách tự động update mới
            //tất cả properties của product sẽ được update
        }

     
    }
}
