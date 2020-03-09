namespace CompareStructs
{
    public struct PointWithNameAndEquals
    {
        public int X { get; set; }

        public int Y { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(PointWithNameAndEquals other)
        {
            return this.X == other.X
                && this.Y == other.Y
                && string.Equals(this.Name, other.Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.X;
                hashCode = (hashCode * 397) ^ this.Y;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
