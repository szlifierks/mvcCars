namespace wzorzec3f2.Models;

public class Profession
{
    public static List<Profession> list = new List<Profession>();
    public int Id{get;set;}
    public string Name{get;set;} 
    
    public Profession() { }
    
    public Profession(string name)
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