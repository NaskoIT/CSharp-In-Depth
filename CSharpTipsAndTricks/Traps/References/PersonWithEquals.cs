namespace References
{
    public class PersonWithEquals : Person
    {
        public PersonWithEquals(string personalNumber, string firstName, string lastName)
           : base(personalNumber, firstName, lastName)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Person)obj);
        }

        public override int GetHashCode()
        {
            return this.PersonalNumber != null ? this.PersonalNumber.GetHashCode() : 0;
        }

        protected bool Equals(Person other)
        {
            return string.Equals(this.PersonalNumber, other.PersonalNumber);
        }
    }
}
