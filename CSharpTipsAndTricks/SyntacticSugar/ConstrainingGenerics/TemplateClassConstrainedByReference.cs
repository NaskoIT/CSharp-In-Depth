namespace ConstrainingGenerics
{
    // T1 has to be a reference type (class, delegate, or array type)
    // T1 can also be any other type parameter that is constrained to be a reference type.
    public class TemplateClassConstrainedByReference<T1, T2>
        where T1 : class
        where T2 : class, T1
    {
    }
}
