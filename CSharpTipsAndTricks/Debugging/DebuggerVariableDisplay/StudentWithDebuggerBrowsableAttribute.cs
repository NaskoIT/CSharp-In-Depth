using System.Collections.Generic;
using System.Diagnostics;

namespace DebuggerVariableDisplay
{
    public class StudentWithDebuggerBrowsableAttribute : Student
    {
        public StudentWithDebuggerBrowsableAttribute(string firstName, string lastName, IEnumerable<int> grades)
           : base(firstName, lastName)
        {
            this.Grades = grades;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public IEnumerable<int> Grades { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Age { get; set; }
    }
}
