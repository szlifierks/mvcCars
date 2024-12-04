using Microsoft.AspNetCore.Mvc;
using wzorzec3f2.Models;

namespace wzorzec3f2.Controllers;

public class PersonController : Controller
{
    private readonly ILogger<HomeController> _logger; 

    public PersonController(ILogger<HomeController> logger)
    {
        _logger = logger;
        if (Person.list.Count == 0)
        {
            Person.list.Add(new Person{Name = "jacus", ProfessionId = 1});
            Person.list.Add(new Person{Name = "marek", ProfessionId = 2});
            Person.list.Add(new Person{Name = "romuald", ProfessionId = 3});
        }
    }
    
    // GET
    [HttpGet]
    public IActionResult Index(int profId)
    {
        if (Profession.list.Count == 0)
        {
            Profession.list.Add(new Profession { Id = 1, Name = "goral" });
            Profession.list.Add(new Profession { Id = 2, Name = "gornik" });
            Profession.list.Add(new Profession { Id = 3, Name = "ogrodnik" });
        }

        ViewBag.ProfList = Profession.list;
        ViewBag.ProfId = profId;

        List<Person> personList = Person.list;
        foreach (var person in personList)
        {
            person.Profession = Profession.list.FirstOrDefault(p => p.Id == person.ProfessionId);
        }

        if (profId != 0)
        {
            personList = personList.Where(x => x.ProfessionId == profId).ToList();
        }

        return View(personList);
    }
    
    
}