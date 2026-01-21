using Microsoft.AspNetCore.Mvc.Rendering;

namespace NationalParkWebApp_1144.Models.ViewModel
{
    public class TrailVM
    {
        public IEnumerable<SelectListItem> NationalParkList { get; set; }
        public Trail trail { get; set; }
    }
}