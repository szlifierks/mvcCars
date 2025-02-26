namespace wzorzec3f2.Models;

public class Profession
{
    public int Id{get;set;}
    public string Name{get;set;} 
    
    public Profession() { }
    
    public Profession(string name)
    {
        Name = name;
    }
}