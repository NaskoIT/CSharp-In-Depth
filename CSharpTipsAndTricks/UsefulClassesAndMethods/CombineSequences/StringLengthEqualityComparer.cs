using System.Collections.Generic;

namespace CombineSequences
{
    public class StringLengthEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => x.Length == y.Length;

        public int GetHashCode(string obj) => obj.Length.GetHashCode();
    }
}
