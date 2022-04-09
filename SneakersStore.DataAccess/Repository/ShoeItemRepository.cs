using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;
using SneakersStore.Models;

namespace SneakersStore.DataAccess.Repository
{
    public class ShoeItemRepository : Repository<ShoeItem>, IShoeItemRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoeItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }        

        public void Update(ShoeItem obj)
        {
            var objFromDb = _db.ShoeItem.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Name = obj.Name;    
            objFromDb.Description = obj.Description;    
            objFromDb.Price = obj.Price;    
            objFromDb.CategoryId = obj.CategoryId;    
            objFromDb.ShoeTypeId = obj.ShoeTypeId;

            if (objFromDb.Image != null)
            {
                objFromDb.Image = objFromDb.Image;
            }
        }
    }
}
