using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using wzorzec3f2.Models;

namespace wzorzec3f2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly podstawyDbContext _db;

        public HomeController(ILogger<HomeController> logger, podstawyDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        
        [HttpGet]
        public IActionResult Index(int engId, string sort)
        {
            ViewBag.EngineList = _db.Engines.ToList();
            ViewBag.EngId = engId;
            List<Car> carList = _db.Cars.ToList();
            if (engId != 0)
            {
                carList = _db.Cars.Where(x => x.EngineId == engId).ToList();
            }
            if(!string.IsNullOrEmpty(sort))
            {
                if(sort == "up")
                {
                    carList = carList.OrderBy(x => x.Brand).ToList();
                }
                else
                {
                    carList = carList.OrderByDescending(x => x.Brand).ToList();
                }
            }
            return View(carList);
        }
        public IActionResult CreateCar()
        {
            ViewBag.EngineList = _db.Engines.Select(x => 
                new SelectListItem 
                    { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateCar(Car car)
        {
            _db.Cars.Add(car);
            _db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteCar(int id)
        {
            _db.Cars.Remove(_db.Cars.FirstOrDefault(x => x.Id == id));
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateCar(int id)
        {
            Car car = _db.Cars.FirstOrDefault(x => x.Id == id);
            if(car != null)
            {
                ViewBag.EngineList = _db.Engines.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = x.Id == car.EngineId
                    }).ToList();
                return View(car);
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public IActionResult UpdateCar(int id, Car car)
        {
            Car carUpt = _db.Cars.FirstOrDefault(x => x.Id == id);
            if (car != null)
            {
                carUpt.Brand = car.Brand;
                carUpt.Model = car.Model;
                carUpt.EngineId = car.EngineId;
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            Car car = _db.Cars.FirstOrDefault(x => x.Id == id);
            if (car != null)
            {
                car.Engine = _db.Engines.FirstOrDefault(x => x.Id == car.EngineId);
                return View(car);
            }
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult SilnikMarka()
        {
            ViewBag.EngId = 0;
            ViewBag.EngineList = _db.Engines;
            return View(_db.Cars.ToList());
        }
        
        [HttpPost]
        public IActionResult SilnikMarka(int engId)
        {
            ViewBag.EngineList = _db.Engines.ToList();
            ViewBag.EngId = engId;
            var filteredCars = _db.Cars.Where(x => x.EngineId == engId)
                .GroupBy(x => x.Brand)
                .Select(g => g.First())
                .ToList();
            return View(filteredCars);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
