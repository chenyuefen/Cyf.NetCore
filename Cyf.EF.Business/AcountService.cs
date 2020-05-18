using Cyf.EntityFramework.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cyf.EntityFramework.Business
{
    public class AcountService : BaseService, IAcountService
    {
        public AcountService(DbContext context) : base(context)
        {
        }

        //public void UpdateLastLogin(Employee user)
        //{
        //    Employee userDB = base.Find<Employee>(user.id);
        //    //userDB.LastLoginTime = DateTime.Now;
        //    this.Commit();
        //}
    }

}
