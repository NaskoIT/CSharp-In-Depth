using System;

namespace CombinableEnumValues
{
    class Program
    {
        static void Main(string[] args)
        {
            var bottomRightMargin = Margins.Bottom | Margins.Right;
            //  100(2) = 4
            // 1000(2) = 8
            // 1100(2) = 12
            var topLeftMargin = Margins.Top | Margins.Left;
            //  1(2) = 1
            // 10(2) = 2
            // 11(2) = 3

            // Getting information
            Console.WriteLine("topLeftMargin string value: {0}", topLeftMargin);
            Console.WriteLine("topLeftMargin integer value: {0}", (int)topLeftMargin);
            Console.WriteLine("bottomRightMargin string value: {0}", bottomRightMargin);
            Console.WriteLine("bottomRightMargin integer value: {0}", (int)bottomRightMargin);
            Console.WriteLine("bottomRightMargin == Margins.Bottom: {0}", bottomRightMargin == Margins.Bottom);
            Console.WriteLine(
                "bottomRightMargin has flag Margins.Bottom: {0}",
                bottomRightMargin.HasFlag(Margins.Bottom)); // Equivalent to (bottomRightMargin & Margins.Bottom) != 0
            
            // Combining combinations
            Console.WriteLine("bottomRightMargin and topLeftMargin: {0}", bottomRightMargin | topLeftMargin);
            Console.WriteLine("bottomRightMargin and topLeftMargin integer value: {0}", (int)(bottomRightMargin | topLeftMargin));
            // 1100(2) = 12
            //   11(2) = 3
            // 1111(2) = 15

            // Toggling values
            bottomRightMargin ^= Margins.Bottom;
            // 1100(2) = 12
            //  100(2) = 4
            // 1000(2) = 8
            Console.WriteLine("bottomRightMargin ^= Margins.Bottom => {0}", bottomRightMargin);
            Console.WriteLine("bottomRightMargin ^= Margins.Bottom integer value => {0}", (int)bottomRightMargin);
            bottomRightMargin ^= Margins.Bottom;
            // 1000(2) = 8
            //  100(2) = 4
            // 1100(2) = 12
            Console.WriteLine("bottomRightMargin ^= Margins.Bottom => {0}", bottomRightMargin);
            Console.WriteLine("bottomRightMargin ^= Margins.Bottom integer value => {0}", (int)bottomRightMargin);
        }
    }
}
