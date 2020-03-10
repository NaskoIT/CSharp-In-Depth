namespace ChangeHashCodeRunTIme
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
    }
}
