using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Local.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Range rng = 1..2;
            var arr = new int[5] { 1, 2, 3, 4, 5 };
            foreach (var item in arr[rng])
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
            //自定义命令行
            CommandLine line = new CommandLine();
            line.Run(args);
        }
    }
}
