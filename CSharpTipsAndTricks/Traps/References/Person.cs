namespace References
{
    public class Person
    {
        public Person(string personalNumber, string firstName, string lastName)
        {
            this.PersonalNumber = personalNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string PersonalNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
