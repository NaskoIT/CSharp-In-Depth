using System.Collections.Generic;

namespace ReflectionDelegatesDemo
{
    public class HomeController
    {
        public HomeController()
        {
            Data = new Dictionary<string, object>
            {
                ["Name"] = "Optimize reflection up to 10 times",
                ["Value"] = 10
            };
        }

        [Data]
        public IDictionary<string, object> Data { get; set; }
    }
}
