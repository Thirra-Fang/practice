using System;
using System.IO;

namespace test_20220312_2_SWF2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                using (StreamReader sr = new StreamReader("输入.txt"))
                {
                    StreamWriter sw = new StreamWriter("输出.txt");
                    String line;
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Replace("Hello world","Hello <你的名字>");
                        sw.WriteLine(line);
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
