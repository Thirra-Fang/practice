using System;

namespace test_20220312_1_SWF
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            str = str.Replace("Hello world","Hello <你的名字>");
            Console.WriteLine(str);
        }
    }
}
