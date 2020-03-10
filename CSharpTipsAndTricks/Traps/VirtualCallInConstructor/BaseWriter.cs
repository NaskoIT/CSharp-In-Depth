namespace VirtualCallInConstructor
{
    public abstract class BaseWriter
    {
        protected BaseWriter()
        {
            // Virtual member call in constructor
            this.WriteHeader();
        }

        protected virtual void WriteHeader()
        {
        }
    }
}
