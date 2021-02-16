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

            Dude ted = bill.Clone() as Dude;
            ted.Name = "Ted";
            ted.LeftShoe.Color = "Red";
            ted.RightShoe.Color = "Red";

            Console.WriteLine(bill.ToString());
            Console.WriteLine(ted.ToString());
        }
    }

    public class Shoe : ICloneable
    {
        public string Color { get; set; }

        public object Clone() => new Shoe
        {
            Color = this.Color.Clone() as string
        };
    }

    public class Dude : ICloneable
    {
        public string Name { get; set; }

        public Shoe RightShoe { get; set; }

        public Shoe LeftShoe { get; set; }

        public object Clone() => new Dude
        {
            Name = this.Name,
            LeftShoe = this.LeftShoe.Clone() as Shoe,
            RightShoe = this.RightShoe.Clone() as Shoe
        };

        public override string ToString()
        {
            return (Name + " : Dude!, I have a " + RightShoe.Color + " shoe on my right foot, and a " + LeftShoe.Color + " on my left foot.");
        }
    }
}
