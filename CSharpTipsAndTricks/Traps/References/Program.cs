using System;

namespace References
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstPerson = new Person("1234567890", "John", "Doe");
            var secondPerson = new Person("1234567890", "John", "Doe");
            Console.WriteLine("firstPerson == secondPerson is {0}", firstPerson == secondPerson);
            Console.WriteLine();

            var thirdPerson = secondPerson;
            Console.WriteLine("var thirdPerson = secondPerson;");
            Console.WriteLine("thirdPerson == secondPerson is {0}", thirdPerson == secondPerson);
            Console.WriteLine();

            CompareObjects(firstPerson, secondPerson);

            var firstPersonWithEquals = new PersonWithEquals("1234567890", "John", "Doe");
            var secondPersonWithEquals = new PersonWithEquals("1234567890", "John", "Doe");
            CompareObjects(firstPersonWithEquals, secondPersonWithEquals);

            var firstPersonWithEqualityOperator = new PersonWithEqualityOperator("1234567890", "John", "Doe");
            var secondPersonWithEqualityOperator = new PersonWithEqualityOperator("1234567890", "John", "Doe");
            CompareObjects(firstPersonWithEqualityOperator, secondPersonWithEqualityOperator);
            Console.WriteLine(
                "Inline fPersonWithEqualityOperator == sPersonWithEqualityOperator is {0}",
                firstPersonWithEqualityOperator == secondPersonWithEqualityOperator);
        }

        private static void CompareObjects<T>(T firstObject, T secondObject)
           where T : class
        {
            Console.WriteLine(
                "f{1} == s{2} is {0}",
                firstObject == secondObject,
                firstObject.GetType().Name,
                secondObject.GetType().Name);
            Console.WriteLine(
                "f{1}.Equals(s{2}) is {0}",
                firstObject.Equals(secondObject),
                firstObject.GetType().Name,
                secondObject.GetType().Name);
            Console.WriteLine(
                "ReferenceEquals(f{1}, s{2}) is {0}",
                object.ReferenceEquals(firstObject, secondObject),
                firstObject.GetType().Name,
                secondObject.GetType().Name);
            Console.WriteLine();
        }
    }
}
