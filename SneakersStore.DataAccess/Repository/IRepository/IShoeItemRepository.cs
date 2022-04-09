using SneakersStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersStore.DataAccess.Repository.IRepository
{
    public interface IShoeItemRepository : IRepository<ShoeItem>
    {
        void Update(ShoeItem obj);        
    }
}
