namespace TaskManagerApp
{
    public class Category
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public Category(string name, string color = "Blue")
        {
            Name = name;
            Color = color;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}