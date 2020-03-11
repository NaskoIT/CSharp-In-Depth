namespace ConstrainingGenerics
{
    public class TemplateClassConstrainedByInterface<T> where T : IHaveInterface
    {
        public TemplateClassConstrainedByInterface(T data)
        {
            data.DoSomething();
        }
    }
}
