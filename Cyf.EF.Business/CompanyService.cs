using Cyf.EntityFramework.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cyf.EntityFramework.Business
{
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(DbContext context) : base(context)
        {
        }
    }
}
