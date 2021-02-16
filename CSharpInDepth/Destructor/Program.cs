using System;

namespace Destructor
{
    public class Program
    {
        static void Main(string[] args)
        {
            Details();
            GC.Collect();
            Console.WriteLine("GC.Collect was invoked!");
        }

        private static void Details()
        {
            var user = new User();
        }
    }

    public class User
    {
        public User()
        {
            Console.WriteLine("An Instance of class created");
        }

        // Destructor
        ~User()
        {
            Console.WriteLine("An Instance of class destroyed");
        }
    }
}
