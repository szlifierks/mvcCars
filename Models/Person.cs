namespace wzorzec3f2.Models;

public class Person
{
    public static List<Person> list = new List<Person>();
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProfessionId { get; set; }
    public Profession Profession { get; set; }
    
    public Person() { }
    
    public Person(string name)
    {
        Id = NextId();
        Name = name;
    }

    public int NextId()
    {
        if(list.Count == 0)
        {
            return 1;
        }
        else
        {
            return list.Max(x => x.Id) + 1;
        }

    }
}