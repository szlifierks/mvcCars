using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using wzorzec3f2.Models;

namespace wzorzec3f2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            if (Car.list.Count == 0)
            {
                Car.list.Add(new Car { Id = 1, Brand = "BMW", Model = "x7", EngineId = 1});
                Car.list.Add(new Car { Id = 2, Brand = "Audi", Model = "a6", EngineId = 2 });
                Car.list.Add(new Car { Id = 3, Brand = "Fiat", Model = "Multipla", EngineId = 3 });
            }

            if(Engine.list.Count == 0)
            {
                Engine.list.Add(new Engine { Id = 1, Name = "Diesel" });
                Engine.list.Add(new Engine { Id = 2, Name = "Petrol" });
                Engine.list.Add(new Engine { Id = 3, Name = "Electric" });
            }
        }
        [HttpGet]
        public IActionResult Index(int engId, string sort)
        {
            ViewBag.EngineList = Engine.list;
            ViewBag.EngId = engId;
             = Car.list;
            if (engId != 0)
            {
                ViewBag.list = Car.list.Where(x => x.EngineId == engId).ToList();
            }
            if(!string.IsNullOrEmpty(sort))
            {
                if(sort == "up")
                {
                    ViewBag.list = ViewBag.list.OrderBy(x => x.Brand).ToList();
                }
                else if(sort == "down")
                {
                    ViewBag.list = ViewBag.list.OrderBy(x => x.Brand).ToList();
                }
            }
        }
        public IActionResult CreateCar()
        {
            ViewBag.EngineList = Engine.list.Select(x => 
                new SelectListItem 
                    { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateCar(Car car)
        {
            car.Id = car.NextId();
            Car.list.Add(car);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteCar(int id)
        {
            Car.list.RemoveAll(x => x.Id == id);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateCar(int id)
        {
            Car car = Car.list.FirstOrDefault(x => x.Id == id);
            if(car != null)
            {
                ViewBag.EngineList = Engine.list.Select(x =>
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
            Car carUpt = Car.list.FirstOrDefault(x => x.Id == id);
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
            Car car = Car.list.FirstOrDefault(x => x.Id == id);
            if (car != null)
            {
                car.Engine = Engine.list.FirstOrDefault(x => x.Id == car.EngineId);
                return View(car);
            }
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult SilnikMarka()
        {
            ViewBag.EngId = 0;
            ViewBag.EngineList = Engine.list;
            return View(Car.list);
        }
        
        [HttpPost]
        [HttpPost]
        public IActionResult SilnikMarka(int engId)
        {
            ViewBag.EngineList = Engine.list;
            ViewBag.EngId = engId;
            var filteredCars = Car.list.Where(x => x.EngineId == engId)
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
