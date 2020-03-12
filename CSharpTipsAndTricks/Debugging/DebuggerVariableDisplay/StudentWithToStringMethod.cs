namespace DebuggerVariableDisplay
{
    public class StudentWithToStringMethod : Student
    {
        public StudentWithToStringMethod(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        public override string ToString()
        {
            return $"StudentWithToStringMethod named {this.FirstName} {this.LastName}.";
        }
    }
}
