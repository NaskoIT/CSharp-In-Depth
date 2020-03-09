namespace References
{
    public class PersonWithEqualityOperator : PersonWithEquals
    {
        public PersonWithEqualityOperator(string personalNumber, string firstName, string lastName) 
            : base(personalNumber, firstName, lastName)
        {
        }

        public static bool operator ==(PersonWithEqualityOperator a, PersonWithEqualityOperator b)
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            // Return true if the objects are equal
            return a.Equals(b);
        }

        public static bool operator !=(PersonWithEqualityOperator a, PersonWithEqualityOperator b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
