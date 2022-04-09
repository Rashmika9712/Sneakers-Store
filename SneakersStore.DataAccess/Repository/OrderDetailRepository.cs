using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;
using SneakersStore.Models;

namespace SneakersStore.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }        

        public void Update(OrderDetails obj)
        {
             _db.OrderDetails.Update(obj); 
        }
    }
}
