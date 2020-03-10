using System;
using System.Collections.Generic;

namespace ChangeHashCodeRunTIme
{
    class Program
    {
        static void Main(string[] args)
        {
            var third = new Student { Id = 1, Name = "Niki" };
            var first = new Student { Id = 2, Name = "Viktor" };
            var second = new Student { Id = 3, Name = "Ivo" };
            var fourth = new Student { Id = 4, Name = "Stoyan" };

            var marks = new Dictionary<Student, int>();
            marks.Add(first, 3);
            marks.Add(second, 4);
            marks.Add(third, 5);
            marks.Add(fourth, 6);

            // this call works correctly as the hash code is still the same
            Console.WriteLine(marks[first]);

            // changing the Name, without realizing the hash code will change too
            first.Name = "Nikolay";

            // the collection is no longer capable of finding the first student
            Console.WriteLine(marks[first]);
        }
    }
}
