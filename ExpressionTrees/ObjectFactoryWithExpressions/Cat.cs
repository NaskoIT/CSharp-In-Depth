namespace ObjectFactoryWithExpressions
{
    public class Cat
    {
        public Cat() { }

        public Cat(string name) => Name = name;

        public Cat(string name, int age) : this(name) => Age = age;

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
