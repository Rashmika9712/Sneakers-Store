using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;
using SneakersStore.Models;

namespace SneakersStore.DataAccess.Repository
{
    public class ShoeTypeRepository : Repository<ShoeType>, IShoeTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoeTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }        

        public void Update(ShoeType obj)
        {
            var objFromDb = _db.ShoeType.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Name = obj.Name;    
        }
    }
}
