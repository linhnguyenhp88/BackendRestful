using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinhNguyen.Repository.Entities;
using LinhNguyen.Repository.DAL;

namespace LinhNguyen.Repository
{
    public class FtagRepository : IFtagRepository
    {
        private SraContext _ctx;

        public FtagRepository(SraContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = true;
        }

        public IQueryable<Ftag> FindAllFtags()
        {
            return _ctx.Ftags; 
        }

        public IQueryable<Menu> FindMenu()
        {
            return _ctx.Menus;
        }

        public IQueryable<Ftag> FtagLogin()
        {
            return _ctx.Ftags;
        }
    }
}
