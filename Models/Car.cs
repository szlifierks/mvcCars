namespace wzorzec3f2.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        
        public int EngineId { get; set; }
        
        public Engine Engine { get; set; }

        public Car() { }

        public Car(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }
    }
}
