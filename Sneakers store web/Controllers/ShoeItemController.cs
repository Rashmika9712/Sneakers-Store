using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO; 
using SneakersStore.DataAccess.Repository.IRepository;

namespace Sneakers_store_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ShoeItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var shoeItemList = _unitOfWork.ShoeItem.GetAll(includeProperties: "Category,ShoeType");
            return Json(new { data = shoeItemList });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ShoeItem.GetFirstOrDefault(u => u.Id == id);

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ShoeItem.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfully." });
        }
    }
}
