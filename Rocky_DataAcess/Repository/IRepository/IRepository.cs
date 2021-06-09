using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Rocky_DataAcess.Repository.IRepository
{   

    //khi làm với reposity thì nếu trong tương lai có thay đổi gì thì chỉ thay đổi reposity
    //không cần phải dùng tới controller để thay đổi
    public interface IRepository<T> where T : class //where T : class là để ràng buộc kiểu T truyền vào là 1 class
    {
       T Find(int id);
        IEnumerable<T> GetAll(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null,
        bool isTracking = true //set cái này chỉ dùng cho việc query để tránh tracking của entity framework
           );
        
        T FirstOrDefault( //get 1 bản record 
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null,
        bool isTracking = true
            );

        void Add(T entity); //thêm 1 record 
        void Remove(T entity); //xóa 1 record

        void RemoveRange(IEnumerable<T> entity);
        void Save();

    }
}
