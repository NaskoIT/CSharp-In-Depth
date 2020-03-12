using System.Diagnostics;

namespace DebuggerVariableDisplay
{
    [DebuggerDisplay("Student named {FirstName} {LastName}")]
    public class StudentWithDebuggerDisplayAttribute : Student
    {
        public StudentWithDebuggerDisplayAttribute(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        public override string ToString()
        {
            return $"ToString method for StudentWithDebuggerDisplayAttribute named {this.FirstName} {this.LastName}.";
        }
    }
}
