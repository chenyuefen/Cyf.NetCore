using Cyf.EntityFramework.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cyf.EntityFramework.Business
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DbContext context) : base(context)
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
