using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Cat cat = new Cat
            {
                Id = 1,
                Health = 100,
                Name = "My cool cat"
            };

            cat.Health += 100;

            cat.OnHealthChanged += CatOnHealthChanged;
            cat.OnHealthChanged += OnCatDead;

            for (int i = 0; i < 10; i++)
            {
                cat.Health -= 20;
            }

            Console.WriteLine();
            Console.WriteLine("When unsubscribe from some method, we stop receiving notifications from this subscription.");
            cat.OnHealthChanged -= OnCatDead;
            cat.Health += 10;
            cat.Health -= 10;
        }

        private static void OnCatDead(object sender, int health)
        {
            Cat cat = (Cat)sender;

            if (health <= 0)
            {
                Console.WriteLine($"{cat.Name} is no longer alive...");
            }
        }

        private static void CatOnHealthChanged(object sender, int health)
        {
            Cat cat = (Cat)sender;
            Console.WriteLine($"{cat.Name} has new helth: {health}");
        }
    }
}
