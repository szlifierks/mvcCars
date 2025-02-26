namespace wzorzec3f2.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProfessionId { get; set; }
    public Profession Profession { get; set; }
    
    public Person() { }
    
    public Person(string name)
    {
        Name = name;
    }
}