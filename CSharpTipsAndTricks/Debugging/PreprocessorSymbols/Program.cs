using System;
using System.Diagnostics;

namespace PreprocessorSymbols
{
    public class Program
    {
        public static void Main()
        {
#if DEBUG
            Console.WriteLine("DEBUG");
#elif CI_BUILD
            Console.WriteLine("CI_BUILD");
#else
            Console.WriteLine("Neither DEBUG nor CI_BUILD");
#error Neither DEBUG nor CI_BUILD
#endif
            CallOnlyInDebug();

            Debug.WriteLine("Hello, its me");
        }

        [Conditional("DEBUG")]
        private static void CallOnlyInDebug()
        {
            Console.WriteLine("CallOnlyInDebug() called");
        }
    }
}
