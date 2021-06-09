﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky_DataAcess.Repository.IRepository;
using Rocky_Models;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocky_DataAcess.Repository
{
    public class InquiryHeaderRepository : Repository<InquiryHeader> , IInquiryHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public InquiryHeaderRepository(ApplicationDbContext db) : base(db) //muốn gọi lại dbcontext thì thêm base(db)
        {
            _db = db;
        }

        

        public void Update(InquiryHeader obj)
        {
            _db.InquiryHeader.Update(obj); //cách tự động update mới
            //tất cả properties của product sẽ được update
        }

        
    }
}
