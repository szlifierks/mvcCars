using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using wzorzec3f2.Models;

namespace wzorzec3f2.Controllers;

public class PersonController : Controller
{
    public PersonController()
    {
        if (Person.list.Count == 0)
        {
            Person.list.Add(new Person{Name = "jacus", ProfessionId = 1});
            Person.list.Add(new Person{Name = "marek", ProfessionId = 2});
            Person.list.Add(new Person{Name = "romuald", ProfessionId = 3});
        }
        if (Profession.list.Count == 0)
        {
            Profession.list.Add(new Profession { Id = 1, Name = "goral" });
            Profession.list.Add(new Profession { Id = 2, Name = "gornik" });
            Profession.list.Add(new Profession { Id = 3, Name = "ogrodnik" });
        }
    }
    
    // GET
    public IActionResult Index()
    {
        int profId = 0;
        ViewBag.ProfList = Profession.list;
        ViewBag.ProfId = profId;

        List<Person> personList = Person.list;
        foreach (var person in personList)
        {
            person.Profession = Profession.list.FirstOrDefault(p => p.Id == person.ProfessionId);
        }
        
        return View(personList);
    }
    
    [HttpPost]
    public IActionResult Index(int profId)
    {
        ViewBag.ProfList = Profession.list;
        ViewBag.ProfId = profId;

        List<Person> personList = Person.list.Where(p => p.ProfessionId == profId).ToList();
        foreach (var person in personList)
        {
            person.Profession = Profession.list.FirstOrDefault(p => p.Id == person.ProfessionId);
        }
        
        if (profId != 0)
        { 
            var personListSelect = personList.Where(x => x.ProfessionId == profId).ToList();
            return View(personListSelect);
        }

        return View(personList);
    }
    
    public IActionResult AddPerson()
    {
        ViewBag.ProfList = Profession.list.Select(x => 
                        new SelectListItem 
                            { Text = x.Name, Value = x.Id.ToString() }).ToList();
        return View();
    }
    [HttpPost]
    public IActionResult AddPerson(Person person)
    {
        person.Id = person.NextId();
        Person.list.Add(person);
        return RedirectToAction("Index", "Home");
    }
}