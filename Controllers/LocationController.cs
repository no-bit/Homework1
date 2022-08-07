using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private static List<Location> _Location = new List<Location>();
        [HttpPost]
        public string Add(Location _location)
        {
            _location.Id = new Random().Next();
            _Location.Add(_location);

            return "Ekleme basarili";
        }
        [HttpGet]
        public List<Location> GetAll()
        {
            return _Location;
        }
        [HttpGet("{id}")]
        public Location GetById(int id)
        {
            var returnLocation = _Location.Find(x => x.Id == id);
            return returnLocation;
        }
        [HttpDelete]
        public string Delete(Location _location, int id)
        {
            var deleteLocation = _Location.FirstOrDefault(n => n.Id == id);
            if (deleteLocation != null)
            {
                _Location.Remove(deleteLocation);
            }

            return "Silme basarili";
        }
        [HttpPut]
        public string Modify(Location _location, int id)
        {
            var updateLocation = _Location.FirstOrDefault(n => n.Id == id);
            if (updateLocation != null)
            {
                updateLocation.Name = _location.Name;
                updateLocation.x = _location.x;
                updateLocation.y = _location.y;
            }
            return "guncellendi";
        }
    }
}
