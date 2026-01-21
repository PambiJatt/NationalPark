using Microsoft.AspNetCore.Mvc;
using NationalParkWebApp_1144.Repository.IRepository;
using NationalParkWebApp_1144.Models.ViewModel;
using NationalParkWebApp_1144.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NationalParkWebApp_1144.Controllers
{
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalparkRepository;
        public TrailController(ITrailRepository trailRepository, INationalParkRepository nationalParkRepository)
        {
            _trailRepository = trailRepository;
            _nationalparkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _trailRepository.GetAllAsync(SD.TrailAPIPath) });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailRepository.DeleteAsync(SD.TrailAPIPath, id);
            if (status)
                return Json(new { success = true, Message = "data deleted successfully !!!" });
            return Json(new { success = false, Message = "Unable to delete data !!!" });
        }
        #endregion

        public async Task<IActionResult> Upsert(int? id)
        {
            var nationalParkList = await _nationalparkRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVM trailVM = new TrailVM()
            {
                trail = new Models.Trail(),
                NationalParkList = nationalParkList.Select(np => new SelectListItem()
                {
                    Text = np.Name,
                    Value = np.Id.ToString()
                })
            };
            if (id == null) return View(trailVM);
            trailVM.trail = await _trailRepository.GetAsync(SD.TrailAPIPath, id.GetValueOrDefault());
            return View(trailVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailVM trailVM)
        {
            if (trailVM.trail == null) return BadRequest();
            if (!ModelState.IsValid) return View(trailVM);
            if (trailVM.trail.Id == 0)
                await _trailRepository.CreateAsync(SD.TrailAPIPath, trailVM.trail);
            else
                await _trailRepository.UpdateAsync(SD.TrailAPIPath + trailVM.trail.Id, trailVM.trail);
            return RedirectToAction(nameof(Index));
        }
    }
}