using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wzorzec3f2.Models;

namespace wzorzec3f2.Controllers;

public class PersonController : Controller
{
    private readonly ILogger<PersonController> _logger;
    private readonly podstawyDbContext _db;

    public PersonController(ILogger<PersonController> logger, podstawyDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    // GET
    public IActionResult Index()
    {
        int profId = 0;
        ViewBag.ProfList = _db.Professions;
        ViewBag.ProfId = profId;

        List<Person> personList = _db.People.ToList();
        foreach (var person in personList)
        {
            person.Profession = _db.Professions.FirstOrDefault(p => p.Id == person.ProfessionId);
        }
        
        return View(personList);
    }
    
    [HttpPost]
    public IActionResult Index(int profId)
    {
        ViewBag.ProfList = _db.Professions.ToList();
        ViewBag.ProfId = profId;

        List<Person> personList = _db.People.ToList();
        foreach (var person in personList)
        {
            person.Profession = _db.Professions.FirstOrDefault(p => p.Id == person.ProfessionId);
        }

        if (profId != 0)
        {
            personList = personList.Where(x => x.ProfessionId == profId).ToList();
        }

        return View(personList);
    }
    
    public IActionResult AddPerson()
    {
        ViewBag.ProfList = _db.Professions.Select(x => 
                        new SelectListItem 
                            { Text = x.Name, Value = x.Id.ToString() }).ToList();
        return View();
    }
    [HttpPost]
    public IActionResult AddPerson(Person person)
    {
        _db.People.Add(person);
        _db.SaveChanges();
        return RedirectToAction("Index", "Person");
    }
    
    
    public IActionResult DeletePerson(int id)
    {
        _db.People.Remove(_db.People.FirstOrDefault(x => x.Id == id));
        _db.SaveChanges();
        return RedirectToAction("Index", "Person");
    }
    
    public IActionResult EditPerson(int id)
    {
        var person = _db.People.FirstOrDefault(x => x.Id == id);
        ViewBag.ProfList = _db.Professions.Select(x => 
            new SelectListItem 
                { Text = x.Name, Value = x.Id.ToString() }).ToList();
        return View(person);
    }
    [HttpPost]
    public IActionResult EditPerson(Person person, int id)
    {
        var updPerson = _db.People.FirstOrDefault(x => x.Id == id);

        if (updPerson != null)
        {
            updPerson.Name = person.Name;
            updPerson.ProfessionId = person.ProfessionId;
        }

        _db.SaveChanges();
        return RedirectToAction("Index", "Person");
    }
}