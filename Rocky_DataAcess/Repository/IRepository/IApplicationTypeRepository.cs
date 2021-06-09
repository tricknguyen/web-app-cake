using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rocky_DataAcess.Repository.IRepository
{
    public interface IApplicationTypeRepository : IRepository<ApplicationType>
    {
        void Update(ApplicationType obj);
    }
}
