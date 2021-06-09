using System;
using System.Collections.Generic;
using System.Text;
using Rocky_Models;
namespace Rocky_DataAcess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj); //khi update bất kỳ cái gì trong reposity thì nó sẽ chỉ update Category
    }
}
