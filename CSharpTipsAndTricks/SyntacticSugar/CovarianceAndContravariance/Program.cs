namespace CovarianceAndContravariance
{
    // BaseClass : MiddleClass : LastClass
    class Program
    {
        public static void Main()
        {
            InvariantGeneric();
            ContravariantGeneric();
            CovariantGeneric();
        }

        public static void InvariantGeneric()
        {
            IInvariantGeneric<MiddleClass> genericMiddle = new InvariantGeneric<MiddleClass>();
            MiddleClass result = genericMiddle.Method(new MiddleClass());

            // This will produce compile-time error:
            // Cannot implicitly convert type 'IInvariantGeneric<MiddleClass>' to 'IInvariantGeneric<BaseClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// IInvariantGeneric<BaseClass> genericBase = genericMiddle;

            // This will produce compile-time error:
            // Cannot implicitly convert type 'IInvariantGeneric<MiddleClass>' to 'IInvariantGeneric<LastClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// IInvariantGeneric<LastClass> genericLast = genericMiddle;
        }

        public static void ContravariantGeneric()
        {
            IContravariantGeneric<MiddleClass> genericMiddle = new ContravariantGeneric<MiddleClass>();
            genericMiddle.Method(new MiddleClass());

            // This will produce compile-time error:
            // Cannot implicitly convert type 'IContravariantGeneric<MiddleClass>' to 'IContravariantGeneric<BaseClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// IContravariantGeneric<BaseClass> genericBase = genericMiddle;

            // This is OK here:
            IContravariantGeneric<LastClass> genericLast = genericMiddle;
            genericLast.Method(new LastClass());
        }

        public static void CovariantGeneric()
        {
            ICovariantGeneric<MiddleClass> genericMiddle = new CovariantGeneric<MiddleClass>();
            MiddleClass result = genericMiddle.Method();

            // This is OK here:
            ICovariantGeneric<BaseClass> genericBase = genericMiddle;
            BaseClass baseResult = genericBase.Method();

            // This will produce compile-time error:
            // Cannot implicitly convert type 'ICovariantGeneric<MiddleClass>' to 'ICovariantGeneric<LastClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// ICovariantGeneric<LastClass> genericLast = genericMiddle;
        }
    }
}
