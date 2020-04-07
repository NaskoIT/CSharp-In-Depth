using System;
using System.Threading.Tasks;

namespace BurgerPreparation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(nameof(SyncCooker));
            SyncCooker.Work();

            Console.WriteLine(new string('-', 50));
            Console.WriteLine(nameof(AllAtOnceCooker));
            await AllAtOnceCooker.Work();

            Console.WriteLine(new string('-', 50));
            Console.WriteLine(nameof(AsyncCooker));
            await AsyncCooker.Work();
        }
    }
}
