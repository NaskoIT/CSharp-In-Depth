using System;

namespace DeepVsShallowCopy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dude bill = new Dude
            {
                Name = "Bill",
                LeftShoe = new Shoe
                {
                    Color = "Blue"
                },
                RightShoe = new Shoe()
                {
                    Color = "Blue"
                }
            };


            Dude ted = bill.GetShallowCopy();
            ted.Name = "Ted";
            ted.LeftShoe.Color = "Red";
            ted.RightShoe.Color = "Red";

            Console.WriteLine(bill.ToString());
            Console.WriteLine(ted.ToString());
        }
    }

    public class Shoe
    {
        public string Color { get; set; }
    }

    public class Dude
    {
        public string Name { get; set; }

        public Shoe RightShoe { get; set; }

        public Shoe LeftShoe { get; set; }

        public Dude GetShallowCopy()
        {
            Dude newPerson = new Dude();
            newPerson.Name = Name;
            newPerson.LeftShoe = LeftShoe;
            newPerson.RightShoe = RightShoe;
            return newPerson;
        }

        public override string ToString()
        {
            return (Name + " : Dude!, I have a " + RightShoe.Color + " shoe on my right foot, and a " + LeftShoe.Color + " on my left foot.");
        }
    }
}
