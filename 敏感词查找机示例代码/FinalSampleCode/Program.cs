using System;
using System.Threading.Tasks;

namespace FinalSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //构建了一个命令行输入,进行文本替换，最后存储到文件并且显示的类
            ConsoleReadBlock consoleReadBlock = new ConsoleReadBlock();
            SensitiveReplaceBlock sensitiveReplaceBlock = new SensitiveReplaceBlock("Hello world","Hello Thirra");
            FileWriteBlock fileWriteBlock = new FileWriteBlock("output.txt");
            ConsoleWriteBlock consoleWriteBlock = new ConsoleWriteBlock();

            consoleReadBlock.DataArrived += (e) =>
            {
                sensitiveReplaceBlock.Enqueue(e);
            };
            sensitiveReplaceBlock.DataArrived += (e) =>
            {
                consoleWriteBlock.Enqueue(e);
                fileWriteBlock.Enqueue(e);
            };
            consoleReadBlock.Start();
        }
    }
}
