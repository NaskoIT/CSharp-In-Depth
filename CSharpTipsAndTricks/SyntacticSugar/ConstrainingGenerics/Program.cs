namespace ConstrainingGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            // This is allowed (where T : ClassB)
            var templateClassInstance = new TemplateClass<ClassB>();

            // This is also allowed (ClassC is successor of ClassB)
            var templateClassInstance2 = new TemplateClass<ClassC>();

            // This is invalid:
            // var templateClassInstance3 = new TemplateClass<ClassA>();
            // 'ClassA' cannot be used as type parameter 'T'
            // There is no implicit reference conversion from 'ClassA' to 'ClassB'.

            // These are allowed (where T : IClassable)
            var templateClassConstrainedByInterface =
                new TemplateClassConstrainedByInterface<ClassA>(new ClassA());
            var templateClassConstrainedByInterface2 =
                new TemplateClassConstrainedByInterface<ClassB>(new ClassB());
            var templateClassConstrainedByInterface3 =
                new TemplateClassConstrainedByInterface<StructureA>(new StructureA());

            // This is allowed (where T : class)
            var templateClassConstrainedByReference =
                new TemplateClassConstrainedByReference<ClassA, ClassB>();

            // This is allowed (where T : class)
            var templateClassConstrainedByReference2 =
                new TemplateClassConstrainedByReference<ClassA, ClassA>();

            // This is invalid
            //// var templateClassConstrainedByReference2 =
            ////     new TemplateClassConstrainedByReference<ClassB, ClassA>();
            // 'ClassA' cannot be used as type parameter 'T2' in the generic type or method 'TemplateClassConstrainedByReference<T,T2>'.
            // There is no implicit reference conversion from 'ClassA' to 'ClassB'.

            // This is also invalid
            //// var templateClassConstrainedByReference3 =
            ////     new TemplateClassConstrainedByReference<StructureA, StructureA>();
            // 'StructureA' must be a reference type in order to use it as parameter 'T'
            // in the generic type or method 'TemplateClassConstrainedByReference<T,T2>'

            // This is allowed (where T : new())
            var templateClassConstrainedByEmptyConstructor =
                new TemplateClassConstrainedByEmptyConstructor<ClassA>();

            // This is invalid
            //// var templateClassConstrainedByEmptyConstructor2 =
            ////     new TemplateClassConstrainedByEmptyConstructor<ClassC>();
            // 'ClassC' must be a non-abstract type with a public parameterless constructor
            // in order to use it as parameter 'T' in the generic type or method 'TemplateClassConstrainedByEmptyConstructor<T>'
        }
    }
}
