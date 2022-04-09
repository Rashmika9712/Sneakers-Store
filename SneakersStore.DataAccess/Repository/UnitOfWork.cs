using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersStore.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            ShoeType = new ShoeTypeRepository(_db);
            ShoeItem = new ShoeItemRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IShoeTypeRepository ShoeType { get; private set; }
        public IShoeItemRepository ShoeItem { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();  
        }
    }
}
