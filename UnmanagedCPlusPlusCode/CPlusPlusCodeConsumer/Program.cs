using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CPlusPlusCodeConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("We will execute c++ code:");

            Console.WriteLine(subtract(20, 10));

            Console.WriteLine(add(10, 20));
        }

        [DllImport("CPlusPlusCode.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int subtract(int a, int b);


        [DllImport("CPlusPlusCode.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int add(int a, int b);
    }
}
