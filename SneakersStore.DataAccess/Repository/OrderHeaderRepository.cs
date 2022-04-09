using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;
using SneakersStore.Models;

namespace SneakersStore.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }        

        public void Update(OrderHeader obj)
        {
             _db.OrderHeader.Update(obj);
        }
    }
}
