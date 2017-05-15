using LinhNguyen.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository
{
    public interface IFtagRepository
    {
        IQueryable<Ftag> FindAllFtags();
        IQueryable<Ftag> FtagLogin();
        IQueryable<Menu> FindMenu();
    }
}
