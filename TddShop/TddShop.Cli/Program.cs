using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddShop.Cli
{
    public class Program
    {
        static void Main(string[] args)
        {
            int a = 20;
            int b = 9;

            var sum = AddTwoNumbers(a, b);
            if (sum == 29)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failure:(");
            }
            Console.ReadLine();
        }

        public static int AddTwoNumbers(int a, int b)
        {
            return a + b;
        }
    }
}
