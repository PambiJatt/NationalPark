namespace NationalParkWebApp_1144.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<NationalPark> NationalParkList { get; set; }
            = new List<NationalPark>();

        public IEnumerable<Trail> TrailList { get; set; }
            = new List<Trail>();
    }
}
